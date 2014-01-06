using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;

namespace libDownload
{
	public enum DOWNLOAD_EXCEPTION_TYPE
	{
		CONNECTION_ERROR,
		FILESYSTEM_ERROR
	}

	public class Length
	{
		public long value;
		public Length (long _value)
		{
			value = _value;
		}

		public override string ToString ()
		{
			if (value > 1024*1024*1024)
			{
				string s = (value/(1024*1024*1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " GB";
				return s.Substring (0, s.IndexOf (".") + 2) + " GB";
			}
			
			if (this.value > 1024*1024)
			{
				string s = (value/(1024*1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " GB";
				return s.Substring (0, s.IndexOf (".") + 2) + " MB";
			}

			if (this.value > 1024)
			{
				string s = (value/(1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " GB";
				return s.Substring (0, s.IndexOf (".") + 2) + " KB";
			}

			return value.ToString () + " Bytes";
		}
	}

	public class Speed
	{
		public long value;
		public Speed (long _value)
		{
			value = _value;
		}

		public override string ToString ()
		{
			if (value > 1024*1024*1024)
			{
				string s = (value/(1024*1024*1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " GB";
				return s.Substring (0, s.IndexOf (".") + 2) + " GB/s";
			}

			if (this.value > 1024*1024)
			{
				string s = (value/(1024*1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " GB";
				return s.Substring (0, s.IndexOf (".") + 2) + " MB/s";
			}

			if (this.value > 1024)
			{
				string s = (value/(1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " GB";
				return s.Substring (0, s.IndexOf (".") + 2) + " KB/s";
			}

			return value.ToString () + " Bytes/s";
		}
	}
	public class DownloadException : Exception
	{
		public DOWNLOAD_EXCEPTION_TYPE type;
		public DownloadException ()
		{
		}

		public DownloadException (string message, 
		                          DOWNLOAD_EXCEPTION_TYPE _type): 
			base (message)
		{
			type = _type;
		}
	}

	public enum DOWNLOAD_PART_STATUS
    {
		ERROR,
		IDLE, 
		DOWNLOADING,
		DOWNLOADED
	};

	public enum DOWNLOAD_STATUS
	{
		NOT_STARTED,
		DOWNLOADING,
		PAUSED,
		MERGING,
		DOWNLOADED,
		ERROR
	};

	public enum DOWNLOAD_SPEED_LEVEL
	{
		LOW,
		MEDIUM,
		HIGH
	};

	public abstract class Download
	{
		protected Length _downloaded;
		protected DOWNLOAD_STATUS _status;
		protected Thread _mergeThread;

		public const int maxReTryingAttempts = 5;
		public string remotePath, localPath;
		public short parts;
		public Length length;
		public List<DownloadPart> listParts;
		public abstract DOWNLOAD_STATUS status {get;set;}
		public bool generateFileName {get;set;}

		public delegate void OnStatusChanged (Download dwnld);
		public OnStatusChanged statusChangeHandler;
		public DownloadException exception;
		public static DOWNLOAD_SPEED_LEVEL speed_level;
		public int mergedParts;

		public abstract void start ();
		public string proxyAddress {get; protected set;}
		public int proxyPort {get; protected set;}
		public string proxyUsername {get; protected set;}
		public string proxyPassword {get; protected set;}

		public string authUsername {get; protected set;}
		public string authPassword {get; protected set;}

		public abstract string getFilename ();
		public abstract void resume (long _length);
		public abstract void incrementParts ();
		public abstract bool isResumeSupported ();

		public void setAuthentication (string _username, string _password)
		{
			authUsername = _username;
			authPassword = _password;
		}

		public void stop ()
		{
			foreach (DownloadPart part in listParts)
				part.stopDownload ();

			status = DOWNLOAD_STATUS.PAUSED;
		}

		public void cancel ()
		{
			stop ();
			foreach (HTTPDownloadPart part in listParts)
				part.cancelDownload ();

			status = DOWNLOAD_STATUS.NOT_STARTED;
		}

		public Length getDownloaded ()
		{
			Length totalDownloaded = new Length(0);
			foreach (DownloadPart part in listParts)
			{
				totalDownloaded.value += part.downloaded;
			}
			return totalDownloaded;
		}

		public Speed getSpeed ()
		{
			Length prev_downloaded = _downloaded;
			_downloaded = getDownloaded ();
			return new Speed (_downloaded.value - prev_downloaded.value);
		}

		public float getPartProgress (int part)
		{
			return (100*listParts [part].downloaded)/listParts [part].length;
		}

		public string getPartStatusString (int part)
		{
			return listParts [part].statusString;
		}

		public void setProxy (string _address, int port, string usr = "", string pwd = "")
		{
			proxyAddress = _address;
			proxyPort = port;
			proxyUsername = usr;
			proxyPassword = pwd;
		}

		public void OnPartDownloaded (DownloadPart part)
		{
			foreach (DownloadPart _part in listParts)
			{
				if (_part.status == DOWNLOAD_PART_STATUS.DOWNLOADING)
					return;
			}
			status = DOWNLOAD_STATUS.MERGING;
			_mergeThread = new Thread (_mergeParts);
			_mergeThread.Start ();
		}

		private void _mergeParts ()
		{
			FileStream fs = new FileStream (localPath,
			                                FileMode.OpenOrCreate,
			                                FileAccess.Write);
			mergedParts = 0;
			foreach (DownloadPart part in listParts)
			{
				FileStream _fs = new FileStream (part.localPath, 
				                                 FileMode.Open,
				                                 FileAccess.Read);
				byte[] read = new byte[1024];
				int count = _fs.Read (read, 0, 1024);

				while (count > 0)
				{
					fs.Write (read, 0, count);
					count = _fs.Read (read, 0, 1024);
				}
				_fs.Close ();
				if (File.Exists (part.localPath))
					File.Delete (part.localPath);
				mergedParts+=1;
			}
			fs.Close ();
			status = DOWNLOAD_STATUS.DOWNLOADED;
		}

		protected void OnPartError (DownloadPart part)
		{
			if (maxReTryingAttempts == part.reTryingAttempts)
			{
				stop ();
				status = DOWNLOAD_STATUS.ERROR;
				exception = new DownloadException ("Connection Failed", 
				                                   DOWNLOAD_EXCEPTION_TYPE.CONNECTION_ERROR);
				return;
			}

			part.statusString = "Retrying...";
			part.startDownload ();
		}

		public static Download loadFromXML (string xml)
		{
			MatchCollection mc = Regex.Matches (xml, ".+");

			return null;
		}
	}

	public abstract class DownloadPart
	{
		public long downloaded;
		public short partNumber;
		public WebProxy webProxy;
		public NetworkCredential credentials;
		private DOWNLOAD_PART_STATUS _status;
		public DOWNLOAD_PART_STATUS status
		{
			get
			{
				return _status;
			} 

			set
			{
				_status = value;
				if (_status == DOWNLOAD_PART_STATUS.DOWNLOADED && 
				    downloadedFunction != null)
					downloadedFunction (this);
			}
		}

		public DOWNLOAD_SPEED_LEVEL speed_level;
		public long length;
		public string statusString;

		protected bool _stop;
		public string remotePath, localPath;
		protected long start, end;
		protected Thread downloadThread;
		public int reTryingAttempts;

		public delegate void OnDownloaded (DownloadPart part);
		public OnDownloaded downloadedFunction {get; set;}
		public delegate void OnError (DownloadPart part);
		public OnError errorFunction {get; set;}

		public abstract void startDownload ();
		public abstract void resumeDownload ();

		public void stopDownload ()
		{
			_stop = true;
			status = DOWNLOAD_PART_STATUS.IDLE;
			downloadThread.Join ();
		}

		public void reTry ()
		{
			reTryingAttempts += 1;
			if (downloaded == 0)
				resumeDownload ();
			else
				startDownload ();
		}

		protected void _download (Stream Answer, FileStream fs)
		{
			reTryingAttempts = 0;
			byte[] read = new byte[1024];
			int count = Answer.Read (read, 0, 1024);

			while (count > 0 && !_stop)
			{
				downloaded += count;
				if (downloaded > length)
				{
					count -= (int)(downloaded - length);
					fs.Write (read, 0, count);
					break;
				}
				fs.Write (read, 0, count);
				count = Answer.Read (read, 0, 1024);
				//Console.WriteLine ("Received {0} {1}", partNumber, downloaded);
				if (Download.speed_level == DOWNLOAD_SPEED_LEVEL.LOW)
					System.Threading.Thread.Sleep (100);
				else //speed_level == DOWNLOAD_SPEED_LEVEL.MEDIUM
					System.Threading.Thread.Sleep (10);
			}
		}

		public void cancelDownload ()
		{
			if (File.Exists (localPath))
				File.Delete (localPath);
		}
	}
}

