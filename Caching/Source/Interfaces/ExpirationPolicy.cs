namespace Smi.Caching.Interfaces
{
	/// <summary>
	/// Options for expiration policy.
	/// </summary>
	public enum ExpirationPolicy
	{
		/// <summary>
		/// No expiration is used.
		/// </summary>
		None,

		/// <summary>
		/// Items are expired based on usage. When an item is used (get or update), expiration for that item is reset.
		/// </summary>
		Sliding,

		/// <summary>
		/// Items are expired based only on when they were added to the cache.
		/// </summary>
		Absolute
	}
}