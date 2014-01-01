using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace libDownload
{
	public enum DOWNLOAD_EXCEPTION_TYPE
	{
		CONNECTION_ERROR,
		FILESYSTEM_ERROR
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
		protected long _downloaded;
		protected DOWNLOAD_STATUS _status;
		protected Thread _mergeThread;

		public const int maxReTryingAttempts = 5;
		public string remotePath, localPath;
		public short parts;
		public long length;
		public List<DownloadPart> listParts;
		public abstract DOWNLOAD_STATUS status {get;set;}
		public bool generateFileName {get;set;}

		public delegate void OnStatusChanged (Download dwnld);
		public OnStatusChanged statusChangeHandler;
		public DownloadException exception;
		private DOWNLOAD_SPEED_LEVEL _speed_level;
		public DOWNLOAD_SPEED_LEVEL speed_level
		{
			get
			{
				return _speed_level;
			}
			set
			{
				foreach (DownloadPart part in listParts)
					part.speed_level = value;
				_speed_level = value;
			}
		}
		public int mergedParts;

		public abstract void start ();
		public void stop ()
		{
			foreach (DownloadPart part in listParts)
				part.stopDownload ();

			status = DOWNLOAD_STATUS.PAUSED;
		}

		public abstract void resume (long _length);
		public void cancel ()
		{
			stop ();
			foreach (HTTPDownloadPart part in listParts)
				part.cancelDownload ();

			status = DOWNLOAD_STATUS.NOT_STARTED;
		}

		public abstract void incrementParts ();

		public long getDownloaded ()
		{
			long totalDownloaded = 0;
			foreach (DownloadPart part in listParts)
			{
				totalDownloaded += part.downloaded;
			}
			return totalDownloaded;
		}

		public abstract bool isResumeSupported ();
		public long getSpeed ()
		{
			long prev_downloaded = _downloaded;
			_downloaded = getDownloaded ();
			return (_downloaded - prev_downloaded);
		}

		public float getPartProgress (int part)
		{
			return (100*listParts [part].downloaded)/listParts [part].length;
		}

		public string getPartStatusString (int part)
		{
			return listParts [part].statusString;
		}
		public abstract string getFilename ();
		public string proxyAddress;
		public int proxyPort;
		public string proxyUsername;
		public string proxyPassword;

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
	}

	public abstract class DownloadPart
	{
		public long downloaded;
		public short partNumber;
		public WebProxy webProxy;
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

		public void stopDownload ()
		{
			_stop = true;
			status = DOWNLOAD_PART_STATUS.IDLE;
			downloadThread.Join ();
		}

		public abstract void resumeDownload ();

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
				if (speed_level == DOWNLOAD_SPEED_LEVEL.LOW)
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

