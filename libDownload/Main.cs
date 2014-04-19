using System;
using libDownload;
using System.Net;

namespace libDownloadTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			HTTPDownload d = new HTTPDownload ("http://kojipkgs.fedoraproject.org//packages/autoconf/2.69/14.fc20/noarch/autoconf-2.69-14.fc20.noarch.rpm", "/home/abhi/autoconf", null, true, 5);
			d.start ();		
		}
	}
}
