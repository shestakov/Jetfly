using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Services;

namespace JetFlyService
{
	[WebService(Namespace = "http://alexshestakov.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class PostImage : IHttpHandler
	{

		#region IHttpHandler Members

		public bool IsReusable
		{
			get { return false; }
		}

		public void ProcessRequest(HttpContext context)
		{
			var request = context.Request;
			if (request.ContentType != "image/png")
				throw new HttpException(415, "Invalid content type");
			if (request.ContentLength > 10485760)
				throw new HttpException(406, "Image can not be of size more than 10MB");

			var decodedImage = new byte[request.ContentLength];
			request.InputStream.Read(decodedImage, 0, request.ContentLength);

			try
			{
				using (var stream = new MemoryStream(decodedImage))
				{
					new Bitmap(stream);
				}
			}
			catch (Exception)
			{
				throw new HttpException(415, "Image is not valid");
			}

			var uniqueFileName = Guid.NewGuid().ToString("N") + ".png";
			var fileName = context.Request.PhysicalApplicationPath + @"Images\" + uniqueFileName;
			using (var fileStream = new FileStream(fileName, FileMode.CreateNew))
			{
				fileStream.Write(decodedImage, 0, decodedImage.Length);
			}

			const string baseUrl = "http://alexshestakov.com/JetFly/Images/";
			byte[] fileUrl = System.Text.Encoding.UTF8.GetBytes(baseUrl + uniqueFileName);
			context.Response.BinaryWrite(fileUrl);
		}

		#endregion
	}
}
