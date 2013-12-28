using System;
using libDownload;
using Gtk;
using System.Threading;

namespace DownloadManager
{
	public partial class ProgressWindow : Gtk.Window
	{
		private Thread downloadThread;
		public DMDownload dwnload {get; set;}

		public ProgressWindow (DMDownload _dwnload) : base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.SetDefaultSize (400,300);
			dwnload = _dwnload;
			lblAddress.Text = dwnload.download.remotePath;
			lblDestination.Text = dwnload.download.localPath;
			downloadThread = new Thread (startDownloading);
			downloadThread.Start ();
			lblStatus.Text = "Sending HEAD";
		}

		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			dwnload.window = null;
			return base.OnDeleteEvent (evnt);
		}

		public void downloadStatusChanged ()
		{
			if (dwnload.download.status == DOWNLOAD_STATUS.DOWNLOADED)
			{
				lblStatus.Text = "Download Completed";
				btnCancel.Sensitive = false;
				btnStartPause.Sensitive = false;
				lblTimeLeft.Text = "";
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				btnStartPause.Label = "Paused";
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.ERROR)
			{
				lblStatus.Text = "Error";
				btnStartPause.Label = "Retry";
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.NOT_STARTED)
			{
				btnStartPause.Label = "Start";
			}
			else //dwnld.status == DOWNLOAD_STATUS.PAUSED
			{
				lblTimeLeft.Text = "Paused";
				btnStartPause.Label = "Start";
			}
		}

		public void startDownloading ()
		{
			dwnload.download.start ();
			Gtk.Application.Invoke (delegate {
				lblStatus.Text = "0 / " + dwnload.download.length.ToString ();
				lblResumeSupport.Text = dwnload.download.isResumeSupported ().ToString ();
				lblAddress.Text = dwnload.download.remotePath;
			});
		}

		public void updateProgress (long downloaded, long speed)
		{
			lblSpeed.Text = speed.ToString ();
			lblStatus.Text = downloaded.ToString () + " / " + dwnload.download.length.ToString ();
			lblTimeLeft.Text = MainWindow.getTime (dwnload.download.length - downloaded, speed);
			if (dwnload.download.length != 0)
			{
				dmprogressbar.Fraction = (double)downloaded / dwnload.download.length;
			}
		}

		protected void btnStartPauseClicked (object sender, EventArgs e)
		{
			if (dwnload.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				dwnload.download.stop ();
			}
			else
			{
				dwnload.download.resume (dwnload.download.length);
			}
		}

		protected void btnCancelClicked (object sender, EventArgs e)
		{
			dwnload.download.stop ();
		}

		protected void btnCloseClicked (object sender, EventArgs e)
		{
			dwnload.window = null;
			Destroy ();
		}
	}
}

