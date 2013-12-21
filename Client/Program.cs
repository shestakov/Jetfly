using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace JetFly
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			SetRunOnSystemStartUp();
			Application.Run(new SettingsForm());
		}

		private static void SetRunOnSystemStartUp()
		{
			var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
			if (key == null) return;
			try
			{
				var location = Assembly.GetExecutingAssembly().Location;
				key.SetValue("MagicClipboard", location, RegistryValueKind.String);
			}
			finally
			{
				key.Close();
			}
		}
	}
}