using System;
using libDownload;

namespace DownloadManager
{
	public class DMDownload
	{
		public Download download;
		public ProgressWindow window;
		public DMTypeCategory typeCategory;
		public Gtk.TreeRowReference rowReference;
		public DMQueue queue;

		public DMDownload (Download _dwnld, ProgressWindow wnd)
		{
			download = _dwnld;
			window = wnd;
			queue = null;
		}
	}
}

