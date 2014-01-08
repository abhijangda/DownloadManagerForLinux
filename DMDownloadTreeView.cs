using System;
using Gtk;
using Gdk;
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
		Gtk.Menu contextMenu;
		Gtk.MenuItem contextStart, contextPause, contextRestart;
		Gtk.MenuItem contextCancel, contextStartAll, contextPauseAll;
		bool ctrlPressed;

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
			filenamecolumn.PackStart (iconColumnRender, false);
			Gtk.CellRendererText stringColumnRender = new Gtk.CellRendererText ();
			filenamecolumn.PackStart (stringColumnRender, false);

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

			contextMenu = new Gtk.Menu ();

			contextStart = new Gtk.MenuItem ("Start");
			contextMenu.Append (contextStart);

			contextPause = new Gtk.MenuItem ("Pause");
			contextMenu.Append (contextPause);

			contextRestart = new Gtk.MenuItem ("Restart");
			contextMenu.Append (contextRestart);

			contextCancel = new Gtk.MenuItem ("Cancel");
			contextMenu.Append (contextCancel);

			contextMenu.Append (new SeparatorMenuItem ());

			contextStartAll = new Gtk.MenuItem ("Start All");
			contextMenu.Append (contextStartAll);

			contextPauseAll = new Gtk.MenuItem ("Pause All");
			contextMenu.Append (contextPauseAll);

			contextMenu.AttachToWidget (this, null);

			ctrlPressed = false;
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
			Gtk.TreeIter iter;
			iter = listStore.AppendValues (new Gdk.Pixbuf ("./../../field.png"), 
			                               dwnld.download.localPath.Substring( 
			                                   dwnld.download.localPath.LastIndexOf ('/')+1),
			                               dwnld.download.length.ToString (),
			                               (100*dwnld.download.getDownloaded ().value)/(float)dwnld.download.length.value,
			                               "", "", dwnld.download.parts, dwnld);

			dwnld.rowReference = new Gtk.TreeRowReference (listStore, 
			                                               listStore.GetPath (iter));
		}

		public void updateDownloadStatus (DMDownload dwnld, Length downloaded, Speed speed)
		{
			Gtk.TreeIter iter;
			if (dwnld.rowReference.Valid ())
			{
				listStore.GetIter (out iter, dwnld.rowReference.Path);
			    setIterValues (dwnld, iter, downloaded, speed);
			}
			//listStore.GetIterFirst (out iter);
			/*do
			{
				DMDownload dmld = (DMDownload) listStore.GetValue (iter, 7);
				if (dmld.download.localPath == dwnld.download.localPath)
				{
					setIterValues (dmld, iter, downloaded, speed);
				}
			}
			while (listStore.IterNext (ref iter));*/
		}

		private void setIterValues (DMDownload dwnld, TreeIter iter, 
		                            Length downloaded = default (Length),
		                            Speed speed = default (Speed))
		{
			if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADING)
			{
				listStore.SetValue (iter, 1, dwnld.download.localPath.Substring (dwnld.download.localPath.LastIndexOf ('/') +1));
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
						dmld.rowReference = new Gtk.TreeRowReference (listStore, 
						                                              listStore.GetPath (iter));
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

		protected override bool OnButtonReleaseEvent (Gdk.EventButton evnt)
		{
			if (evnt.Button != 3)
				return base.OnButtonReleaseEvent (evnt);

			TreePath[] selectedPaths = Selection.GetSelectedRows ();
			if (selectedPaths.Length == 0)
			{
				contextStart.Sensitive = false;
				contextPause.Sensitive = false;
				contextRestart.Sensitive = false;
				contextCancel.Sensitive = false;
			}

			else
			{
				if (selectedPaths.Length == 1)
				{
					TreeIter iter;
					listStore.GetIter (out iter, selectedPaths [0]);
					DMDownload dmld = (DMDownload) listStore.GetValue (iter, 7);
					if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADED)
					{
						contextStart.Sensitive = false;
						contextPause.Sensitive = false;
						contextRestart.Sensitive = true;
						contextCancel.Sensitive = false;
					}
					else if (dmld.download.status == DOWNLOAD_STATUS.NOT_STARTED ||
					    dmld.download.status == DOWNLOAD_STATUS.ERROR)
					{
						contextStart.Sensitive = true;
						contextPause.Sensitive = false;
						contextRestart.Sensitive = false;
						contextCancel.Sensitive = false;
					}
					else if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADING)
					{
						contextStart.Sensitive = false;
						contextPause.Sensitive = true;
						contextRestart.Sensitive = true;
						contextCancel.Sensitive = true;
					}
					else if (dmld.download.status == DOWNLOAD_STATUS.PAUSED)
					{
						contextStart.Sensitive = true;
						contextPause.Sensitive = false;
						contextRestart.Sensitive = true;
						contextCancel.Sensitive = false;
					}
				}
			}


			contextMenu.Popup ();
			contextMenu.ShowAll ();

			return base.OnButtonReleaseEvent (evnt);
		}

		protected override bool OnKeyPressEvent (EventKey evnt)
		{
			if (evnt.KeyValue == 65507 || evnt.KeyValue == 65508) /*For Ctrl*/
			{
				ctrlPressed = true;
			}

			return base.OnKeyPressEvent (evnt);
		}

		protected override bool OnKeyReleaseEvent (EventKey evnt)
		{
			if (evnt.KeyValue == 65507 || evnt.KeyValue == 65508) /*For Ctrl*/
			{
				ctrlPressed = false;
			}

			return base.OnKeyReleaseEvent (evnt);
		}

		protected override bool OnButtonPressEvent (EventButton evnt)
		{
			if (ctrlPressed == false)
			{
				this.Selection.Mode = SelectionMode.Single;
			    return base.OnButtonPressEvent (evnt);
			}

			if (evnt.Button == 1)
			{
				this.Selection.Mode = SelectionMode.Multiple;
			}

			return base.OnButtonPressEvent (evnt);
		}

		public void loadDownloads ()
		{
			foreach (DMDownload dmld in listAllDownloads)
				addDownloadRow (dmld);
		}
	}
}