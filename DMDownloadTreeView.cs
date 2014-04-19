using System;
using Gtk;
using Gdk;
using libDownload;
using System.Collections.Generic;

namespace DownloadManager
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class DMDownloadTreeView : DMTreeView
	{
		public List<DMDownload> listAllDownloads {private get; set;}

		public DMDownloadTreeView ()
		{
			treeModel = new Gtk.ListStore (typeof(Gdk.Pixbuf), typeof (string), 
			                               typeof (string), typeof (float), 
			                               typeof (string), typeof (string), 
			                               typeof (string), typeof (DMDownload));
			createTreeView ();
		}

		protected override int sizeSortFunc (TreeModel _treeModel, TreeIter iter1,
		                                     TreeIter iter2)
		{
		    DMDownload dmld1 = (DMDownload)((Gtk.ListStore)treeModel).GetValue (iter1, 2);
			DMDownload dmld2 = (DMDownload)((Gtk.ListStore)treeModel).GetValue (iter2, 2);

			if (dmld1.download.length.value == dmld2.download.length.value)
				return 0;

			if (dmld1.download.length.value > dmld2.download.length.value)
				return 1;

			return -1;
		}

		public override void addRow (object o)
		{
			DMDownload dwnld = (DMDownload)o;
			Gtk.TreeIter iter;
			float length = (float)dwnld.download.length.value;
			if (length == 0)
				length = 1;

			iter = ((Gtk.ListStore)treeModel).AppendValues (MainWindow.getPixbufForStatus (dwnld.download.status), 
			                      				            dwnld.download.localPath.Substring(
							                                  dwnld.download.localPath.LastIndexOf ('/')+1),
							                                dwnld.download.length.ToString (),
							                                (100*dwnld.download.getDownloaded ().value)/length,
							                                "", "", 
							                                dwnld.download.parts.ToString (),
							                                dwnld);

			dwnld.rowReference = new Gtk.TreeRowReference (((Gtk.ListStore)treeModel), 
			                                               ((Gtk.ListStore)treeModel).GetPath (iter));
		}

		public override void updateDownloadStatus (object o, Length downloaded, Speed speed)
		{
			DMDownload dwnld = (DMDownload)o;
			Gtk.TreeIter iter;
			if (dwnld.rowReference.Valid ())
			{
				((Gtk.ListStore)treeModel).GetIter (out iter, dwnld.rowReference.Path);
			    setIterValues (dwnld, iter, downloaded, speed);
			}
			//((Gtk.ListStore)treeModel).GetIterFirst (out iter);
			/*do
			{
				DMDownload dmld = (DMDownload) ((Gtk.ListStore)treeModel).GetValue (iter, 7);
				if (dmld.download.localPath == dwnld.download.localPath)
				{
					setIterValues (dmld, iter, downloaded, speed);
				}
			}
			while (((Gtk.ListStore)treeModel).IterNext (ref iter));*/
		}

		public override void setImageForStatus (object o)
		{
			DMDownload dwnld = (DMDownload) o;
			if (!dwnld.rowReference.Valid ())
				return;

			TreeIter iter;
			((Gtk.ListStore)treeModel).GetIter (out iter, dwnld.rowReference.Path);
			((Gtk.ListStore)treeModel).SetValue (
				iter, 0, MainWindow.getPixbufForStatus (dwnld.download.status));
		}

		protected override void setIterValues (object o, TreeIter iter, 
		                           			   Length downloaded = default (Length),
		                            		   Speed speed = default (Speed))
		{
			DMDownload dwnld = (DMDownload)o;
			if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADING)
			{
				((Gtk.ListStore)treeModel).SetValue (iter, 1, dwnld.download.localPath.Substring (dwnld.download.localPath.LastIndexOf ('/') +1));
				((Gtk.ListStore)treeModel).SetValue (iter, 2, dwnld.download.length.ToString ());
				((Gtk.ListStore)treeModel).SetValue (iter, 3, (100*downloaded.value)/(float)dwnld.download.length.value);
				((Gtk.ListStore)treeModel).SetValue (iter, 4, MainWindow.getTime (dwnld.download.length.value - downloaded.value, speed.value));
				((Gtk.ListStore)treeModel).SetValue (iter, 5, speed.ToString ());
				((Gtk.ListStore)treeModel).SetValue (iter, 6, dwnld.download.parts.ToString ());
			}
			else if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.MERGING)
			{
				((Gtk.ListStore)treeModel).SetValue (iter, 3, (float)100.0);
				((Gtk.ListStore)treeModel).SetValue (iter, 4, "");
				((Gtk.ListStore)treeModel).SetValue (iter, 5, "");
				((Gtk.ListStore)treeModel).SetValue (iter, 6, dwnld.download.parts.ToString ());
			}
			else if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADED)
			{
				((Gtk.ListStore)treeModel).SetValue (iter, 3, (float)100.0);
				((Gtk.ListStore)treeModel).SetValue (iter, 4, "");
				((Gtk.ListStore)treeModel).SetValue (iter, 5, "");
				((Gtk.ListStore)treeModel).SetValue (iter, 6, dwnld.download.parts.ToString ());
			}
		}

		public void showCategory (params string[] cats)
		{
			((Gtk.ListStore)((Gtk.ListStore)treeModel)).Clear ();
			foreach (DMDownload dmld in listAllDownloads)
			{
				foreach (string cat in cats)
				{
					TreeIter iter;
					if (dmld.typeCategory.name == cat ||
					    Enum.GetName (typeof (DOWNLOAD_STATUS) ,dmld.download.status) == cat)
					{
						iter = ((Gtk.ListStore)((Gtk.ListStore)treeModel)).Append ();
						setIterValues (dmld, iter);
						dmld.rowReference = new Gtk.TreeRowReference (((Gtk.ListStore)treeModel), 
						                                              ((Gtk.ListStore)treeModel).GetPath (iter));
					}
				}
			}
		}

		public override void searchInColumns (string text, List<int> columns)
		{
			TreeIter iter;
			if (this.Selection.GetSelected (out iter))
			    ((Gtk.ListStore)treeModel).IterNext (ref iter);
			else
				((Gtk.ListStore)treeModel).GetIterFirst (out iter);

			while (((Gtk.ListStore)((Gtk.ListStore)treeModel)).IterIsValid (iter))
			{
				string value = "";
				foreach (int column in columns)
				{
					value = (string) ((Gtk.ListStore)treeModel).GetValue (iter, column+1);
					if (value == text)
					{
						this.Selection.SelectIter (iter);
						return;
					}
				}

				((Gtk.ListStore)treeModel).IterNext (ref iter);
			}
		}

		protected override bool OnButtonReleaseEvent (Gdk.EventButton evnt)
		{
			if (evnt.Button != 3)
				return base.OnButtonReleaseEvent (evnt);

			TreePath[] selectedPaths = Selection.GetSelectedRows ();
			if (selectedPaths.Length == 0)
			{
				contextStart.Sensitive = false;
				contextPause.Sensitive = false;
				contextRestart.Sensitive = false;
				contextCancel.Sensitive = false;
			}

			else
			{
				if (selectedPaths.Length == 1)
				{
					TreeIter iter;
					((Gtk.ListStore)treeModel).GetIter (out iter, selectedPaths [0]);
					DMDownload dmld = (DMDownload) ((Gtk.ListStore)treeModel).GetValue (iter, 7);
					if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADED)
					{
						contextStart.Sensitive = false;
						contextPause.Sensitive = false;
						contextRestart.Sensitive = true;
						contextCancel.Sensitive = false;
					}
					else if (dmld.download.status == DOWNLOAD_STATUS.NOT_STARTED ||
					    dmld.download.status == DOWNLOAD_STATUS.ERROR)
					{
						contextStart.Sensitive = true;
						contextPause.Sensitive = false;
						contextRestart.Sensitive = false;
						contextCancel.Sensitive = false;
					}
					else if (dmld.download.status == DOWNLOAD_STATUS.DOWNLOADING)
					{
						contextStart.Sensitive = false;
						contextPause.Sensitive = true;
						contextRestart.Sensitive = true;
						contextCancel.Sensitive = true;
					}
					else if (dmld.download.status == DOWNLOAD_STATUS.PAUSED)
					{
						contextStart.Sensitive = true;
						contextPause.Sensitive = false;
						contextRestart.Sensitive = true;
						contextCancel.Sensitive = false;
					}
				}
			}


			contextMenu.Popup ();
			contextMenu.ShowAll ();

			return base.OnButtonReleaseEvent (evnt);
		}

		protected override bool OnButtonPressEvent (EventButton evnt)
		{
			if (ctrlPressed == false)
			{
				this.Selection.Mode = SelectionMode.Single;
				return base.OnButtonPressEvent (evnt);
			}

			if (evnt.Button == 1)
			{
				this.Selection.Mode = SelectionMode.Multiple;
			}

			return base.OnButtonPressEvent (evnt);
		}

		protected override bool OnKeyPressEvent (EventKey evnt)
		{
			if (evnt.KeyValue == 65507 || evnt.KeyValue == 65508) /*For Ctrl*/
			{
				ctrlPressed = true;
			}

			return base.OnKeyPressEvent (evnt);
		}

		protected override bool OnKeyReleaseEvent (EventKey evnt)
		{
			if (evnt.KeyValue == 65507 || evnt.KeyValue == 65508) /*For Ctrl*/
			{
				ctrlPressed = false;
			}

			return base.OnKeyReleaseEvent (evnt);
		}

		public void loadDownloads ()
		{
			foreach (DMDownload dmld in listAllDownloads)
				addRow (dmld);
		}
	}
}