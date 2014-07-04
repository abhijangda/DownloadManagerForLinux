using System;
using Gtk;
using Gdk;
using libDownload;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

/* TODO: Add POPUP Menu with 
 * "Properties", "Download".
 * 
 */
namespace DownloadManager
{
	public class BreadCrumbWidget : VBox
	{
		List<ToggleButton> listToggleButton;
		List<string> listPath;
		HBox addressBar;
		Entry entryAddress;
		Button btnGo;
		HBox toggleBox;
		public delegate void OpenAddress (string address);
		public OpenAddress openAddressHandler;
		public string address
		{
			get
			{
				return entryAddress.Text;
			}
		}

		public BreadCrumbWidget () : base (false, 2)
		{
			Gtk.Image btnGoImage;
			btnGoImage = new Gtk.Image ("gtk-ok", IconSize.Button);
			listToggleButton = new List<ToggleButton> ();
			btnGo = new Button();
			btnGo.Add (btnGoImage);
			btnGo.Clicked += btnGoClicked;
			addressBar = new HBox (false, 2);
			entryAddress = new Entry ();
			addressBar.PackStart (entryAddress, true, true, 2);
			addressBar.PackStart (btnGo, false, false, 2);
			toggleBox = new HBox (false, 2);
			openAddressHandler = null;
			listPath = new List<string>();
			entryAddress.Text = "ftp://slackbuilds.org/11.0/";

			PackStart (addressBar, false, false, 2);
			PackStart (toggleBox, false, false, 2);
		}

		public ToggleButton appendButton (string address)
		{
			ToggleButton btn = new ToggleButton (address);
			listPath.Add (address);
			toggleBox.PackStart (btn, false, false, 2);
			listToggleButton.Add (btn);
			btn.Show ();
			return btn;
		}

		private void toggleBtnClicked (object sender, EventArgs args)
		{
			Console.WriteLine ("Clicked");
			if (openAddressHandler != null)
			{
				openAddressHandler (listPath [listToggleButton.IndexOf ((ToggleButton)sender)]);
			}
		}

		public void removeToEnd (int start)
		{
			for (int i = start; i < listToggleButton.Count; i++)
			{
				toggleBox.Remove (listToggleButton[i]);
			}

			listToggleButton.RemoveRange (start, listToggleButton.Count - start);
			listPath.RemoveRange (start, listToggleButton.Count - start);
		}

		public void goToAddress (string address)
		{
			removeToEnd (0);
			ToggleButton btn, rootBtn;
			int index = address.IndexOf ("/", "ftp://".Length + 1);
			if (index == -1)
			{
				rootBtn = appendButton (address.Trim());
				rootBtn.Active = true;
				rootBtn.Clicked += btnGoClicked;

				return;
			}

			string root = address.Substring (0, index);
			string[] split = address.Substring (index).Split ("/".ToCharArray (),
			                                					 StringSplitOptions.RemoveEmptyEntries);
			rootBtn = appendButton (root);
			btn = null;
			for (int i = 0; i < split.Length; i++)
			{
				btn = appendButton (split[i].Trim ());
				if (i < split.Length - 1)
				{
					btn.Clicked += toggleBtnClicked;
				}
			}

			if (btn != null)
			{
				btn.Active = true;
				btn.Clicked += toggleBtnClicked;
			}
			else
			{
				rootBtn.Active = true;
			}

			rootBtn.Clicked += toggleBtnClicked;
			entryAddress.Text = address;
		}

		private void btnGoClicked (object sender, EventArgs args)
		{
			if (openAddressHandler != null)
				openAddressHandler (entryAddress.Text);
		}
	}

	public class FTPFile
	{
		public enum FTPFileType
		{
			Directory,
			File,
		}

		public string filePermissions {get; private set;}
		public FTPFileType fileType {get; private set;}
		public int fileInodes {get; private set;}
		public string fileOwner {get; private set;}
		public string fileGroup {get; private set;}
		public long fileSize {get; private set;}
		public string fileDate {get; private set;}
		public string fileName {get; private set;}
		public string filePath {get; private set;}
		public FTPFile parent {get; private set;}

