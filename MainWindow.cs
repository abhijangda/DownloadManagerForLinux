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
	public Length total_downloaded;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		total_downloaded = new Length (0);
		listRunningDownloads = new List<DMDownload> ();
		GLib.Timeout.Add (1000, this.updateFunc);
		settingsManager = new DownloadManager.Settings (
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
		listAllDownloads = new List<DMDownload> ();
		settingsManager.loadDownloads (ref listAllDownloads);
		Build ();
		HighSpeedAction.Active = true;
		dmDownloadTreeView.listAllDownloads = listAllDownloads;
		dmDownloadTreeView.loadDownloads ();
		notebook.CurrentPage = 0;
		dmDownloadTreeView.Selection.Changed += dmDownloadTreeViewSelectionChanged;
		toolbar1.ToolbarStyle = ToolbarStyle.Icons;
	}

	private void dmDownloadTreeViewSelectionChanged (object o, EventArgs args)
	{
		TreeIter iter;
		dmDownloadTreeView.Selection.GetSelected (out iter);
		DMDownload dmld = (DMDownload) dmCategoryTreeView.Model.GetValue (iter, 7);
		if (dmld == null)
			return;

		if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADED)
		{
			toolbarStart.Sensitive = StartAction.Sensitive = false;
			toolbarCancel.Sensitive = CancelAction.Sensitive = false;
			toolbarPause.Sensitive = PauseAction.Sensitive = false;
			toolbarRestart.Sensitive = RestartAction.Sensitive = true;
		}
		else if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADING)
		{
			toolbarStart.Sensitive = StartAction.Sensitive = false;
			toolbarCancel.Sensitive = CancelAction.Sensitive = true;
			toolbarPause.Sensitive = PauseAction.Sensitive = true;
			toolbarRestart.Sensitive = RestartAction.Sensitive = true;
		}
		else if (dmld.download.status == DOWNLOAD_STATUS.ERROR ||
		         dmld.download.status == DOWNLOAD_STATUS.NOT_STARTED)
		{
			toolbarStart.Sensitive = StartAction.Sensitive = true;
			toolbarCancel.Sensitive = CancelAction.Sensitive = false;
			toolbarPause.Sensitive = PauseAction.Sensitive = false;
			toolbarRestart.Sensitive = RestartAction.Sensitive = false;
		}
		else if (dmld.download.status == DOWNLOAD_STATUS.PAUSED)
		{
			toolbarStart.Sensitive = StartAction.Sensitive = true;
			toolbarCancel.Sensitive = CancelAction.Sensitive = false;
			toolbarPause.Sensitive = PauseAction.Sensitive = true;
			toolbarRestart.Sensitive = RestartAction.Sensitive = true;
		}
	}

	private void updateDMDownloadTreeView ()
	{
		foreach (DMDownload dmld in listAllDownloads)
			dmDownloadTreeView.addDownloadRow (dmld);
	}

	public void addToListRunningDownloads (DMDownload dmld)
	{
		listRunningDownloads.Add (dmld);
		listAllDownloads.Add (dmld);
	}

	public void OnDownloadStatusChanged (Download dwnld)
	{
		DMDownload toRemove = null;
		foreach (DMDownload dmld in listRunningDownloads)
		{
			if (dmld.download == dwnld)
			{
				if (dmld.window != null)
					dmld.window.downloadStatusChanged ();

				if (dwnld.status == DOWNLOAD_STATUS.DOWNLOADED)
				{
					toRemove = dmld;
				}

				else if (dwnld.status == DOWNLOAD_STATUS.DOWNLOADING)
				{
				}

				else if (dwnld.status == DOWNLOAD_STATUS.ERROR)
				{
					toRemove = dmld;
				}

				else if (dwnld.status == DOWNLOAD_STATUS.NOT_STARTED)
				{
				}

				else if (dwnld.status == DOWNLOAD_STATUS.PAUSED)
				{
					toRemove = dmld;
				}

				else //dwnld.status == DOWNLOAD_STATUS.MERGING
				{
				}
			}
		}
		if (toRemove != null)
		    listRunningDownloads.Remove (toRemove);
	}

	public void startDownload (DMDownload dwnld)
	{
		listRunningDownloads.Add (dwnld);
		dwnld.download.start ();
	}

	public void resumeDownload (DMDownload dwnld)
	{
		listRunningDownloads.Add (dwnld);
		dwnld.download.resume (dwnld.download.length.value);
	}

	public static string getTime (long remaining, long speed)
	{
		if (speed == 0)
			return "";

		long time = remaining/speed;
		if (time > 3600)
		{
			string s = (time/3600).ToString (), toret = "";
			if (s.Length == 1)
				toret += "0" + s;
			else
				toret += s;

			s = ((time%3600)/60).ToString ();
			if (s.Length == 1)
				toret += ":0" + s;
			else
				toret += ":" + s;

			s = ((time%3600)%60).ToString ();
			if (s.Length == 1)
				toret += ":0" + s;
			else
				toret += ":" + s;

			return toret;
		}
			
		if (time > 60)
		{
			string s = (time/60).ToString (), toret = "";
			if (s.Length == 1)
				toret += "0" + s;
			else
				toret += s;

			s = (time%60).ToString ();
			if (s.Length == 1)
				toret += ":0" + s;
			else
				toret += ":" + s;

			return toret;
		}

		string k = time.ToString ();
		if (k.Length == 1)
			k = "0" + k;

		return k + " sec";
	}

	bool updateFunc ()
	{
		Speed total_speed = new Speed (0);

		foreach (DMDownload dwnld in listRunningDownloads)
		{
			Length downloaded = dwnld.download.getDownloaded ();
			Speed speed = dwnld.download.getSpeed ();
			if (dwnld.window != null)
			    dwnld.window.updateProgress (downloaded, speed);

			dmDownloadTreeView.updateDownloadStatus (dwnld, downloaded, speed);
			total_speed.value += speed.value;
		}

		lblSpeed.Text = "Speed " + total_speed.ToString ();
		total_downloaded.value += total_speed.value;
		lblDownloaded.Text = "Downloaded " + total_downloaded.ToString ();
		return true;
	}

	private void quitWindow ()
	{
		foreach (DMDownload dmld in listAllDownloads)
			if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADING)
			    dmld.download.stop ();

		settingsManager.storeInfo (listAllDownloads);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		if (listRunningDownloads.Count > 0)
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

		quitWindow ();
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnNewDownloadActivated (object sender, EventArgs e)
	{
		NewDialog new_dlg = new NewDialog ();
		new_dlg.ShowAll ();
		if (new_dlg.Run () == (int)Gtk.ResponseType.Ok)
		{
			createNewDownload (new_dlg.remotePath, new_dlg.localPath,
			                   new_dlg.genFilename, new_dlg.typeCategory,
			                   new_dlg.start);
		}

		new_dlg.Destroy ();
	}

	private bool createNewDownload (string remotePath, string localPath, bool genFilename,
			                        string typeCategory, int start)
	{
		if (remotePath == "")
		{
			Gtk.MessageDialog dlg = new Gtk.MessageDialog (
				this, DialogFlags.DestroyWithParent, 
				MessageType.Error, ButtonsType.Ok, 
				"Please enter valid Address");

			dlg.Show ();
			dlg.Run ();
			dlg.Destroy ();
			return false;
		}

		if (localPath == "")
		{
			Gtk.MessageDialog dlg = new Gtk.MessageDialog (
				this, DialogFlags.DestroyWithParent, 
				MessageType.Error, ButtonsType.Ok, 
				"Please enter valid Destination");

			dlg.Show ();
			dlg.Run ();
			dlg.Destroy ();
			return false;
		}

		Download dl;

		if (remotePath.IndexOf ("ftp://") == 0)
		{
			dl = new FTPDownload (remotePath,
			                      localPath,
			                      OnDownloadStatusChanged,
			                      genFilename);
		}
		else
		{
			dl = new HTTPDownload (remotePath,
			                       localPath,
			                       OnDownloadStatusChanged,
			                       genFilename);
		}

		DMDownload dmdl = new DMDownload (dl, null);
		dmdl.typeCategory = new DMTypeCategory (typeCategory);
		listAllDownloads.Add (dmdl);
		dmDownloadTreeView.addDownloadRow (dmdl);

		if (start == 0)
		{
			/*Start Download Now*/
			ProgressWindow progress_dlg = new ProgressWindow (dmdl);
			progress_dlg.ShowAll ();
			dmdl.window = progress_dlg;
		}
		else if (start == 2)
		{
			/*TODO: Schedule download*/
		}
		return true;
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
			MainWindow.main_instance.startDownload (dmld);
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
		TreeViewColumn [] columns = dmDownloadTreeView.Columns;
		List<string> array = new List<string> ();
		foreach (TreeViewColumn column in columns)
		{
			array.Add (column.Title);
		}

		FindDialog find_dlg = new FindDialog (array, dmDownloadTreeView.searchInColumns);
		find_dlg.ShowAll ();
	}

	protected void OnAddExistingDownloadActivated (object sender, EventArgs e)
	{
		AddExistingDialog dlg = new AddExistingDialog ();
		dlg.ShowAll ();
		if (dlg.Run () == (int)ResponseType.Ok)
		{
			createNewDownload (dlg.remotePath, dlg.localPath, false, 
			                   dlg.typeCategory, dlg.start);
		}
		dlg.Destroy ();
	}

	protected void OnShowToolbarActivated (object sender, EventArgs e)
	{
		Console.WriteLine ("show toolbar");
		Gtk.ToggleAction chk_item = (Gtk.ToggleAction) sender;
		if (chk_item.Label == "New Download")
			toolbarNewDownload.Visible = chk_item.Active;

		else if (chk_item.Label == "Add Existing Download")
			toolbarAddDownload.Visible = chk_item.Active;

		else if (chk_item.Label == "Start")
			toolbarStart.Visible = chk_item.Active;

		else if (chk_item.Label == "Pause")
			toolbarPause.Visible = chk_item.Active;

		else if (chk_item.Label == "Cancel")
			toolbarCancel.Visible = chk_item.Active;

		else if (chk_item.Label == "Restart")
			toolbarRestart.Visible = chk_item.Active;

		else if (chk_item.Label == "Speed Limit")
		{
			toolbarSpeedLow.Visible = chk_item.Active;
			toolbarSpeedMedium.Visible = chk_item.Active;
			toolbarSpeedFull.Visible = chk_item.Active;
		}

		else if (chk_item.Label == "Find")
			toolbarFind.Visible = chk_item.Active;
	}

	protected void OnProgressWindowActivated (object sender, EventArgs e)
	{

	}
}
