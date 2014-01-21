using System;

namespace DownloadManager
{
	public partial class CreateQueueDialog : Gtk.Dialog
	{
		public string name;
		public int start;

		public CreateQueueDialog ()
		{
			this.Build ();
		}

		protected void buttonOkClicked (object sender, EventArgs e)
		{
			name = entryQueueName.Text;

			if (rbNow.Active == true)
				start = 1;

			else if (rbLater.Active == true)
				start = 2;

			else
				start = 3;

			Respond (Gtk.ResponseType.Ok);
		}
	}
}

