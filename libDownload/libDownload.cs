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

	public abstract class Download
	{
		protected string remotePath, localPath;
		protected short parts;

		public long length;
		public List<DownloadPart> listParts;

		public abstract void start ();
		public abstract void stop ();
		public abstract void resume (long _length);
		public abstract void incrementParts ();
		public abstract short getParts ();
		public abstract long getDownloaded ();
	}

	public abstract class DownloadPart
	{
		public long downloaded;
		public short partNumber;
		public DOWNLOAD_PART_STATUS status;

		protected bool _stop;
		protected string remotePath, localPath;
		protected long start, end;
		protected Thread downloadThread;

		public abstract void startDownload ();
		public abstract void stopDownload ();
		public abstract void resumeDownload ();
	}

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
		}

		public override void start ()
		{
			webReq = (HttpWebRequest)WebRequest.Create (remotePath);
			webReq.Method = "HEAD";
			webResp = (HttpWebResponse)webReq.GetResponse ();
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
		}

		public override void stop ()
		{
			foreach (HTTPDownloadPart part in listParts)
				part.stopDownload ();
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
	}
}

