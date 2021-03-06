
// This file has been generated by the GUI designer. Do not modify.
public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action FileAction;
	private global::Gtk.Action NewDownloadAction;
	private global::Gtk.Action AddExistingDownloadAction;
	private global::Gtk.Action toolbarNewDownload;
	private global::Gtk.Action toolbarAddDownload;
	private global::Gtk.Action toolbarStart;
	private global::Gtk.Action toolbarPause;
	private global::Gtk.Action toolbarCancel;
	private global::Gtk.Action toolbarRestart;
	private global::Gtk.Action preferencesAction;
	private global::Gtk.Action toolbarSpeedLow;
	private global::Gtk.Action toolbarSpeedMedium;
	private global::Gtk.Action toolbarSpeedFull;
	private global::Gtk.Action DownloadAction;
	private global::Gtk.Action StartAction;
	private global::Gtk.Action PauseAction;
	private global::Gtk.Action RestartAction;
	private global::Gtk.Action CancelAction;
	private global::Gtk.Action CreateQueueAction;
	private global::Gtk.Action StartQueueAction;
	private global::Gtk.Action StopQueueAction;
	private global::Gtk.Action DeleteQueueAction;
	private global::Gtk.Action ViewAction;
	private global::Gtk.Action ToolbarAction;
	private global::Gtk.ToggleAction NewDownloadAction1;
	private global::Gtk.ToggleAction AddExistingDownloadAction1;
	private global::Gtk.ToggleAction StartAction1;
	private global::Gtk.ToggleAction PauseAction1;
	private global::Gtk.ToggleAction CancelAction1;
	private global::Gtk.ToggleAction RestartAction1;
	private global::Gtk.ToggleAction SpeedLimitAction;
	private global::Gtk.Action ProgressWindowAction;
	private global::Gtk.ToggleAction PartsStatusAction;
	private global::Gtk.ToggleAction ProgressBarAction;
	private global::Gtk.ToggleAction StatusAction;
	private global::Gtk.ToggleAction TimeLeftAction;
	private global::Gtk.ToggleAction SpeedAction;
	private global::Gtk.ToggleAction StatusBarAction;
	private global::Gtk.ToggleAction DownloadedAction;
	private global::Gtk.ToggleAction SpeedAction1;
	private global::Gtk.ToggleAction DownloadsAction;
	private global::Gtk.ToggleAction TorrentsAction;
	private global::Gtk.ToggleAction FTPGetAction;
	private global::Gtk.ToggleAction FindAction;
	private global::Gtk.Action OptionsAction;
	private global::Gtk.Action SettingsAction;
	private global::Gtk.Action TrafficUsageModeAction;
	private global::Gtk.RadioAction LowSpeedAction;
	private global::Gtk.RadioAction MediumSpeedAction;
	private global::Gtk.RadioAction HighSpeedAction;
	private global::Gtk.Action toolbarStopAll;
	private global::Gtk.Action toolbarPauseAll;
	private global::Gtk.Action toolbarFind;
	private global::Gtk.Action toolbarStartQueue;
	private global::Gtk.Action toolbarStopQueue;
	private global::Gtk.Action toolbarCancelQueue;
	private global::Gtk.Action toolbarNewQueue;
	private global::Gtk.Action toolbarRestartQueue;
	private global::Gtk.Action CancelQueueAction;
	private global::Gtk.Action RestartQueueAction;
	private global::Gtk.Action FTPViewerAction;
	private global::Gtk.Action OpenAction;
	private global::Gtk.Action ReloadAction;
	private global::Gtk.Action DownloadAction1;
	private global::Gtk.Action DownloadAllAction;
	private global::Gtk.Action SelectAllAction;
	private global::Gtk.Action CopyFilnameToClipboardAction;
	private global::Gtk.Action CopyFilepathToClipboardAction;
	private global::Gtk.Action SearchAction;
	private global::Gtk.Action PropertiesAction;
	private global::Gtk.Action ArrangeItemsByNameAction;
	private global::Gtk.Action ArrangeItemsBySizeAction;
	private global::Gtk.Action ArrangeItemsByTypeAction;
	private global::Gtk.Action ArrangeItemsByDateAction;
	private global::Gtk.Action IconsAction;
	private global::Gtk.Action ListAction;
	private global::Gtk.VBox vbox1;
	private global::Gtk.MenuBar menubar1;
	private global::Gtk.Toolbar toolbar1;
	private global::Gtk.Notebook notebook;
	private global::Gtk.HPaned hpaned1;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::DownloadManager.DMCategoryTreeView dmCategoryTreeView;
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	private global::DownloadManager.DMDownloadTreeView dmDownloadTreeView;
	private global::Gtk.Label label1;
	private global::Gtk.ScrolledWindow GtkScrolledWindow2;
	private global::DownloadManager.DMQueueTreeView dmqueuetreeview;
	private global::Gtk.Label label2;
	private global::Gtk.Label label3;
	private global::DownloadManager.FTPGetWidget ftpgetwidget1;
	private global::Gtk.Label label4;
	private global::Gtk.Label label5;
	private global::Gtk.Statusbar statusbar1;
	private global::Gtk.Label lblDownloaded;
	private global::Gtk.VSeparator vseparator1;
	private global::Gtk.Label lblSpeed;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.NewDownloadAction = new global::Gtk.Action ("NewDownloadAction", global::Mono.Unix.Catalog.GetString ("New Download"), null, null);
		this.NewDownloadAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("New Download");
		w1.Add (this.NewDownloadAction, null);
		this.AddExistingDownloadAction = new global::Gtk.Action ("AddExistingDownloadAction", global::Mono.Unix.Catalog.GetString ("Add Existing Download"), null, null);
		this.AddExistingDownloadAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Add Existing Download");
		w1.Add (this.AddExistingDownloadAction, null);
		this.toolbarNewDownload = new global::Gtk.Action ("toolbarNewDownload", null, null, "gtk-new");
		w1.Add (this.toolbarNewDownload, null);
		this.toolbarAddDownload = new global::Gtk.Action ("toolbarAddDownload", null, null, "gtk-open");
		w1.Add (this.toolbarAddDownload, null);
		this.toolbarStart = new global::Gtk.Action ("toolbarStart", null, null, "gtk-media-play");
		w1.Add (this.toolbarStart, null);
		this.toolbarPause = new global::Gtk.Action ("toolbarPause", null, null, "gtk-media-pause");
		w1.Add (this.toolbarPause, null);
		this.toolbarCancel = new global::Gtk.Action ("toolbarCancel", null, null, "gtk-stop");
		w1.Add (this.toolbarCancel, null);
		this.toolbarRestart = new global::Gtk.Action ("toolbarRestart", null, null, "gtk-refresh");
		w1.Add (this.toolbarRestart, null);
		this.preferencesAction = new global::Gtk.Action ("preferencesAction", null, null, "gtk-preferences");
		w1.Add (this.preferencesAction, null);
		this.toolbarSpeedLow = new global::Gtk.Action ("toolbarSpeedLow", null, null, null);
		w1.Add (this.toolbarSpeedLow, null);
		this.toolbarSpeedMedium = new global::Gtk.Action ("toolbarSpeedMedium", null, null, null);
		w1.Add (this.toolbarSpeedMedium, null);
		this.toolbarSpeedFull = new global::Gtk.Action ("toolbarSpeedFull", null, null, null);
		w1.Add (this.toolbarSpeedFull, null);
		this.DownloadAction = new global::Gtk.Action ("DownloadAction", global::Mono.Unix.Catalog.GetString ("Download"), null, null);
		this.DownloadAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Download");
		w1.Add (this.DownloadAction, null);
		this.StartAction = new global::Gtk.Action ("StartAction", global::Mono.Unix.Catalog.GetString ("Start"), null, null);
		this.StartAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Start");
		w1.Add (this.StartAction, null);
		this.PauseAction = new global::Gtk.Action ("PauseAction", global::Mono.Unix.Catalog.GetString ("Pause"), null, null);
		this.PauseAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Pause");
		w1.Add (this.PauseAction, null);
		this.RestartAction = new global::Gtk.Action ("RestartAction", global::Mono.Unix.Catalog.GetString ("Restart"), null, null);
		this.RestartAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Restart");
		w1.Add (this.RestartAction, null);
		this.CancelAction = new global::Gtk.Action ("CancelAction", global::Mono.Unix.Catalog.GetString ("Cancel"), null, null);
		this.CancelAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Cancel");
		w1.Add (this.CancelAction, null);
		this.CreateQueueAction = new global::Gtk.Action ("CreateQueueAction", global::Mono.Unix.Catalog.GetString ("Create Queue"), null, null);
		this.CreateQueueAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Create Queue Download");
		w1.Add (this.CreateQueueAction, null);
		this.StartQueueAction = new global::Gtk.Action ("StartQueueAction", global::Mono.Unix.Catalog.GetString ("Start Queue"), null, null);
		this.StartQueueAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Start Queue");
		w1.Add (this.StartQueueAction, null);
		this.StopQueueAction = new global::Gtk.Action ("StopQueueAction", global::Mono.Unix.Catalog.GetString ("Stop Queue"), null, null);
		this.StopQueueAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Stop Queue");
		w1.Add (this.StopQueueAction, null);
		this.DeleteQueueAction = new global::Gtk.Action ("DeleteQueueAction", global::Mono.Unix.Catalog.GetString ("Delete Queue"), null, null);
		this.DeleteQueueAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Delete Queue");
		w1.Add (this.DeleteQueueAction, null);
		this.ViewAction = new global::Gtk.Action ("ViewAction", global::Mono.Unix.Catalog.GetString ("View"), null, null);
		this.ViewAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("View");
		w1.Add (this.ViewAction, null);
		this.ToolbarAction = new global::Gtk.Action ("ToolbarAction", global::Mono.Unix.Catalog.GetString ("Toolbar"), null, null);
		this.ToolbarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Toolbar");
		w1.Add (this.ToolbarAction, null);
		this.NewDownloadAction1 = new global::Gtk.ToggleAction ("NewDownloadAction1", global::Mono.Unix.Catalog.GetString ("New Download"), null, null);
		this.NewDownloadAction1.Active = true;
		this.NewDownloadAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("New Download");
		w1.Add (this.NewDownloadAction1, null);
		this.AddExistingDownloadAction1 = new global::Gtk.ToggleAction ("AddExistingDownloadAction1", global::Mono.Unix.Catalog.GetString ("Add Existing Download"), null, null);
		this.AddExistingDownloadAction1.Active = true;
		this.AddExistingDownloadAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Add Existing Download");
		w1.Add (this.AddExistingDownloadAction1, null);
		this.StartAction1 = new global::Gtk.ToggleAction ("StartAction1", global::Mono.Unix.Catalog.GetString ("Start "), null, null);
		this.StartAction1.Active = true;
		this.StartAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Start ");
		w1.Add (this.StartAction1, null);
		this.PauseAction1 = new global::Gtk.ToggleAction ("PauseAction1", global::Mono.Unix.Catalog.GetString ("Pause"), null, null);
		this.PauseAction1.Active = true;
		this.PauseAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Pause");
		w1.Add (this.PauseAction1, null);
		this.CancelAction1 = new global::Gtk.ToggleAction ("CancelAction1", global::Mono.Unix.Catalog.GetString ("Cancel"), null, null);
		this.CancelAction1.Active = true;
		this.CancelAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Cancel");
		w1.Add (this.CancelAction1, null);
		this.RestartAction1 = new global::Gtk.ToggleAction ("RestartAction1", global::Mono.Unix.Catalog.GetString ("Restart"), null, null);
		this.RestartAction1.Active = true;
		this.RestartAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Restart");
		w1.Add (this.RestartAction1, null);
		this.SpeedLimitAction = new global::Gtk.ToggleAction ("SpeedLimitAction", global::Mono.Unix.Catalog.GetString ("Speed Limit"), null, null);
		this.SpeedLimitAction.Active = true;
		this.SpeedLimitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Speed Limit");
		w1.Add (this.SpeedLimitAction, null);
		this.ProgressWindowAction = new global::Gtk.Action ("ProgressWindowAction", global::Mono.Unix.Catalog.GetString ("Progress Window"), null, null);
		this.ProgressWindowAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Progress Window");
		w1.Add (this.ProgressWindowAction, null);
		this.PartsStatusAction = new global::Gtk.ToggleAction ("PartsStatusAction", global::Mono.Unix.Catalog.GetString ("Parts Status"), null, null);
		this.PartsStatusAction.Active = true;
		this.PartsStatusAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Parts Status");
		w1.Add (this.PartsStatusAction, null);
		this.ProgressBarAction = new global::Gtk.ToggleAction ("ProgressBarAction", global::Mono.Unix.Catalog.GetString ("Progress Bar"), null, null);
		this.ProgressBarAction.Active = true;
		this.ProgressBarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Progress Bar");
		w1.Add (this.ProgressBarAction, null);
		this.StatusAction = new global::Gtk.ToggleAction ("StatusAction", global::Mono.Unix.Catalog.GetString ("Status"), null, null);
		this.StatusAction.Active = true;
		this.StatusAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Status");
		w1.Add (this.StatusAction, null);
		this.TimeLeftAction = new global::Gtk.ToggleAction ("TimeLeftAction", global::Mono.Unix.Catalog.GetString ("Time Left"), null, null);
		this.TimeLeftAction.Active = true;
		this.TimeLeftAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Time Left");
		w1.Add (this.TimeLeftAction, null);
		this.SpeedAction = new global::Gtk.ToggleAction ("SpeedAction", global::Mono.Unix.Catalog.GetString ("Speed"), null, null);
		this.SpeedAction.Active = true;
		this.SpeedAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Speed");
		w1.Add (this.SpeedAction, null);
		this.StatusBarAction = new global::Gtk.ToggleAction ("StatusBarAction", global::Mono.Unix.Catalog.GetString ("Status Bar"), null, null);
		this.StatusBarAction.Active = true;
		this.StatusBarAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Status Bar");
		w1.Add (this.StatusBarAction, null);
		this.DownloadedAction = new global::Gtk.ToggleAction ("DownloadedAction", global::Mono.Unix.Catalog.GetString ("Downloaded"), null, null);
		this.DownloadedAction.Active = true;
		this.DownloadedAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Downloaded");
		w1.Add (this.DownloadedAction, null);
		this.SpeedAction1 = new global::Gtk.ToggleAction ("SpeedAction1", global::Mono.Unix.Catalog.GetString ("Speed"), null, null);
		this.SpeedAction1.Active = true;
		this.SpeedAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Speed");
		w1.Add (this.SpeedAction1, null);
		this.DownloadsAction = new global::Gtk.ToggleAction ("DownloadsAction", global::Mono.Unix.Catalog.GetString ("Downloads"), null, null);
		this.DownloadsAction.Active = true;
		this.DownloadsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Downloads");
		w1.Add (this.DownloadsAction, null);
		this.TorrentsAction = new global::Gtk.ToggleAction ("TorrentsAction", global::Mono.Unix.Catalog.GetString ("Torrents"), null, null);
		this.TorrentsAction.Active = true;
		this.TorrentsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Torrents");
		w1.Add (this.TorrentsAction, null);
		this.FTPGetAction = new global::Gtk.ToggleAction ("FTPGetAction", global::Mono.Unix.Catalog.GetString ("FTP Get"), null, null);
		this.FTPGetAction.Active = true;
		this.FTPGetAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("FTP Get");
		w1.Add (this.FTPGetAction, null);
		this.FindAction = new global::Gtk.ToggleAction ("FindAction", global::Mono.Unix.Catalog.GetString ("Find"), null, null);
		this.FindAction.Active = true;
		this.FindAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Find");
		w1.Add (this.FindAction, null);
		this.OptionsAction = new global::Gtk.Action ("OptionsAction", global::Mono.Unix.Catalog.GetString ("Options"), null, null);
		this.OptionsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Options");
		w1.Add (this.OptionsAction, null);
		this.SettingsAction = new global::Gtk.Action ("SettingsAction", global::Mono.Unix.Catalog.GetString ("Settings"), null, null);
		this.SettingsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Settings");
		w1.Add (this.SettingsAction, null);
		this.TrafficUsageModeAction = new global::Gtk.Action ("TrafficUsageModeAction", global::Mono.Unix.Catalog.GetString ("Traffic Usage Mode"), null, null);
		this.TrafficUsageModeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Traffic Usage Mode");
		w1.Add (this.TrafficUsageModeAction, null);
		this.LowSpeedAction = new global::Gtk.RadioAction ("LowSpeedAction", global::Mono.Unix.Catalog.GetString ("Low Speed"), null, null, 0);
		this.LowSpeedAction.Group = new global::GLib.SList (global::System.IntPtr.Zero);
		this.LowSpeedAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Low Speed");
		w1.Add (this.LowSpeedAction, null);
		this.MediumSpeedAction = new global::Gtk.RadioAction ("MediumSpeedAction", global::Mono.Unix.Catalog.GetString ("Medium Speed"), null, null, 0);
		this.MediumSpeedAction.Group = this.LowSpeedAction.Group;
		this.MediumSpeedAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Medium Speed");
		w1.Add (this.MediumSpeedAction, null);
		this.HighSpeedAction = new global::Gtk.RadioAction ("HighSpeedAction", global::Mono.Unix.Catalog.GetString ("High Speed"), null, null, 0);
		this.HighSpeedAction.Group = this.MediumSpeedAction.Group;
		this.HighSpeedAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("High Speed");
		w1.Add (this.HighSpeedAction, null);
		this.toolbarStopAll = new global::Gtk.Action ("toolbarStopAll", global::Mono.Unix.Catalog.GetString ("Stop All"), null, null);
		this.toolbarStopAll.ShortLabel = global::Mono.Unix.Catalog.GetString ("Stop All");
		w1.Add (this.toolbarStopAll, null);
		this.toolbarPauseAll = new global::Gtk.Action ("toolbarPauseAll", global::Mono.Unix.Catalog.GetString ("Pause All"), null, null);
		this.toolbarPauseAll.ShortLabel = global::Mono.Unix.Catalog.GetString ("Pause All");
		w1.Add (this.toolbarPauseAll, null);
		this.toolbarFind = new global::Gtk.Action ("toolbarFind", null, null, "gtk-find");
		w1.Add (this.toolbarFind, null);
		this.toolbarStartQueue = new global::Gtk.Action ("toolbarStartQueue", null, null, "gtk-yes");
		w1.Add (this.toolbarStartQueue, null);
		this.toolbarStopQueue = new global::Gtk.Action ("toolbarStopQueue", null, null, "gtk-no");
		w1.Add (this.toolbarStopQueue, null);
		this.toolbarCancelQueue = new global::Gtk.Action ("toolbarCancelQueue", null, null, "gtk-dialog-error");
		w1.Add (this.toolbarCancelQueue, null);
		this.toolbarNewQueue = new global::Gtk.Action ("toolbarNewQueue", null, null, "gtk-save");
		w1.Add (this.toolbarNewQueue, null);
		this.toolbarRestartQueue = new global::Gtk.Action ("toolbarRestartQueue", null, null, "gtk-refresh");
		w1.Add (this.toolbarRestartQueue, null);
		this.CancelQueueAction = new global::Gtk.Action ("CancelQueueAction", global::Mono.Unix.Catalog.GetString ("Cancel Queue"), null, null);
		this.CancelQueueAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Cancel Queue");
		w1.Add (this.CancelQueueAction, null);
		this.RestartQueueAction = new global::Gtk.Action ("RestartQueueAction", global::Mono.Unix.Catalog.GetString ("Restart Queue"), null, null);
		this.RestartQueueAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Restart Queue");
		w1.Add (this.RestartQueueAction, null);
		this.FTPViewerAction = new global::Gtk.Action ("FTPViewerAction", global::Mono.Unix.Catalog.GetString ("FTPViewer"), null, null);
		this.FTPViewerAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("FTPViewer");
		w1.Add (this.FTPViewerAction, null);
		this.OpenAction = new global::Gtk.Action ("OpenAction", global::Mono.Unix.Catalog.GetString ("Open"), null, null);
		this.OpenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open");
		w1.Add (this.OpenAction, null);
		this.ReloadAction = new global::Gtk.Action ("ReloadAction", global::Mono.Unix.Catalog.GetString ("Reload"), null, null);
		this.ReloadAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Reload");
		w1.Add (this.ReloadAction, null);
		this.DownloadAction1 = new global::Gtk.Action ("DownloadAction1", global::Mono.Unix.Catalog.GetString ("Download"), null, null);
		this.DownloadAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Download");
		w1.Add (this.DownloadAction1, null);
		this.DownloadAllAction = new global::Gtk.Action ("DownloadAllAction", global::Mono.Unix.Catalog.GetString ("Download All"), null, null);
		this.DownloadAllAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Download All");
		w1.Add (this.DownloadAllAction, null);
		this.SelectAllAction = new global::Gtk.Action ("SelectAllAction", global::Mono.Unix.Catalog.GetString ("Select All"), null, null);
		this.SelectAllAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Select All");
		w1.Add (this.SelectAllAction, null);
		this.CopyFilnameToClipboardAction = new global::Gtk.Action ("CopyFilnameToClipboardAction", global::Mono.Unix.Catalog.GetString ("Copy Filname to Clipboard"), null, null);
		this.CopyFilnameToClipboardAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy Filname to Clipboard");
		w1.Add (this.CopyFilnameToClipboardAction, null);
		this.CopyFilepathToClipboardAction = new global::Gtk.Action ("CopyFilepathToClipboardAction", global::Mono.Unix.Catalog.GetString ("Copy Filepath to Clipboard"), null, null);
		this.CopyFilepathToClipboardAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy Filepath to Clipboard");
		w1.Add (this.CopyFilepathToClipboardAction, null);
		this.SearchAction = new global::Gtk.Action ("SearchAction", global::Mono.Unix.Catalog.GetString ("Search"), null, null);
		this.SearchAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Search");
		w1.Add (this.SearchAction, null);
		this.PropertiesAction = new global::Gtk.Action ("PropertiesAction", global::Mono.Unix.Catalog.GetString ("Properties"), null, null);
		this.PropertiesAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Properties");
		w1.Add (this.PropertiesAction, null);
		this.ArrangeItemsByNameAction = new global::Gtk.Action ("ArrangeItemsByNameAction", global::Mono.Unix.Catalog.GetString ("Arrange Items by Name"), null, null);
		this.ArrangeItemsByNameAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Arrange Items by Name");
		w1.Add (this.ArrangeItemsByNameAction, null);
		this.ArrangeItemsBySizeAction = new global::Gtk.Action ("ArrangeItemsBySizeAction", global::Mono.Unix.Catalog.GetString ("Arrange Items by Size"), null, null);
		this.ArrangeItemsBySizeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Arrange Items by Size");
		w1.Add (this.ArrangeItemsBySizeAction, null);
		this.ArrangeItemsByTypeAction = new global::Gtk.Action ("ArrangeItemsByTypeAction", global::Mono.Unix.Catalog.GetString ("Arrange Items by Type"), null, null);
		this.ArrangeItemsByTypeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Arrange Items by Type");
		w1.Add (this.ArrangeItemsByTypeAction, null);
		this.ArrangeItemsByDateAction = new global::Gtk.Action ("ArrangeItemsByDateAction", global::Mono.Unix.Catalog.GetString ("Arrange Items by Date"), null, null);
		this.ArrangeItemsByDateAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Arrange Items by Date");
		w1.Add (this.ArrangeItemsByDateAction, null);
		this.IconsAction = new global::Gtk.Action ("IconsAction", global::Mono.Unix.Catalog.GetString ("Icons"), null, null);
		this.IconsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Icons");
		w1.Add (this.IconsAction, null);
		this.ListAction = new global::Gtk.Action ("ListAction", global::Mono.Unix.Catalog.GetString ("List"), null, null);
		this.ListAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("List");
		w1.Add (this.ListAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='FileAction' action='FileAction'><menuitem name='NewDownloadAction' action='NewDownloadAction'/><menuitem name='AddExistingDownloadAction' action='AddExistingDownloadAction'/><separator/><menuitem name='CreateQueueAction' action='CreateQueueAction'/><menuitem name='DeleteQueueAction' action='DeleteQueueAction'/></menu><menu name='DownloadAction' action='DownloadAction'><menuitem name='StartAction' action='StartAction'/><menuitem name='PauseAction' action='PauseAction'/><menuitem name='RestartAction' action='RestartAction'/><menuitem name='CancelAction' action='CancelAction'/><separator/><menuitem name='StartQueueAction' action='StartQueueAction'/><menuitem name='StopQueueAction' action='StopQueueAction'/><menuitem name='CancelQueueAction' action='CancelQueueAction'/><menuitem name='RestartQueueAction' action='RestartQueueAction'/></menu><menu name='ViewAction' action='ViewAction'><menu name='ToolbarAction' action='ToolbarAction'><menuitem name='NewDownloadAction1' action='NewDownloadAction1'/><menuitem name='AddExistingDownloadAction1' action='AddExistingDownloadAction1'/><menuitem name='StartAction1' action='StartAction1'/><menuitem name='PauseAction1' action='PauseAction1'/><menuitem name='CancelAction1' action='CancelAction1'/><menuitem name='RestartAction1' action='RestartAction1'/><menuitem name='SpeedLimitAction' action='SpeedLimitAction'/><menuitem name='FindAction' action='FindAction'/></menu><menu name='ProgressWindowAction' action='ProgressWindowAction'><menuitem name='PartsStatusAction' action='PartsStatusAction'/><menuitem name='ProgressBarAction' action='ProgressBarAction'/><menuitem name='StatusAction' action='StatusAction'/><menuitem name='TimeLeftAction' action='TimeLeftAction'/><menuitem name='SpeedAction' action='SpeedAction'/></menu><menuitem name='StatusBarAction' action='StatusBarAction'/><menuitem name='DownloadsAction' action='DownloadsAction'/><menuitem name='TorrentsAction' action='TorrentsAction'/><menuitem name='FTPGetAction' action='FTPGetAction'/></menu><menu name='OptionsAction' action='OptionsAction'><menuitem name='SettingsAction' action='SettingsAction'/><menu name='TrafficUsageModeAction' action='TrafficUsageModeAction'><menuitem name='LowSpeedAction' action='LowSpeedAction'/><menuitem name='MediumSpeedAction' action='MediumSpeedAction'/><menuitem name='HighSpeedAction' action='HighSpeedAction'/></menu></menu><menu name='FTPViewerAction' action='FTPViewerAction'><menuitem name='OpenAction' action='OpenAction'/><menuitem name='ReloadAction' action='ReloadAction'/><separator/><menuitem name='DownloadAction1' action='DownloadAction1'/><menuitem name='DownloadAllAction' action='DownloadAllAction'/><separator/><menuitem name='SelectAllAction' action='SelectAllAction'/><menuitem name='CopyFilnameToClipboardAction' action='CopyFilnameToClipboardAction'/><menuitem name='CopyFilepathToClipboardAction' action='CopyFilepathToClipboardAction'/><menuitem name='SearchAction' action='SearchAction'/><separator/><menuitem name='PropertiesAction' action='PropertiesAction'/><menuitem name='ArrangeItemsByNameAction' action='ArrangeItemsByNameAction'/><menuitem name='ArrangeItemsBySizeAction' action='ArrangeItemsBySizeAction'/><menuitem name='ArrangeItemsByTypeAction' action='ArrangeItemsByTypeAction'/><menuitem name='ArrangeItemsByDateAction' action='ArrangeItemsByDateAction'/><separator/><menuitem name='IconsAction' action='IconsAction'/><menuitem name='ListAction' action='ListAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbar1'><toolitem name='toolbarNewDownload' action='toolbarNewDownload'/><toolitem name='toolbarAddDownload' action='toolbarAddDownload'/><separator/><toolitem name='toolbarStart' action='toolbarStart'/><toolitem name='toolbarPause' action='toolbarPause'/><toolitem name='toolbarCancel' action='toolbarCancel'/><toolitem name='toolbarRestart' action='toolbarRestart'/><separator/><toolitem name='toolbarNewQueue' action='toolbarNewQueue'/><toolitem name='toolbarStartQueue' action='toolbarStartQueue'/><toolitem name='toolbarStopQueue' action='toolbarStopQueue'/><toolitem name='toolbarCancelQueue' action='toolbarCancelQueue'/><toolitem name='toolbarRestartQueue' action='toolbarRestartQueue'/><toolitem/><separator/><toolitem name='toolbarSpeedLow' action='toolbarSpeedLow'/><toolitem name='toolbarSpeedMedium' action='toolbarSpeedMedium'/><toolitem name='toolbarSpeedFull' action='toolbarSpeedFull'/><separator/><toolitem name='toolbarFind' action='toolbarFind'/><toolitem name='preferencesAction' action='preferencesAction'/></toolbar></ui>");
		this.toolbar1 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbar1")));
		this.toolbar1.Name = "toolbar1";
		this.toolbar1.ShowArrow = false;
		this.toolbar1.ToolbarStyle = ((global::Gtk.ToolbarStyle)(0));
		this.vbox1.Add (this.toolbar1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.toolbar1]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.notebook = new global::Gtk.Notebook ();
		this.notebook.CanFocus = true;
		this.notebook.Name = "notebook";
		this.notebook.CurrentPage = 4;
		// Container child notebook.Gtk.Notebook+NotebookChild
		this.hpaned1 = new global::Gtk.HPaned ();
		this.hpaned1.CanFocus = true;
		this.hpaned1.Name = "hpaned1";
		this.hpaned1.Position = 174;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.WidthRequest = 515;
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.dmCategoryTreeView = new global::DownloadManager.DMCategoryTreeView ();
		this.dmCategoryTreeView.CanFocus = true;
		this.dmCategoryTreeView.Name = "dmCategoryTreeView";
		this.GtkScrolledWindow.Add (this.dmCategoryTreeView);
		this.hpaned1.Add (this.GtkScrolledWindow);
		global::Gtk.Paned.PanedChild w5 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.GtkScrolledWindow]));
		w5.Resize = false;
		// Container child hpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.dmDownloadTreeView = new global::DownloadManager.DMDownloadTreeView ();
		this.dmDownloadTreeView.CanFocus = true;
		this.dmDownloadTreeView.Name = "dmDownloadTreeView";
		this.GtkScrolledWindow1.Add (this.dmDownloadTreeView);
		this.hpaned1.Add (this.GtkScrolledWindow1);
		this.notebook.Add (this.hpaned1);
		// Notebook tab
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Downloads");
		this.notebook.SetTabLabel (this.hpaned1, this.label1);
		this.label1.ShowAll ();
		// Container child notebook.Gtk.Notebook+NotebookChild
		this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
		this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
		this.dmqueuetreeview = new global::DownloadManager.DMQueueTreeView ();
		this.dmqueuetreeview.CanFocus = true;
		this.dmqueuetreeview.Name = "dmqueuetreeview";
		this.GtkScrolledWindow2.Add (this.dmqueuetreeview);
		this.notebook.Add (this.GtkScrolledWindow2);
		global::Gtk.Notebook.NotebookChild w10 = ((global::Gtk.Notebook.NotebookChild)(this.notebook [this.GtkScrolledWindow2]));
		w10.Position = 1;
		// Notebook tab
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Queues");
		this.notebook.SetTabLabel (this.GtkScrolledWindow2, this.label2);
		this.label2.ShowAll ();
		// Notebook tab
		global::Gtk.Label w11 = new global::Gtk.Label ();
		w11.Visible = true;
		this.notebook.Add (w11);
		this.label3 = new global::Gtk.Label ();
		this.label3.Name = "label3";
		this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Torrents");
		this.notebook.SetTabLabel (w11, this.label3);
		this.label3.ShowAll ();
		// Container child notebook.Gtk.Notebook+NotebookChild
		this.ftpgetwidget1 = new global::DownloadManager.FTPGetWidget ();
		this.ftpgetwidget1.Name = "ftpgetwidget1";
		this.notebook.Add (this.ftpgetwidget1);
		global::Gtk.Notebook.NotebookChild w12 = ((global::Gtk.Notebook.NotebookChild)(this.notebook [this.ftpgetwidget1]));
		w12.Position = 3;
		// Notebook tab
		this.label4 = new global::Gtk.Label ();
		this.label4.Name = "label4";
		this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("FTPViewer");
		this.notebook.SetTabLabel (this.ftpgetwidget1, this.label4);
		this.label4.ShowAll ();
		// Notebook tab
		global::Gtk.Label w13 = new global::Gtk.Label ();
		w13.Visible = true;
		this.notebook.Add (w13);
		this.label5 = new global::Gtk.Label ();
		this.label5.Name = "label5";
		this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("SiteGet");
		this.notebook.SetTabLabel (w13, this.label5);
		this.label5.ShowAll ();
		this.vbox1.Add (this.notebook);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.notebook]));
		w14.Position = 2;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar1 = new global::Gtk.Statusbar ();
		this.statusbar1.Name = "statusbar1";
		this.statusbar1.Spacing = 6;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.lblDownloaded = new global::Gtk.Label ();
		this.lblDownloaded.Name = "lblDownloaded";
		this.lblDownloaded.LabelProp = global::Mono.Unix.Catalog.GetString ("Downloaded");
		this.statusbar1.Add (this.lblDownloaded);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.statusbar1 [this.lblDownloaded]));
		w15.Position = 1;
		w15.Expand = false;
		w15.Fill = false;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.vseparator1 = new global::Gtk.VSeparator ();
		this.vseparator1.Name = "vseparator1";
		this.statusbar1.Add (this.vseparator1);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.statusbar1 [this.vseparator1]));
		w16.Position = 2;
		w16.Expand = false;
		w16.Fill = false;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.lblSpeed = new global::Gtk.Label ();
		this.lblSpeed.Name = "lblSpeed";
		this.lblSpeed.LabelProp = global::Mono.Unix.Catalog.GetString ("Speed");
		this.statusbar1.Add (this.lblSpeed);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.statusbar1 [this.lblSpeed]));
		w17.Position = 3;
		w17.Expand = false;
		w17.Fill = false;
		this.vbox1.Add (this.statusbar1);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbar1]));
		w18.Position = 3;
		w18.Expand = false;
		w18.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1052;
		this.DefaultHeight = 438;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.NewDownloadAction.Activated += new global::System.EventHandler (this.OnNewDownloadActivated);
		this.AddExistingDownloadAction.Activated += new global::System.EventHandler (this.OnAddExistingDownloadActivated);
		this.toolbarStart.Activated += new global::System.EventHandler (this.OnToolbarStartActivated);
		this.toolbarPause.Activated += new global::System.EventHandler (this.OnToolbarPauseActivated);
		this.toolbarCancel.Activated += new global::System.EventHandler (this.OnToolbarCancelActivated);
		this.toolbarRestart.Activated += new global::System.EventHandler (this.OnToolbarRestartActivated);
		this.toolbarSpeedLow.Activated += new global::System.EventHandler (this.OnSpeedLowActivated);
		this.toolbarSpeedMedium.Activated += new global::System.EventHandler (this.OnSpeedMediumActivated);
		this.toolbarSpeedFull.Activated += new global::System.EventHandler (this.OnSpeedHighActivated);
		this.StartAction.Activated += new global::System.EventHandler (this.OnToolbarStartActivated);
		this.PauseAction.Activated += new global::System.EventHandler (this.OnToolbarPauseActivated);
		this.RestartAction.Activated += new global::System.EventHandler (this.OnToolbarRestartActivated);
		this.CancelAction.Activated += new global::System.EventHandler (this.OnToolbarCancelActivated);
		this.CreateQueueAction.Activated += new global::System.EventHandler (this.CreateQueueActivated);
		this.NewDownloadAction1.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.AddExistingDownloadAction1.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.StartAction1.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.PauseAction1.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.CancelAction1.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.RestartAction1.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.SpeedLimitAction.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.PartsStatusAction.Toggled += new global::System.EventHandler (this.OnProgressWindowActivated);
		this.ProgressBarAction.Toggled += new global::System.EventHandler (this.OnProgressWindowActivated);
		this.StatusAction.Toggled += new global::System.EventHandler (this.OnProgressWindowActivated);
		this.TimeLeftAction.Toggled += new global::System.EventHandler (this.OnProgressWindowActivated);
		this.SpeedAction.Toggled += new global::System.EventHandler (this.OnProgressWindowActivated);
		this.FindAction.Toggled += new global::System.EventHandler (this.OnShowToolbarActivated);
		this.SettingsAction.Activated += new global::System.EventHandler (this.OnSettingsActionActivated);
		this.MediumSpeedAction.Activated += new global::System.EventHandler (this.OnSpeedMediumActivated);
		this.HighSpeedAction.Activated += new global::System.EventHandler (this.OnSpeedHighActivated);
		this.toolbarFind.Activated += new global::System.EventHandler (this.OnToolbarFindActivated);
		this.toolbarStartQueue.Activated += new global::System.EventHandler (this.toolbarStartQueueActivated);
		this.toolbarStopQueue.Activated += new global::System.EventHandler (this.toolbarStopQueueActivated);
		this.toolbarCancelQueue.Activated += new global::System.EventHandler (this.toolbarCancelQueueActivated);
		this.toolbarNewQueue.Activated += new global::System.EventHandler (this.toolbarNewQueueActivated);
		this.toolbarRestartQueue.Activated += new global::System.EventHandler (this.toolbarRestartQueuectivated);
		this.dmDownloadTreeView.RowActivated += new global::Gtk.RowActivatedHandler (this.dmDownloadTreeViewRowActivated);
	}
}
