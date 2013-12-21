using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace JetFlyService_Test
{
	[TestFixture]
	public class JetFlyService_Test
	{
		[Test]
		public void HttpPostTest()
		{
			const string targetUrl = "http://localhost:59828/PostImage.ashx";
//			const string targetUrl = "http://alexshestakov.com/JetFly/PostImage.ashx";
			var bitmap = new Bitmap(@"Images\CarnivoreMushrooms.jpg");

			string resultUrl;
			using (var stream = new MemoryStream())
			{
				bitmap.Save(stream, ImageFormat.Png);
				var serializedJpegBitmap = stream.GetBuffer();
				var client = new WebClient();
				client.Headers.Add("Content-Type", "image/png");
				var response = client.UploadData(targetUrl, "POST", serializedJpegBitmap);
				resultUrl = System.Text.Encoding.UTF8.GetString(response);
			}

			Console.WriteLine(resultUrl);
		}
	}
}
