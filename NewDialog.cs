using System;

namespace DownloadManager
{
	public partial class NewDialog : Gtk.Dialog
	{
		public string localPath;
		public string remotePath;
		public int start;

		public NewDialog ()
		{
			this.Build ();
		}

		/*TODO: Correct below code*/
		protected void chkAuthenticateToggled (object sender, EventArgs e)
		{
			if (chkAuthenticate.Active == false)
				frameAuthenticate.State = Gtk.StateType.Insensitive;

			else
				frameAuthenticate.State = Gtk.StateType.Active;
		}

		protected void OnEntryAddressChanged (object sender, EventArgs e)
		{
			remotePath = entryAddress.Text;
		}

		protected void OnbtnSaveToSelectionChanged (object sender, EventArgs e)
		{
			localPath = btnSaveTo.Filename;
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
	}
}

