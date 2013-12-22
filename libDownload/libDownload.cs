using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace libDownload
{
	public abstract class Download
	{
		public abstract void start ();
		public abstract void stop ();
		public abstract void resume ();
		public abstract void incrementParts ();
		public short getParts ();
		public long getDownloaded ();
	}
	
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

	public class HTTPDownloadPart
	{
		string remotePath, localPath;
		HttpWebRequest webReq;
		HttpWebResponse webResp;
		long start, end;
		Thread downloadThread;
		public long downloaded;
		public short partNumber;
		bool _stop;
		public DOWNLOAD_PART_STATUS status;

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

		public void startDownload ()
		{
			downloadThread = new Thread (_startDownload);
			downloadThread.Start ();
		}

		public void stopDownload ()
		{
			_stop = true;
			status = DOWNLOAD_PART_STATUS.IDLE;
			downloadThread.Join ();
		}

		public void resumeDownload ()
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

	class HTTPDownload : Download
	{
		string remotePath, localPath;
		HttpWebRequest webReq;
		HttpWebResponse webResp;
		public long length;
		short parts;
		public List<HTTPDownloadPart> listParts;

		public HTTPDownload (string _remotePath, string _localPath,
		                     short _parts = 5)
		{
			remotePath = _remotePath;
			localPath = _localPath;
			parts = _parts;
			listParts = new List<HTTPDownloadPart> ();
		}

		public void start ()
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

		public void stop ()
		{
			foreach (HTTPDownloadPart part in listParts)
				part.stopDownload ();
		}

		public void resume (long _length)
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

		public void incrementParts ()
		{

		}

		public short getParts ()
		{
			return parts;
		}

		public long getDownloaded ()
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

