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
					return s + " MB";
				return s.Substring (0, s.IndexOf (".") + 2) + " MB";
			}

			if (this.value > 1024)
			{
				string s = (value/(1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " KB";
				return s.Substring (0, s.IndexOf (".") + 2) + " KB";
			}

			return value.ToString () + " Bytes";
		}

		public static Length operator +(Length l1, Length l2)
		{
			return new Length (l1.value + l2.value);
		}

		public static Length operator *(long value, Length l1)
		{
			return new Length (l1.value * value);
		}

		public static float operator /(Length l1, Length l2)
		{
			return (float)l1.value/l2.value;
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
					return s + " GB/s";
				return s.Substring (0, s.IndexOf (".") + 2) + " GB/s";
			}

			if (this.value > 1024*1024)
			{
				string s = (value/(1024*1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " MB/s";
				return s.Substring (0, s.IndexOf (".") + 2) + " MB/s";
			}

			if (this.value > 1024)
			{
				string s = (value/(1024.0)).ToString ();
				if (s.IndexOf (".") == -1)
					return s + " KB/s";
				return s.Substring (0, s.IndexOf (".") + 2) + " KB/s";
			}

			return value.ToString () + " Bytes/s";
		}

		public static Speed operator +(Speed l1, Speed l2)
		{
			return new Speed (l1.value + l2.value);
		}

		public static Speed operator *(long l, Speed sp)
		{
			return new Speed (sp.value * l);
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
		public string remotePath {get; protected set;}
		public string localPath {get; protected set;}
		public short parts;
		public Length length {get; protected set;}
		public List<DownloadPart> listParts {get; protected set;}
		public abstract DOWNLOAD_STATUS status {get; protected set;}
		public bool generateFileName {get; protected set;}
		public string proxyAddress {get; protected set;}
		public int proxyPort {get; protected set;}
		public string proxyUsername {get; protected set;}
		public string proxyPassword {get; protected set;}
		public string authUsername {get; protected set;}
		public string authPassword {get; protected set;}

		public delegate void OnStatusChanged (Download dwnld);
		public OnStatusChanged statusChangeHandler;
		public DownloadException exception;
		public static DOWNLOAD_SPEED_LEVEL speed_level;
		public int mergedParts;

		public abstract void start ();
		protected abstract string getFilename ();
		public abstract void resume (long _length = 0);
		public abstract void incrementParts ();
		public abstract bool isResumeSupported ();
		protected abstract void createPartsFromFiles (long _length = 0);

		public void restart ()
		{
			cancel ();
			start ();
		}

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

			if (totalDownloaded.value == 0)
				return _downloaded;

			return totalDownloaded;
		}

		public virtual Speed getSpeed ()
		{
			Length prev_downloaded = _downloaded;
			_downloaded = getDownloaded ();
			return new Speed (_downloaded.value - prev_downloaded.value);
		}

		public float getPartProgress (int part)
		{
			if (part >= listParts.Count)
				return (float)-1.0;

			return (100*listParts [part].downloaded)/listParts [part].length;
		}

		public string getPartStatusString (int part)
		{
			if (part >= listParts.Count)
				return "";

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

		protected void _mergeParts ()
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

		protected void OnPartError (DownloadPart part, bool start)
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

			if (start)
			    part.startDownload ();
			else
				part.resumeDownload ();
		}

		public void initFromXML (string xml)
		{
			MatchCollection mc = Regex.Matches (xml, ".+", RegexOptions.None);
			for (int i = 0; i < mc.Count; i++)
			{
				Match m = mc [i];
				if (m.Value.Contains ("<address>"))
				{
					i += 1;
					remotePath = mc [i].Value.Trim ();
					i += 1;
				}

				else if (m.Value.Contains ("<localpath>"))
				{
					i += 1;
					localPath = mc [i].Value.Trim ();
					i += 1;
				}

				else if (m.Value.Contains ("<size>"))
				{
					i += 1;
					length = new Length (long.Parse (mc [i].Value.Trim ()));
					i += 1;
				}

				else if (m.Value.Contains ("<sections>"))
				{
					i += 1;
					parts = short.Parse (mc [i].Value.Trim ());
					i += 1;
				}

				else if (m.Value.Contains ("<downloaded>"))
				{
					i += 1;
					_downloaded = new Length (long.Parse (mc [i].Value.Trim ()));
					i += 1;
				}

				else if (m.Value.Contains ("<download-state>"))
				{
					i += 1;
					string [] arr = Enum.GetNames (typeof (DOWNLOAD_STATUS));
					for (int j = 0; j < arr.Length; j++)
					{
					    if (mc [i].Value.Trim () == arr [j])
						{
							Array array = Enum.GetValues (typeof (DOWNLOAD_STATUS));
							status = (DOWNLOAD_STATUS) array.GetValue (j);
							if (status != DOWNLOAD_STATUS.DOWNLOADED &&
							    status != DOWNLOAD_STATUS.NOT_STARTED)
							{
								Console.WriteLine ("Creating parts");
								createPartsFromFiles ();
							}

							break;
						}
					}

					i += 1;
				}
			}
		}

		public void writeToFiles ()
		{
			foreach (DownloadPart part in listParts)
			{
				part.writeToFile ();
			}
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
				if (_status != DOWNLOAD_PART_STATUS.DOWNLOADING)
					writeToFile ();

				if (_status == DOWNLOAD_PART_STATUS.DOWNLOADED && 
				    downloadedFunction != null)
					downloadedFunction (this);
			}
		}

		protected List<byte []> listdataArray;
		protected List<int> listdataCounts;

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
		public delegate void OnError (DownloadPart part, bool start);
		public OnError errorFunction {get; set;}

		public abstract void startDownload ();
		public abstract void resumeDownload ();
		protected FileStream fs;
		protected Mutex mutex;

		public void stopDownload ()
		{
			if (status != DOWNLOAD_PART_STATUS.DOWNLOADING)
				return;

			//downloadThread.Abort ();
			_stop = true;
			if (fs != null)
				fs.Close ();

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

		protected void _download (Stream Answer)
		{
			reTryingAttempts = 0;
			byte[] read = new byte [1024];
			int count = Answer.Read (read, 0, 1024);

			while (count > 0 && !_stop)
			{
				if (downloaded + count > length)
				{
					count -= (int)(downloaded + count - length);
					mutex.WaitOne ();
					listdataCounts.Add (count);
					listdataArray.Add (read);
					mutex.ReleaseMutex ();
					break;
				}

				//fs.Write (read, 0, count);
				mutex.WaitOne ();
				listdataCounts.Add (count);
				listdataArray.Add (read);
				mutex.ReleaseMutex ();

				downloaded += count;
				count = Answer.Read (read, 0, 1024);

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

		public void writeToFile ()
		{
			if (mutex == null)
				return;

			if (listdataArray.Count == 0)
				return;

			if (!File.Exists (localPath))
				fs = new FileStream (localPath, FileMode.OpenOrCreate);
			else
				fs = new FileStream (localPath, FileMode.Append);

			mutex.WaitOne ();
			for (int i = 0; i < listdataArray.Count; i++)
			{
				fs.Write (listdataArray [i], 0, listdataCounts [i]);
			}
			listdataArray.Clear ();
			listdataCounts.Clear ();
			mutex.ReleaseMutex ();
			fs.Close ();
		}
	}
}

