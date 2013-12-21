using System;
using System.IO;
using System.Text;

namespace JetFly
{
	public class ImageUrlLog
	{
		private StreamWriter streamWriter;

		public bool Initialized { get; private set; }

		public bool Start(string logFileName)
		{
			try
			{
				streamWriter = new StreamWriter(logFileName, true, Encoding.UTF8);
				Initialized = true;
			}
			catch
			{
				Initialized = false;
			}
			return Initialized;
		}

		public void Stop()
		{
			if (streamWriter == null) return;
			streamWriter.Close();
			streamWriter = null;
		}

		public void LogImageUrl(string imageUrl)
		{
			try
			{
				if (streamWriter == null)
					throw new Exception("Log not started");
				streamWriter.WriteLine(imageUrl);
				streamWriter.Flush();
			}
			catch
			{
			}
		}
	}
}
