using System;
using Gtk;

namespace DownloadManager
{
	public partial class FTPViewPropDialog : Gtk.Dialog
	{
		public FTPViewPropDialog (FTPFile file)
		{
			this.Build ();
			lblName.Text = file.fileName;
			lblGroup.Text = file.fileGroup;
			lblInodes.Text = file.fileInodes.ToString ();
			lblOwner.Text = file.fileOwner;
			lblPath.Text = file.filePath;
			lblPermissions.Text = file.filePermissions;
			lblSize.Text = file.fileSize.ToString ();
			if (file.fileType == FTPFile.FTPFileType.Directory)
				lblType.Text = "Directory";
			else
				lblType.Text = "File";
		}
	}
}

