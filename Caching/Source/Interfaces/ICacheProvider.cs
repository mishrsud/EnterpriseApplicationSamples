using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Providers;

namespace Interfaces
{
	public interface ICacheProvider : IEnumerable<KeyValuePair<string, object>>
	{
		/// <summary>
		/// Gets the number of items in the cache.
		/// </summary>
		long Count { get; }

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		bool Add(string key, object value);

		/// <summary>
		/// Adds an item to the cache with the specified key with an absolute expiration.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		bool Add(string key, object value, UtcDateTime absoluteExpiration);

		/// <summary>
		/// Adds an item to the cache with the specified key with a sliding expiration.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		bool Add(string key, object value, TimeSpan slidingExpiration);

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		/// <param name="priority"></param>
		/// <param name="afterRemovedCallback"></param>
		bool Add(string key, object value, UtcDateTime absoluteExpiration, CacheItemPriority priority,
				 CacheEntryRemovedCallback afterRemovedCallback);

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		/// <param name="priority"></param>
		/// <param name="afterRemovedCallback"></param>
		bool Add(string key, object value, TimeSpan slidingExpiration, CacheItemPriority priority,
				 CacheEntryRemovedCallback afterRemovedCallback);


		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		///<param name="onUpdateCallback">the callback to be called before item is expired and removed from the cache.</param>
		///<returns>A boolean indicating whether the item was added succesfully to the cache or not.</returns>
		bool Add(string key, object value, TimeSpan slidingExpiration, CacheEntryUpdateCallback onUpdateCallback);

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		///<param name="onUpdateCallback">the callback to be called before item is expired and removed from the cache.</param>
		///<returns>A boolean indicating whether the item was added succesfully to the cache or not.</returns>
		bool Add(string key, object value, UtcDateTime absoluteExpiration, CacheEntryUpdateCallback onUpdateCallback);

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		void AddOrReplace(string key, object value);

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		void AddOrReplace(string key, object value, UtcDateTime absoluteExpiration);

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		void AddOrReplace(string key, object value, TimeSpan slidingExpiration);

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		/// <param name="onUpdateCallback">the callback to be called before item is expired and removed from the cache.</param>
		void AddOrReplace(string key, object value, TimeSpan slidingExpiration, CacheEntryUpdateCallback onUpdateCallback);

		/// <summary>
		/// Gets an item, with the specified key, from the cache.
		/// </summary>
		/// <typeparam name="T">The type of the cache item that will be returned.</typeparam>
		/// <param name="key">The key under which the item was cached.</param>
		/// <returns>
		/// An object of type T, if it exists in the cache, otherwise null.
		/// </returns>
		T Get<T>(string key);

		/// <summary>
		/// Tries to get an item, with the specified key, from the cache.
		/// </summary>
		/// <typeparam name="T">The type of the cache item that will be returned.</typeparam>
		/// <param name="key">The key under which the item was cached.</param>
		/// <param name="result">The item from the cache if found.</param>
		/// <returns>
		/// A boolean indicating whether the key was found in the cache or not.
		/// </returns>
		bool TryGet<T>(string key, out T result);

		/// <summary>
		/// Flushes an item, with the specified key, from the cache.
		/// </summary>
		/// <typeparam name="T">The type that the removed item will be cast to.</typeparam>
		/// <param name="key">Unique key of the item to flush.</param>
		/// <returns>
		/// The removed item.
		/// </returns>
		T Remove<T>(string key);

		/// <summary>
		/// Flushes all cached items.
		/// </summary>
		void Clear();
	}
}
