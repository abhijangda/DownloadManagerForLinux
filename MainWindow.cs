using System;
using Gtk;
using System.Collections.Generic;
using libDownload;
using DownloadManager;

public partial class MainWindow: Gtk.Window
{	
	List<DMDownload> listRunningDownloads;
	List<DMDownload> listAllDownloads;
	public static DownloadManager.Settings settingsManager;
	public static MainWindow main_instance;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		listRunningDownloads = new List<DMDownload> ();
		GLib.Timeout.Add (1000, this.updateFunc);
		settingsManager = new DownloadManager.Settings (
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
		listAllDownloads = new List<DMDownload> ();
		settingsManager.loadDownloads (ref listAllDownloads);
		Build ();
		notebook.CurrentPage = 0;
	}

	public void addToListRunningDownloads (DMDownload dmld)
	{
		listRunningDownloads.Add (dmld);
		listAllDownloads.Add (dmld);
	}

	public void OnDownloadStatusChanged (Download dwnld)
	{
		foreach (DMDownload dmld in listRunningDownloads)
		{
			if (dmld.download == dwnld)
			{
				if (dmld.window != null)
					dmld.window.downloadStatusChanged ();

				if (dwnld.status == DOWNLOAD_STATUS.DOWNLOADED)
				{
					listRunningDownloads.Remove (dmld);
				}
				else if (dwnld.status == DOWNLOAD_STATUS.DOWNLOADING)
				{
				}
				else if (dwnld.status == DOWNLOAD_STATUS.ERROR)
				{
					listRunningDownloads.Remove (dmld);
				}
				else if (dwnld.status == DOWNLOAD_STATUS.NOT_STARTED)
				{
				}
				else if (dwnld.status == DOWNLOAD_STATUS.PAUSED)
				{
					listRunningDownloads.Remove (dmld);
				}
				else //dwnld.status == DOWNLOAD_STATUS.MERGING
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

		return time.ToString () + " sec";
	}

	bool updateFunc ()
	{
		foreach (DMDownload dwnld in listRunningDownloads)
		{
			Length downloaded = dwnld.download.getDownloaded ();
			Speed speed = dwnld.download.getSpeed ();
			if (dwnld.window != null)
			    dwnld.window.updateProgress (downloaded, speed);

			dmDownloadTreeView.updateDownloadStatus (dwnld, downloaded, speed);
		}
		return true;
	}

	private void quitWindow ()
	{
		foreach (DMDownload dmld in listRunningDownloads)
			dmld.download.stop ();

		settingsManager.storeInfo (listAllDownloads);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		foreach (DMDownload dmld in listRunningDownloads)
		{
			if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			{
				Gtk.MessageDialog msg_dlg =
					new Gtk.MessageDialog (this, DialogFlags.Modal, MessageType.Warning,
					                       ButtonsType.YesNo, 
					                       "Downloads are still in progress."+
					                       " Are you sure you want to close?");
				msg_dlg.ShowAll ();
				if (msg_dlg.Run () == (int)ResponseType.No)
				{
					a.RetVal = false;
					return;
				}
				quitWindow ();
				a.RetVal = true;
			}
		}
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

			Download dl;

			if (new_dlg.remotePath.IndexOf ("ftp://") == 0)
			{
				dl = new FTPDownload (new_dlg.remotePath,
				                      new_dlg.localPath,
				                      OnDownloadStatusChanged,
				                      new_dlg.genFilename);
			}
			else
			{
				dl = new HTTPDownload (new_dlg.remotePath,
				                       new_dlg.localPath,
				                       OnDownloadStatusChanged,
				                       new_dlg.genFilename);
			}

			DMDownload dmdl = new DMDownload (dl, null);
			dmdl.typeCategory = new DMTypeCategory (new_dlg.typeCategory);
			addToListRunningDownloads (dmdl);
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
		Download.speed_level = DOWNLOAD_SPEED_LEVEL.MEDIUM;
	}

	protected void OnSpeedHighActivated (object sender, EventArgs e)
	{
		Download.speed_level = DOWNLOAD_SPEED_LEVEL.HIGH;
	}

	protected void OnSpeedLowActivated (object o, EventArgs args)
	{
		Download.speed_level = DOWNLOAD_SPEED_LEVEL.LOW;
	}

	protected override bool OnDestroyEvent (Gdk.Event evnt)
	{
		return true;
	}

	protected void OnToolbarStartActivated (object sender, EventArgs e)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmDownloadTreeView.Selection.GetSelected (out model, out iter);
		DMDownload dmld = (DMDownload) model.GetValue (iter, 7);
		if (dmld != null)
		{
			dmld.download.start ();
		    dmld.window = new ProgressWindow (dmld);
		    dmld.window.ShowAll ();
			listRunningDownloads.Add (dmld);
		}
	}

	protected void OnToolbarPauseActivated (object sender, EventArgs e)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmDownloadTreeView.Selection.GetSelected (out model, out iter);
		DMDownload dmld = (DMDownload) model.GetValue (iter, 7);
		if (dmld != null)
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
