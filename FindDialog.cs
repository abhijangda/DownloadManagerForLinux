using System;
using Gtk;
using System.Collections.Generic;

namespace DownloadManager
{
	public partial class FindDialog : Gtk.Dialog
	{
		public delegate void OnFindDialogSearchPressed (string text,
		                                                List<int> columns);
		public OnFindDialogSearchPressed onFindDialogSearchPressed;
		List<CheckButton> listCheckButtons;

		public FindDialog (List<string> columns, OnFindDialogSearchPressed _handler)
		{
			this.Build ();
			onFindDialogSearchPressed = _handler;
			table.Resize ((uint)Math.Ceiling (columns.Count/2.0), 2);
			listCheckButtons = new List<CheckButton> ();
			foreach (string column in columns)
			{
				CheckButton chkbtn = new CheckButton (column);
				table.Attach (chkbtn, (uint)listCheckButtons.Count/2, (uint)listCheckButtons.Count/2 + 1, 
				              (uint)listCheckButtons.Count%2, (uint)listCheckButtons.Count%2 + 1);
				listCheckButtons.Add (chkbtn);
			}
		}

		protected void OnSearchClicked (object sender, EventArgs e)
		{
			List<int> columns = new List<int> ();
			for (int i = 0; i < listCheckButtons.Count; i++)
				if (listCheckButtons [i].Active == true)
					columns.Add (i);
	
			onFindDialogSearchPressed (lblSearchFor.Text, columns);
		}

		protected void OnCancelClicked (object sender, EventArgs e)
		{
			Destroy ();
		}
	}
}

