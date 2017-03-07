using System;
using System.Runtime.InteropServices;

namespace Wapplo.Utils
{
	/// <summary>
	///     A simple console output helper
	/// </summary>
	public static class ConsoleOutput
	{
		private const uint AttachParentProcess = 0x0ffffffff; // default value if not specifing a process ID
		private static bool _attached;

		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AllocConsole();

		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AttachConsole(uint dwProcessId);

		/// <summary>
		/// Close the Console
		/// </summary>
		public static void Close()
		{
			if (!_attached)
			{
				Console.ReadLine();
			}
			FreeConsole();
		}

		[DllImport("kernel32", SetLastError = true)]
		private static extern bool FreeConsole();

		/// <summary>
		/// Show the console
		/// </summary>
		/// <param name="text"></param>
		public static void Show(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			_attached = AttachConsole(AttachParentProcess);
			if (!_attached)
			{
				AllocConsole();
			}
			Console.Error.WriteLine(text);
			Console.Error.Flush();
		}
	}
}