		public class FTPFileComparator : IComparer<FTPFile>
		{
			public int Compare (FTPFile file1, FTPFile file2)
			{
				return file1.fileName.CompareTo (file2.fileName);
			}
		}

		public FTPFile (List<string> listString, string parentAddress)
		{
			if (listString.Count < 8)
				throw new Exception ("List String invalid");

			filePermissions = listString [0];
			if (filePermissions.ToCharArray()[0] == 'd')
				fileType = FTPFileType.Directory;
			else
				fileType = FTPFileType.File;
			fileInodes = int.Parse (listString [1]);
			fileOwner = listString [2];
			fileGroup = listString [3];
			fileSize = long.Parse (listString [4]);
			fileDate = listString[5] + " " + listString [6] + " " + listString [7];
			fileName = listString [8];
			filePath = Path.Combine (parentAddress, fileName) + "/";
			Console.WriteLine (filePath);
			this.parent = parent;
		}
	}

	[System.ComponentModel.ToolboxItem(true)]
	public class FTPGetWidget : VBox
	{
		BreadCrumbWidget breadCrumb;
		IconView iconView;
		ScrolledWindow scrolledWindow;
		Thread ftpThread;
		string returnedString;
		FtpStatusCode status;
		ListStore fileListStore;
		string parentAddress;
		Statusbar statusBar;
		string errorMessage;
		Menu popupMenu;

		enum FileListStoreCols
		{
			FILE_LIST_STORE_ICON = 0,
			FILE_LIST_STORE_NAME = 1,
			FILE_LIST_STORE_PATH = 2,
			FILE_LIST_STORE_COLS_N = 3,
		}

		public FTPGetWidget () : base (false, 2)
		{
			MenuItem menuItem;

			breadCrumb = new BreadCrumbWidget ();
			breadCrumb.openAddressHandler = openAddressHandler;
			fileListStore = new ListStore (typeof(Gdk.Pixbuf), typeof (string), 
			                               typeof (object));
			iconView = new IconView (fileListStore);
			iconView.TextColumn = 1;
			iconView.PixbufColumn = 0;
			iconView.SelectionMode = SelectionMode.Multiple;
			iconView.ItemActivated += iconViewItemActivated;
			scrolledWindow = new ScrolledWindow ();
			scrolledWindow.Add (iconView);
			parentAddress = "";
			statusBar = new Statusbar();
			errorMessage = "";

			popupMenu = new Menu ();

			menuItem = new MenuItem ("Open");
			popupMenu.Append (menuItem);
			menuItem.Activated += openMenuItemActivated;

			menuItem = new MenuItem ("Select All");
			popupMenu.Append (menuItem);
			menuItem.Activated += selectAllMenuItemActivated;

			menuItem = new MenuItem ("Download");
			popupMenu.Append (menuItem);
			menuItem.Activated += downloadMenuItemActivated;

			menuItem = new MenuItem ("Properties");
			popupMenu.Append (menuItem);
			menuItem.Activated += propertiesMenuItemActivated;

			iconView.ButtonPressEvent += OnIconViewButtonPressEvent;

		    PackStart (breadCrumb, false, false, 2);
			PackStart (scrolledWindow, true, true, 2);
			PackStart (statusBar, false, false, 2);
		}

		private void selectAllMenuItemActivated (object sender, EventArgs args)
		{
		}

		private void openMenuItemActivated (object sender, EventArgs args)
		{
		}

		private void downloadMenuItemActivated (object sender, EventArgs args)
		{
		}

		private void propertiesMenuItemActivated (object sender, EventArgs args)
		{
		}

		protected void OnIconViewButtonPressEvent (object sender, 
		                                           ButtonPressEventArgs args)
		{
			if (args.Event.Button != 3)
				return;

			//TreePath[] selectedPaths = iconView.GetSelectedRows ();

			popupMenu.Popup ();
			popupMenu.ShowAll ();

			return;
		}

