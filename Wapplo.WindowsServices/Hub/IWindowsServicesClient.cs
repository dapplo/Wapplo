using System.Collections.Generic;

namespace Wapplo.WindowsServices.Hub
{
	/// <summary>
	/// Methods which clients "can" implement
	/// </summary>
	public interface IWindowsServicesClient
	{
		void ClipboardChanged(IList<string> clipboardFormats);
	}
}
