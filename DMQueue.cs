using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using libDownload;

namespace DownloadManager
{
	public class DMQueue
	{
		private DOWNLOAD_STATUS _status;
		private Length _downloaded;

		public Gtk.TreeRowReference treeRowReference;
		public List<DMDownload> listDownloads {get; private set;}
		public List<Gtk.TreeRowReference> listDMRowReference {get; private set;}
		public string name{get; private set;}
		public DOWNLOAD_STATUS status
		{
			get
			{
				return _status;
			}

			private set
			{
				_status = value;
				if (onQueueStatusChanged != null)
					onQueueStatusChanged (this);
			}
		}

		public delegate void OnQueueStatusChanged (DMQueue queue);
		public OnQueueStatusChanged onQueueStatusChanged {get; set;}

		public DMQueue (string _name)
		{
			name = _name;
			listDMRowReference = new List<Gtk.TreeRowReference> ();
		}

		public void addDownload (DMDownload dmld)
		{
			listDownloads.Add (dmld);
			dmld.queue = this;
		}

		public void start ()
		{
			for (int i = 0; i < listDownloads.Count; i++)
				startDownload (i);
		}

		public void stop ()
		{
			for (int i = 0; i < listDownloads.Count; i++)
				stopDownload (i);
		}

		public void cancel ()
		{
			for (int i = 0; i < listDownloads.Count; i++)
				listDownloads [i].download.start ();
		}

		public void cancel (int index)
		{
			listDownloads [index].download.start ();
		}

		public void startDownload (int index)
		{
			listDownloads [index].download.start ();
		}

		public void stopDownload (int index)
		{
			listDownloads [index].download.stop ();
		}

		public float getDownloadProgress (int index)
		{
			return ((long)100*listDownloads [index].download.getDownloaded ())/listDownloads [index].download.length;
		}

		public Length getDownloadLength (int index)
		{
			return listDownloads [index].download.length;
		}

		public Speed getDownloadSpeed (int index)
		{
			return new Speed (listDownloads [index].download.getSpeed ().value*2);
		}

		public float getTotalProgress ()
		{
			Length downloaded = new Length (0), length = new Length (0);

			foreach (DMDownload d in listDownloads)
			{
				downloaded += d.download.getDownloaded ();
				length += d.download.length;
			}
			return 100*downloaded/length;
		}

		public Length getTotalLength ()
		{
			Length length = new Length (0);

			foreach (DMDownload d in listDownloads)
			{
				length += d.download.length;
			}

			return length;
		}

		public Speed getTotalSpeed ()
		{
			Length prev_downloaded = _downloaded;
			_downloaded = getTotalDownloaded ();
			return new Speed (_downloaded.value - prev_downloaded.value);
		}

		public Length getTotalDownloaded ()
		{
			Length length = new Length (0);

			foreach (DMDownload d in listDownloads)
			{
				length += d.download.getDownloaded ();
			}

			return length;
		}

		public void OnDownloadStatusChanged (DMDownload dmld)
		{
			DOWNLOAD_STATUS _status = dmld.download.status;
			bool allStatusSame = true;

			foreach (DMDownload d in listDownloads)
			{
				if (d.download.status != _status)
				{
					allStatusSame = false;
					return;
				}
			}
			if (allStatusSame)
			{
			    status = _status;
				return;
			}
		}

		public short getTotalParts ()
		{
			short parts = 0;
			foreach (DMDownload d in listDownloads)
				parts += d.download.parts;

			return parts;
		}

		public override string ToString ()
		{
			string s = "<queue>\n";
			s += "\t<name>\n\t\t" + name + "\n\t</name>";
			foreach (DMDownload download in listDownloads)
			{
				s += "\t<dwnld>\n\t\t" + download.download.localPath + "\n\t</dwnld>\n";
			}

			s += "\t<status>\n\t\t" + status.ToString () + "\n\t</status>\n";

			s += "</queue>\n";
			return s;
		}

		public void loadQueueFromXML (string xml, Dictionary<string, DMDownload> dict)
		{
			MatchCollection mc = Regex.Matches (xml, ".+", RegexOptions.None);
			for (int i = 0; i < mc.Count; i++)
			{
				Match m = mc [i];
				if (m.Value.Contains ("<name>"))
				{
					i += 1;
					name = mc [i].Value.Trim ();
					i += 1;
				}

				else if (m.Value.Contains ("<dwnld>"))
				{
					i += 1;
					string localPath = mc [i].Value.Trim ();
					listDownloads.Add (dict [localPath]);
					i += 1;
				}
			}
		}
	}
}

