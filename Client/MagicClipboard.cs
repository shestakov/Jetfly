using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JetFly
{
	public class MagicClipboard
	{
		private bool imageDetected;

		public MagicClipboard(IMagicClipboardListener listener)
		{
			this.listener = listener;
		}

		private readonly IMagicClipboardListener listener;
		public Settings Settings { get; set; }

		public ImageUrlLog ImageUrlLog { get; set; }

		public void UrlFromClipboard()
		{
			if (!Clipboard.ContainsImage()) return;

			Image image = Clipboard.GetImage();
			if (image == null) return;
			string url = UploadTo(Settings.UploadPath, image);
			if (String.IsNullOrEmpty(url)) return;
			ProcessImageUrl(url);
			TryToDo(() => Clipboard.SetText(url));

			listener.OnUrlFromClipboard(url);
		}

		private void ProcessImageUrl(string url)
		{
			if (ImageUrlLog != null)
				ImageUrlLog.LogImageUrl(url);
		}

		private static void TryToDo(ThreadStart action)
		{
			for (int i = 0; i < 10; i++)
			{
				try
				{
					action();
					return;
				}
				catch (Exception)
				{
					Thread.Sleep(1000);
				}
			}
		}

		private string UploadTo(string destinationPath, Image image)
		{
			if (Directory.Exists(Settings.UploadPath))
				return UploadImageToDirectory(destinationPath, image);
			return UploadImageToSite(image);
		}

		private string UploadImageToSite(Image image)
		{
			listener.OnUploadStart();

			const string targetUrl = "http://alexshestakov.com/JetFly/PostImage.ashx";
			byte[] serializedJpegBitmap;
			using (var stream = new MemoryStream())
			{
				image.Save(stream, ImageFormat.Png);
				serializedJpegBitmap = stream.GetBuffer();
			}
			var client = new WebClient();
			if (Settings.UseProxy)
			{
				client.Proxy = WebRequest.DefaultWebProxy;
				client.Proxy.Credentials = CredentialCache.DefaultCredentials;
			}
			client.Headers.Add("Content-Type", "image/png");
			byte[] response = client.UploadData(targetUrl, "POST", serializedJpegBitmap);
			return Encoding.UTF8.GetString(response);
		}

		private static string UploadImageToDirectory(string path, Image image)
		{
			var filename = path + Path.DirectorySeparatorChar + Guid.NewGuid().ToString("N") + ".png";
			image.Save(filename, ImageFormat.Png);
			return filename;
		}

		public void ScanClipboard()
		{
			if (ContainsImage())
			{
				if (!imageDetected)
				{
					imageDetected = true;
					listener.OnClipboardContentChanged(imageDetected);
				}
			}
			else
			{
				if (imageDetected)
					listener.OnClipboardContentChanged(imageDetected = false);
			}
		}

		#region Public Static

		public static Image GetImage()
		{
			if (Clipboard.ContainsImage())
				return Clipboard.GetImage();
			return null;
		}

		public static bool ContainsImage()
		{
			return Clipboard.ContainsImage();
			//if (!Clipboard.ContainsImage()) return false;
			//var image = Clipboard.GetImage(); //This code virtually makes memory leak
			//return image != null;
		}

		public static void PutImage(Image image)
		{
			if (image == null) throw new ArgumentNullException("image");
			Clipboard.SetImage(image);
		}

		#endregion
	}
}