using System;
using System.Drawing;
using System.Windows.Forms;

namespace JetFly
{
	public partial class PreviewForm : Form
	{
		public PreviewForm()
		{
			InitializeComponent();
			CreateGraphics();
			Click += OnClick;
		}

		private void OnClick(object sender, EventArgs args)
		{
			Hide();
		}

		public void Show(Image image)
		{
			const int margin = 10;
			var workingArea = Screen.PrimaryScreen.WorkingArea;

			float scaleFactorWidth = 1;
			float scaleFactorHeight = 1;

			if (image.Width > workingArea.Width / 2 - 2 * margin)
				scaleFactorWidth = (workingArea.Width/2 - 2*margin)/(float)image.Width;
			if (image.Height > workingArea.Height/2 - 2*margin)
				scaleFactorHeight = (workingArea.Height/2 - 2*margin)/(float) image.Height;
			var scaleFactor = scaleFactorWidth < scaleFactorHeight ? scaleFactorWidth : scaleFactorHeight;

			Graphics.FromImage(image).DrawRectangle(new Pen(Color.Black, 10 / scaleFactor), new Rectangle(0, 0, image.Width - 1, image.Height - 1));

			if (image.Width <= workingArea.Width / 2 - 2 * margin && image.Height <= workingArea.Height / 2 - 2 * margin)
			{
				Size = image.Size;
			}
			else
			{
				Size = new Size( (int) (image.Width * scaleFactor), (int) (image.Height * scaleFactor));
			}

			BackgroundImage = image;
			Location = new Point(workingArea.Width - Width - margin, workingArea.Height - Height - margin);
			Show();
			timerAutoHide.Start();
		}

		private void timerAutoHide_Tick(object sender, EventArgs e)
		{
			timerAutoHide.Stop();
			Hide();
		}
	}
}