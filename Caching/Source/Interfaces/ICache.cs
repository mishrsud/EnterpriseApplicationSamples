using System;

namespace Interfaces
{
	/// <summary>
	/// Responsible for caching items of a specific types.
	/// </summary>
	public interface ICache<TKey, TValue>
	{
		/// <summary>
		/// Gets the expiration for items in this cache.
		/// </summary>
		TimeSpan Expiration { get; }

		/// <summary>
		/// Gets the expiration policy for the cache.
		/// </summary>
		ExpirationPolicy ExpirationPolicy { get; }

		/// <summary>
		/// Gets the maximum number of items in the cache until the oldest items measured by usage is evicted to get under the threshold.
		/// </summary>
		int Threshold { get; }

		/// <summary>
		/// The interval between scavenging operations are performed.
		/// </summary>
		TimeSpan PollingInterval { get; }

		/// <summary>
		/// Gets an item if key is found in the cache, or falls back to the value factory and adds the value to the cache.
		/// </summary>
		/// <param name="key">The key to find.</param>
		/// <param name="valueFactory">The delegate providing the value if key is not found in the cache.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="valueFactory"/> is null.</exception>
		TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);

		/// <summary>
		/// Adds an item to the cache, if the key is not found, or updates the value if it is.
		/// </summary>
		/// <param name="key">The key to find.</param>
		/// <param name="addValueFactory">The delegate providing the value if the key is not found.</param>
		/// <param name="updateValueFactory">The delegate providing the update value if the key is found.</param>
		/// <returns>The value after add or update was performed.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="addValueFactory"/> or <paramref name="updateValueFactory"/> is null.</exception>
		TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);

		/// <summary>
		/// Tries to update an item in the cache with a new value, if the comparison value matches.
		/// </summary>
		/// <param name="key">The key for the item to be updated.</param>
		/// <param name="newValue">The new value for if the comparison value matches.</param>
		/// <param name="comparisonValue">The value to compare the existing value with.</param>
		/// <returns>True if key was found, comparison value was equal to existing and update succeeded, otherwise false.</returns>
		bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue);

		/// <summary>
		/// Tries to get an item from the cache.
		/// </summary>
		/// <param name="key">The key to find.</param>
		/// <param name="value">The value if found.</param>
		/// <returns>True if the item was found, otherwise false.</returns>
		bool TryGet(TKey key, out TValue value);

		/// <summary>
		/// Clears all items in the cache.
		/// </summary>
		void Clear();

		/// <summary>
		/// Sets the action called when items in the cache expires.
		/// </summary>
		/// <param name="expiryAction">The action to be called on expiry.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="expiryAction"/> is null.</exception>
		void OnExpiry(Action<IExpiryOptions<TValue>> expiryAction);
	}
}
