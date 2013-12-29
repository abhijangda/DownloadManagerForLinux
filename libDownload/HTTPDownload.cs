using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace libDownload
{
	public class HTTPDownloadPart : DownloadPart
	{
		HttpWebRequest webReq;
		HttpWebResponse webResp;

		public HTTPDownloadPart (string _remotePath, string _localPath,
		                         long _start, long _end, short _number)
		{
			remotePath = _remotePath;
			localPath = _localPath;
			start = _start;
			end = _end;
			length = end - start;
			partNumber = _number;
			downloaded = 0;
			_stop = false;
			status = DOWNLOAD_PART_STATUS.IDLE;
			speed_level = DOWNLOAD_SPEED_LEVEL.HIGH;
			statusString = "";
		}

		public override void startDownload ()
		{
			downloadThread = new Thread (_startDownload);
			downloadThread.Start ();
		}

		void _startDownload ()
		{
			try
			{
				statusString = "Sending GET...";
				status = DOWNLOAD_PART_STATUS.DOWNLOADING;
				webReq = (HttpWebRequest)WebRequest.Create (remotePath);
				webReq.Method = "GET";
				webReq.AddRange (start, end);
				downloaded = 0;
				Console.WriteLine ("Sending Get {0} {1} {2}", 
				                   partNumber, start, end);
				webResp = (HttpWebResponse)webReq.GetResponse ();
				Console.WriteLine ("Start Receiving {0} {1} {2}", 
				                   partNumber, start, end);
				Stream Answer = webResp.GetResponseStream ();
				FileStream fs = new FileStream (localPath, 
				                                FileMode.OpenOrCreate);
				statusString = "Downloading...";
				_download (Answer, fs);
				webResp.Close();
				fs.Close ();
				_stop = false;
				status = DOWNLOAD_PART_STATUS.DOWNLOADED;
				statusString = "Done";
			}
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				status = DOWNLOAD_PART_STATUS.ERROR;
				errorFunction (this);
				statusString = "Error...";
			}

			Console.WriteLine ("DONEDDDDDDDDD {0}", partNumber);
		}

		public override void stopDownload ()
		{
			_stop = true;
			status = DOWNLOAD_PART_STATUS.IDLE;
			downloadThread.Join ();
		}

		public override void resumeDownload ()
		{
			downloadThread = new Thread (_resumeDownload);
			downloadThread.Start ();
		}

		void _resumeDownload ()
		{
			try
			{
				statusString = "Sending GET...";
				status = DOWNLOAD_PART_STATUS.DOWNLOADING;
				webReq = (HttpWebRequest)WebRequest.Create (remotePath);
				webReq.Method = "GET";
				FileStream fs = new FileStream (localPath, FileMode.Append);
				start = downloaded = fs.Length;

				webReq.AddRange (start, end);
				Console.WriteLine ("Sending Get {0} {1} {2}",
				                   partNumber, start, end);
				webResp = (HttpWebResponse)webReq.GetResponse ();
				Console.WriteLine ("Start Receiving {0} {1} {2}", 
				                   partNumber, start, end);
				Stream Answer = webResp.GetResponseStream ();
				statusString = "Downloading...";
				_download (Answer, fs);
				webResp.Close();
				fs.Close ();
				_stop = false;
				status = DOWNLOAD_PART_STATUS.DOWNLOADED;
				statusString = "Done";
			}
			catch (Exception e)
			{
				status = DOWNLOAD_PART_STATUS.ERROR;
				errorFunction (this);
				statusString = "Connection Error...";
			}
		}

		void _download (Stream Answer, FileStream fs)
		{
			byte[] read = new byte[1024];
			int count = Answer.Read (read, 0, 1024);

			while (count > 0 && !_stop)
			{
				downloaded += count;
				fs.Write (read, 0, count);
				count = Answer.Read (read, 0, 1024);
				//Console.WriteLine ("Received {0} {1}", partNumber, downloaded);
				if (speed_level == DOWNLOAD_SPEED_LEVEL.LOW)
				    System.Threading.Thread.Sleep (100);
				else //speed_level == DOWNLOAD_SPEED_LEVEL.MEDIUM
					System.Threading.Thread.Sleep (10);
			}
		}

		public override void cancelDownload ()
		{
			if (File.Exists (localPath))
				File.Delete (localPath);
		}
	}

	public class HTTPDownload : Download
	{
		HttpWebRequest webReq;
		HttpWebResponse webResp;
		Thread _mergeThread;

		public override DOWNLOAD_STATUS status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
				if (statusChangeHandler != null)
				     statusChangeHandler (this);
			}
		}

		public HTTPDownload (string _remotePath, string _localPath, 
		                     OnStatusChanged _statusChangedHandler,
		                     short _parts = 5)
		{
			remotePath = _remotePath;
			localPath = _localPath;
			parts = _parts;
			listParts = new List<DownloadPart> ();
			_downloaded = 0;
			statusChangeHandler = _statusChangedHandler;
			status = DOWNLOAD_STATUS.NOT_STARTED;
			webResp = null;
			webReq = null;
			speed_level = DOWNLOAD_SPEED_LEVEL.HIGH;
		}


		public override bool isResumeSupported ()
		{
			if (webResp != null && webResp.Headers.ToString ().Contains ("Accept-Ranges: bytes"))
				return true;

			return false;
		}

		bool canResume ()
		{
			if (status == DOWNLOAD_STATUS.PAUSED)
				return true;

			return false;
		}

		public override void start ()
		{
			if (status == DOWNLOAD_STATUS.DOWNLOADING || 
			    status == DOWNLOAD_STATUS.DOWNLOADED)
				return;

			if (canResume ())
			{
				resume (length);
				return;
			}

			string uri = remotePath;
			try
			{
				do
				{
					webReq = (HttpWebRequest)WebRequest.Create (uri);
					webReq.Method = "HEAD";
					webReq.UserAgent = "DML"; /*TODO: Change user agent*/
					webResp = (HttpWebResponse)webReq.GetResponse ();
					remotePath = uri;
				}
				while ((uri = getRedirectUrl (webResp)) != "");
			}

			catch (Exception e)
			{
				Console.WriteLine ("GOT EXCEPTION");
				exception = new DownloadException (e.Message,
				                               DOWNLOAD_EXCEPTION_TYPE.CONNECTION_ERROR);
				status = DOWNLOAD_STATUS.ERROR;
				return;
			}

			if (isResumeSupported () == false)
				parts = 1;

			length = webResp.ContentLength;
			long part_length = length/parts;
			long prev_length = 0, next_length = part_length;
			string _localPath;

			for (short i = 1; i < parts; i++)
			{
				_localPath = localPath + ".part" + i.ToString();
				listParts.Add (new HTTPDownloadPart (remotePath, _localPath, 
				                                     prev_length, next_length - 1, i));
				Console.WriteLine ("Part {0}", i);
				prev_length += part_length;
				next_length += part_length;
			}
			_localPath = localPath + ".part" + (parts).ToString();
			listParts.Add (new HTTPDownloadPart (remotePath, _localPath, 
			                                     prev_length, length - 1, parts));
			foreach (HTTPDownloadPart part in listParts)
			{
				part.downloadedFunction = OnPartDownloaded;
				part.errorFunction = OnPartError;
				part.startDownload ();
			}

			status = DOWNLOAD_STATUS.DOWNLOADING;
		}

		public override void stop ()
		{
			foreach (HTTPDownloadPart part in listParts)
				part.stopDownload ();

			status = DOWNLOAD_STATUS.PAUSED;
		}

		public override void cancel ()
		{
			stop ();
			foreach (HTTPDownloadPart part in listParts)
				part.cancelDownload ();

			status = DOWNLOAD_STATUS.NOT_STARTED;
		}

		public override void resume (long _length)
		{
			if (status != DOWNLOAD_STATUS.PAUSED)
				return;

			if (listParts.Count == 0)
			{
				for (int i = 1; i <= parts; i++)
				{
					if (!File.Exists (localPath + ".part" + i.ToString ()))
					{
						status = DOWNLOAD_STATUS.ERROR;
						exception = new DownloadException ("Cannot find file " + 
						                             localPath + ".part" +
						                             i.ToString (),
						                                   DOWNLOAD_EXCEPTION_TYPE.FILESYSTEM_ERROR);
					}
				}

				length = _length;
				long part_length = length/parts;
				long prev_length = 0, next_length = part_length;
				string _localPath;

				for (short i = 1; i < parts; i++)
				{
					_localPath = localPath + ".part" + i.ToString();
					listParts.Add (new HTTPDownloadPart (remotePath, _localPath, 
					                                     prev_length, next_length, i));
					Console.WriteLine ("Part {0}", i);
					prev_length += part_length;
					next_length += part_length;
				}

				_localPath = localPath + ".part" + (parts).ToString();
				listParts.Add (new HTTPDownloadPart (remotePath, _localPath, 
				                                     prev_length, length, parts));
			}

			foreach (HTTPDownloadPart part in listParts)
			{
				part.downloadedFunction = OnPartDownloaded;
				part.resumeDownload ();
			}

			status = DOWNLOAD_STATUS.DOWNLOADING;
		}

		public override void OnPartDownloaded (DownloadPart part)
		{
			foreach (HTTPDownloadPart _part in listParts)
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

		public override string getRedirectUrl (HttpWebResponse webresponse)
		{
			string uri="";

			WebHeaderCollection headers = webresponse.Headers;

			if ((webresponse.StatusCode == HttpStatusCode.Found) || 
			    (webresponse.StatusCode == HttpStatusCode.Redirect) ||
			    (webresponse.StatusCode == HttpStatusCode.Moved) ||
			    (webresponse.StatusCode == HttpStatusCode.MovedPermanently))
			{
				// Get redirected uri
				uri = headers["Location"] ;
				uri = uri.Trim();
			}
			return uri;
		}
		public override void incrementParts ()
		{

		}

		public override short getParts ()
		{
			return parts;
		}

		public override long getDownloaded ()
		{
			long totalDownloaded = 0;
			foreach (HTTPDownloadPart part in listParts)
			{
				totalDownloaded += part.downloaded;
			}
			return totalDownloaded;
		}

		public override long getSpeed ()
		{
			long prev_downloaded = _downloaded;
			_downloaded = getDownloaded ();
			return (_downloaded - prev_downloaded);
		}

		private void OnPartError (DownloadPart part)
		{
			part.statusString = "Trying Again...";
			part.startDownload ();
		}
	}
}