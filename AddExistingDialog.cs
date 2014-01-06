using System;
using Gtk;

namespace DownloadManager
{
	public partial class AddExistingDialog : Gtk.Dialog
	{
		public string remotePath, localPath;
		public int parts;
		public int start;
		public string typeCategory;

		public AddExistingDialog ()
		{
			this.Build ();
			Title = "Add Existing Download";
			cbCategory.Active = 0;
			typeCategory = cbCategory.ActiveText;
			chkAuthenticate.Active = false;
			chkAuthenticateToggled (chkAuthenticate, new EventArgs ());
			remotePath = entryAddress.Text;
			localPath = entryFilePath.Text;
			SetSizeRequest (500,450);
			Resizable = false;

			foreach (string a in MainWindow.settingsManager.getKeyValueArray ("type-category"))
			{
				cbCategory.AppendText (a);
			}

			cbCategory.Active = 0;
		}

		protected void OnbuttonAddClicked (object sender, EventArgs e)
		{
			remotePath = entryAddress.Text;
			localPath = entryFilePath.Text;
			parts = spinbutton.ValueAsInt;
			Respond (Gtk.ResponseType.Ok);
		}

		protected void chkAuthenticateToggled (object sender, EventArgs e)
		{
			frameAuthenticate.Sensitive = chkAuthenticate.Active;
		}

		protected void OnRbNowToggled (object sender, EventArgs e)
		{
			start = 0; /*Start Now*/
		}

		protected void OnRbLaterToggled (object sender, EventArgs e)
		{
			start = 1; /*Start Later*/
		}

		protected void OnRbScheduleToggled (object sender, EventArgs e)
		{
			start = 2; /*Scheduled*/
		}

		protected void btnSelectFileClicked (object sender, EventArgs e)
		{
			FileChooserDialog dlg = new FileChooserDialog ("Select Folder", this, 
			                                               FileChooserAction.SelectFolder);
			dlg.AddButton (Stock.Cancel, ResponseType.Cancel);
			dlg.AddButton (Stock.Open, ResponseType.Accept);
			dlg.ShowAll ();

			if (dlg.Run () == (int)ResponseType.Accept)
			{
				entryFilePath.Text = dlg.Filename;
			}
			dlg.Destroy ();
		}
	}
}