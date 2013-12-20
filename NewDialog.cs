using System;

namespace DownloadManager
{
	public partial class NewDialog : Gtk.Dialog
	{
		public NewDialog ()
		{
			this.Build ();
		}

		protected void chkAuthenticateToggled (object sender, EventArgs e)
		{
			frameAuthenticate.State = chkAuthenticate.State;
		}
	}
}

