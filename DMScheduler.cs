using System;
using System.Collections.Generic;

namespace DownloadManager
{
	public enum ScheduleType
	{
		Start,
		Pause,
		Resume,
		Restart
	}

	public class DMScheduledDownload
	{
		public DateTime time {get; private set;}
		DMDownload dmld;
		ScheduleType type;

		public DMScheduledDownload (DMDownload _dmld, DateTime _time, 
		                            ScheduleType _type)
		{
			time = _time;
			dmld = _dmld;
			type = _type;
		}

		public void perform ()
		{
			if (type == ScheduleType.Start)
			{
				MainWindow.main_instance.startDownload (dmld);
			}

			else if (type == ScheduleType.Pause)
				dmld.download.stop ();

			else if (type == ScheduleType.Restart)
			{
				dmld.download.cancel ();
				MainWindow.main_instance.startDownload (dmld);
			}

			else if (type == ScheduleType.Resume)
			{
				MainWindow.main_instance.resumeDownload (dmld);
			}
		}
	}

	public class DMScheduler
	{
		List<DMScheduledDownload> listScheduled;
		List<DMScheduledDownload> listPerformed;

		public DMScheduler ()
		{
			listScheduled = new List<DMScheduledDownload> ();
			listPerformed = new List<DMScheduledDownload> ();
		}

		public void scheduleDownloadAt (DMDownload dmld, DateTime time, 
		                                ScheduleType type)
		{
			int i;
			for (i = 0; i < listScheduled.Count && 
			     time < listScheduled [i].time; i++);

			listScheduled.Insert (i, new DMScheduledDownload (dmld, time, type));
		}

		public void check (DateTime time)
		{
			List<DMScheduledDownload> toRemove = new List<DMScheduledDownload> ();
			foreach (DMScheduledDownload dmsd in listScheduled)
			{
				if (time >= dmsd.time)
				{
					dmsd.perform ();
					toRemove.Add (dmsd);
				}
				else
				{
					break;
				}
			}

			foreach (DMScheduledDownload d in toRemove)
				listScheduled.Remove (d);
		}
	}
}

