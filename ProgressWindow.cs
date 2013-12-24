using System;
using libDownload;
using Gtk;
using System.Threading;

namespace DownloadManager
{
	public partial class ProgressWindow : Gtk.Window
	{
		private Thread downloadThread;
		public Download dwnload {get; set;}

		public ProgressWindow (Download _dwnload) : base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.SetDefaultSize (400,300);
			gotStatus = false;
			dwnload = _dwnload;
			lblAddress.Text = dwnload.remotePath;
			lblDestination.Text = dwnload.localPath;
			downloadThread = new Thread (startDownloading);
			downloadThread.Start ();
			lblStatus.Text = "Sending HEAD";
		}

		public void startDownloading ()
		{
			dwnload.start ();
			Gtk.Application.Invoke (delegate {
				lblStatus.Text = "0 / " + dwnload.length.ToString ();
				lblResumeSupport.Text = dwnload.isResumeSupported ().ToString ();
				lblAddress.Text = dwnload.remotePath;
			});
		}

		public void updateProgress (long downloaded, long speed)
		{
			lblSpeed.Text = speed.ToString ();
			lblStatus.Text = downloaded.ToString () + " / " + dwnload.length.ToString ();
			if (dwnload.length != 0)
			{
			    dmprogressbar.Fraction = (double)downloaded / dwnload.length;
			}
		}
	}
}

