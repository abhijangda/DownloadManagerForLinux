using System;
using System.IO;
using Gtk;

namespace DownloadManager
{
	public partial class NewDialog : Gtk.Dialog
	{
		public string localPath;
		public string remotePath;
		public string typeCategory;
		public int start;
		public bool genFilename;

		public NewDialog ()
		{
			this.Build ();
			entrySaveTo.Text = MainWindow.settingsManager.getKeyValue ("default-dir");
			foreach (string a in MainWindow.settingsManager.getKeyValueArray ("type-category"))
			{
				cbCategory.AppendText (a);
			}
			cbCategory.Active = 0;
			typeCategory = cbCategory.ActiveText;
			chkAuthenticate.Active = false;
			chkAuthenticateToggled (chkAuthenticate, new EventArgs ());
			remotePath = entryAddress.Text;
			localPath = entrySaveTo.Text;
			chkGenFilename.Active = true;
			SetSizeRequest (500,450);
			Resizable = false;
			Default = buttonOk;
		}

		protected void chkAuthenticateToggled (object sender, EventArgs e)
		{
			frameAuthenticate.Sensitive = chkAuthenticate.Active;
		}

		protected void OnEntryAddressChanged (object sender, EventArgs e)
		{
			remotePath = entryAddress.Text;
		}

		protected void OnEntrySaveToChanged (object sender, EventArgs e)
		{
			localPath = entrySaveTo.Text;
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

		protected void OncbCategoryChanged (object sender, EventArgs e)
		{
			typeCategory = cbCategory.ActiveText;
		}

		protected void chkGenFilenameToggled (object sender, EventArgs e)
		{
			label6.Sensitive = !chkGenFilename.Active;
			entryFilename.Sensitive = !chkGenFilename.Active;
		}

		protected void OnAddDownloadClicked (object sender, EventArgs e)
		{
			genFilename = chkGenFilename.Active;
			if (chkGenFilename.Active)
			   localPath = System.IO.Path.Combine (entrySaveTo.Text, entryFilename.Text);

			Respond (Gtk.ResponseType.Ok);
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
				entrySaveTo.Text = dlg.Filename;
			}
			dlg.Destroy ();
		}
	}
}

