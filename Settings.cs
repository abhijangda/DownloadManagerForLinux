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
		Dictionary<string, Dictionary<string, dynamic>> dict;

		public Settings (string _path)
		{
			path = _path;
			dict = new Dictionary<string, Dictionary<string, dynamic>> ();
		}

		public string getKeyValue (string section, string key)
		{
			try
			{
				Dictionary<string, dynamic> _dict = dict[section];
				if (key == "default-dir")
				{
					string dir = _dict[key];

					if (dir.IndexOf ("~") == 0)
						dir = dir.Replace ("~", Environment.GetFolderPath (Environment.SpecialFolder.UserProfile));

					return dir;
				}

				if (key == "save-time-out")
					return "2000";

				return _dict[key];
			}
			catch (Exception e)
			{
			}

			return "";
		}

		public List<string> getKeyValueArray (string section, string key)
		{
			return dict[section][key];
		}

		public void setKeyValue (string section, string key, dynamic value)
		{
			if (!dict.ContainsKey (section))
			{
				Console.WriteLine (section + "not in dict");
				dict.Add (section, new Dictionary<string, dynamic> ());
			}
			Console.WriteLine ("set " + section + " " + key + " " + value);
			Dictionary<string, dynamic> section_dict = dict[section];
			if (!section_dict.ContainsKey (key))
			{
				section_dict.Add (key, value);
			}
			else
			{
				dict[section][key] = value;
			}
		}

		public void loadSettings ()
		{
			if (!File.Exists (Path.Combine (path, "dmsettings.conf")))
				return;

			string text = File.ReadAllText (Path.Combine (path, "dmsettings.conf"));
			MatchCollection mc = Regex.Matches (text, ".+");
			int iter = 0;
			while (iter < mc.Count)
			{
				Match m = mc [iter];
				if (!(m.Value[0] == '[' && m.Value[m.Value.Length-1] == ']'))
				{
					iter++;
					continue;
				}

				string section = m.Value.Substring (1, m.Value.Length - 2);
				Dictionary <string, dynamic> d;
				d = new Dictionary<string, dynamic> ();
				dict.Add (section, d);

				while (true)
				{
					iter++;
					if (iter == mc.Count)
					{
						iter --;
						break;
					}

					m = mc [iter];
					Console.WriteLine (m.Value);
					if (m.Value [0] == '[' && m.Value [m.Value.Length - 1] == ']')
					{
						Console.WriteLine ("DSD"+m.Value);
						iter--;
						break;
					}
					if (Regex.IsMatch (m.Value, ".+=.+"))
					{
						string key = m.Value.Substring (0, m.Value.IndexOf ("=")).Trim ();
						string value = m.Value.Substring (m.Value.IndexOf ("=")+1).Trim ();
						d.Add (key, value);
					}
					else if (m.Value.Trim () [0] == '<' && m.Value.Trim () [1] != '/' && m.Value.Trim () [m.Value.Trim ().Length - 1] == '>')
					{
						Console.WriteLine (m.Value+"f");
						string key = m.Value.Trim ().Substring (1, m.Value.Trim().Length - 2);
						List<string> value = new List<string> ();
						d.Add (key, value);
						while (true)
						{
							iter++;
							if (iter == mc.Count)
							{
								iter--;
								break;
							}

							m = mc [iter];
							Console.WriteLine (m.Value + " " + key);
							if (m.Value.Trim () == "</"+key+">")
							{
								iter--;
								break;
							}
							value.Add (m.Value.Trim ());
						}
					}
				}
				iter++;
			}

			List<string> l = dict["group"]["groups"];
			if (l.Count == 0)
			{
				l.Add ("All");
				l.Add ("Media");
				l.Add ("Compressed");
				l.Add ("Executable");
				l.Add ("Documents");
				l.Add ("Others");
			}
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

			/* Storing settings */
			string text = "";
			Dictionary<string, Dictionary<string, dynamic>>.Enumerator e = dict.GetEnumerator ();
			do
			{
				string key = e.Current.Key;
				if (key == null)
				{
					Console.WriteLine("nulll key");
					continue;
				}
				Dictionary <string, dynamic> value = e.Current.Value;
				text += "[" + key + "]\n";

				if (value == null)
				{
					Console.WriteLine("nulll value");
					continue;
				}

				Dictionary <string, dynamic>.Enumerator ee = value.GetEnumerator ();
				do
				{
					Console.WriteLine (ee.Current.Key + " " + ee.Current.Value);
					if (ee.Current.Value is string)
						text += "\t" + ee.Current.Key + "=" + ee.Current.Value.ToString () + "\n";
					else if (ee.Current.Value is List<string>)
					{
						text += "\t<" + ee.Current.Key + ">\n";
						List<string> list = (List<string>)ee.Current.Value;
						foreach (string v in list)
						{
							text += "\t" + v + "\n";
						}
						text += "\t</" + ee.Current.Key + ">\n";
					}
					else if (e.Current.Value == null)
					{
						text += "\t" + ee.Current.Key + "=\n";
					}
				}
				while (ee.MoveNext ());
			}
			while (e.MoveNext ());
			File.WriteAllText (Path.Combine (path, "dmsettings.conf"), text);
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
