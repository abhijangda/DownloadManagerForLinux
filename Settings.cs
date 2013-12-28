using System;
using System.Collections.Generic;

namespace DownloadManager
{
	public class Settings
	{
		public Settings (string path)
		{

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
	}
}
