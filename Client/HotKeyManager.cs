using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JetFly
{
	public static class HotKeyManager
	{
		public static event EventHandler HotKeyPressedPost;
		public static event EventHandler HotKeyPressedCut;
		public static event EventHandler HotKeyPressedPost2;
		public static event EventHandler HotKeyPressedPreview;

		public static void ProcessHotKeyPress(Message msg)
		{
			if (msg.WParam.ToInt32() == HOTKEY_ID_PREVIEW && HotKeyPressedPreview != null)
				HotKeyPressedPreview(null, EventArgs.Empty);
			else if (msg.WParam.ToInt32() == HOTKEY_ID_POST && HotKeyPressedPost != null)
				HotKeyPressedPost(null, EventArgs.Empty);
			else if (msg.WParam.ToInt32() == HOTKEY_ID_CUT && HotKeyPressedCut != null)
				HotKeyPressedCut(null, EventArgs.Empty);
			else if (msg.WParam.ToInt32() == HOTKEY_ID_POST_2 && HotKeyPressedCut != null)
				HotKeyPressedPost2(null, EventArgs.Empty);
		}

		public static void RegisterHotKeys(IntPtr handle)
		{
			try
			{
				bool result = RegisterHotKey(handle, HOTKEY_ID_PREVIEW, KeyModifiers.Windows | KeyModifiers.Control, Keys.A);
				if (!result) throw new Win32Exception();

				result = RegisterHotKey(handle, HOTKEY_ID_POST, KeyModifiers.Windows | KeyModifiers.Control, Keys.PrintScreen);
				if (!result) throw new Win32Exception();

				result = RegisterHotKey(handle, HOTKEY_ID_CUT, KeyModifiers.Windows | KeyModifiers.Control, Keys.Q);
				if (!result) throw new Win32Exception();

				result = RegisterHotKey(handle, HOTKEY_ID_POST_2, KeyModifiers.Windows | KeyModifiers.Control, Keys.W);
				if (!result) throw new Win32Exception();
			}
			catch (Win32Exception)
			{
				MessageBox.Show("Не удалось инициализировать горячие клавиши", "JetFly", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		public static void UnregisterHotKeys(IntPtr handle)
		{
			UnregisterHotKey(handle, HOTKEY_ID_PREVIEW);
			UnregisterHotKey(handle, HOTKEY_ID_POST);
			UnregisterHotKey(handle, HOTKEY_ID_CUT);
			UnregisterHotKey(handle, HOTKEY_ID_POST_2);
		}

		// ReSharper disable InconsistentNaming
		private const int HOTKEY_ID_PREVIEW = 20102009;
		private const int HOTKEY_ID_CUT = 290184; //;)
		private const int HOTKEY_ID_POST_2 = 090545;
		private const int HOTKEY_ID_POST = 151182; //any number to be used as an id within this app
		// ReSharper restore InconsistentNaming

		#region WinAPI

		// ReSharper disable InconsistentNaming
		public const int WM_HOTKEY = 0x0312;
		// ReSharper restore InconsistentNaming

		/// <summary>
		/// WinAPI function to register system scope hotkeys
		/// </summary>
		/// <param name="hWnd">Window handle to send WM_HOTKEY message</param>
		/// <param name="id">Application scope unique hotkey identifier</param>
		/// <param name="fsModifiers">CtrlAltShiftWin modifier</param>
		/// <param name="vk">Virtual key code to assign a hotkey</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

		/// <summary>
		/// WinAPI function to unregister system scope hotkeys
		/// </summary>
		/// <param name="hWnd">Window handle</param>
		/// <param name="id">Application scope unique hotkey identifier</param>
		/// <returns></returns>
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		/// <summary>
		/// Enum to call 3rd parameter of RegisterHotKey easily
		/// </summary>
		// ReSharper disable UnusedMember.Local
		[Flags]
		private enum KeyModifiers
		{
			None = 0,
			Alt = 1,
			Control = 2,
			Shift = 4,
			Windows = 8
		}
		// ReSharper restore UnusedMember.Local
		
		#endregion
	}
}