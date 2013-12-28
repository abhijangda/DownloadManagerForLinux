using System;

namespace DownloadManager
{
	public partial class NewDialog : Gtk.Dialog
	{
		public string localPath;
		public string remotePath;
		public string typeCategory;
		public int start;

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
			remotePath = "";
			localPath = "";
		}

		/*TODO: Correct below code*/
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
	}
}

