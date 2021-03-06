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
			reTryingAttempts = 0;
			credentials = null;
			webResp = null;

			Console.WriteLine ("Part {0} Downloaded {1}", 
			                   _number, downloaded);

			listdataArray = new List<byte []> ();
			listdataCounts = new List<int> ();
			mutex = new Mutex ();
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
				if (File.Exists (localPath))
					File.Delete (localPath);

				statusString = "Sending GET...";
				status = DOWNLOAD_PART_STATUS.CONNECTING;
				webReq = (HttpWebRequest)WebRequest.Create (remotePath);
				webReq.Proxy = webProxy;
				webReq.Method = "GET";
				webReq.Credentials = credentials;
				webReq.AddRange (start, end);
				downloaded = 0;
				length = end - start;
				Console.WriteLine ("Sendivvvvng Get {0} {1} {2}", 
				                   partNumber, start, end);

				webResp = (HttpWebResponse)webReq.GetResponse ();
				Console.WriteLine ("Start Receiving {0} {1} {2}", 
				                   partNumber, start, end);
				Stream Answer = webResp.GetResponseStream ();
				statusString = "Downloading...";
				fs = new FileStream (localPath, FileMode.OpenOrCreate);
				status = DOWNLOAD_PART_STATUS.DOWNLOADING;
				_download (Answer);
				webResp.Close();
				fs.Close ();
				if (_stop == true)
				{
					status = DOWNLOAD_PART_STATUS.IDLE;
					statusString = "Paused";
					_stop=false;
				}
				else
				{
					status = DOWNLOAD_PART_STATUS.DOWNLOADED;
					statusString = "Done";
				}
			}
			catch (WebException e)
			{
				if (fs != null)
					fs.Close ();

				if (webResp != null)
					webResp.Close ();

				Console.WriteLine (e.Message);
				status = DOWNLOAD_PART_STATUS.ERROR;
				errorFunction (this, true);
				statusString = "Error...";
			}

			Console.WriteLine ("DONEDDDDDDDDD {0}", partNumber);
		}

		public override void resumeDownload ()
		{
			downloadThread = new Thread (_resumeDownload);
			downloadThread.Start ();
		}

		void _resumeDownload ()
		{
			fs = null;
			try
			{
				if (File.Exists (localPath))
					fs = new FileStream (localPath, FileMode.Append);
				else
					fs = new FileStream (localPath, FileMode.Create);

				downloaded = fs.Length;

				statusString = "Sending GET...";
				status = DOWNLOAD_PART_STATUS.DOWNLOADING;
				webReq = (HttpWebRequest)WebRequest.Create (remotePath);
				webReq.Method = "GET";

				webReq.AddRange (start + downloaded, end);
				Console.WriteLine ("resuming Get {0} {1} {2} {3}",
				                   partNumber, start, end, start/1024,
				                   end/1024, end - start - downloaded);

				webResp = (HttpWebResponse)webReq.GetResponse ();
				Console.WriteLine ("Start resuming {0} {1} {2}", 
				                   partNumber, start, end);

				Stream Answer = webResp.GetResponseStream ();
				statusString = "Downloading...";
				Console.WriteLine ("Part {0} Dowkkknloading {1}", 
				                   partNumber, downloaded);
				_download (Answer);
				webResp.Close();
				fs.Close ();
				if (_stop == true)
				{
					status = DOWNLOAD_PART_STATUS.IDLE;
					statusString = "Paused";
					_stop = false;
				}
				else
				{
					status = DOWNLOAD_PART_STATUS.DOWNLOADED;
					statusString = "Done";
				}

			}

			catch (WebException e)
			{
				if (webResp != null)
					webResp.Close ();

				if (fs != null)
					fs.Close ();

				status = DOWNLOAD_PART_STATUS.ERROR;
				errorFunction (this, false);
				statusString = "Connection Error...";
			}
		}
	}

	public class HTTPDownload : Download
	{
		HttpWebRequest webReq;
		HttpWebResponse webResp;

		public override DOWNLOAD_STATUS status
		{
			get
			{
				return _status;
			}
			protected set
			{
				_status = value;
				if (statusChangeHandler != null)
				     statusChangeHandler (this);
			}
		}

		public HTTPDownload (string _remotePath, string _localPath, 
		                     OnStatusChanged _statusChangedHandler,
		                     bool _genFile, short _parts = 5)
		{
			remotePath = _remotePath;
			localPath = _localPath;
			parts = _parts;
			listParts = new List<DownloadPart> ();
			_downloaded = new Length (0);
			length = new Length (0);
			statusChangeHandler = _statusChangedHandler;
			status = DOWNLOAD_STATUS.NOT_STARTED;
			webResp = null;
			webReq = null;
			speed_level = DOWNLOAD_SPEED_LEVEL.HIGH;
			generateFileName = _genFile;
			/*proxyAddress = "10.1.1.19";
			proxyPort = 80;
			authUsername = "063.7359";
			authPassword = "chandj";
			*/
			proxyAddress = "";
			proxyPort = 0;
			authUsername = "";
			authPassword = "";
		}

		public override bool isResumeSupported ()
		{
			if (webResp != null && 
			    webResp.Headers.ToString ().Contains ("Accept-Ranges: bytes"))
				return true;

			return false;
		}

		bool canResume ()
		{
			if (status == DOWNLOAD_STATUS.PAUSED)
				return true;

			return false;
		}

		protected override string getFilename ()
		{
			if (webResp == null)
				return "";

			string filename = webResp.GetResponseHeader("content-disposition");
			if (filename != "")
			{
				filename = filename.Substring (filename.IndexOf ("filename=") + "filename=".Length);
				filename = filename.Replace ("\"","");
				return filename;
			}
			else
			{
				filename = remotePath.Substring (remotePath.LastIndexOf ("/") +1);
				if (filename.Contains ("."))
				{
					return System.Net.WebUtility.UrlDecode (filename);
				}
				return "file";
			}
		}

		public override void start ()
		{
			if (status == DOWNLOAD_STATUS.DOWNLOADING || 
			    status == DOWNLOAD_STATUS.DOWNLOADED)
				return;

			if (canResume ())
			{
				resume (length.value);
				return;
			}

			if (generateFileName == false)
			{
				if (!Directory.Exists (Path.GetPathRoot (localPath)))
				{
					exception = new DownloadException (
						"Cannot find "+Path.GetPathRoot (localPath), 
						DOWNLOAD_EXCEPTION_TYPE.FILESYSTEM_ERROR);
					status = DOWNLOAD_STATUS.ERROR;
					return;
				}
			}
			else 
			{
				if (!Directory.Exists (localPath))
				{
					exception = new DownloadException (
						"Cannot find "+ localPath, DOWNLOAD_EXCEPTION_TYPE.FILESYSTEM_ERROR);
					status = DOWNLOAD_STATUS.ERROR;
					return;
				}
			}

			string uri = remotePath;
			WebProxy proxy = null;
			if (proxyAddress != "")
			{
				proxy = new WebProxy (proxyAddress + ":" + proxyPort.ToString (), true);
				if (proxyUsername != "")
					proxy.Credentials = new NetworkCredential (proxyUsername, 
					                                           proxyPassword);
			}
			NetworkCredential _credential = null;
			if (authUsername != "")
			{
				_credential = new NetworkCredential (authUsername, authPassword);
			}

			try
			{
				do
				{
					webReq = (HttpWebRequest)WebRequest.Create (uri);
					webReq.Proxy = proxy;
					webReq.Method = "HEAD";
					webReq.UserAgent = "DML"; /*TODO: Change user agent*/
					webReq.Headers.Add ("Accept-Encoding: gzip");
					webReq.Headers.Add ("TransferEncoding: gzip");
					webReq.Credentials = _credential;
					webResp = (HttpWebResponse)webReq.GetResponse ();
					remotePath = uri;
				}
				while ((uri = getRedirectUrl (webResp)) != "");
			}

			catch (Exception e)
			{
				exception = new DownloadException (e.Message,
				                               DOWNLOAD_EXCEPTION_TYPE.CONNECTION_ERROR);
				status = DOWNLOAD_STATUS.ERROR;
				return;
			}

			if (isResumeSupported () == false)
				parts = 1;

			if (generateFileName == true)
			{
			    string filename = getFilename ();
				localPath = Path.Combine (localPath, filename);
			}

			length = new Length (webResp.ContentLength);
			long part_length = length.value/parts;
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
			Console.WriteLine (prev_length + " prev");
			_localPath = localPath + ".part" + (parts).ToString();
			listParts.Add (new HTTPDownloadPart (remotePath, _localPath, 
			                                     prev_length, length.value,
			                                     parts));

			foreach (HTTPDownloadPart part in listParts)
			{
				part.webProxy = proxy;
				part.downloadedFunction = OnPartDownloaded;
				part.errorFunction = OnPartError;
				part.credentials = _credential;
				part.startDownload ();
			}

			status = DOWNLOAD_STATUS.DOWNLOADING;
		}

		public override void resume (long _length)
		{
			if (status != DOWNLOAD_STATUS.PAUSED)
				return;

			createPartsFromFiles (_length);

			NetworkCredential credential = null;
			if (authUsername != "")
				credential = new NetworkCredential (authUsername, authPassword);

			Console.WriteLine ("resuming"+listParts.Count.ToString ());
			foreach (HTTPDownloadPart part in listParts)
			{
				part.downloadedFunction = OnPartDownloaded;
				part.credentials = credential;
				part.errorFunction = OnPartError;
				part.resumeDownload ();
			}

			status = DOWNLOAD_STATUS.DOWNLOADING;
		}

		public string getRedirectUrl (HttpWebResponse webresponse)
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

		/*TODO: Display Error when file is not found in below function*/
		protected override void createPartsFromFiles (long _length = 0)
		{
			if (listParts.Count == 0)
			{
				Console.WriteLine ("list empty" + parts.ToString ());
				for (int i = 1; i <= parts; i++)
				{
					if (!File.Exists (localPath + ".part" + i.ToString ()))
					{
						exception = new DownloadException ("Cannot find file " + 
						                                   localPath + ".part" +
						    				                               i.ToString (),
						                                   DOWNLOAD_EXCEPTION_TYPE.FILESYSTEM_ERROR);
						status = DOWNLOAD_STATUS.ERROR;
						return;
					}
				}

				if (_length != 0)
				    length = new Length (_length);

				long part_length = length.value/parts;
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
				                                     prev_length, length.value,
				                                     parts));
			}

			Console.WriteLine (listParts.Count + "sll");
		}
	}
}