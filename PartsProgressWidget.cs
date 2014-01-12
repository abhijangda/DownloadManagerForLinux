using System;
using Gtk;
using System.Collections.Generic;

namespace DownloadManager
{
	[System.ComponentModel.ToolboxItem(true)]
	public class PartsProgressWidget : Gtk.ScrolledWindow
	{
		public Gtk.TreeView _treeView;
		public Gtk.ListStore _listStore;

		private int count;
		private List<TreeRowReference> listRowReference;

		public PartsProgressWidget () : base ()
		{
			count = 0;

			_treeView = new Gtk.TreeView ();
			_treeView.HeadersVisible = true;
			Add (_treeView);

			Gtk.TreeViewColumn partNoColumn = new Gtk.TreeViewColumn ();
			partNoColumn.Title = "Part";
			_treeView.AppendColumn (partNoColumn);
			partNoColumn.Resizable = true;

			Gtk.TreeViewColumn statusColumn = new Gtk.TreeViewColumn ();
			statusColumn.Title = "Status";
			_treeView.AppendColumn (statusColumn);
			statusColumn.Resizable = true;

			Gtk.TreeViewColumn progressColumn = new Gtk.TreeViewColumn ();
			progressColumn.Title = "Progress";
			_treeView.AppendColumn (progressColumn);
			progressColumn.Resizable = true;

			_listStore = new Gtk.ListStore (typeof (string), 
			                                typeof (string),
			                                typeof (float));
			_treeView.Model = _listStore;

			Gtk.CellRendererText stringColumnRender = new Gtk.CellRendererText ();
			partNoColumn.PackStart (stringColumnRender, true);
			partNoColumn.AddAttribute (stringColumnRender, "text", 0);
			stringColumnRender.Height = 20;

			stringColumnRender = new Gtk.CellRendererText ();
			statusColumn.PackStart (stringColumnRender, true);
			statusColumn.AddAttribute (stringColumnRender, "text", 1);
			stringColumnRender.Height = 20;

			Gtk.CellRendererProgress progressColumnRender = new Gtk.CellRendererProgress ();
			progressColumn.PackStart (progressColumnRender, true);
			progressColumn.AddAttribute (progressColumnRender, "value", 2);
			progressColumnRender.Height = 20;

			_treeView.Selection.Changed += _treeViewSelectionChanged;

			listRowReference = new List<TreeRowReference> ();
		}

		public void _treeViewSelectionChanged (object o, EventArgs args)
		{
			_treeView.Selection.Changed -= _treeViewSelectionChanged;
			_treeView.Selection.UnselectAll ();
			_treeView.Selection.Changed += _treeViewSelectionChanged;

		}

		public void appendPart ()
		{
			count ++;
			Gtk.TreeIter iter = _listStore.Append ();
			_listStore.SetValue (iter, 0, count.ToString ());
			listRowReference.Add (new TreeRowReference (_listStore, 
			                                            _listStore.GetPath (iter)));
		}

		public void setPartStatus (int part, string status)
		{
			TreeIter iter;
			_listStore.GetIter (out iter, listRowReference [part].Path);
			_listStore.SetValue (iter, 1, status);
		}

		public void setPartProgress (int part, float progress)
		{
			TreeIter iter;
			_listStore.GetIter (out iter, listRowReference [part].Path);
			_listStore.SetValue (iter, 2, progress);
		}
	}
}

