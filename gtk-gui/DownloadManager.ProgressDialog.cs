
// This file has been generated by the GUI designer. Do not modify.
namespace DownloadManager
{
	public partial class ProgressDialog
	{
		private global::Gtk.Table table2;
		private global::Gtk.Label label19;
		private global::Gtk.Label label20;
		private global::Gtk.Label label21;
		private global::Gtk.Label label22;
		private global::Gtk.Label label23;
		private global::Gtk.Label label24;
		private global::Gtk.Label label25;
		private global::Gtk.Label label26;
		private global::Gtk.Table table4;
		private global::Gtk.Label label27;
		private global::Gtk.Label label28;
		private global::Gtk.Label label29;
		private global::Gtk.Label label30;
		private global::Gtk.Label label31;
		private global::Gtk.Label label32;
		private global::Gtk.CheckButton checkbutton2;
		private global::Gtk.CheckButton checkbutton3;
		private global::DownloadManager.DMProgressBar dmprogressbar1;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget DownloadManager.ProgressDialog
			this.Name = "DownloadManager.ProgressDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child DownloadManager.ProgressDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.Spacing = 2;
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table2 = new global::Gtk.Table (((uint)(4)), ((uint)(2)), false);
			this.table2.Name = "table2";
			this.table2.RowSpacing = ((uint)(6));
			this.table2.ColumnSpacing = ((uint)(6));
			// Container child table2.Gtk.Table+TableChild
			this.label19 = new global::Gtk.Label ();
			this.label19.Name = "label19";
			this.label19.Xalign = 0F;
			this.label19.LabelProp = global::Mono.Unix.Catalog.GetString ("File Size:");
			this.table2.Add (this.label19);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table2 [this.label19]));
			w2.TopAttach = ((uint)(2));
			w2.BottomAttach = ((uint)(3));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label20 = new global::Gtk.Label ();
			this.label20.Name = "label20";
			this.table2.Add (this.label20);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table2 [this.label20]));
			w3.TopAttach = ((uint)(2));
			w3.BottomAttach = ((uint)(3));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label21 = new global::Gtk.Label ();
			this.label21.Name = "label21";
			this.table2.Add (this.label21);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table2 [this.label21]));
			w4.TopAttach = ((uint)(3));
			w4.BottomAttach = ((uint)(4));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label22 = new global::Gtk.Label ();
			this.label22.Name = "label22";
			this.label22.Xalign = 0F;
			this.label22.LabelProp = global::Mono.Unix.Catalog.GetString ("Resume Support:");
			this.table2.Add (this.label22);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table2 [this.label22]));
			w5.TopAttach = ((uint)(3));
			w5.BottomAttach = ((uint)(4));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label23 = new global::Gtk.Label ();
			this.label23.Name = "label23";
			this.label23.Xalign = 0F;
			this.label23.LabelProp = global::Mono.Unix.Catalog.GetString ("Address:");
			this.table2.Add (this.label23);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table2 [this.label23]));
			w6.XOptions = ((global::Gtk.AttachOptions)(6));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label24 = new global::Gtk.Label ();
			this.label24.Name = "label24";
			this.table2.Add (this.label24);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table2 [this.label24]));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label25 = new global::Gtk.Label ();
			this.label25.Name = "label25";
			this.label25.Xalign = 0F;
			this.label25.LabelProp = global::Mono.Unix.Catalog.GetString ("Destination:");
			this.table2.Add (this.label25);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table2 [this.label25]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label26 = new global::Gtk.Label ();
			this.label26.Name = "label26";
			this.table2.Add (this.label26);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table2 [this.label26]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add (this.table2);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(w1 [this.table2]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table4 = new global::Gtk.Table (((uint)(3)), ((uint)(2)), false);
			this.table4.Name = "table4";
			this.table4.RowSpacing = ((uint)(6));
			this.table4.ColumnSpacing = ((uint)(6));
			// Container child table4.Gtk.Table+TableChild
			this.label27 = new global::Gtk.Label ();
			this.label27.Name = "label27";
			this.label27.Xalign = 0F;
			this.label27.LabelProp = global::Mono.Unix.Catalog.GetString ("Speed:");
			this.table4.Add (this.label27);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table4 [this.label27]));
			w11.TopAttach = ((uint)(1));
			w11.BottomAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label28 = new global::Gtk.Label ();
			this.label28.Name = "label28";
			this.table4.Add (this.label28);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table4 [this.label28]));
			w12.TopAttach = ((uint)(1));
			w12.BottomAttach = ((uint)(2));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label29 = new global::Gtk.Label ();
			this.label29.Name = "label29";
			this.table4.Add (this.label29);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table4 [this.label29]));
			w13.TopAttach = ((uint)(2));
			w13.BottomAttach = ((uint)(3));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label30 = new global::Gtk.Label ();
			this.label30.Name = "label30";
			this.label30.Xalign = 0F;
			this.label30.LabelProp = global::Mono.Unix.Catalog.GetString ("Time Left:");
			this.table4.Add (this.label30);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table4 [this.label30]));
			w14.TopAttach = ((uint)(2));
			w14.BottomAttach = ((uint)(3));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label31 = new global::Gtk.Label ();
			this.label31.Name = "label31";
			this.table4.Add (this.label31);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table4 [this.label31]));
			w15.LeftAttach = ((uint)(1));
			w15.RightAttach = ((uint)(2));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table4.Gtk.Table+TableChild
			this.label32 = new global::Gtk.Label ();
			this.label32.Name = "label32";
			this.label32.Xalign = 0F;
			this.label32.LabelProp = global::Mono.Unix.Catalog.GetString ("Downloaded:");
			this.table4.Add (this.label32);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table4 [this.label32]));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add (this.table4);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(w1 [this.table4]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.checkbutton2 = new global::Gtk.CheckButton ();
			this.checkbutton2.CanFocus = true;
			this.checkbutton2.Name = "checkbutton2";
			this.checkbutton2.Label = global::Mono.Unix.Catalog.GetString ("Launch download automatically");
			this.checkbutton2.DrawIndicator = true;
			this.checkbutton2.UseUnderline = true;
			w1.Add (this.checkbutton2);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(w1 [this.checkbutton2]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.checkbutton3 = new global::Gtk.CheckButton ();
			this.checkbutton3.CanFocus = true;
			this.checkbutton3.Name = "checkbutton3";
			this.checkbutton3.Label = global::Mono.Unix.Catalog.GetString ("Shutdown when done");
			this.checkbutton3.DrawIndicator = true;
			this.checkbutton3.UseUnderline = true;
			w1.Add (this.checkbutton3);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(w1 [this.checkbutton3]));
			w19.Position = 3;
			w19.Expand = false;
			w19.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.dmprogressbar1 = new global::DownloadManager.DMProgressBar ();
			this.dmprogressbar1.Name = "dmprogressbar1";
			w1.Add (this.dmprogressbar1);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(w1 [this.dmprogressbar1]));
			w20.Position = 4;
			w20.Expand = false;
			w20.Fill = false;
			// Internal child DownloadManager.ProgressDialog.ActionArea
			global::Gtk.HButtonBox w21 = this.ActionArea;
			w21.Name = "dialog1_ActionArea";
			w21.Spacing = 10;
			w21.BorderWidth = ((uint)(5));
			w21.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w22 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w21 [this.buttonCancel]));
			w22.Expand = false;
			w22.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w23 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w21 [this.buttonOk]));
			w23.Position = 1;
			w23.Expand = false;
			w23.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 566;
			this.DefaultHeight = 375;
			this.Show ();
		}
	}
}
