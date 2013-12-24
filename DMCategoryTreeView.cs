using System;
using System.Collections.Generic;
using System.Reflection;
using libDownload;
using Gdk;

namespace DownloadManager
{
	public class DMTypeCategory
	{
		public string name;

		public DMTypeCategory (string _name)
		{
			name = _name;
		}
	}

	public class DMStatusCategory
	{
		public string name;

		public DMStatusCategory (string _name)
		{
			name = _name;
		}
	}

	[System.ComponentModel.ToolboxItem(true)]
	public partial class DMCategoryTreeView : Gtk.TreeView
	{
		private List<DMTypeCategory> listTypeCategories;
		private List<DMStatusCategory> listStatusCategories;
		private Gtk.TreeStore treeStore;

		public DMCategoryTreeView ()
		{
			listTypeCategories = new List<DMTypeCategory> ();
			listStatusCategories = new List<DMStatusCategory> ();

			MemberInfo[] memberinfos = typeof (DOWNLOAD_STATUS).GetMembers (BindingFlags.Public |
			                                                                BindingFlags.Static);
			foreach (MemberInfo info in memberinfos)
			{
				listStatusCategories.Add (new DMStatusCategory (info.Name));
			}

			listTypeCategories.Add (new DMTypeCategory ("All"));
			listTypeCategories.Add (new DMTypeCategory ("Media"));
			listTypeCategories.Add (new DMTypeCategory ("Compressed"));
			listTypeCategories.Add (new DMTypeCategory ("Executable"));
			listTypeCategories.Add (new DMTypeCategory ("Documents"));

			Gtk.TreeViewColumn Column = new Gtk.TreeViewColumn ();
			Column.Title = "Categories";

			AppendColumn (Column);

			treeStore = new Gtk.TreeStore (typeof(Gdk.Pixbuf), typeof (string));
			Model = treeStore;

			HeadersVisible = false;

			Gtk.CellRendererPixbuf iconColumnRender = new Gtk.CellRendererPixbuf ();
			Column.PackStart (iconColumnRender, true);

			Gtk.CellRendererText stringColumnRender = new Gtk.CellRendererText ();
			Column.PackStart (stringColumnRender, true);

			Column.AddAttribute (iconColumnRender, "pixbuf", 0);
			Column.AddAttribute (stringColumnRender, "text", 1);

			Gtk.TreeIter iter = treeStore.AppendValues (new Gdk.Pixbuf ("./../../field.png"), "Status");
			foreach (DMStatusCategory satCat in listStatusCategories)
			{
				treeStore.AppendValues (iter, new Gdk.Pixbuf ("./../../field.png"), satCat.name);
			}

			iter = treeStore.AppendValues (new Gdk.Pixbuf ("./../../field.png"), "Type");
			foreach (DMTypeCategory typeCat in listTypeCategories)
			{
				treeStore.AppendValues (iter, new Gdk.Pixbuf ("./../../field.png"), typeCat.name);
			}
		}

//		protected override bool OnKeyPressEvent (EventKey evnt)
//		{
//			if (evnt.KeyValue == 65507 || evnt.KeyValue == 65508) /*For Ctrl*/
//			{
//				ctrlPressed = true;
//			}
//
//			return base.OnKeyPressEvent (evnt);
//		}
//
//		protected override bool OnKeyReleaseEvent (EventKey evnt)
//		{
//			if (evnt.KeyValue == 65507 || evnt.KeyValue == 65508) /*For Ctrl*/
//			{
//				ctrlPressed = false;
//			}
//
//			return base.OnKeyReleaseEvent (evnt);
//		}
//
//		protected override bool OnButtonReleaseEvent (EventButton evnt)
//		{
//			if (ctrlPressed == false)
//			    return base.OnButtonReleaseEvent (evnt);
//
//			if (evnt.Button == 1)
//			{
//
//			}
//		}
	}
}