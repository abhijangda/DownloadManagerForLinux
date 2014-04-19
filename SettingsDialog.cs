using System;

namespace DownloadManager
{
	public partial class SettingsDialog : Gtk.Dialog
	{
		public SettingsDialog ()
		{
			this.Build ();
			SetSizeRequest (600, 400);
		}

		protected void OnChkHTTPProxyToggled (object sender, EventArgs e)
		{
			if (((Gtk.CheckButton)sender).Active)
			{
				httpFrame.Sensitive = true;
			}

			else
			{
				httpFrame.Sensitive = false;
			}
		}
	}
}

