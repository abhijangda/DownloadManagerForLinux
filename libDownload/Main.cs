using System;
using libDownload;
using System.Net;

namespace libDownloadTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			FTPDownload d = new FTPDownload ("ftp://ftp.kernel.org/pub/tools/crosstool/index.html", "/home/abhi/dffs", null, true, 5);
			Console.WriteLine ("dddd");
			d.start ();
		
		}
	}
}
