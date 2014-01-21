using System;
using Gtk;
using System.Collections.Generic;
using libDownload;

namespace DownloadManager
{
	[System.ComponentModel.ToolboxItem(true)]
	public class DMQueueTreeView : DMTreeView
	{
		public List<DMQueue> listAllQueues {private get; set;}

		public DMQueueTreeView ()
		{
			treeModel = new Gtk.TreeStore (typeof(Gdk.Pixbuf), typeof (string), 
			                               typeof (string), typeof (float), 
			                               typeof (string), typeof (string), 
			                               typeof (string), typeof (object));

			createTreeView ();
		}

		protected override int sizeSortFunc (TreeModel treeModel, TreeIter iter1,
		                          TreeIter iter2)
		{
			DMDownload dmld1 = (DMDownload)treeModel.GetValue (iter1, 2);
			DMDownload dmld2 = (DMDownload)treeModel.GetValue (iter2, 2);

			if (dmld1.download.length.value == dmld2.download.length.value)
				return 0;

			if (dmld1.download.length.value > dmld2.download.length.value)
				return 1;

			return -1;
		}

		public override void addRow (object o)
		{
			DMQueue queue = (DMQueue)o;
			TreeIter iter;
			iter = ((Gtk.TreeStore)treeModel).AppendValues (MainWindow.getPixbufForStatus (queue.status),
			                                                queue.name, 
			                                                queue.getTotalLength ().ToString (),
			                                                100*queue.getTotalProgress (),
			                                                "", "", 
			                                                queue.getTotalParts ().ToString (),
			                                                queue);

			queue.treeRowReference = new TreeRowReference (treeModel,
			                                               treeModel.GetPath (iter));

			foreach (DMDownload d in queue.listDownloads)
			{
				TreeIter iter2;
				iter2 = ((Gtk.TreeStore)treeModel).AppendValues (
					iter, MainWindow.getPixbufForStatus (d.download.status), 
					d.download.localPath.Substring(
						d.download.localPath.LastIndexOf ('/')+1), 
                    d.download.length.ToString (), 
					(100*d.download.getDownloaded ())/d.download.length, "", "",
				    d.download.parts.ToString (), d);

				queue.listDMRowReference.Add (new TreeRowReference (treeModel, 
				                                                    treeModel.GetPath (iter2)));
			}
		}

		protected override void setIterValues (object o, TreeIter iter, 
		                                       Length downloaded, Speed speed)
		{
			if (o is DMDownload)
			{
				DMDownload dmld = (DMDownload)o;

			}

			if (o is DMQueue)
			{
				DMQueue queue = (DMQueue)o;
			}
		}

		public override void searchInColumns (string text, List<int> columns)
		{
			TreeIter iter;
			if (this.Selection.GetSelected (out iter))
				((Gtk.TreeStore)treeModel).IterNext (ref iter);
			else
				((Gtk.TreeStore)treeModel).GetIterFirst (out iter);

			while (((Gtk.TreeStore)treeModel).IterIsValid (iter))
			{
				string value = "";
				foreach (int column in columns)
				{
					value = (string) ((Gtk.TreeStore)treeModel).GetValue (iter, column+1);
					if (value == text)
					{
						this.Selection.SelectIter (iter);
						return;
					}
				}

				((Gtk.TreeStore)treeModel).IterNext (ref iter);
			}
		}

		public override void setImageForStatus (object o)
		{
			DMQueue queue = (DMQueue) o;
			if (!queue.treeRowReference.Valid ())
				return;

			TreeIter iter;
			((Gtk.TreeStore)treeModel).GetIter (out iter, queue.treeRowReference.Path);
			((Gtk.TreeStore)treeModel).SetValue (
				iter, 0, MainWindow.getPixbufForStatus (queue.status));
		}

		public override void updateDownloadStatus (object o, Length downloaded,
		                                           Speed speed)
		{
			if (o is DMQueue)
			{
				DMQueue queue = (DMQueue)o;
				TreeIter iter;
				treeModel.GetIter (out iter, queue.treeRowReference.Path);
				if (queue.status == libDownload.DOWNLOAD_STATUS.DOWNLOADING)
				{
					((Gtk.TreeStore)treeModel).SetValue (iter, 1, queue.name);
					((Gtk.TreeStore)treeModel).SetValue (iter, 2, queue.getTotalLength ().ToString ());
					((Gtk.TreeStore)treeModel).SetValue (iter, 3, (100*downloaded)/queue.getTotalLength ());
					((Gtk.TreeStore)treeModel).SetValue (iter, 4, MainWindow.getTime (queue.getTotalLength ().value - downloaded.value, speed.value));
					((Gtk.TreeStore)treeModel).SetValue (iter, 5, speed.ToString ());
					((Gtk.TreeStore)treeModel).SetValue (iter, 6, queue.getTotalParts ().ToString ());
				}
				else if (queue.status == libDownload.DOWNLOAD_STATUS.MERGING)
				{
					((Gtk.TreeStore)treeModel).SetValue (iter, 3, (float)100.0);
					((Gtk.TreeStore)treeModel).SetValue (iter, 4, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 5, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 6, queue.getTotalParts ().ToString ());
				}
				else if (queue.status == libDownload.DOWNLOAD_STATUS.DOWNLOADED)
				{
					((Gtk.TreeStore)treeModel).SetValue (iter, 3, (float)100.0);
					((Gtk.TreeStore)treeModel).SetValue (iter, 4, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 5, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 6, queue.getTotalParts ().ToString ());
				}
			}

			else if (o is DMDownload)
			{
				DMDownload dwnld = (DMDownload)o;
				TreeIter iter;
				treeModel.GetIter (
					out iter,
					dwnld.queue.listDMRowReference [dwnld.queue.listDownloads.IndexOf (dwnld)].Path);

				if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADING)
				{
					((Gtk.TreeStore)treeModel).SetValue (iter, 1, dwnld.download.localPath.Substring (dwnld.download.localPath.LastIndexOf ('/') +1));
					((Gtk.TreeStore)treeModel).SetValue (iter, 2, dwnld.download.length.ToString ());
					((Gtk.TreeStore)treeModel).SetValue (iter, 3, (100*downloaded.value)/(float)dwnld.download.length.value);
					((Gtk.TreeStore)treeModel).SetValue (iter, 4, MainWindow.getTime (dwnld.download.length.value - downloaded.value, speed.value));
					((Gtk.TreeStore)treeModel).SetValue (iter, 5, speed.ToString ());
					((Gtk.TreeStore)treeModel).SetValue (iter, 6, dwnld.download.parts.ToString ());
				}
				else if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.MERGING)
				{
					((Gtk.TreeStore)treeModel).SetValue (iter, 3, (float)100.0);
					((Gtk.TreeStore)treeModel).SetValue (iter, 4, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 5, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 6, dwnld.download.parts.ToString ());
				}
				else if (dwnld.download.status == libDownload.DOWNLOAD_STATUS.DOWNLOADED)
				{
					((Gtk.TreeStore)treeModel).SetValue (iter, 3, (float)100.0);
					((Gtk.TreeStore)treeModel).SetValue (iter, 4, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 5, "");
					((Gtk.TreeStore)treeModel).SetValue (iter, 6, dwnld.download.parts.ToString ());
				}
			}
		}
	}
}

