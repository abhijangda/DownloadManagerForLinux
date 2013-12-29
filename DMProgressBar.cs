using System;

namespace DownloadManager
{
	[System.ComponentModel.ToolboxItem(true)]
	public class DMProgressBar : Gtk.ProgressBar
	{
		public DMProgressBar ()
		{

		}		

		public void setProgress (float _fraction)
		{
			Fraction = _fraction;
			string _str = (_fraction * 100).ToString ();
			if (_str.Contains ("."))
			    Text =  _str.Substring (0, _str.IndexOf (".") + 2) + "%";
			else
				Text =  _str + "%";
		}

		protected override bool OnButtonPressEvent (Gdk.EventButton ev)
		{
			// Insert button press handling code here.
			return base.OnButtonPressEvent (ev);
		}

		protected override bool OnExposeEvent (Gdk.EventExpose ev)
		{
			base.OnExposeEvent (ev);
			// Insert drawing code here.
			return true;
		}

		protected override void OnSizeAllocated (Gdk.Rectangle allocation)
		{
			base.OnSizeAllocated (allocation);
			// Insert layout code here.
		}

		protected override void OnSizeRequested (ref Gtk.Requisition requisition)
		{
			// Calculate desired size here.
			requisition.Height = 20;
			requisition.Width = 50;
		}
	}
}

