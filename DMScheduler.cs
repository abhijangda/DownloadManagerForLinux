using System;
using System.Collections.Generic;
using System.Threading;

namespace DownloadManager
{
	public enum ScheduleOperation
	{
		Start,
		Pause,
		Resume,
		Restart
	}

	public enum ScheduleType
	{
		Once,
		OnDays,
		AtNextStartup
	}

	public class ScheduleDateTime
	{
		public DateTime time {get; private set;}
		public DayOfWeek day {get; private set;}
		public ScheduleType scheduleType {get; private set;}

		public ScheduleDateTime (ScheduleType type, DateTime _time)
		{
			time = _time;
			scheduleType = type;
		}

		public ScheduleDateTime (ScheduleType type, DayOfWeek _day)
		{
			day = _day;
			scheduleType = type;
		}

		public bool check (DateTime _time)
		{
			if (scheduleType == ScheduleType.Once &&
			    time >= _time)
			{
				return true;
			}

			if (scheduleType == ScheduleType.OnDays &&
			    day == DateTime.Today.DayOfWeek)
			{
				return true;
			}

			return false;
		}
	}

	public class DMScheduledDownload
	{
		DMDownload dmld;
		ScheduleOperation operation;
		ScheduleType type;
		ScheduleDateTime dateTime;

		public DMScheduledDownload (DMDownload _dmld, 
		                            ScheduleOperation _operation, 
		                            ScheduleType type, 
		                            DateTime _dateTime)
		{
			dmld = _dmld;
			operation = _operation;
			dateTime = new ScheduleDateTime (type, _dateTime);
			this.type = type;
		}

		public DMScheduledDownload (DMDownload _dmld, 
		                            ScheduleOperation _operation, 
		                            ScheduleType type, 
		                            DayOfWeek day)
		{
			dmld = _dmld;
			operation = _operation;
			dateTime = new ScheduleDateTime (type, day);
			this.type = type;
		}
		public bool perform ()
		{
			if (dateTime.check (DateTime.Now) == false)
				return false;

			if (operation == ScheduleOperation.Start)
			{
				MainWindow.main_instance.startDownload (dmld);
			}

			else if (operation == ScheduleOperation.Pause)
			{
				dmld.download.stop ();
			}

			else if (operation == ScheduleOperation.Restart)
			{
				dmld.download.cancel ();
				MainWindow.main_instance.startDownload (dmld);
			}

			else if (operation == ScheduleOperation.Resume)
			{
				MainWindow.main_instance.resumeDownload (dmld);
			}

			return true;
		}
	}

	public class DMScheduler
	{
		List<DMScheduledDownload> listScheduled;
		List<DMScheduledDownload> listPerformed;
		bool stop;

		public DMScheduler ()
		{
			listScheduled = new List<DMScheduledDownload> ();
			listPerformed = new List<DMScheduledDownload> ();
			stop = false;
		}

		public void stopScheduler ()
		{
			stop = true;
		}

		public void scheduleDownloadAtTime (DMDownload dmld, DateTime time, 
		                                	ScheduleOperation op)
		{
			listScheduled.Add (new DMScheduledDownload (dmld, op, ScheduleType.Once, 
			                                             time));
		}

		public void scheduleDownloadOnDay (DMDownload dmld, DayOfWeek day,
		                                   ScheduleOperation op)
		{
			listScheduled.Add (new DMScheduledDownload (dmld, op, ScheduleType.Once, 
			                                            day));
		}
	
		public void startScheduler ()
		{
			Thread th = new Thread (threadFunc);
			th.Start ();
		}

		private void threadFunc ()
		{
			while (!stop)
			{
				check (DateTime.Now);
				Thread.Sleep (500);
			}
		}

		public void check (DateTime time)
		{
			List<DMScheduledDownload> toRemove = new List<DMScheduledDownload> ();
			foreach (DMScheduledDownload dmsd in listScheduled)
			{
				if (dmsd.perform ())
				{
					toRemove.Add (dmsd);
				}
			}

			foreach (DMScheduledDownload d in toRemove)
				listScheduled.Remove (d);
		}
	}
}

