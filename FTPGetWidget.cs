using System;
using Gtk;
using Gdk;
using libDownload;
using System.Collections.Generic;

namespace DownloadManager
{
	public class BreadCrumbWidget : HBox
	{
		List<ToggleButton> listToggleButton;

		public BreadCrumbWidget () : base (false, 2)
		{
			listToggleButton = new List<ToggleButton> ();
		}

		public void appendButton (ToggleButton btn)
		{
			PackStart (btn, false, false, 2);
			listToggleButton.Add (btn);
		}

		public void removeToEnd (int start)
		{
			for (int i = start; i < listToggleButton.Count; i++)
			{
				Remove (listToggleButton[i]);
			}
		}
	}

	[System.ComponentModel.ToolboxItem(true)]
	public class FTPGetWidget : VBox
	{
		BreadCrumbWidget breadCrumb;
		IconView iconView;
		ScrolledWindow scrolledWindow;
		HBox addressBar;
		Entry entryAddress;
		Button btnGo;

		public FTPGetWidget () : base (false, 2)
		{
			btnGo = new Button();
			btnGo.Label = "Go";
			btnGo.Clicked += btnGoClicked;
			addressBar = new HBox (false, 2);
			entryAddress = new Entry ();
			addressBar.PackStart (entryAddress, true, true, 2);
			addressBar.PackStart (btnGo, false, false, 2);

			breadCrumb = new BreadCrumbWidget ();
			iconView = new IconView ();
			scrolledWindow = new ScrolledWindow ();

			scrolledWindow.Add (iconView);
			PackStart (addressBar, false, false, 2);
			PackStart (breadCrumb, false, false, 2);
			PackStart (scrolledWindow, true, true, 2);
		}

		private void btnGoClicked (object sender, EventArgs args)
		{

		}
	}
}