		private void iconViewItemActivated (object sender, ItemActivatedArgs args)
		{
			TreeIter iter;
			FTPFile ftpFile;

			if (!fileListStore.GetIter (out iter, args.Path))
			{
				return;
			}

			ftpFile = (FTPFile)fileListStore.GetValue (iter, 2);

			if (ftpFile.fileType == FTPFile.FTPFileType.File)
				return;

			openAddressHandler (ftpFile.filePath);
		}

		private void ftpRequestThreadFunc (object url)
		{
			FtpWebRequest request = null;
			FtpWebResponse response = null;
			try
			{
				request = (FtpWebRequest)WebRequest.Create((string)url);
				request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
				response = (FtpWebResponse)request.GetResponse();

				Stream responseStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(responseStream);
				returnedString = reader.ReadToEnd();
				Console.WriteLine (returnedString);
				status = response.StatusCode;

				reader.Close();
				responseStream.Close();
				response.Close();
	    		GLib.Timeout.Add (10, this.callback);
				parentAddress = (string)url;
				errorMessage = "";
			}
			catch (Exception e)
			{
				errorMessage = e.Message;
				GLib.Timeout.Add (10, this.callback);
			}
		}

		bool callback ()
		{
			if (errorMessage != "")
			{
				statusBar.Push (0, errorMessage);
				errorMessage = "";
				return false;
			}

			if (status != FtpStatusCode.ClosingControl)
			{
				/* Error */
				statusBar.Push (0, "Error");
			}

			if (returnedString == "")
			{
				/* Empty */
			}

			fileListStore.Clear ();
			statusBar.Push (0, "Transaction Completed.");

			List<FTPFile> listFiles = new List<FTPFile> ();
			List<FTPFile> listDirs = new List<FTPFile> ();

			foreach (Match match in Regex.Matches (returnedString, @".+"))
			{
				List<string> stringList = splitStringOverSpace (match.Value);
				FTPFile file = new FTPFile(stringList, parentAddress);
				if (file.fileType == FTPFile.FTPFileType.Directory)
					listDirs.Add (file);
				else
					listFiles.Add (file);
			}

			IComparer<FTPFile> comparer = new FTPFile.FTPFileComparator ();
			listDirs.Sort (comparer);

			foreach (FTPFile file in listDirs)
			{
				insertInIconView (file);
			}
	
			listFiles.Sort (comparer);

			foreach (FTPFile file in listFiles)
			{
				insertInIconView (file);
			}

			returnedString = "";
			breadCrumb.goToAddress (parentAddress);

			return false;
		}
	
		private List<string> splitStringOverSpace (string s)
		{
			List<string> list = new List<string> ();
			bool isfirst = false;
			string _s = "";
			foreach (char c in s.Trim ())
			{
				if (c == ' ')
				{
					if (isfirst == true)
					{
						list.Add (_s);
						_s = "";
					}
					isfirst = false;
					continue;
				}
				_s += c.ToString ();
				isfirst = true;
			}
			if (isfirst == true)
			{
				list.Add (_s);
			}
			return list;
		}

		private void insertInIconView (FTPFile file)
		{
			TreeIter iter;
			Gdk.Pixbuf pixbuf;
			IconTheme iconTheme;

			iconTheme = IconTheme.Default;

			if (file.fileType == FTPFile.FTPFileType.Directory)
			{
				pixbuf = MainWindow.main_instance.RenderIcon (Stock.Directory,
				                                              IconSize.Dialog,
				                                              "");
			}
			else
			{
				pixbuf = MainWindow.main_instance.RenderIcon (Stock.File,
				                                              IconSize.Dialog,
				                                              "");
			}

			iter = fileListStore.AppendValues (pixbuf, file.fileName, file);
		}

		private void openAddressHandler (string address)
		{
			ftpThread = new Thread (ftpRequestThreadFunc);
			ftpThread.Start (address);
			statusBar.Push (0, "Opening " + address);
		}
	}
}

