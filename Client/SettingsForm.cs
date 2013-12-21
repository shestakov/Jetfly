using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using JetFly.Properties;

namespace JetFly
{
	public partial class SettingsForm : Form, IMagicClipboardListener
	{
		private readonly MagicClipboard magicClipboard;
		private Settings settings = new Settings();
		private bool firstTime = true;
		private bool realClose;

		public SettingsForm()
		{
			var userPersonalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			settingsFileName = Path.Combine(userPersonalDirectory, "settings.xml");
			LoadSettings();
			var urlLog = new ImageUrlLog();
			if (!urlLog.Start(Path.Combine(userPersonalDirectory, "url.log")))
				MessageBox.Show("Не удалось инициализировать сохранение ссылок на добавленные изображения");
			magicClipboard = new MagicClipboard(this)
			{
				Settings = settings,
				ImageUrlLog = urlLog.Initialized ? urlLog : null
			};
			InitializeComponent();
			Icon = Resources.New;
			notifyIcon.Icon = Resources.New;
			SetupHotKeyManager();
		}

		#region Clipboard preview

		private void ShowPreview()
		{
			if (MagicClipboard.ContainsImage())
			{
				var image = MagicClipboard.GetImage();
				if (image == null) return;
				if (previewForm == null) previewForm = new PreviewForm();
				previewForm.Show(image);
			}
		}

		#endregion

		#region HotKeyManager setup

		private void SetupHotKeyManager()
		{
			HotKeyManager.HotKeyPressedPreview += HotKeyManagerOnHotKeyPressedPreview;
			HotKeyManager.HotKeyPressedPost += HotKeyManagerOnHotKeyPressedPost;
			HotKeyManager.HotKeyPressedCut += HotKeyManagerOnHotKeyPressedCut;
			HotKeyManager.HotKeyPressedPost2 += HotKeyManagerOnHotKeyPressedPost2;
			HotKeyManager.RegisterHotKeys(Handle);
		}

		private void HotKeyManagerOnHotKeyPressedPreview(object sender, EventArgs e)
		{
			ShowPreview();
		}

		private void HotKeyManagerOnHotKeyPressedPost2(object sender, EventArgs e)
		{
			if (currentState == State.Busy) return;
			UrlFromClipboard();
		}

		private void HotKeyManagerOnHotKeyPressedCut(object sender, EventArgs e)
		{
			if (currentState == State.Busy) return;
			CutScreenArea();
		}

		private void HotKeyManagerOnHotKeyPressedPost(object sender, EventArgs e)
		{
			if (currentState == State.Busy) return;
			UrlFromClipboard();
		}

		#endregion

		#region IMagicClipboardListener Members

		public void OnUrlFromClipboard(string url)
		{
			notifyIcon.ShowBalloonTip(1000, "URL в буфере обмена", url, ToolTipIcon.Info);
			AddImageUrlToContextMenu(url);
		}

		public void OnClipboardContentChanged(bool imageDetected)
		{
			if (imageDetected)
			{
				notifyIcon.Icon = Resources.Copy;
				notifyIcon.Text = "Нажмите Win+Ctrl+W для отправки или Win+Ctrl+A для просмотра";
				iconChangeTimer.Start();
			}
			else
			{
				notifyIcon.Icon = Resources.New;
				notifyIcon.Text = "Win+Ctrl+Q - вырезать, Win+Ctrl+W - отправить!";
			}
		}

		private void AddImageUrlToContextMenu(string url)
		{
			if(toolStripMenuItemRecent.DropDownItems.Count == 1 && toolStripMenuItemRecent.DropDownItems[0].Name == "emptyToolStripMenuItem")
				toolStripMenuItemRecent.DropDownItems.RemoveAt(0);

			const int maxRecentMenuCount = 5;
			var toolStripMenuItem = new ToolStripMenuItem(DateTime.Now.ToString()) {Tag = url};
			toolStripMenuItemRecent.DropDownItems.Add(toolStripMenuItem);
			toolStripMenuItem.Click += ToolStripMenuItemClick;

			if (toolStripMenuItemRecent.DropDownItems.Count > maxRecentMenuCount)
				toolStripMenuItemRecent.DropDownItems.RemoveAt(0);
		}

		private static void ToolStripMenuItemClick(object sender, EventArgs e)
		{
			var toolStripMenuItem = (ToolStripMenuItem) sender;
			Process.Start((string) toolStripMenuItem.Tag);
		}

		public void OnUploadStart()
		{
			notifyIcon.ShowBalloonTip(500, "Загрузка на сервер...", " ", ToolTipIcon.None);
		}

		#endregion

		#region SettingsForm behaviour

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			HotKeyManager.UnregisterHotKeys(Handle);
		}

		private void ButtonSaveOnClick(object sender, EventArgs e)
		{
			settings.UploadPath = textBoxUploadPath.Text;
			settings.UseProxy = checkBoxUseProxy.Checked;
			settings.UploadImmediately = checkBoxUploadImmediately.Checked;
			magicClipboard.Settings = settings;
			SaveSettings();
			Hide();
		}

		private void ButtonCancelOnClick(object sender, EventArgs e)
		{
			Hide();
		}

