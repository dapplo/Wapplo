namespace Wapplo.ShareContext
{
	/// <summary>
	/// This is the interface which the client must implement
	/// </summary>
	public interface IShareContextClient
	{
		/// <summary>
		/// A context is shared
		/// </summary>
		/// <param name="sharingContext"></param>
		void ContextShared(SharingContext sharingContext);
	}
}
