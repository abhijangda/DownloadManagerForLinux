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
			SetSizeRequest (500,450);
			dwnload = _dwnload;
			lblAddress.Text = dwnload.download.remotePath;
			lblDestination.Text = dwnload.download.localPath;
			downloadThread = new Thread (startDownloading);
			downloadThread.Start ();
			lblStatus.Text = "Sending HEAD";
			for (int i = 0; i < dwnload.download.parts; i++)
			{
			    partsProgress.appendPart ();
			}
			Resizable = false;
		}

		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			if (dwnload.download.status == DOWNLOAD_STATUS.MERGING)
				return true;

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
				btnClose.Sensitive = true;
				Console.WriteLine ("Complete");
				dmprogressbar.setProgress ((float)1.0);
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				btnStartPause.Label = "Pause";
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.ERROR)
			{
				lblStatus.Text = dwnload.download.exception.Message;
				btnStartPause.Label = "Retry";
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.NOT_STARTED)
			{
				btnStartPause.Label = "Start";
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.MERGING)
			{
				lblStatus.Text = "Merging...";
				btnCancel.Sensitive = false;
				btnStartPause.Sensitive = false;
				btnClose.Sensitive = false;
				Gtk.TreeIter iter;
				partsProgress._treeView.Model.GetIterFirst (out iter);
				do
				{
					partsProgress._listStore.SetValue (iter, 1, "Downloaded");
					partsProgress._listStore.SetValue (iter, 2, (float)100.0);
				}
				while (partsProgress._treeView.Model.IterNext (ref iter));
			}
			else //dwnld.status == DOWNLOAD_STATUS.PAUSED
			{
				lblTimeLeft.Text = "Pause";
				btnStartPause.Label = "Start";
			}
		}

		public void startDownloading ()
		{
			dwnload.download.start ();
			if (dwnload.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				Gtk.Application.Invoke (delegate {
					lblStatus.Text = "0 / " + dwnload.download.length.ToString ();
					lblResumeSupport.Text = dwnload.download.isResumeSupported ().ToString ();
					lblAddress.Text = dwnload.download.remotePath;
					lblDestination.Text = dwnload.download.localPath;
				});
			}
		}

		public void updateProgress (Length downloaded, Speed speed)
		{
			if (dwnload.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				lblSpeed.Text = speed.ToString ();
				lblStatus.Text = downloaded.ToString () + " / " + dwnload.download.length.ToString ();
				lblTimeLeft.Text = MainWindow.getTime (dwnload.download.length.value - downloaded.value, speed.value);
				if (dwnload.download.length.value != 0)
				{
					dmprogressbar.setProgress (
						(float)((double)downloaded.value / dwnload.download.length.value));
					Gtk.TreeIter iter;
					partsProgress._listStore.GetIterFirst (out iter);
					for (int i = 0; i < dwnload.download.parts; i++)
					{
						partsProgress._listStore.SetValue (
							iter, 1, dwnload.download.getPartStatusString (i));
						partsProgress._listStore.SetValue (
							iter, 2, dwnload.download.getPartProgress (i));
						partsProgress._listStore.IterNext (ref iter);
					}
				}
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.MERGING)
			{
				lblStatus.Text = "Merging...";
				lblSpeed.Text = "";
				lblTimeLeft.Text = "";
				btnClose.Sensitive = false;
				btnStartPause.Sensitive = false;
				btnCancel.Sensitive = false;
				dmprogressbar.setProgress (
					(float)dwnload.download.mergedParts / dwnload.download.parts);
			}
		}

		protected void btnStartPauseClicked (object sender, EventArgs e)
		{
			if (dwnload.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				dwnload.download.stop ();
			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.ERROR)
			{
				downloadThread = new Thread (startDownloading);
				downloadThread.Start ();
				lblStatus.Text = "Connecting";
				btnStartPause.Label = "Pause";
			}
			else
			{
				dwnload.download.resume (dwnload.download.length.value);
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

