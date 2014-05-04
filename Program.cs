using System;
using Gtk;
using System.Net;

namespace DownloadManager
{
	class MainClass
	{
		public static short countInString (string str1, string str2)
		{
			short count = 0;
			int index = 0;
			index = str1.IndexOf (str2, index + 1);
			while (index != -1)
			{
				count += 1;
				index = str1.IndexOf (str2, index + 1);
			}

			return count;
		}

		public static void Main (string[] args)
		{
			//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments
			ServicePointManager.DefaultConnectionLimit = 50;
			Application.Init ();
			Settings settingsManager = new DownloadManager.Settings (
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
			settingsManager.loadSettings();
			MainWindow win = new MainWindow (settingsManager);
			MainWindow.main_instance = win;
			win.loadSettings ();
			win.Maximize ();
			win.Show ();
			Application.Run ();
		}
	}
}
