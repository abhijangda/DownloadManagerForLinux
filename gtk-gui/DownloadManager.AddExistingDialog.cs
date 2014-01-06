
// This file has been generated by the GUI designer. Do not modify.
namespace DownloadManager
{
	public partial class AddExistingDialog
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.Table table1;
		private global::Gtk.Entry entryAddress;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Entry entryFilePath;
		private global::Gtk.Button btnSelectFile;
		private global::Gtk.Label label1;
		private global::Gtk.Label label2;
		private global::Gtk.Label label3;
		private global::Gtk.SpinButton spinbutton;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label label5;
		private global::Gtk.ComboBox cbCategory;
		private global::Gtk.CheckButton chkAuthenticate;
		private global::Gtk.Frame frameAuthenticate;
		private global::Gtk.Alignment GtkAlignment2;
		private global::Gtk.VBox vbox3;
		private global::Gtk.Table table2;
		private global::Gtk.Entry entryPassword;
		private global::Gtk.Entry entryUserID;
		private global::Gtk.Label label4;
		private global::Gtk.Label label6;
		private global::Gtk.Frame frame2;
		private global::Gtk.Alignment GtkAlignment3;
		private global::Gtk.VBox vbox4;
		private global::Gtk.RadioButton rbNow;
		private global::Gtk.RadioButton rbLater;
		private global::Gtk.RadioButton rbSchedule;
		private global::Gtk.Label GtkLabel6;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonAdd;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget DownloadManager.AddExistingDialog
			this.Name = "DownloadManager.AddExistingDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child DownloadManager.AddExistingDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryAddress = new global::Gtk.Entry ();
			this.entryAddress.CanFocus = true;
			this.entryAddress.Name = "entryAddress";
			this.entryAddress.IsEditable = true;
			this.entryAddress.InvisibleChar = '•';
			this.table1.Add (this.entryAddress);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryAddress]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryFilePath = new global::Gtk.Entry ();
			this.entryFilePath.CanFocus = true;
			this.entryFilePath.Name = "entryFilePath";
			this.entryFilePath.IsEditable = true;
			this.entryFilePath.InvisibleChar = '•';
			this.hbox1.Add (this.entryFilePath);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryFilePath]));
			w3.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.btnSelectFile = new global::Gtk.Button ();
			this.btnSelectFile.CanFocus = true;
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.UseUnderline = true;
			this.btnSelectFile.Label = global::Mono.Unix.Catalog.GetString ("...");
			this.hbox1.Add (this.btnSelectFile);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.btnSelectFile]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.table1.Add (this.hbox1);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.hbox1]));
			w5.TopAttach = ((uint)(1));
			w5.BottomAttach = ((uint)(2));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Address");
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.Xalign = 0F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("File Path");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w7.TopAttach = ((uint)(1));
			w7.BottomAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xalign = 0F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Parts");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w8.TopAttach = ((uint)(2));
			w8.BottomAttach = ((uint)(3));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton = new global::Gtk.SpinButton (0, 100, 1);
			this.spinbutton.CanFocus = true;
			this.spinbutton.Name = "spinbutton";
			this.spinbutton.Adjustment.PageIncrement = 10;
			this.spinbutton.ClimbRate = 1;
			this.spinbutton.Numeric = true;
			this.table1.Add (this.spinbutton);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinbutton]));
			w9.TopAttach = ((uint)(2));
			w9.BottomAttach = ((uint)(3));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add (this.table1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.table1]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Category");
			this.hbox3.Add (this.label5);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label5]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.cbCategory = global::Gtk.ComboBox.NewText ();
			this.cbCategory.Name = "cbCategory";
			this.hbox3.Add (this.cbCategory);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.cbCategory]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			this.vbox2.Add (this.hbox3);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox3]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.chkAuthenticate = new global::Gtk.CheckButton ();
			this.chkAuthenticate.CanFocus = true;
			this.chkAuthenticate.Name = "chkAuthenticate";
			this.chkAuthenticate.Label = global::Mono.Unix.Catalog.GetString ("Authenticate on Server");
			this.chkAuthenticate.DrawIndicator = true;
			this.chkAuthenticate.UseUnderline = true;
			this.vbox2.Add (this.chkAuthenticate);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.chkAuthenticate]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frameAuthenticate = new global::Gtk.Frame ();
			this.frameAuthenticate.Name = "frameAuthenticate";
			this.frameAuthenticate.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frameAuthenticate.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.table2 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), false);
			this.table2.Name = "table2";
			this.table2.RowSpacing = ((uint)(6));
			this.table2.ColumnSpacing = ((uint)(8));
			// Container child table2.Gtk.Table+TableChild
			this.entryPassword = new global::Gtk.Entry ();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.InvisibleChar = '•';
			this.table2.Add (this.entryPassword);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table2 [this.entryPassword]));
			w15.TopAttach = ((uint)(1));
			w15.BottomAttach = ((uint)(2));
			w15.LeftAttach = ((uint)(1));
			w15.RightAttach = ((uint)(2));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.entryUserID = new global::Gtk.Entry ();
			this.entryUserID.CanFocus = true;
			this.entryUserID.Name = "entryUserID";
			this.entryUserID.IsEditable = true;
			this.entryUserID.InvisibleChar = '•';
			this.table2.Add (this.entryUserID);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table2 [this.entryUserID]));
			w16.LeftAttach = ((uint)(1));
			w16.RightAttach = ((uint)(2));
			w16.XOptions = ((global::Gtk.AttachOptions)(0));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Password");
			this.table2.Add (this.label4);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table2 [this.label4]));
			w17.TopAttach = ((uint)(1));
			w17.BottomAttach = ((uint)(2));
			w17.XOptions = ((global::Gtk.AttachOptions)(0));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.Xalign = 0F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("User ID");
			this.table2.Add (this.label6);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table2 [this.label6]));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add (this.table2);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.table2]));
			w19.Position = 0;
			w19.Expand = false;
			w19.Fill = false;
			this.GtkAlignment2.Add (this.vbox3);
			this.frameAuthenticate.Add (this.GtkAlignment2);
			this.vbox2.Add (this.frameAuthenticate);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.frameAuthenticate]));
			w22.Position = 3;
			w22.Expand = false;
			w22.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment3 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment3.Name = "GtkAlignment3";
			this.GtkAlignment3.LeftPadding = ((uint)(12));
			// Container child GtkAlignment3.Gtk.Container+ContainerChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.rbNow = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Now"));
			this.rbNow.CanFocus = true;
			this.rbNow.Name = "rbNow";
			this.rbNow.DrawIndicator = true;
			this.rbNow.UseUnderline = true;
			this.rbNow.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.vbox4.Add (this.rbNow);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.rbNow]));
			w23.Position = 0;
			w23.Expand = false;
			w23.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.rbLater = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Later"));
			this.rbLater.CanFocus = true;
			this.rbLater.Name = "rbLater";
			this.rbLater.DrawIndicator = true;
			this.rbLater.UseUnderline = true;
			this.rbLater.Group = this.rbNow.Group;
			this.vbox4.Add (this.rbLater);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.rbLater]));
			w24.Position = 1;
			w24.Expand = false;
			w24.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.rbSchedule = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Schedule"));
			this.rbSchedule.CanFocus = true;
			this.rbSchedule.Name = "rbSchedule";
			this.rbSchedule.DrawIndicator = true;
			this.rbSchedule.UseUnderline = true;
			this.rbSchedule.Group = this.rbNow.Group;
			this.vbox4.Add (this.rbSchedule);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.rbSchedule]));
			w25.Position = 2;
			w25.Expand = false;
			w25.Fill = false;
			this.GtkAlignment3.Add (this.vbox4);
			this.frame2.Add (this.GtkAlignment3);
			this.GtkLabel6 = new global::Gtk.Label ();
			this.GtkLabel6.Name = "GtkLabel6";
			this.GtkLabel6.LabelProp = global::Mono.Unix.Catalog.GetString ("Start");
			this.GtkLabel6.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel6;
			this.vbox2.Add (this.frame2);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.frame2]));
			w28.Position = 4;
			w28.Expand = false;
			w28.Fill = false;
			w1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox2]));
			w29.Position = 0;
			w29.Expand = false;
			w29.Fill = false;
			// Internal child DownloadManager.AddExistingDialog.ActionArea
			global::Gtk.HButtonBox w30 = this.ActionArea;
			w30.Name = "dialog1_ActionArea";
			w30.Spacing = 10;
			w30.BorderWidth = ((uint)(5));
			w30.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w31 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w30 [this.buttonCancel]));
			w31.Expand = false;
			w31.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonAdd = new global::Gtk.Button ();
			this.buttonAdd.CanDefault = true;
			this.buttonAdd.CanFocus = true;
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.UseUnderline = true;
			this.buttonAdd.Label = global::Mono.Unix.Catalog.GetString ("Add Download");
			this.AddActionWidget (this.buttonAdd, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w32 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w30 [this.buttonAdd]));
			w32.Position = 1;
			w32.Expand = false;
			w32.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 422;
			this.Show ();
			this.btnSelectFile.Clicked += new global::System.EventHandler (this.btnSelectFileClicked);
			this.chkAuthenticate.Toggled += new global::System.EventHandler (this.chkAuthenticateToggled);
			this.rbNow.Toggled += new global::System.EventHandler (this.OnRbNowToggled);
			this.rbLater.Toggled += new global::System.EventHandler (this.OnRbLaterToggled);
			this.rbSchedule.Toggled += new global::System.EventHandler (this.OnRbScheduleToggled);
			this.buttonAdd.Clicked += new global::System.EventHandler (this.OnbuttonAddClicked);
		}
	}
}
