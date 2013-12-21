namespace JetFly
{
	partial class SettingsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.textBoxUploadPath = new System.Windows.Forms.TextBox();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemCutScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemPublish = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemRecent = new System.Windows.Forms.ToolStripMenuItem();
			this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.clipboardWatcherTimer = new System.Windows.Forms.Timer(this.components);
			this.iconChangeTimer = new System.Windows.Forms.Timer(this.components);
			this.checkBoxUseProxy = new System.Windows.Forms.CheckBox();
			this.checkBoxUploadImmediately = new System.Windows.Forms.CheckBox();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxUploadPath
			// 
			this.textBoxUploadPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxUploadPath.Location = new System.Drawing.Point(12, 31);
			this.textBoxUploadPath.Name = "textBoxUploadPath";
			this.textBoxUploadPath.Size = new System.Drawing.Size(404, 20);
			this.textBoxUploadPath.TabIndex = 0;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(341, 149);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.ButtonSaveOnClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(260, 149);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelOnClick);
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon.BalloonTipText = " ";
			this.notifyIcon.BalloonTipTitle = " ";
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Text = "Win+Ctrl+Q - вырезать, Win+Ctrl+W - отправить!";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconMouseDoubleClick);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCutScreen,
            this.toolStripMenuItemPublish,
            this.toolStripMenuItemSettings,
            this.toolStripMenuItemRecent,
            this.toolStripMenuItemExit});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(263, 114);
			// 
			// toolStripMenuItemCutScreen
			// 
			this.toolStripMenuItemCutScreen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripMenuItemCutScreen.Name = "toolStripMenuItemCutScreen";
			this.toolStripMenuItemCutScreen.Size = new System.Drawing.Size(262, 22);
			this.toolStripMenuItemCutScreen.Text = "Вырезать область экрана Win+Q";
			this.toolStripMenuItemCutScreen.Click += new System.EventHandler(this.ToolStripMenuItemCutScreenClick);
			// 
			// toolStripMenuItemPublish
			// 
			this.toolStripMenuItemPublish.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.toolStripMenuItemPublish.Name = "toolStripMenuItemPublish";
			this.toolStripMenuItemPublish.Size = new System.Drawing.Size(262, 22);
			this.toolStripMenuItemPublish.Text = "Поделиться ссылкой Win+W";
			this.toolStripMenuItemPublish.Click += new System.EventHandler(this.ToolStripMenuItemPublishClick);
			// 
			// toolStripMenuItemSettings
			// 
			this.toolStripMenuItemSettings.Name = "toolStripMenuItemSettings";
			this.toolStripMenuItemSettings.Size = new System.Drawing.Size(262, 22);
			this.toolStripMenuItemSettings.Text = "Настройка";
			this.toolStripMenuItemSettings.Click += new System.EventHandler(this.ToolStripMenuItemSettingsClick);
			// 
			// toolStripMenuItemRecent
			// 
			this.toolStripMenuItemRecent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem});
			this.toolStripMenuItemRecent.Name = "toolStripMenuItemRecent";
			this.toolStripMenuItemRecent.Size = new System.Drawing.Size(262, 22);
			this.toolStripMenuItemRecent.Text = "Последние...";
			// 
			// emptyToolStripMenuItem
			// 
			this.emptyToolStripMenuItem.Enabled = false;
			this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
			this.emptyToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
			this.emptyToolStripMenuItem.Tag = "";
			this.emptyToolStripMenuItem.Text = "Вы еще ничего не отправляли";
			// 
			// toolStripMenuItemExit
			// 
			this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
			this.toolStripMenuItemExit.Size = new System.Drawing.Size(262, 22);
			this.toolStripMenuItemExit.Text = "Выход";
			this.toolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExitClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Куда заливать данные?";
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label2.Location = new System.Drawing.Point(91, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(325, 42);
			this.label2.TabIndex = 4;
			this.label2.Text = "Укажите существующую сетевую директорию, либо оставьте поле пустым, чтобы использ" +
    "овать наш сервер в интернете";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// clipboardWatcherTimer
			// 
			this.clipboardWatcherTimer.Enabled = true;
			this.clipboardWatcherTimer.Interval = 500;
			this.clipboardWatcherTimer.Tick += new System.EventHandler(this.TimerTick);
			// 
			// iconChangeTimer
			// 
			this.iconChangeTimer.Interval = 2000;
			this.iconChangeTimer.Tick += new System.EventHandler(this.IconChangeTimerTick);
			// 
			// checkBoxUseProxy
			// 
			this.checkBoxUseProxy.AutoSize = true;
			this.checkBoxUseProxy.Location = new System.Drawing.Point(12, 99);
			this.checkBoxUseProxy.Name = "checkBoxUseProxy";
			this.checkBoxUseProxy.Size = new System.Drawing.Size(138, 17);
			this.checkBoxUseProxy.TabIndex = 5;
			this.checkBoxUseProxy.Text = "Использовать прокси";
			this.checkBoxUseProxy.UseVisualStyleBackColor = true;
			// 
			// checkBoxUploadImmediately
			// 
			this.checkBoxUploadImmediately.AutoSize = true;
			this.checkBoxUploadImmediately.Location = new System.Drawing.Point(12, 127);
			this.checkBoxUploadImmediately.Name = "checkBoxUploadImmediately";
			this.checkBoxUploadImmediately.Size = new System.Drawing.Size(332, 17);
			this.checkBoxUploadImmediately.TabIndex = 6;
			this.checkBoxUploadImmediately.Text = "Загружать на сервер, как только область экрана выделена";
			this.checkBoxUploadImmediately.UseVisualStyleBackColor = true;
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.buttonSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(428, 184);
			this.Controls.Add(this.checkBoxUploadImmediately);
			this.Controls.Add(this.checkBoxUseProxy);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxUploadPath);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.Text = "Настройка JetFly";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsFormFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Shown += new System.EventHandler(this.SettingsFormShown);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxUploadPath;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSettings;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPublish;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
		private System.Windows.Forms.Timer clipboardWatcherTimer;
		private System.Windows.Forms.Timer iconChangeTimer;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCutScreen;
		private System.Windows.Forms.CheckBox checkBoxUseProxy;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRecent;
		private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
		private System.Windows.Forms.CheckBox checkBoxUploadImmediately;
	}
}