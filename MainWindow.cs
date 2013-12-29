using System;
using Gtk;
using System.Collections.Generic;
using libDownload;
using DownloadManager;

public partial class MainWindow: Gtk.Window
{	
	List<DMDownload> listDownloads;
	public static DownloadManager.Settings settingsManager;
	public static MainWindow main_instance;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		listDownloads = new List<DMDownload> ();
		GLib.Timeout.Add (1000, this.updateFunc);
		settingsManager = new DownloadManager.Settings ("");
		Build ();
	}

	public void OnDownloadStatusChanged (Download dwnld)
	{
		foreach (DMDownload dmld in listDownloads)
		{
			if (dmld.download == dwnld)
			{
				if (dmld.window != null)
					dmld.window.downloadStatusChanged ();

				if (dwnld.status == DOWNLOAD_STATUS.DOWNLOADED)
				{
				}
				else if (dwnld.status == DOWNLOAD_STATUS.DOWNLOADING)
				{
				}
				else if (dwnld.status == DOWNLOAD_STATUS.ERROR)
				{
				}
				else if (dwnld.status == DOWNLOAD_STATUS.NOT_STARTED)
				{
				}
				else
				{
				}
			}
		}
	}

	public static string getTime (long remaining, long speed)
	{
		if (speed == 0)
			return "";

		long time = remaining/speed;
		if (time > 3600)
			return (time/3600).ToString () + ":" + ((time%3600)/60).ToString ()
				+ ":" + ((time%3600)%60).ToString ();
			
		if (time > 60)
			return (time/60).ToString () + ":" + (time%60).ToString ();

		return time.ToString ();
	}

	bool updateFunc ()
	{
		foreach (DMDownload dwnld in listDownloads)
		{
			if (dwnld.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				long downloaded = dwnld.download.getDownloaded ();
				long speed = dwnld.download.getSpeed ();
				if (dwnld.window != null)
				    dwnld.window.updateProgress (downloaded, speed);

				dmDownloadTreeView.updateDownloadStatus (dwnld, downloaded, speed);
			}
		}
		return true;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnNewDownloadActivated (object sender, EventArgs e)
	{
		NewDialog new_dlg = new NewDialog ();
		new_dlg.ShowAll ();
		if (new_dlg.Run () == (int)Gtk.ResponseType.Ok)
		{
			if (new_dlg.remotePath == "")
			{
				Gtk.MessageDialog dlg = new Gtk.MessageDialog (
					this, DialogFlags.DestroyWithParent, 
					MessageType.Error, ButtonsType.Ok, 
					"Please enter valid Address");

				dlg.Show ();
				dlg.Run ();
				dlg.Destroy ();
				new_dlg.Destroy ();
				return;
			}

			if (new_dlg.localPath == "")
			{
				Gtk.MessageDialog dlg = new Gtk.MessageDialog (
					this, DialogFlags.DestroyWithParent, 
				    MessageType.Error, ButtonsType.Ok, 
				    "Please enter valid Destination");

				dlg.Show ();
				dlg.Run ();
				dlg.Destroy ();
				new_dlg.Destroy ();
				return;
			}

			Download dl = new HTTPDownload (new_dlg.remotePath,
			                                new_dlg.localPath,
			                                OnDownloadStatusChanged);

			Console.WriteLine (new_dlg.localPath);
			DMDownload dmdl = new DMDownload (dl, null);
			dmdl.typeCategory = new DMTypeCategory (new_dlg.typeCategory);
			listDownloads.Add (dmdl);
			dmDownloadTreeView.addDownloadRow (dmdl);

			if (new_dlg.start == 0)
			{
				/*Start Download Now*/
				ProgressWindow progress_dlg = new ProgressWindow (dmdl);
				progress_dlg.ShowAll ();
				dmdl.window = progress_dlg;
			}
			else if (new_dlg.start == 2)
			{
				/*TODO: Schedule download*/
			}
		}

		new_dlg.Destroy ();
	}

	protected void dmDownloadTreeViewRowActivated (object o, RowActivatedArgs args)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmDownloadTreeView.Selection.GetSelected (out model, out iter);
		DMDownload dmld = (DMDownload) model.GetValue (iter, 7);
		if (dmld.window == null)
			dmld.window = new ProgressWindow (dmld);

		dmld.window.ShowAll ();
	}

	protected void dmCategoryTreeViewRowActivated (object o, RowActivatedArgs args)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmCategoryTreeView.Selection.GetSelected (out model, out iter);
		dmDownloadTreeView.showCategory ((string) model.GetValue (iter, 1));
	}
	
	protected void OnSpeedMediumActivated (object sender, EventArgs e)
	{
		foreach (DMDownload dmld in listDownloads)
			dmld.download.speed_level = DOWNLOAD_SPEED_LEVEL.MEDIUM;
	}

	protected void OnSpeedHighActivated (object sender, EventArgs e)
	{
		foreach (DMDownload dmld in listDownloads)
			dmld.download.speed_level = DOWNLOAD_SPEED_LEVEL.HIGH;
	}

	protected void OnSpeedLowActivated (object o, EventArgs args)
	{
		foreach (DMDownload dmld in listDownloads)
			dmld.download.speed_level = DOWNLOAD_SPEED_LEVEL.LOW;
	}

	protected override bool OnDestroyEvent (Gdk.Event evnt)
	{
		foreach (DMDownload dmld in listDownloads)
		{
			if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				Gtk.MessageDialog msg_dlg =
					new Gtk.MessageDialog (this, DialogFlags.Modal, MessageType.Warning,
				                           ButtonsType.YesNo, 
					                       "Downloads are still in progress."+
					                       " Are you sure you want to close?");
				msg_dlg.ShowAll ();
				if (msg_dlg.Run () == (int)ResponseType.Yes)
					return false;
				return true;
			}
		}
		return true;
	}

	protected void OnToolbarStartActivated (object sender, EventArgs e)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmDownloadTreeView.Selection.GetSelected (out model, out iter);
		DMDownload dmld = (DMDownload) model.GetValue (iter, 7);
		dmld.download.start ();
		dmld.window = new ProgressWindow (dmld);
		dmld.window.ShowAll ();
	}

	protected void OnToolbarPauseActivated (object sender, EventArgs e)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmDownloadTreeView.Selection.GetSelected (out model, out iter);
		DMDownload dmld = (DMDownload) model.GetValue (iter, 7);
		dmld.download.stop ();
	}

	protected void OnToolbarCancelActivated (object sender, EventArgs e)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmDownloadTreeView.Selection.GetSelected (out model, out iter);
		DMDownload dmld = (DMDownload) model.GetValue (iter, 7);
		if (dmld != null)
		    dmld.download.cancel ();
	}

	protected void OnToolbarRestartActivated (object sender, EventArgs e)
	{

	}
	protected void OnToolbarFindActivated (object sender, EventArgs e)
	{

	}
}
