
// This file has been generated by the GUI designer. Do not modify.
namespace DownloadManager
{
	public partial class NewDialog
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Label label1;
		private global::Gtk.Entry entryAddress;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Label label2;
		private global::Gtk.Entry entrySaveTo;
		private global::Gtk.Button btnSelectFile;
		private global::Gtk.CheckButton chkGenFilename;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Label label6;
		private global::Gtk.Entry entryFilename;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label label5;
		private global::Gtk.ComboBox cbCategory;
		private global::Gtk.CheckButton chkAuthenticate;
		private global::Gtk.Frame frameAuthenticate;
		private global::Gtk.Alignment GtkAlignment2;
		private global::Gtk.VBox vbox3;
		private global::Gtk.Table table1;
		private global::Gtk.Entry entryPassword;
		private global::Gtk.Entry entryUserID;
		private global::Gtk.Label label3;
		private global::Gtk.Label label4;
		private global::Gtk.Frame frame2;
		private global::Gtk.Alignment GtkAlignment3;
		private global::Gtk.VBox vbox4;
		private global::Gtk.RadioButton rbNow;
		private global::Gtk.RadioButton rbLater;
		private global::Gtk.RadioButton rbSchedule;
		private global::Gtk.Label GtkLabel5;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget DownloadManager.NewDialog
			this.Name = "DownloadManager.NewDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Create New Download");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child DownloadManager.NewDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Address");
			this.hbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.label1]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryAddress = new global::Gtk.Entry ();
			this.entryAddress.CanFocus = true;
			this.entryAddress.Name = "entryAddress";
			this.entryAddress.Text = global::Mono.Unix.Catalog.GetString ("http://kojipkgs.fedoraproject.org//packages/epiphany/3.10.3/1.fc20/x86_64/epiphany-3.10.3-1.fc20.x86_64.rpm");
			this.entryAddress.IsEditable = true;
			this.entryAddress.InvisibleChar = '•';
			this.hbox1.Add (this.entryAddress);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryAddress]));
			w3.Position = 1;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Save To:");
			this.hbox2.Add (this.label2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label2]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.entrySaveTo = new global::Gtk.Entry ();
			this.entrySaveTo.CanFocus = true;
			this.entrySaveTo.Name = "entrySaveTo";
			this.entrySaveTo.IsEditable = true;
			this.entrySaveTo.InvisibleChar = '•';
			this.hbox2.Add (this.entrySaveTo);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.entrySaveTo]));
			w6.Position = 1;
			// Container child hbox2.Gtk.Box+BoxChild
			this.btnSelectFile = new global::Gtk.Button ();
			this.btnSelectFile.CanFocus = true;
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.UseUnderline = true;
			this.btnSelectFile.Label = global::Mono.Unix.Catalog.GetString ("...");
			this.hbox2.Add (this.btnSelectFile);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.btnSelectFile]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			this.vbox2.Add (this.hbox2);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox2]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.chkGenFilename = new global::Gtk.CheckButton ();
			this.chkGenFilename.CanFocus = true;
			this.chkGenFilename.Name = "chkGenFilename";
			this.chkGenFilename.Label = global::Mono.Unix.Catalog.GetString ("Generate Filename Automatically");
			this.chkGenFilename.DrawIndicator = true;
			this.chkGenFilename.UseUnderline = true;
			this.vbox2.Add (this.chkGenFilename);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.chkGenFilename]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Filename");
			this.hbox4.Add (this.label6);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.label6]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.entryFilename = new global::Gtk.Entry ();
			this.entryFilename.CanFocus = true;
			this.entryFilename.Name = "entryFilename";
			this.entryFilename.IsEditable = true;
			this.entryFilename.InvisibleChar = '•';
			this.hbox4.Add (this.entryFilename);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.entryFilename]));
			w11.Position = 1;
			this.vbox2.Add (this.hbox4);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox4]));
			w12.Position = 3;
			w12.Expand = false;
			w12.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Category");
			this.hbox3.Add (this.label5);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label5]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.cbCategory = global::Gtk.ComboBox.NewText ();
			this.cbCategory.Name = "cbCategory";
			this.hbox3.Add (this.cbCategory);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.cbCategory]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			this.vbox2.Add (this.hbox3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox3]));
			w15.Position = 4;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.chkAuthenticate = new global::Gtk.CheckButton ();
			this.chkAuthenticate.CanFocus = true;
			this.chkAuthenticate.Name = "chkAuthenticate";
			this.chkAuthenticate.Label = global::Mono.Unix.Catalog.GetString ("Authenticate on Server");
			this.chkAuthenticate.DrawIndicator = true;
			this.chkAuthenticate.UseUnderline = true;
			this.vbox2.Add (this.chkAuthenticate);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.chkAuthenticate]));
			w16.Position = 5;
			w16.Expand = false;
			w16.Fill = false;
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
			this.table1 = new global::Gtk.Table (((uint)(2)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(8));
			// Container child table1.Gtk.Table+TableChild
			this.entryPassword = new global::Gtk.Entry ();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.InvisibleChar = '•';
			this.table1.Add (this.entryPassword);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryPassword]));
			w17.TopAttach = ((uint)(1));
			w17.BottomAttach = ((uint)(2));
			w17.LeftAttach = ((uint)(1));
			w17.RightAttach = ((uint)(2));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryUserID = new global::Gtk.Entry ();
			this.entryUserID.CanFocus = true;
			this.entryUserID.Name = "entryUserID";
			this.entryUserID.IsEditable = true;
			this.entryUserID.InvisibleChar = '•';
			this.table1.Add (this.entryUserID);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryUserID]));
			w18.LeftAttach = ((uint)(1));
			w18.RightAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(0));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xalign = 0F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("User ID");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Password");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w20.TopAttach = ((uint)(1));
			w20.BottomAttach = ((uint)(2));
			w20.XOptions = ((global::Gtk.AttachOptions)(0));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add (this.table1);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.table1]));
			w21.Position = 0;
			w21.Expand = false;
			w21.Fill = false;
			this.GtkAlignment2.Add (this.vbox3);
			this.frameAuthenticate.Add (this.GtkAlignment2);
			this.vbox2.Add (this.frameAuthenticate);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.frameAuthenticate]));
			w24.Position = 6;
			w24.Expand = false;
			w24.Fill = false;
			w1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox2]));
			w25.Position = 0;
			w25.Expand = false;
			w25.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
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
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.rbNow]));
			w26.Position = 0;
			w26.Expand = false;
			w26.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.rbLater = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Later"));
			this.rbLater.CanFocus = true;
			this.rbLater.Name = "rbLater";
			this.rbLater.DrawIndicator = true;
			this.rbLater.UseUnderline = true;
			this.rbLater.Group = this.rbNow.Group;
			this.vbox4.Add (this.rbLater);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.rbLater]));
			w27.Position = 1;
			w27.Expand = false;
			w27.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.rbSchedule = new global::Gtk.RadioButton (global::Mono.Unix.Catalog.GetString ("Schedule"));
			this.rbSchedule.CanFocus = true;
			this.rbSchedule.Name = "rbSchedule";
			this.rbSchedule.DrawIndicator = true;
			this.rbSchedule.UseUnderline = true;
			this.rbSchedule.Group = this.rbNow.Group;
			this.vbox4.Add (this.rbSchedule);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.rbSchedule]));
			w28.Position = 2;
			w28.Expand = false;
			w28.Fill = false;
			this.GtkAlignment3.Add (this.vbox4);
			this.frame2.Add (this.GtkAlignment3);
			this.GtkLabel5 = new global::Gtk.Label ();
			this.GtkLabel5.Name = "GtkLabel5";
			this.GtkLabel5.LabelProp = global::Mono.Unix.Catalog.GetString ("Start");
			this.GtkLabel5.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel5;
			w1.Add (this.frame2);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(w1 [this.frame2]));
			w31.Position = 1;
			w31.Expand = false;
			w31.Fill = false;
			// Internal child DownloadManager.NewDialog.ActionArea
			global::Gtk.HButtonBox w32 = this.ActionArea;
			w32.Name = "dialog1_ActionArea";
			w32.Spacing = 10;
			w32.BorderWidth = ((uint)(5));
			w32.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w33 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w32 [this.buttonCancel]));
			w33.Expand = false;
			w33.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Label = global::Mono.Unix.Catalog.GetString ("Add Download");
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w34 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w32 [this.buttonOk]));
			w34.Position = 1;
			w34.Expand = false;
			w34.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 455;
			this.DefaultHeight = 474;
			this.Show ();
			this.entryAddress.Changed += new global::System.EventHandler (this.OnEntryAddressChanged);
			this.entrySaveTo.Changed += new global::System.EventHandler (this.OnEntrySaveToChanged);
			this.btnSelectFile.Clicked += new global::System.EventHandler (this.btnSelectFileClicked);
			this.chkGenFilename.Toggled += new global::System.EventHandler (this.chkGenFilenameToggled);
			this.cbCategory.Changed += new global::System.EventHandler (this.OncbCategoryChanged);
			this.chkAuthenticate.Toggled += new global::System.EventHandler (this.chkAuthenticateToggled);
			this.rbNow.Toggled += new global::System.EventHandler (this.OnRbNowToggled);
			this.rbLater.Toggled += new global::System.EventHandler (this.OnRbLaterToggled);
			this.rbSchedule.Toggled += new global::System.EventHandler (this.OnRbScheduleToggled);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnAddDownloadClicked);
		}
	}
}
