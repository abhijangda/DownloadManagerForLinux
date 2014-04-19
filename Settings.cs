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
			{
				string dir = "~/autoconf";

				if (dir.IndexOf ("~") == 0)
					dir = dir.Replace ("~", Environment.GetFolderPath (Environment.SpecialFolder.UserProfile));
				return dir;
			}

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

		public void storeInfo (List<DMDownload> listDownloads, 
		                       List<DMQueue> listQueues)
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

			foreach (DMQueue queue in listQueues)
			{
				s += queue.ToString ();
			}

			string _path = Path.Combine (path, "downloadsInfo.conf");
			if (File.Exists (_path))
				File.Delete (_path);

			File.WriteAllText (_path, s);
		}

		public void loadDwnldsQueues (ref List<DMDownload> listDownloads,
		                              ref List<DMQueue> listQueues)
		{
			if (!File.Exists (Path.Combine (path, "downloadsInfo.conf")))
				return;

			Dictionary<string, DMDownload> dict = new Dictionary<string, DMDownload> ();

			string text = File.ReadAllText (Path.Combine (path, "downloadsInfo.conf"));
			MatchCollection mc = Regex.Matches (text, "<download.+?</download>", RegexOptions.Singleline);
			DMDownload dmld = null;
			Download dwld = null;
			foreach (Match m in mc)
			{
				if (m.Value.Contains ("<download-http>"))
					dwld = new HTTPDownload ("", "", 
					                         MainWindow.main_instance.OnDownloadStatusChanged,
					                         false, 5);

				else if (m.Value.Contains ("<download-ftp>"))
					dwld = new FTPDownload ("", "", 
					                        MainWindow.main_instance.OnDownloadStatusChanged, 
					                        false, 5);

				if (dwld != null)
				{
					dwld.initFromXML (m.Value);
					dmld = new DMDownload (dwld, null);
					listDownloads.Add (dmld);
					dict.Add (dmld.download.localPath, dmld);
				}

				if (m.Value.Contains ("<type>"))
				{
					string s = m.Value;
					int start = s.IndexOf ("<type>") + "<type>".Length;
					dmld.typeCategory = new DMTypeCategory (s.Substring (start, s.IndexOf ("</type>") - start).Trim ());
				}
			}

			mc = Regex.Matches (text, "<queue.+?</queue>", RegexOptions.Singleline);
			DMQueue queue = new DMQueue ("");
			foreach (Match m in mc)
			{
				queue.loadQueueFromXML (m.Value, dict);
				if (queue.name != "")
					listQueues.Add (queue);
			}
		}
	}
}
