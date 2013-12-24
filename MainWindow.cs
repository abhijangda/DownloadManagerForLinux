using System;
using Gtk;
using System.Collections.Generic;
using libDownload;
using DownloadManager;

public partial class MainWindow: Gtk.Window
{	
	List<DMDownload> listDownloads;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		listDownloads = new List<DMDownload> ();
		GLib.Timeout.Add (1000, this.updateFunc);
	}

	bool updateFunc ()
	{
		foreach (DMDownload dwnld in listDownloads)
		{
			long downloaded = dwnld.download.getDownloaded ();
			long speed = dwnld.download.getSpeed ();
			dwnld.window.updateProgress (downloaded, speed);

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
			Download dl = new HTTPDownload (new_dlg.remotePath, new_dlg.localPath);
			DMDownload dmdl = new DMDownload (dl, null);
			listDownloads.Add (dmdl);
			if (new_dlg.start == 0)
			{
				/*Start Download Now*/
				ProgressWindow progress_dlg = new ProgressWindow (dl);
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

	}

	protected void dmCategoryTreeViewRowActivated (object o, RowActivatedArgs args)
	{
		Gtk.TreeIter iter;
		Gtk.TreeModel model;

		dmCategoryTreeView.Selection.GetSelected (out model, out iter);
		dmDownloadTreeView.showCategory ((string) model.GetValue (iter, 1));
	}
}
