namespace JetFly
{
	public interface IMagicClipboardListener
	{
		void OnUrlFromClipboard(string url);
		void OnClipboardContentChanged(bool imageDetected);
		void OnUploadStart();
	}
}