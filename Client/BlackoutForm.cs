using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JetFly
{
	public partial class BlackoutForm : Form
	{
		private Point pointClick;
		private Rectangle selectionRect;

		private readonly Graphics graphics;
		private static readonly Color backgroundColor = Color.FromArgb(255, 255, 192);
		private readonly Pen rectanglePen = new Pen(Color.Blue, 1) { DashStyle = DashStyle.Dot };
		private readonly Pen eraserPen = new Pen(backgroundColor, 1);
		private event EventHandler ScreenAreaPicked;

		private BlackoutForm(Rectangle rectangle)
		{
			InitializeComponent();
			Bounds = rectangle;
			MouseDown += OnMouseDown;
			MouseUp += OnMouseUp;
			MouseMove += OnMouseMove;
			graphics = CreateGraphics();
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				selectionRect = new Rectangle(0, 0, 0, 0);
				ScreenAreaPicked(sender, e);
			}
			if (e.Button != MouseButtons.Left) return;
			graphics.Clear(backgroundColor);
			pointClick = PointToClient(new Point(MousePosition.X, MousePosition.Y));
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			Hide();
			if (pointClick == PointToClient(new Point(MousePosition.X, MousePosition.Y)))
				selectionRect = new Rectangle(0, 0, Width, Height);
			TakeScreenshot(RectangleToScreen(selectionRect));
			ScreenAreaPicked(sender, e);
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;

			graphics.DrawRectangle(eraserPen, selectionRect);

			var rightBottom = Point.Empty;
			var leftTop =Point.Empty;


			var position = PointToClient(Cursor.Position);

			if (position.X < pointClick.X)
			{
				leftTop.X = position.X;
				rightBottom.X = pointClick.X;
			}
			else
			{
				leftTop.X = pointClick.X;
				rightBottom.X = position.X;
			}

			if (position.Y < pointClick.Y)
			{
				leftTop.Y = position.Y;
				rightBottom.Y = pointClick.Y;
			}
			else
			{
				leftTop.Y = pointClick.Y;
				rightBottom.Y = position.Y;
			}

			selectionRect = new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
			graphics.DrawRectangle(rectanglePen, selectionRect);
		}

		private static void TakeScreenshot(Rectangle rectangleSelection)
		{
			if (rectangleSelection.Width == 0 || rectangleSelection.Height == 0)
			{
				Bitmap = null;
				return;
			}

			Bitmap = new Bitmap(rectangleSelection.Width, rectangleSelection.Height);
			using (var g = Graphics.FromImage(Bitmap))
			{
				g.CopyFromScreen(rectangleSelection.Location, Point.Empty, rectangleSelection.Size);
			}
		}

		#region Static

		/// <summary>
		/// Prompts the user to pick a display area to capture
		/// </summary>
		public static void CaptureScreen()
		{
			forms = new List<BlackoutForm>();
			foreach (var screen in Screen.AllScreens)
			{
				var blackoutForm = new BlackoutForm(screen.Bounds);
				forms.Add(blackoutForm);
				blackoutForm.ScreenAreaPicked += BlackoutFormScreenAreaPicked;
				blackoutForm.Show();
			}
		}

		public static event EventHandler ScreenCaptured;
		public static event EventHandler ScreenCaptureCancelled;

		private static void BlackoutFormScreenAreaPicked(object sender, EventArgs e)
		{
			foreach (var blackoutForm in forms)
			{
				blackoutForm.ScreenAreaPicked -= BlackoutFormScreenAreaPicked;
				blackoutForm.Close();
			}
			if (Bitmap != null && ScreenCaptured != null)
				ScreenCaptured(sender, e);
			else if (Bitmap == null && ScreenCaptureCancelled != null)
				ScreenCaptureCancelled(sender, e);
		}

		private static List<BlackoutForm> forms;
		public static Bitmap Bitmap;

		#endregion
	}
}