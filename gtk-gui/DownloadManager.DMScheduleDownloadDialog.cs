
// This file has been generated by the GUI designer. Do not modify.
namespace DownloadManager
{
	public partial class DMScheduleDownloadDialog
	{
		private global::Gtk.HBox hbox2;
		private global::Gtk.Label label5;
		private global::Gtk.ComboBox cbAction;
		private global::Gtk.Frame frame1;
		private global::Gtk.Alignment GtkAlignment2;
		private global::Gtk.Table table1;
		private global::Gtk.Label label1;
		private global::Gtk.Label label2;
		private global::Gtk.Label label3;
		private global::Gtk.Label label4;
		private global::Gtk.SpinButton spinbutton1;
		private global::Gtk.SpinButton spinbutton3;
		private global::Gtk.SpinButton spinbutton4;
		private global::Gtk.Label GtkLabel2;
		private global::Gtk.Frame frame2;
		private global::Gtk.Alignment GtkAlignment3;
		private global::Gtk.VBox vbox5;
		private global::Gtk.HBox hbox1;
		private global::Gtk.RadioButton rbOnce;
		private global::Gtk.Calendar calendar;
		private global::Gtk.HBox hbox3;
		private global::Gtk.RadioButton rbOnDays;
		private global::Gtk.ComboBox cbDays;
		private global::Gtk.RadioButton rbStartup;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget DownloadManager.DMScheduleDownloadDialog
			this.Name = "DownloadManager.DMScheduleDownloadDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child DownloadManager.DMScheduleDownloadDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Action");
			this.hbox2.Add (this.label5);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label5]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.cbAction = global::Gtk.ComboBox.NewText ();
			this.cbAction.Name = "cbAction";
			this.hbox2.Add (this.cbAction);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.cbAction]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			w1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox2]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(1)), ((uint)(7)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Start");
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.Xalign = 0F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Hour");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w6.LeftAttach = ((uint)(2));
			w6.RightAttach = ((uint)(3));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xalign = 0F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Minute");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w7.LeftAttach = ((uint)(4));
			w7.RightAttach = ((uint)(5));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.Xalign = 0F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Seconds");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w8.LeftAttach = ((uint)(6));
			w8.RightAttach = ((uint)(7));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton1 = new global::Gtk.SpinButton (0, 12, 1);
			this.spinbutton1.CanFocus = true;
			this.spinbutton1.Name = "spinbutton1";
			this.spinbutton1.Adjustment.PageIncrement = 1;
			this.spinbutton1.ClimbRate = 1;
			this.spinbutton1.Numeric = true;
			this.table1.Add (this.spinbutton1);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinbutton1]));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton3 = new global::Gtk.SpinButton (0, 60, 1);
			this.spinbutton3.CanFocus = true;
			this.spinbutton3.Name = "spinbutton3";
			this.spinbutton3.Adjustment.PageIncrement = 5;
			this.spinbutton3.ClimbRate = 1;
			this.spinbutton3.Numeric = true;
			this.table1.Add (this.spinbutton3);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinbutton3]));
			w10.LeftAttach = ((uint)(3));
			w10.RightAttach = ((uint)(4));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton4 = new global::Gtk.SpinButton (0, 60, 1);
			this.spinbutton4.CanFocus = true;
			this.spinbutton4.Name = "spinbutton4";
			this.spinbutton4.Adjustment.PageIncrement = 5;
			this.spinbutton4.ClimbRate = 1;
			this.spinbutton4.Numeric = true;
			this.table1.Add (this.spinbutton4);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinbutton4]));
			w11.LeftAttach = ((uint)(5));
			w11.RightAttach = ((uint)(6));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			this.GtkAlignment2.Add (this.table1);
			this.frame1.Add (this.GtkAlignment2);
			this.GtkLabel2 = new global::Gtk.Label ();
			this.GtkLabel2.Name = "GtkLabel2";
			this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString ("Time");
			this.GtkLabel2.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel2;
			w1.Add (this.frame1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(w1 [this.frame1]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment3 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment3.Name = "GtkAlignment3";
			this.GtkAlignment3.LeftPadding = ((uint)(12));
			// Container child GtkAlignment3.Gtk.Container+ContainerChild
			this.vbox5 = new global::Gtk.VBox ();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.rbOnce = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Once"));
			this.rbOnce.CanFocus = true;
			this.rbOnce.Name = "rbOnce";
			this.rbOnce.DrawIndicator = true;
			this.rbOnce.UseUnderline = true;
			this.rbOnce.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.hbox1.Add (this.rbOnce);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.rbOnce]));
			w15.Position = 0;
			this.vbox5.Add (this.hbox1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.hbox1]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.calendar = new global::Gtk.Calendar ();
			this.calendar.CanFocus = true;
			this.calendar.Name = "calendar";
			this.calendar.DisplayOptions = ((global::Gtk.CalendarDisplayOptions)(35));
			this.vbox5.Add (this.calendar);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.calendar]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.rbOnDays = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("On Days"));
			this.rbOnDays.CanFocus = true;
			this.rbOnDays.Name = "rbOnDays";
			this.rbOnDays.DrawIndicator = true;
			this.rbOnDays.UseUnderline = true;
			this.rbOnDays.Group = this.rbOnce.Group;
			this.hbox3.Add (this.rbOnDays);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.rbOnDays]));
			w18.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.cbDays = global::Gtk.ComboBox.NewText ();
			this.cbDays.Name = "cbDays";
			this.hbox3.Add (this.cbDays);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.cbDays]));
			w19.Position = 1;
			w19.Expand = false;
			w19.Fill = false;
			this.vbox5.Add (this.hbox3);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.hbox3]));
			w20.Position = 2;
			w20.Expand = false;
			w20.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.rbStartup = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("At Next Startup"));
			this.rbStartup.CanFocus = true;
			this.rbStartup.Name = "rbStartup";
			this.rbStartup.DrawIndicator = true;
			this.rbStartup.UseUnderline = true;
			this.rbStartup.Group = this.rbOnce.Group;
			this.vbox5.Add (this.rbStartup);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.rbStartup]));
			w21.Position = 3;
			w21.Expand = false;
			w21.Fill = false;
			this.GtkAlignment3.Add (this.vbox5);
			this.frame2.Add (this.GtkAlignment3);
			w1.Add (this.frame2);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(w1 [this.frame2]));
			w24.Position = 2;
			w24.Expand = false;
			w24.Fill = false;
			// Internal child DownloadManager.DMScheduleDownloadDialog.ActionArea
			global::Gtk.HButtonBox w25 = this.ActionArea;
			w25.Name = "dialog1_ActionArea";
			w25.Spacing = 10;
			w25.BorderWidth = ((uint)(5));
			w25.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w26 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w25 [this.buttonCancel]));
			w26.Expand = false;
			w26.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w27 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w25 [this.buttonOk]));
			w27.Position = 1;
			w27.Expand = false;
			w27.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 441;
			this.DefaultHeight = 552;
			this.Show ();
			this.rbOnce.Toggled += new global::System.EventHandler (this.OnRbOnceToggled);
			this.rbOnDays.Toggled += new global::System.EventHandler (this.OnRbOnDaysToggled);
			this.rbStartup.Toggled += new global::System.EventHandler (this.OnRbStartupToggled);
		}
	}
}
