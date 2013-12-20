using System;
using libDownload;
using System.Net;

namespace libDownloadTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			Download d = new Download ("http://free.nchc.org.tw/fedora/linux/releases/19/Live/x86_64/Fedora-Live-Desktop-x86_64-19-1.iso", "/home/abhi/Desktop/ff.zip");
			ServicePointManager.DefaultConnectionLimit = 10;
			d.start ();
			while (true)
			{
				Console.WriteLine (d.getDownloaded ());
				foreach (DownloadPart part in d.listParts)
					Console.WriteLine ("Received {0} {1}", part.partNumber, part.downloaded);
			    System.Threading.Thread.Sleep (1000);
			}
		}
	}
}
