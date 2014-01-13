using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace libDownload
{
	public class FTPDownload : Download
	{
		FtpWebRequest webReq;
		FtpWebResponse webResp;

		public override DOWNLOAD_STATUS status {get; protected set;}

		public FTPDownload (string _remotePath, string _localPath, 
		                    OnStatusChanged _statusChangedHandler,
		                    bool _genFile, short _parts = 5)
		{
			listParts = new List<DownloadPart> ();
			remotePath = _remotePath;
			localPath = _localPath;
			_statusChangedHandler = null;
			generateFileName = _genFile;
			parts = _parts;
			webResp = null;
			webReq = null;
			speed_level = DOWNLOAD_SPEED_LEVEL.HIGH;
			proxyAddress = "";
			authUsername = "";
			authPassword = "";
		}

		public override void start ()
		{
			if (status == DOWNLOAD_STATUS.DOWNLOADING || 
			    status == DOWNLOAD_STATUS.DOWNLOADED)
				return;

//			if (canResume ())
//			{
//				resume (length);
//				return;
//			}

			if (generateFileName == false)
			{
				if (!Directory.Exists (Path.GetPathRoot (localPath)))
				{
					status = DOWNLOAD_STATUS.ERROR;
					exception = new DownloadException (
						"Cannot find "+Path.GetPathRoot (localPath), 
						DOWNLOAD_EXCEPTION_TYPE.FILESYSTEM_ERROR);

					return;
				}
			}
			else 
			{
				if (!Directory.Exists (localPath))
				{
					status = DOWNLOAD_STATUS.ERROR;
					exception = new DownloadException (
						"Cannot find "+ localPath, DOWNLOAD_EXCEPTION_TYPE.FILESYSTEM_ERROR);
					return;
				}
			}

			WebProxy proxy = null;
			if (proxyAddress != "")
			{
				proxy = new WebProxy (proxyAddress + ":" + proxyPort.ToString (), true);
				if (proxyUsername != "")
					proxy.Credentials = new NetworkCredential (proxyUsername, 
					                                           proxyPassword);
			}

			NetworkCredential credential = null;
			if (authUsername != "")
				credential = new NetworkCredential (authUsername, authPassword);

			try
			{
			    webReq = (FtpWebRequest)WebRequest.Create (remotePath);
				webReq.UseBinary = true;
				webReq.Proxy = proxy;
				webReq.Method = WebRequestMethods.Ftp.DownloadFile;
				webReq.Credentials = credential;
				webResp = (FtpWebResponse)webReq.GetResponse ();
			}

			catch (Exception e)
			{
				exception = new DownloadException (e.Message,
				                                   DOWNLOAD_EXCEPTION_TYPE.CONNECTION_ERROR);
				status = DOWNLOAD_STATUS.ERROR;
				return;
			}

			//if (isResumeSupported () == false)
			//	parts = 1;
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
				listParts.Add (new FTPDownloadPart (remotePath, _localPath, 
				                                     prev_length, next_length - 1, i));
				Console.WriteLine ("Part {0}", i);
				prev_length += part_length;
				next_length += part_length;
			}
			_localPath = localPath + ".part" + (parts).ToString();
			listParts.Add (new FTPDownloadPart (remotePath, _localPath, 
			                                     prev_length, length.value - 1, parts));
			foreach (DownloadPart part in listParts)
			{
				part.webProxy = proxy;
				part.downloadedFunction = OnPartDownloaded;
				part.errorFunction = OnPartError;
				part.credentials = credential;
				part.startDownload ();
			}

			status = DOWNLOAD_STATUS.DOWNLOADING;
		}

		protected override void createPartsFromFiles (long _length = 0)
		{
			 
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

				length = new Length (_length);
				long part_length = length.value/parts;
				long prev_length = 0, next_length = part_length;
				string _localPath;

				for (short i = 1; i < parts; i++)
				{
					_localPath = localPath + ".part" + i.ToString();
					listParts.Add (new FTPDownloadPart (remotePath, _localPath, 
					                                    prev_length, next_length, i));
					Console.WriteLine ("Part {0}", i);
					prev_length += part_length;
					next_length += part_length;
				}

				_localPath = localPath + ".part" + (parts).ToString();
				listParts.Add (new FTPDownloadPart (remotePath, _localPath, 
				                                    prev_length, length.value, parts));
			}

			NetworkCredential credential = null;
			if (authUsername != "")
				credential = new NetworkCredential (authUsername, authPassword);

			foreach (DownloadPart part in listParts)
			{
				part.downloadedFunction = OnPartDownloaded;
				part.credentials = credential;
				part.resumeDownload ();
			}

			status = DOWNLOAD_STATUS.DOWNLOADING;
		}

		public override void incrementParts ()
		{
		}

		public override bool isResumeSupported ()
		{
			return true;
		}

		protected override string getFilename ()
		{
			string filename = remotePath.Substring (remotePath.LastIndexOf ("/") +1);
			return System.Net.WebUtility.UrlDecode (filename);
		}
	}

	public class FTPDownloadPart : DownloadPart
	{
		FtpWebRequest webReq;
		FtpWebResponse webResp;

		public FTPDownloadPart (string _remotePath, string _localPath,
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
		}

		public override void startDownload ()
		{
			downloadThread = new Thread (_startDownload);
			downloadThread.Start ();
		}

		void _startDownload ()
		{
			Stream Answer = null;
			FileStream fs = null;
			try
			{
				statusString = "Sending RETR...";
				status = DOWNLOAD_PART_STATUS.DOWNLOADING;
				webReq = (FtpWebRequest)WebRequest.Create (remotePath);
				webReq.Proxy = webProxy;
				webReq.Method = WebRequestMethods.Ftp.DownloadFile;
				webReq.ContentOffset = start;
				webReq.Credentials = credentials;
				downloaded = 0;
				length = end - start;
				Console.WriteLine ("Sending Get {0} {1} {2}", 
				                   partNumber, start, end);
				webResp = (FtpWebResponse)webReq.GetResponse ();
				Console.WriteLine ("Start Receiving {0} {1} {2}", 
				                   partNumber, start, end);
				Answer = webResp.GetResponseStream ();
				fs = new FileStream (localPath, 
				                     FileMode.OpenOrCreate);
				statusString = "Downloading...";
				_download (Answer);
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
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				status = DOWNLOAD_PART_STATUS.ERROR;
				errorFunction (this, true);
				statusString = "Error...";
			}
			finally
			{
				if (Answer != null)
				    Answer.Close ();
				if (fs != null)
				    fs.Close ();
				webResp.Close();
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
			Stream Answer = null;
			FileStream fs = null;
			try
			{
				statusString = "Sending RETR...";
				status = DOWNLOAD_PART_STATUS.DOWNLOADING;
				webReq = (FtpWebRequest)WebRequest.Create (remotePath);
				webReq.Method = WebRequestMethods.Ftp.DownloadFile;
				webReq.Credentials = credentials;
				fs = new FileStream (localPath, FileMode.Append);
				start = downloaded = fs.Length;

				webReq.ContentOffset = start;
				Console.WriteLine ("Sending Get {0} {1} {2}",
				                   partNumber, start, end);
				webResp = (FtpWebResponse)webReq.GetResponse ();
				Console.WriteLine ("Start Receiving {0} {1} {2}", 
				                   partNumber, start, end);
				Answer = webResp.GetResponseStream ();
				statusString = "Downloading...";
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
			catch (Exception e)
			{
				status = DOWNLOAD_PART_STATUS.ERROR;
				errorFunction (this, false);
				statusString = "Connection Error...";
			}
			finally
			{
				if (fs != null)
    				fs.Close ();
				if (Answer !=  null)
				    Answer.Close ();
				webResp.Close ();
			}
		}
	}
}

