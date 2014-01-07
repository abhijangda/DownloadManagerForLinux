using System;
using Gtk;
using libDownload;
using System.Collections.Generic;

namespace DownloadManager
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DMDownloadTreeView : Gtk.TreeView
	{
		public Gtk.ListStore listStore {get; private set;}
		public List<DMDownload> listAllDownloads {private get; set;}
		Gtk.TreeSortable sortable;

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

			HeadersClickable = true;

			sortable = (TreeSortable)listStore;

			sortable.SetSortFunc (1, filenameSortFunc);
			sortable.SetSortColumnId (1, SortType.Descending);

			sortable.SetSortFunc (2, sizeSortFunc);
			sortable.SetSortColumnId (2, SortType.Descending);

			sortable.SetSortFunc (3, downloadedOrSpeedSortFunc);
			sortable.SetSortColumnId (3, SortType.Descending);

			sortable.SetSortFunc (4, timeSortFunc);
			sortable.SetSortColumnId (4, SortType.Descending);

			sortable.SetSortFunc (5, downloadedOrSpeedSortFunc);
			sortable.SetSortColumnId (5, SortType.Descending);
		}

		private int downloadedOrSpeedSortFunc (TreeModel treeModel, TreeIter iter1,
		                              TreeIter iter2)
		{
			string str1 = (string)listStore.GetValue (iter1, 1);
			string str2 = (string)listStore.GetValue (iter2, 1);

			if (str1.Substring (str1.IndexOf (" ")) == str2.Substring (str2.IndexOf (" ")))
				return str1.CompareTo (str2);

			if (str1.Contains ("Bytes"))
				return -1;

			if (str2.Contains ("Bytes"))
				return 1;

			if (str1.Contains ("GB"))
				return 1;

			if (str2.Contains ("GB"))
				return -1;

			if (str2.Contains ("MB") && !str1.Contains ("MB"))
				return -1;

			if (str1.Contains ("MB") && !str2.Contains ("MB"))
				return 1;

			if (str1.Contains ("KB") && !str2.Contains ("KB"))
				return 1;

			if (str2.Contains ("KB") && !str1.Contains ("KB"))
				return -1;

			return 0;
		}

		private int timeSortFunc (TreeModel treeModel, TreeIter iter1,
		                              TreeIter iter2)
		{
			string str1 = (string)listStore.GetValue (iter1, 4);
			string str2 = (string)listStore.GetValue (iter2, 4);
			short countInstr1 = MainClass.countInString (str1, ":");
			short countInstr2 = MainClass.countInString (str2, ":");

			if (countInstr1 < countInstr2)
				return 1;
			else if (countInstr1 > countInstr2)
				return -1;

			return str1.CompareTo (str2);
		}

		private int sizeSortFunc (TreeModel treeModel, TreeIter iter1,
		                              TreeIter iter2)
		{
		    DMDownload dmld1 = (DMDownload)listStore.GetValue (iter1, 7);
			DMDownload dmld2 = (DMDownload)listStore.GetValue (iter2, 1);

			if (dmld1.download.length.value == dmld2.download.length.value)
				return 0;

			if (dmld1.download.length.value > dmld2.download.length.value)
				return 1;

			return -1;
		}

		private int filenameSortFunc (TreeModel treeModel, TreeIter iter1,
		                              TreeIter iter2)
		{
			string str1 = (string)listStore.GetValue (iter1, 1);
			string str2 = (string)listStore.GetValue (iter2, 1);

			return str1.CompareTo (str2);
		}

		public void addDownloadRow (DMDownload dwnld)
		{
			listStore.AppendValues (new Gdk.Pixbuf ("./../../field.png"), 
			                               dwnld.download.localPath.Substring( 
			                                   dwnld.download.localPath.LastIndexOf ('/')+1),
			                               "", "", "", "", "", dwnld);
		}

		public void updateDownloadStatus (DMDownload dwnld, Length downloaded, Speed speed)
		{
			Gtk.TreeIter iter;
			listStore.GetIterFirst (out iter);

			do
			{
				DMDownload dmld = (DMDownload) listStore.GetValue (iter, 7);
				if (dmld.download.localPath == dwnld.download.localPath)
				{
					setIterValues (dmld, iter, downloaded, speed);
				}
			}
			while (listStore.IterNext (ref iter));
		}

		private void setIterValues (DMDownload dwnld, TreeIter iter, 
		                            Length downloaded = default (Length),
		                            Speed speed = default (Speed))
		{
			if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADING)
			{
				listStore.SetValue (iter, 2, dwnld.download.length.ToString ());
				listStore.SetValue (iter, 3, (100*downloaded.value)/(float)dwnld.download.length.value);
				listStore.SetValue (iter, 4, MainWindow.getTime (dwnld.download.length.value - downloaded.value, speed.value));
				listStore.SetValue (iter, 5, speed.ToString ());
				listStore.SetValue (iter, 6, dwnld.download.parts.ToString ());
			}
			else if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.MERGING)
			{
				listStore.SetValue (iter, 3, (float)100.0);
				listStore.SetValue (iter, 4, "");
				listStore.SetValue (iter, 5, "");
				listStore.SetValue (iter, 6, dwnld.download.parts.ToString ());
			}
			else if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADED)
			{
				listStore.SetValue (iter, 3, (float)100.0);
				listStore.SetValue (iter, 4, "");
				listStore.SetValue (iter, 5, "");
				listStore.SetValue (iter, 6, dwnld.download.parts.ToString ());
			}
		}

		public void showCategory (params string[] cats)
		{
			listStore.Clear ();
			foreach (DMDownload dmld in listAllDownloads)
			{
				foreach (string cat in cats)
				{
					TreeIter iter;
					if (dmld.typeCategory.name == cat ||
					    Enum.GetName (typeof (DOWNLOAD_STATUS) ,dmld.download.status) == cat)
					{
						iter = listStore.Append ();
						setIterValues (dmld, iter);
					}
				}
			}
		}

		public void searchInColumns (string text, List<int> columns)
		{
			TreeIter iter;
			if (this.Selection.GetSelected (out iter))
			    listStore.IterNext (ref iter);
			else
				listStore.GetIterFirst (out iter);

			while (listStore.IterIsValid (iter))
			{
				string value = "";
				foreach (int column in columns)
				{
					value = (string) listStore.GetValue (iter, column+1);
					if (value == text)
					{
						this.Selection.SelectIter (iter);
						return;
					}
				}

				listStore.IterNext (ref iter);
			}
		}
	}
}