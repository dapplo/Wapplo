using System.Collections.Generic;

namespace Wapplo.ShareContext
{
	/// <summary>
	/// The actual context which is shared among all subscribers
	/// </summary>
	public class SharingContext
	{
		/// <summary>
		/// The application which shared this context
		/// </summary>
		public string Origin { get; set; }

		/// <summary>
		/// A map of name and value pairs
		/// </summary>
		public IDictionary<string, string> Values { get; set; }

	}
}
