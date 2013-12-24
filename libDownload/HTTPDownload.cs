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
			partNumber = _number;
			downloaded = 0;
			_stop = false;
			status = DOWNLOAD_PART_STATUS.IDLE;
		}

		public override void startDownload ()
		{
			downloadThread = new Thread (_startDownload);
			downloadThread.Start ();
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
			status = DOWNLOAD_PART_STATUS.DOWNLOADING;
			webReq = (HttpWebRequest)WebRequest.Create (remotePath);
			webReq.Method = "GET";
			FileStream fs = new FileStream (localPath, FileMode.Append);
			start = fs.Length;
			webReq.AddRange (start, end);
			Console.WriteLine ("Sending Get {0} {1} {2}",
			                   partNumber, start, end);
			webResp = (HttpWebResponse)webReq.GetResponse ();
			Console.WriteLine ("Start Receiving {0} {1} {2}", 
			                   partNumber, start, end);
			Stream Answer = webResp.GetResponseStream ();

			byte[] read = new byte[1024];
			int count = Answer.Read (read, 0, 1024);

			while (count > 0 && !_stop)
			{
				downloaded += count;
				fs.Write (read, 0, count);
				count = Answer.Read (read, 0, 1024);
				//Console.WriteLine ("Received {0} {1}", partNumber, downloaded);
			}

			webResp.Close();
			fs.Close ();
			_stop = false;
			status = DOWNLOAD_PART_STATUS.DOWNLOADED;
		}

		void _startDownload ()
		{
			status = DOWNLOAD_PART_STATUS.DOWNLOADING;
			webReq = (HttpWebRequest)WebRequest.Create (remotePath);
			webReq.Method = "GET";
			webReq.AddRange (start, end);
			Console.WriteLine ("Sending Get {0} {1} {2}", 
			                   partNumber, start, end);
			webResp = (HttpWebResponse)webReq.GetResponse ();
			Console.WriteLine ("Start Receiving {0} {1} {2}", 
			                   partNumber, start, end);
			Stream Answer = webResp.GetResponseStream ();
			FileStream fs = new FileStream (localPath, 
			                                FileMode.OpenOrCreate);

			byte[] read = new byte[1024];
			int count = Answer.Read (read, 0, 1024);

			while (count > 0 && !_stop)
			{
				downloaded += count;
				fs.Write (read, 0, count);
				count = Answer.Read (read, 0, 1024);
				//Console.WriteLine ("Received {0} {1}", partNumber, downloaded);
			}

			webResp.Close();
			fs.Close ();
			_stop = false;
			status = DOWNLOAD_PART_STATUS.DOWNLOADED;
		}
	}

	public class HTTPDownload : Download
	{
		HttpWebRequest webReq;
		HttpWebResponse webResp;

		public HTTPDownload (string _remotePath, string _localPath,
		                     short _parts = 5)
		{
			remotePath = _remotePath;
			localPath = _localPath;
			parts = _parts;
			listParts = new List<DownloadPart> ();
			_downloaded = 0;
			status = DOWNLOAD_STATUS.NOT_STARTED;
		}

		public override bool isResumeSupported ()
		{
			if (webResp.Headers.ToString ().Contains ("Accept-Ranges: bytes"))
				return true;

			return false;
		}

		public override void start ()
		{
			string uri = remotePath;
			do
			{
				try
				{
					webReq = (HttpWebRequest)WebRequest.Create (uri);
					webReq.Method = "HEAD";
					webReq.UserAgent = "DML"; /*TODO: Change user agent*/
					webResp = (HttpWebResponse)webReq.GetResponse ();
					remotePath = uri;
				}
				catch (Exception e)
				{
					status = DOWNLOAD_STATUS.ERROR;
					throw new DownloadException (e.Message);
				}
			}
			while ((uri = getRedirectUrl (webResp)) != "");

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
				                                     prev_length, next_length, i));
				Console.WriteLine ("Part {0}", i);
				prev_length += part_length;
				next_length += part_length;
			}
			_localPath = localPath + ".part" + (parts).ToString();
			listParts.Add (new HTTPDownloadPart (remotePath, _localPath, 
			                                     prev_length, length, parts));
			foreach (HTTPDownloadPart part in listParts)
				part.startDownload ();

			status = DOWNLOAD_STATUS.DOWNLOADING;
		}

		public override void stop ()
		{
			foreach (HTTPDownloadPart part in listParts)
				part.stopDownload ();

			status = DOWNLOAD_STATUS.PAUSED;
		}

		public override void resume (long _length)
		{
			for (int i = 1; i <= parts; i++)
			{
				if (!File.Exists (localPath + ".part" + i.ToString ()))
				{
					throw new DownloadException ("Cannot find file " + 
					                             localPath + ".part" +
					                             i.ToString ());
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
			foreach (HTTPDownloadPart part in listParts)
				part.resumeDownload ();

			status = DOWNLOAD_STATUS.DOWNLOADING;
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
	}
}