		private void SettingsFormShown(object sender, EventArgs e)
		{
			if (firstTime)
			{
				firstTime = false;
				Visible = false;
			}
			else
			{
				WindowState = FormWindowState.Normal;
			}
			textBoxUploadPath.Text = settings.UploadPath;
			checkBoxUseProxy.Checked = settings.UseProxy;
			checkBoxUploadImmediately.Checked = settings.UploadImmediately;
		}

		private void OpenForm()
		{
			Show();
			BringToFront();
			WindowState = FormWindowState.Normal;
		}

		#endregion

		#region WndProc

		protected override void WndProc(ref Message msg)
		{
			switch (msg.Msg)
			{
				case HotKeyManager.WM_HOTKEY:
					HotKeyManager.ProcessHotKeyPress(msg);
					break;
			}
			base.WndProc(ref msg);
		}

		#endregion

		#region Settings management

		private void SaveSettings()
		{
			try
			{
				var serializer = new XmlSerializer(typeof(Settings));
				using (var writer = new StreamWriter(settingsFileName))
				{
					serializer.Serialize(writer, settings);
				}
			}
			catch (IOException e)
			{
				MessageBox.Show(this, e.ToString(), "Ошибка при сохранении настроек", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void LoadSettings()
		{
			try
			{
				var serializer = new XmlSerializer(typeof(Settings));
				using (var reader = new StreamReader(settingsFileName))
				{
					settings = (Settings)serializer.Deserialize(reader);
				}
			}
			catch
			{
				settings = new Settings();
			}
		}

		#endregion

		#region Scanning clipboard

		private void TimerTick(object sender, EventArgs e)
		{
			magicClipboard.ScanClipboard();
		}

		#endregion

		#region Processing user input

		private void NotifyIconMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			UrlFromClipboard();
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (currentState == State.Busy) return;
				CutScreenArea();
			}

		}

		private void ToolStripMenuItemExitClick(object sender, EventArgs e)
		{
			realClose = true;
			Close();
		}

		private void ToolStripMenuItemSettingsClick(object sender, EventArgs e)
		{
			OpenForm();
		}

		private void ToolStripMenuItemPublishClick(object sender, EventArgs e)
		{
			UrlFromClipboard();
		}

		private void IconChangeTimerTick(object sender, EventArgs e)
		{
			notifyIcon.Icon = Resources.New;
			iconChangeTimer.Stop();
		}

		private void SettingsFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if(realClose || e.CloseReason == CloseReason.WindowsShutDown) return;
			e.Cancel = true;
			Hide();
		}

		private void ToolStripMenuItemCutScreenClick(object sender, EventArgs e)
		{
			CutScreenArea();
		}

		#endregion

		private void CutScreenArea()
		{
			SwitchState(State.Busy);
			BlackoutForm.ScreenCaptured += BlackoutFormOnScreenCaptured;
			BlackoutForm.ScreenCaptureCancelled += BlackoutFormOnScreenCaptureCancelled;
			BlackoutForm.CaptureScreen();
		}

		private void BlackoutFormOnScreenCaptureCancelled(object sender, EventArgs e)
		{
			BlackoutForm.ScreenCaptured -= BlackoutFormOnScreenCaptured;
			BlackoutForm.ScreenCaptureCancelled -= BlackoutFormOnScreenCaptureCancelled;
			SwitchState(State.Ready);
		}

		private void BlackoutFormOnScreenCaptured(object sender, EventArgs e)
		{
			//If this even occures the bitmap is not null
			BlackoutForm.ScreenCaptured -= BlackoutFormOnScreenCaptured;
			BlackoutForm.ScreenCaptureCancelled -= BlackoutFormOnScreenCaptureCancelled;
			MagicClipboard.PutImage(BlackoutForm.Bitmap);
			SwitchState(State.Ready);
			if (settings.UploadImmediately)
				UrlFromClipboard();
		}

		private void UrlFromClipboard()
		{
			SwitchState(State.Busy);
			try
			{
				magicClipboard.UrlFromClipboard();
			}
			catch (Exception ex)
			{
				notifyIcon.ShowBalloonTip(1000, "Ошибка при загрузке изображения на сервер", ex.Message, ToolTipIcon.Error);
			}
			SwitchState(State.Ready);
		}

		#region Application currentState

		private enum State { Ready, Busy }

		private State currentState = State.Ready;
		private PreviewForm previewForm;
		private readonly string settingsFileName;

		private void SwitchState(State newState)
		{
			if (currentState == newState)
				throw new ArgumentException("Error switching internal state", "newState");
			if (newState == State.Ready)
			{
				notifyIcon.ContextMenuStrip = contextMenuStrip;
				notifyIcon.MouseDoubleClick -= NotifyIconMouseDoubleClick;
				notifyIcon.MouseClick -= notifyIcon_MouseClick;
			}
			else if (newState == State.Busy)
			{
				notifyIcon.ContextMenuStrip = null;
				notifyIcon.MouseDoubleClick += NotifyIconMouseDoubleClick;
				notifyIcon.MouseClick += notifyIcon_MouseClick;
			}
			currentState = newState;
		}

		#endregion
	}
}