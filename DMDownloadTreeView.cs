using System;
using Gtk;

namespace DownloadManager
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DMDownloadTreeView : Gtk.TreeView
	{
		Gtk.ListStore listStore;
		public DMDownloadTreeView ()
		{
			Gtk.TreeViewColumn filenamecolumn = new Gtk.TreeViewColumn ();
			filenamecolumn.Title = "File Name";
			filenamecolumn.Resizable = true;
			filenamecolumn.Expand = true;

			Gtk.TreeViewColumn sizeColumn = new Gtk.TreeViewColumn ();
			sizeColumn.Title = "Size";
			sizeColumn.Resizable = true;
			sizeColumn.Expand = true;

			Gtk.TreeViewColumn downloadedColumn = new Gtk.TreeViewColumn ();
			downloadedColumn.Title = "Downloaded";
			downloadedColumn.Resizable = true;
			downloadedColumn.Expand = true;

			Gtk.TreeViewColumn timeColumn = new Gtk.TreeViewColumn ();
			timeColumn.Title = "Time Remaining";
			timeColumn.Resizable = true;
			timeColumn.Expand = true;

			Gtk.TreeViewColumn speedColumn = new Gtk.TreeViewColumn ();
			speedColumn.Title = "Speed";
			speedColumn.Resizable = true;
			speedColumn.Expand = true;

			Gtk.TreeViewColumn sectionsColumn = new Gtk.TreeViewColumn ();
			sectionsColumn.Title = "Sections";
			sectionsColumn.Resizable = true;
			sectionsColumn.Expand = true;

			AppendColumn (filenamecolumn);
			AppendColumn (sizeColumn);
			AppendColumn (downloadedColumn);
			AppendColumn (timeColumn);
			AppendColumn (speedColumn);
			AppendColumn (sectionsColumn);

			listStore = new Gtk.ListStore (typeof(Gdk.Pixbuf), typeof (string), 
			                               typeof (string), typeof (float), 
			                               typeof (string), typeof (string), 
			                               typeof (string), typeof (DMDownload));
			Model = listStore;

			Gtk.CellRendererPixbuf iconColumnRender = new Gtk.CellRendererPixbuf ();
			filenamecolumn.PackStart (iconColumnRender, true);
			Gtk.CellRendererText stringColumnRender = new Gtk.CellRendererText ();
			filenamecolumn.PackStart (stringColumnRender, true);

			filenamecolumn.AddAttribute (iconColumnRender, "pixbuf", 0);
			filenamecolumn.AddAttribute (stringColumnRender, "text", 1);

			stringColumnRender = new Gtk.CellRendererText ();
			sizeColumn.PackStart (stringColumnRender, true);
			sizeColumn.AddAttribute (stringColumnRender, "text", 2);

			Gtk.CellRendererProgress progressColumnRender = new Gtk.CellRendererProgress ();
			downloadedColumn.PackStart (progressColumnRender, true);
			downloadedColumn.AddAttribute (progressColumnRender, "value", 3);

			stringColumnRender = new Gtk.CellRendererText ();
			timeColumn.PackStart (stringColumnRender, true);
			timeColumn.AddAttribute (stringColumnRender, "text", 4);

			stringColumnRender = new Gtk.CellRendererText ();
			speedColumn.PackStart (stringColumnRender, true);
			speedColumn.AddAttribute (stringColumnRender, "text", 5);

			stringColumnRender = new Gtk.CellRendererText ();
			sectionsColumn.PackStart (stringColumnRender, true);
			sectionsColumn.AddAttribute (stringColumnRender, "text", 6);
		}

		public void addDownloadRow (DMDownload dwnld)
		{
			listStore.AppendValues (new Gdk.Pixbuf ("./../../field.png"), 
			                               dwnld.download.localPath.Substring( 
			                                   dwnld.download.localPath.LastIndexOf ('/')+1),
			                               "", "", "", "", "", dwnld);
		}

		public void updateDownloadStatus (DMDownload dwnld, long downloaded, long speed)
		{
			Gtk.TreeIter iter;
			listStore.GetIterFirst (out iter);

			do
			{
				DMDownload dmld = (DMDownload) listStore.GetValue (iter, 7);
				if (dmld.download.localPath == dwnld.download.localPath)
				{
					if (dmld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADING)
					{
						listStore.SetValue (iter, 2, dwnld.download.length.ToString ());
						listStore.SetValue (iter, 3, (100*downloaded)/(float)dwnld.download.length);
						listStore.SetValue (iter, 4, MainWindow.getTime (dwnld.download.length - downloaded, speed));
						listStore.SetValue (iter, 5, speed.ToString ());
						listStore.SetValue (iter, 6, dwnld.download.parts.ToString ());
					}
					else if (dmld.download.status == libDownload.DOWNLOAD_STATUS.MERGING)
					{
						listStore.SetValue (iter, 3, (float)100.0);
						listStore.SetValue (iter, 4, "");
						listStore.SetValue (iter, 5, "");
						listStore.SetValue (iter, 6, dwnld.download.parts.ToString ());
					}
					else if (dmld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADED)
					{
						listStore.SetValue (iter, 3, (float)100.0);
						listStore.SetValue (iter, 4, "");
						listStore.SetValue (iter, 5, "");
						listStore.SetValue (iter, 6, dwnld.download.parts.ToString ());
					}
				}
			}
			while (listStore.IterNext (ref iter));
		}

		public void showCategory (params string[] cats)
		{

		}
	}
}