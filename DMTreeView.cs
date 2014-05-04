using System;
using Gtk;
using libDownload;
using System.Collections.Generic;

namespace DownloadManager
{
	public abstract class DMTreeView : TreeView
	{
		public Gtk.TreeModel treeModel {get; protected set;}
		protected Gtk.TreeSortable sortable;
		protected Gtk.Menu contextMenu;
		protected Gtk.MenuItem contextStart, contextPause, contextRestart;
		protected Gtk.MenuItem contextCancel, contextStartAll, contextPauseAll;
		protected bool ctrlPressed;

		public abstract void setImageForStatus (object o);

		protected int SpeedSortFunc (TreeModel treeModel, TreeIter iter1,
		                             TreeIter iter2)
		{
			string str1 = (string)treeModel.GetValue (iter1, 5);
			string str2 = (string)treeModel.GetValue (iter2, 5);

			if (str1 == "" && str2 != "")
				return -1;
			else if (str1 == "" && str2 == "")
				return 0;
			else if (str1 != "" && str2 == "")
				return 1;

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

		protected int timeSortFunc (TreeModel treeModel, TreeIter iter1,
		                          TreeIter iter2)
		{
			string str1 = (string)treeModel.GetValue (iter1, 4);
			string str2 = (string)treeModel.GetValue (iter2, 4);
			short countInstr1 = MainClass.countInString (str1, ":");
			short countInstr2 = MainClass.countInString (str2, ":");

			if (countInstr1 < countInstr2)
				return 1;
			else if (countInstr1 > countInstr2)
				return -1;

			return str1.CompareTo (str2);
		}

		protected int filenameSortFunc (TreeModel treeModel, TreeIter iter1,
		                              TreeIter iter2)
		{
			string str1 = (string)treeModel.GetValue (iter1, 1);
			string str2 = (string)treeModel.GetValue (iter2, 1);

			return str1.CompareTo (str2);
		}

		protected void createTreeView ()
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

			Model = treeModel;

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

			sortable = (TreeSortable)treeModel;

			sortable.SetSortFunc (1, filenameSortFunc);
			sortable.SetSortColumnId (1, SortType.Descending);

			sortable.SetSortFunc (2, sizeSortFunc);
			sortable.SetSortColumnId (2, SortType.Descending);

			sortable.SetSortFunc (4, timeSortFunc);
			sortable.SetSortColumnId (4, SortType.Descending);

			sortable.SetSortFunc (5, SpeedSortFunc);
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

		protected abstract int sizeSortFunc (TreeModel treeModel,
		                                     TreeIter iter1,
		                                     TreeIter iter2);
		public abstract void addRow (object o);
		protected abstract void setIterValues (object dwnld, TreeIter iter, 
		                                       Length downloaded = default (Length),
		                                       Speed speed = default (Speed));
		public abstract void searchInColumns (string text, List<int> columns);
		public abstract void updateDownloadStatus (object o, Length downloaded, 
		                                           Speed speed);
	}
}

