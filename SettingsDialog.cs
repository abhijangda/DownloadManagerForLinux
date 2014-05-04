using System;
using System.Collections.Generic;
using Gtk;

namespace DownloadManager
{
	public partial class SettingsDialog : Gtk.Dialog
	{
		Settings settingsManager;
		public SettingsDialog (Settings _settingsManager)
		{
			this.Build ();
			SetSizeRequest (600, 400);
			notebook1.CurrentPage = 0;
			settingsManager = _settingsManager;
		}

		public void setSettings ()
		{
			try
			{
				/* General Tab */
				string section = "general";
				chk_load_on_startup.Active = bool.Parse (settingsManager.getKeyValue (section, "load_on_startup"));
				chk_start_minimized.Active = bool.Parse (settingsManager.getKeyValue (section, "start_minimized"));
				chk_minimize_to_tray.Active = bool.Parse (settingsManager.getKeyValue (section, "minimize_to_tray"));
				chk_minimize_on_close.Active = bool.Parse (settingsManager.getKeyValue (section, "minimize_on_close"));
				sb_launch_download_time.Value = int.Parse (settingsManager.getKeyValue (section, "launch_download_time"));
				sb_exit_time.Value = int.Parse (settingsManager.getKeyValue (section, "exit_time"));
				sb_shutdown_time.Value = int.Parse (settingsManager.getKeyValue (section, "shutdown_time"));
				sb_auto_save_timeout.Value = int.Parse (settingsManager.getKeyValue (section, "auto_save_timeout"));

				/* Group Tab */
				section = "group";
				foreach (String l in (List<string>)settingsManager.getKeyValueArray (section, "groups"))
					cb_groups.AppendText (l);

				if (settingsManager.getKeyValue (section, "new_download_group") == "group_auto")
					rb_group_auto.Active = true;
				else
					cb_groups.Active = ((List<string>)settingsManager.getKeyValueArray (section, "groups")).IndexOf (settingsManager.getKeyValue (section, "new_download_group"));

				/* Proxy Tab */
				section = "proxy";
				/* HTTP Proxy */
				chkHTTPProxy.Active = bool.Parse (settingsManager.getKeyValue (section, "http_proxy"));
				chkAllHTTP.Active = bool.Parse (settingsManager.getKeyValue (section, "all_http_proxy"));
				entryHTTPAddress.Text = settingsManager.getKeyValue (section, "http_proxy_address");
				sbHTTPPort.Value = int.Parse (settingsManager.getKeyValue (section, "http_proxy_port"));
				entryHTTPUserName.Text = settingsManager.getKeyValue (section, "http_proxy_user_name");
				entryHTTPPassword.Text = settingsManager.getKeyValue (section, "http_proxy_password");

				/* FTP Proxy */
				chkFTPProxy.Active = bool.Parse (settingsManager.getKeyValue (section, "ftp_proxy"));
				chkAllFTP.Active = bool.Parse (settingsManager.getKeyValue (section, "all_ftp_proxy"));
				entryFTPAddress.Text = settingsManager.getKeyValue (section, "ftp_proxy_address");
				sbFTPPort.Value = int.Parse (settingsManager.getKeyValue (section, "ftp_proxy_port"));
				entryFTPUserName.Text = settingsManager.getKeyValue (section, "ftp_proxy_user_name");
				entryFTPPassword.Text = settingsManager.getKeyValue (section, "ftp_proxy_password");

				/* SOCKS Proxy */
				chkSOCKSProxy.Active = bool.Parse (settingsManager.getKeyValue (section, "socks_proxy"));
				chkAllSOCKS.Active = bool.Parse (settingsManager.getKeyValue (section, "all_socks_proxy"));
				entrySOCKSAddress.Text = settingsManager.getKeyValue (section, "socks_proxy_address");
				sbSOCKSPort.Value = int.Parse (settingsManager.getKeyValue (section, "socks_proxy_port"));
				entrySOCKSUserName.Text = settingsManager.getKeyValue (section, "socks_proxy_user_name");
				entrySOCKSPassword.Text = settingsManager.getKeyValue (section, "socks_proxy_password");

				OnChkHTTPProxyToggled (chkHTTPProxy, null);
				OnChkSOCKSProxyToggled (chkSOCKSProxy, null);
				OnChkFTPProxyToggled (chkFTPProxy, null);
			}

			catch (Exception e)
			{
			}

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

		protected void chkAllSOCKSToggled (object sender, EventArgs e)
		{
			chkHTTPProxy.Active = chkFTPProxy.Active = true;
			entryFTPAddress.Text = entryHTTPAddress.Text = entrySOCKSAddress.Text;
			entryFTPPassword.Text = entryHTTPPassword.Text = entrySOCKSPassword.Text;
			sbFTPPort.Value = sbHTTPPort.Value = sbSOCKSPort.Value;
			entryFTPUserName.Text = entryHTTPUserName.Text = entrySOCKSUserName.Text;
		}

		protected void chkAllFTPToggled (object sender, EventArgs e)
		{
			chkHTTPProxy.Active = chkSOCKSProxy.Active = true;
			entryHTTPAddress.Text = entrySOCKSAddress.Text = entryFTPAddress.Text;
			entryHTTPPassword.Text = entrySOCKSPassword.Text = entryFTPPassword.Text;
			sbHTTPPort.Value = sbSOCKSPort.Value = sbFTPPort.Value;
			entryHTTPUserName.Text = entrySOCKSUserName.Text = entryFTPUserName.Text;
		}

		protected void chkAllHTTPToggled (object sender, EventArgs e)
		{
			chkFTPProxy.Active = chkSOCKSProxy.Active = true;
			entryHTTPAddress.Text = entrySOCKSAddress.Text = entryFTPAddress.Text;
			entryHTTPPassword.Text = entrySOCKSPassword.Text = entryFTPPassword.Text;
			sbHTTPPort.Value = sbSOCKSPort.Value = sbFTPPort.Value;
			entryHTTPUserName.Text = entrySOCKSUserName.Text = entryFTPUserName.Text;
		}

		protected void OnChkSOCKSProxyToggled (object sender, EventArgs e)
		{
			if (((Gtk.CheckButton)sender).Active)
			{
				socksFrame.Sensitive = true;
			}

			else
			{
				socksFrame.Sensitive = false;
			}
		}

		protected void OnChkFTPProxyToggled (object sender, EventArgs e)
		{
			if (((Gtk.CheckButton)sender).Active)
			{
				ftpFrame.Sensitive = true;
			}

			else
			{
				ftpFrame.Sensitive = false;
			}
		}

		protected void OnButtonOkClicked (object sender, EventArgs e)
		{
			string section;
			/* General Tab */
			section = "general";
			settingsManager.setKeyValue (section, "load_on_startup",
			                             chk_load_on_startup.Active.ToString ());
			settingsManager.setKeyValue (section, "start_minimized", 
			                             chk_start_minimized.Active.ToString ());
			settingsManager.setKeyValue (section, "minimize_to_tray",
			                             chk_minimize_to_tray.Active.ToString ());
			settingsManager.setKeyValue (section, "minimize_on_close", 
			                             chk_minimize_on_close.Active.ToString ());
			settingsManager.setKeyValue (section, "launch_download_time",
			                             sb_launch_download_time.ValueAsInt.ToString ());
			settingsManager.setKeyValue (section, "exit_time", 
			                             sb_exit_time.ValueAsInt.ToString ());
			settingsManager.setKeyValue (section, "shutdown_time", 
			                             sb_shutdown_time.ValueAsInt.ToString ());
			settingsManager.setKeyValue (section, "auto_save_timeout", 
			                             sb_auto_save_timeout.ValueAsInt.ToString ());

			/* Group Tab*/
			section = "group";
			if (rb_group_auto.Active == true)
				settingsManager.setKeyValue (section, "new_download_group", "group_auto");
			else if (rb_group_manual.Active == true)
				settingsManager.setKeyValue (section, "new_download_group", cb_groups.ActiveText);

			List<string> list_groups = new List<string>();
			TreeModel model = cb_groups.Model;
			int n = model.IterNChildren () - 1;
			while (n >= 0)
			{
				cb_groups.Active = n;
				list_groups.Add (cb_groups.ActiveText);
				n--;
			}

			settingsManager.setKeyValue (section, "groups", list_groups);

			/* Proxy Tab */
			section = "proxy";
			/* HTTP Proxy */
			settingsManager.setKeyValue (section, "http_proxy", 
			                             chkHTTPProxy.Active.ToString ());
			settingsManager.setKeyValue (section, "all_http_proxy", 
			                             chkAllHTTP.Active.ToString ());
			settingsManager.setKeyValue (section, "http_proxy_address", 
			                             entryHTTPAddress.Text);
			settingsManager.setKeyValue (section, "http_proxy_port", 
			                             sbHTTPPort.ValueAsInt.ToString ());
			settingsManager.setKeyValue (section, "http_proxy_user_name", 
			                             entryHTTPUserName.Text);
			settingsManager.setKeyValue (section, "http_proxy_password", 
			                             entryHTTPPassword.Text);
			/* FTP Proxy */
			settingsManager.setKeyValue (section, "ftp_proxy", 
			                             chkFTPProxy.Active.ToString ());
			settingsManager.setKeyValue (section, "all_ftp_proxy", 
			                             chkAllFTP.Active.ToString ());
			settingsManager.setKeyValue (section, "ftp_proxy_address", 
			                             entryFTPAddress.Text);
			settingsManager.setKeyValue (section, "ftp_proxy_port", 
			                             sbFTPPort.ValueAsInt.ToString ());
			settingsManager.setKeyValue (section, "ftp_proxy_user_name", 
			                             entryFTPUserName.Text);
			settingsManager.setKeyValue (section, "ftp_proxy_password", 
			                             entryFTPPassword.Text);

			/* SOCKS Proxy */
			settingsManager.setKeyValue (section, "socks_proxy", 
			                             chkSOCKSProxy.Active.ToString ());
			settingsManager.setKeyValue (section, "all_socks_proxy", 
			                             chkAllSOCKS.Active.ToString ());
			settingsManager.setKeyValue (section, "socks_proxy_address", 
			                             entrySOCKSAddress.Text);
			settingsManager.setKeyValue (section, "socks_proxy_port", 
			                             sbSOCKSPort.ValueAsInt.ToString ());
			settingsManager.setKeyValue (section, "socks_proxy_user_name", 
			                             entrySOCKSUserName.Text);
			settingsManager.setKeyValue (section, "socks_proxy_password", 
			                             entrySOCKSPassword.Text);
		}

		protected void OnCmdNewGroupClicked (object sender, EventArgs e)
		{
			TreeIter iter;
			cb_groups.Model.GetIterFirst (out iter);
			do
			{
				string s = (string) cb_groups.Model.GetValue (iter, 1);
				if (s == entryGroupName.Text)
					return;
			}
			while (cb_groups.Model.IterNext (ref iter));

			cb_groups.AppendText (entryGroupName.Text);
		}
	}
}

