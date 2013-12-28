using System;
using Gtk;
using System.Net;

namespace DownloadManager
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ServicePointManager.DefaultConnectionLimit = 10;
			Application.Init ();
			MainWindow win = new MainWindow ();
			MainWindow.main_instance = win;
			win.Show ();
			Application.Run ();
		}
	}
}
