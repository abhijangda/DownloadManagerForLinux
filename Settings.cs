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
				s += "\t<size>\n\t\t" + dmld.download.length + "\n\t</size>\n";
				s += "\t<downloaded>\n\t\t" + dmld.download.getDownloaded ().value + "\n\t</downloaded>\n";
				s += "\t<sections>\n\t\t" + dmld.download.parts.ToString () + "\n\t</sections>";
				s += "\t<type>\n\t\t" + dmld.typeCategory.name + "\n\t</type>\n";
				s += "</download>\n";
			}

			string _path = Path.Combine (path, "downloadsInfo.conf");
			if (File.Exists (_path))
				File.Delete (_path);

			File.WriteAllText(path, s);
		}

		public void loadDownloads (ref List<DMDownload> listDownloads)
		{
			if (!File.Exists (Path.Combine (path, "downloadsInfo.conf")))
				return;

			string text = File.ReadAllText (Path.Combine (path, "downloadsInfo.conf"));
			MatchCollection mc =  Regex.Matches (text, ".+", RegexOptions.Multiline);
			DMDownload dmld = new DMDownload (null, null);
			Download dwld = null;
			for (int i = 0; i < mc.Count; i++)
			{
				Match m = mc [i];
				if (m.Value.Contains ("<download-http>"))
					dwld = new HTTPDownload ("", "", null, true, 5);

				else if (m.Value.Contains ("<download-ftp>"))
					dwld = new FTPDownload ("", "", null, true, 5);

				else if (dwld != null && m.Value.Contains ("<address>"))
				{
					i += 1;
					dwld.remotePath = mc [i].Value.Trim ();
					i += 1;
				}

				else if (dwld != null && m.Value.Contains ("<localPath>"))
				{
					i += 1;
					dwld.localPath = mc [i].Value.Trim ();
					i += 1;
				}

				else if (dwld != null && m.Value.Contains ("<size>"))
				{
					i += 1;
					dwld.length = new Length (long.Parse (mc [i].Value.Trim ()));
					i += 1;
				}

				else if (dwld != null && m.Value.Contains ("<sections>"))
				{
					i += 1;
					dwld.parts = short.Parse (mc [i].Value.Trim ());
					i += 1;
				}

				else if (dwld != null && m.Value.Contains ("<type>"))
				{
					i += 1;
					dmld.typeCategory = new DMTypeCategory (mc [i].Value);
					i += 1;
				}

				else if (dwld != null && m.Value.Contains ("</download>"))
				{
					dmld.download = dwld;
					listDownloads.Add (dmld);
				}
			}
		}
	}
}
