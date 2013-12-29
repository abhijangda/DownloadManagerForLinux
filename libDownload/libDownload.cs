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

		public string remotePath, localPath;
		public short parts;
		public long length;
		public List<DownloadPart> listParts;
		public abstract DOWNLOAD_STATUS status {get;set;}

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
		public float getPartProgress (int part)
		{
			return (100*listParts [part].downloaded)/listParts [part].length;
		}

		public string getPartStatusString (int part)
		{
			return listParts [part].statusString;
		}
	}

	public abstract class DownloadPart
	{
		public long downloaded;
		public short partNumber;
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

		public delegate void OnDownloaded (DownloadPart part);
		public OnDownloaded downloadedFunction {get; set;}
		public delegate void OnError (DownloadPart part);
		public OnError errorFunction {get; set;}
		public abstract void startDownload ();
		public abstract void stopDownload ();
		public abstract void resumeDownload ();
		public abstract void cancelDownload ();
	}
}

