using System;
using System.Reflection;
using Gtk;

namespace DownloadManager
{
	public partial class DMScheduleDownloadDialog : Gtk.Dialog
	{
		public DMScheduleDownloadDialog ()
		{
			this.Build ();
			MemberInfo[] memberinfos = typeof (ScheduleOperation).GetMembers (BindingFlags.Public |
			                                                                  BindingFlags.Static);
			foreach (MemberInfo info in memberinfos)
			{
				cbAction.AppendText (info.Name);
			}
			cbAction.Active = 0;
			memberinfos = typeof (DayOfWeek).GetMembers (BindingFlags.Public |
			                                             BindingFlags.Static);
			foreach (MemberInfo info in memberinfos)
			{
				cbDays.AppendText (info.Name);
			}
			cbDays.Active = 0;
			GLib.Timeout.Add (1, delegate () {
				dateSelected ();
				return false;
			});
		}		

		protected void OnRbOnceToggled (object sender, EventArgs e)
		{
			dateSelected ();
		}

		protected void OnRbOnDaysToggled (object sender, EventArgs e)
		{
			dateSelected ();
		}

		protected void OnRbStartupToggled (object sender, EventArgs e)
		{
			dateSelected ();
		}

		private void dateSelected ()
		{
			calendar.Visible = rbOnce.Active;
			cbDays.Visible = rbOnDays.Active;
		}

		private ScheduleOperation getOperation ()
		{
			MemberInfo[] memberinfos = typeof (ScheduleOperation).GetMember (cbAction.ActiveText);
			return (ScheduleOperation)Enum.Parse (typeof (ScheduleOperation), memberinfos [0].Name);
		}

		public bool scheduleDownload (DMDownload dmld)
		{
			if (Run () == (int)ResponseType.Ok)
			{
				if (rbOnce.Active == true)
					MainWindow.main_instance.scheduler.scheduleDownloadAtTime (dmld, 
					                                                           calendar.Date, 
					                                                           getOperation ());
				else if (rbOnDays.Active == true)
				{
					DayOfWeek day = (DayOfWeek)Enum.Parse (typeof (DayOfWeek), cbDays.ActiveText);
					MainWindow.main_instance.scheduler.scheduleDownloadOnDay (dmld,
					                                                          day,
					                                                          getOperation ());
				}

				return true;
			}
			return false;
		}
	}
}