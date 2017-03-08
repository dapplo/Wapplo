namespace Wapplo.WindowsServices.Hub
{
	/// <summary>
	/// Windows services made available to clients
	/// </summary>
	public interface IWindowsServicesServer
	{
		/// <summary>
		/// Copy text to the clipboard
		/// </summary>
		/// <param name="origin">Where does the clipboard content come from</param>
		/// <param name="text">String to place onto the clipboard</param>
		void CopyToClipboard(string origin, string text);

		/// <summary>
		/// Enable or disable clipboard monitoring for the client
		/// </summary>
		/// <param name="enable">bool to enable / disable clipboard monitoring for the client</param>
		void MonitorClipboard(bool enable);
	}
}
