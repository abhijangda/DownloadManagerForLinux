using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using libDownload;

namespace DownloadManager
{
	public class Settings
	{
		string path;
		public Settings (string _path)
		{
			path = _path;
		}

		public string getKeyValue (string key)
		{
			if (key == "default-dir")
				return "~/Downloads";

			if (key == "save-time-out")
				return "2000";

			return "";
		}

		public List<string> getKeyValueArray (string key)
		{
			List<string> a = new List<string> ();

			if (key == "type-category")
			{
				a.Add ("All");
				a.Add ("Media");
				a.Add ("Compressed");
				a.Add ("Executable");
				a.Add ("Documents");
				a.Add ("Others");
			}
			return a;
		}

		public void storeInfo (List<DMDownload> listDownloads)
		{
			string s = "";
			foreach (DMDownload dmld in listDownloads)
			{
				if (dmld.download is HTTPDownload)
				    s += "<download-" + "http>\n";
				else if (dmld.download is FTPDownload)
					s += "<download-" + "ftp>\n";

				s += "\t<address>\n\t\t" + dmld.download.remotePath + "\n\t</address>\n";
				s += "\t<localpath>\n\t\t" + dmld.download.localPath + "\n\t</localpath>\n";
				s += "\t<size>\n\t\t" + dmld.download.length.value + "\n\t</size>\n";
				s += "\t<downloaded>\n\t\t" + dmld.download.getDownloaded ().value + "\n\t</downloaded>\n";
				s += "\t<sections>\n\t\t" + dmld.download.parts.ToString () + "\n\t</sections>\n";
				s += "\t<type>\n\t\t" + dmld.typeCategory.name + "\n\t</type>\n";
				s += "\t<download-state>\n\t\t" + dmld.download.status.ToString () +"\n\t</download-state>\n";
				s += "</download>\n";
			}

			string _path = Path.Combine (path, "downloadsInfo.conf");
			if (File.Exists (_path))
				File.Delete (_path);

			File.WriteAllText (_path, s);
		}

		public void loadDownloads (ref List<DMDownload> listDownloads)
		{
			if (!File.Exists (Path.Combine (path, "downloadsInfo.conf")))
				return;

			string text = File.ReadAllText (Path.Combine (path, "downloadsInfo.conf"));
			MatchCollection mc = Regex.Matches (text, "<download.+?</download>", RegexOptions.Singleline);
			DMDownload dmld = new DMDownload (null, null);
			Download dwld = null;
			foreach (Match m in mc)
			{
				if (m.Value.Contains ("<download-http>"))
					dwld = new HTTPDownload ("", "", 
					                         MainWindow.main_instance.OnDownloadStatusChanged,
					                         true, 5);

				else if (m.Value.Contains ("<download-ftp>"))
					dwld = new FTPDownload ("", "", 
					                        MainWindow.main_instance.OnDownloadStatusChanged, 
					                        true, 5);

				if (m.Value.Contains ("<type>"))
				{
					string s = m.Value;
					int start = s.IndexOf ("<type>") + "<type>".Length;
					dmld.typeCategory = new DMTypeCategory (s.Substring (start, s.IndexOf ("</type>") - start).Trim ());
				}

				if (dwld != null)
				{
					dwld.initFromXML (m.Value);
					dmld.download = dwld;
					listDownloads.Add (dmld);
				}
			}
		}
	}
}
