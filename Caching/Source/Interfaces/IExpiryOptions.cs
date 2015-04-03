using System;

namespace Interfaces
{
	/// <summary>
	/// Provides the options accessible on cache item expiry, ie. to revive or replace the item. If no action is taken the item is removed from the cache.
	/// </summary>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public interface IExpiryOptions<TValue>
	{
		/// <summary>
		/// Replaces the value in the cache and sets expiry to default expiry measured from the time of the call to this method.
		/// </summary>
		/// <param name="updateValueFactory">The delegate providing the update value.</param>
		void Replace(Func<TValue, TValue> updateValueFactory);
	}
}
