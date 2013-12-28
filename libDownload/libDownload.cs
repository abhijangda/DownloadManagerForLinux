using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace libDownload
{
	public class DownloadException : Exception
	{
		public DownloadException ()
		{
		}

		public DownloadException (string message): base (message)
		{
		}
	}

	public enum DOWNLOAD_PART_STATUS
	{
		IDLE, 
		DOWNLOADING,
		DOWNLOADED
	};

	public enum DOWNLOAD_STATUS
	{
		NOT_STARTED,
		DOWNLOADING,
		PAUSED,
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

		public string remotePath, localPath;
		public short parts;
		public long length;
		public List<DownloadPart> listParts;
		public abstract DOWNLOAD_STATUS status {get;set;}

		public delegate void OnStatusChanged (Download dwnld);
		public OnStatusChanged statusChangeHandler;
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

		public abstract void start ();
		public abstract void stop ();
		public abstract void resume (long _length);
		public abstract void cancel ();
		public abstract void incrementParts ();
		public abstract short getParts ();
		public abstract long getDownloaded ();
		public abstract bool isResumeSupported ();
		public abstract long getSpeed ();
		public abstract string getRedirectUrl (HttpWebResponse webresponse);
		public abstract void OnPartDownloaded (DownloadPart part);
	}

	public abstract class DownloadPart
	{
		public long downloaded;
		public short partNumber;
		public DOWNLOAD_PART_STATUS status;
		public DOWNLOAD_SPEED_LEVEL speed_level;

		protected bool _stop;
		protected string remotePath, localPath;
		protected long start, end;
		protected Thread downloadThread;

		public delegate void OnDownloaded (DownloadPart part);
		public OnDownloaded downloadedFunction {get; set;}
		public abstract void startDownload ();
		public abstract void stopDownload ();
		public abstract void resumeDownload ();
		public abstract void cancelDownload ();
	}
}

