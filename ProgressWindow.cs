using System;
using System.Collections.Generic;
using libDownload;
using Gtk;
using System.Threading;

namespace DownloadManager
{
	public partial class ProgressWindow : Gtk.Window
	{
		private Thread downloadThread;
		public DMDownload dwnload {get; set;}
		static bool _isPartStatusVisible;
		public static bool isPartStatusVisible 
		{
			get
			{
				return _isPartStatusVisible;
			}
			set
			{
				_isPartStatusVisible = value;
				foreach (ProgressWindow wnd in listObjects)
				{
					wnd.setVisibleWidget ();
				}
			}

		}
		static bool _isProgressBarVisible;
		public static bool isProgressBarVisible 
		{
			get
			{
				return _isProgressBarVisible;
			}
			set
			{
				_isProgressBarVisible = value;
				foreach (ProgressWindow wnd in listObjects)
				{
					wnd.setVisibleWidget ();
				}
			}
		}

		static bool _isStatusVisible;
		public static bool isStatusVisible
		{
			get
			{
				return _isStatusVisible;
			}
			set
			{
				_isStatusVisible = value;
				foreach (ProgressWindow wnd in listObjects)
				{
					wnd.setVisibleWidget ();
				}
			}
		}
		static bool _isTimeLeftVisible;
		public static bool isTimeLeftVisible
		{
			get
			{
				return _isTimeLeftVisible;
			}
			set
			{
				_isTimeLeftVisible = value;
				foreach (ProgressWindow wnd in listObjects)
				{
					wnd.setVisibleWidget ();
				}
			}
		}

		static bool _isSpeedVisible;
		public static bool isSpeedVisible
		{
			get
			{
				return _isSpeedVisible;
			}
			set
			{
				_isSpeedVisible = value;
				foreach (ProgressWindow wnd in listObjects)
				{
					wnd.setVisibleWidget ();
				}
			}
		}

		static List<ProgressWindow> listObjects;

		static ProgressWindow ()
		{
			listObjects = new List<ProgressWindow> ();
			isPartStatusVisible = true;
			isProgressBarVisible = true;
			isStatusVisible = true;
			isSpeedVisible = true;
			isTimeLeftVisible = true;
		}

		public ProgressWindow (DMDownload _dwnload) : base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			SetSizeRequest (500,450);
			dwnload = _dwnload;
			lblAddress.Text = dwnload.download.remotePath;
			lblDestination.Text = dwnload.download.localPath;
			for (int i = 0; i < dwnload.download.parts; i++)
			{
			    partsProgress.appendPart ();
				partsProgress.setPartStatus (
					i, dwnload.download.getPartStatusString (i));
				partsProgress.setPartProgress (
					i, dwnload.download.getPartProgress (i));
			}
			if (dwnload.download.length.value > 0 && 
			    dwnload.download.status != DOWNLOAD_STATUS.NOT_STARTED)
			{
				Length downloaded = dwnload.download.getDownloaded ();
				lblStatus.Text = downloaded.ToString () + " / " + 
					dwnload.download.length.ToString ();
				dmprogressbar.setProgress (
					(float)((double)downloaded.value / dwnload.download.length.value));
			}

			Resizable = false;
			setVisibleWidget ();
			listObjects.Add (this);
			btnStartPause.Label = "Start";
		}

		public void startDownload ()
		{
			lblStatus.Text = "Connecting...";
			btnStartPause.Label = "Pause";
			dmprogressbar.setProgress ((float)0.0);
			for (int i = 0; i < dwnload.download.parts; i++)
			{
				partsProgress.appendPart ();
				partsProgress.setPartStatus (i, "");
				partsProgress.setPartProgress (i, 0);
			}

			downloadThread = new Thread (_startDownloading);
			downloadThread.Start ();
		}

		public void reset ()
		{

		}

		public void setVisibleWidget ()
		{
			foreach (ProgressWindow wnd in listObjects)
			{
				partsProgress.Visible = isPartStatusVisible;
				dmprogressbar.Visible = isProgressBarVisible;
				lblStatus.Visible = isStatusVisible;
				lblSpeed.Visible = isSpeedVisible;
				lblTimeLeft.Visible = isTimeLeftVisible;
			}
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
				for (int i = 0; i < dwnload.download.parts; i++)
				{
					partsProgress.setPartStatus (i, "Downloaded");
					partsProgress.setPartProgress (i, (float)100.0);
				}
			}
			else //dwnld.status == DOWNLOAD_STATUS.PAUSED
			{
				lblTimeLeft.Text = "Pause";
				btnStartPause.Label = "Start";
			}
		}

		public void _startDownloading ()
		{
			MainWindow.main_instance.startDownload (dwnload);
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

					for (int i = 0; i < dwnload.download.parts; i++)
					{
						partsProgress.setPartStatus (
							i, dwnload.download.getPartStatusString (i));
						partsProgress.setPartProgress (
							i, dwnload.download.getPartProgress (i));
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
				Console.WriteLine ("DOWNLOADING");
				dwnload.download.stop ();

			}
			else if (dwnload.download.status == DOWNLOAD_STATUS.ERROR)
			{
				startDownload ();
				Console.WriteLine ("ERROR");
			}
			else
			{
				Console.WriteLine ("Other");
				if (dwnload.download.length != null)
					MainWindow.main_instance.resumeDownload (dwnload);
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