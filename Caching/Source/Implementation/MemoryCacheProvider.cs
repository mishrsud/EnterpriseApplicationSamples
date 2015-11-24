using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Caching;
using Smi.Caching.Interfaces;
using Smi.Caching.Providers;

namespace Smi.Caching.Implementation
{
	/// <summary>
	/// Provides a <see cref="System.Runtime.Caching.MemoryCache"/>.
	/// </summary>
	public class MemoryCacheProvider : ICacheProvider, IDisposable
	{
		private readonly MemoryCache _cache;
		private static readonly object s_nullValue = new object();

		#region Constructors

		/// <summary>
		/// Creates an instance of the <see cref="MemoryCacheProvider" />.
		/// </summary>
		/// <param name="guidProvider">The GUID provider.</param>
		public MemoryCacheProvider(IGuidProvider guidProvider)
		{
			if (guidProvider == null) throw new ArgumentNullException("guidProvider");

			// Note that the constructor directly instantiates a <see cref="MemoryCache"/> which
			// makes it hard to unit test. This should be dependency injected instead.
			// However, getting an IoC container to instantiate a memory cache is rather involved due
			// to it's dependencies on NameValueCollections and other stuff.
			// The provider is a mere wrapper and is therefore not crucial to unit test.

			_cache = new MemoryCache(guidProvider.NewGuid().ToString());
		}

		#endregion

		#region ICacheProvider Members

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		public bool Add(string key, object value)
		{
			return _cache.Add(key, NullSafe(value), new CacheItemPolicy());
		}

		/// <summary>
		/// Adds an item to the cache with the specified key with an absolute expiration.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		public bool Add(string key, object value, UtcDateTime absoluteExpiration)
		{
			return _cache.Add(key, NullSafe(value), absoluteExpiration.ToDateTime());
		}

		/// <summary>
		/// Adds an item to the cache with the specified key with a sliding expiration.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		public bool Add(string key, object value, TimeSpan slidingExpiration)
		{
			return _cache.Add(key, NullSafe(value), new CacheItemPolicy
			{
				SlidingExpiration = slidingExpiration,
			});
		}

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		/// <param name="priority"></param>
		/// <param name="afterRemovedCallback"></param>
		/// <returns></returns>
		public bool Add(string key, object value, UtcDateTime absoluteExpiration, CacheItemPriority priority, CacheEntryRemovedCallback afterRemovedCallback)
		{
			return _cache.Add(key, NullSafe(value), new CacheItemPolicy
			{
				AbsoluteExpiration = absoluteExpiration.ToDateTime(),
				Priority = priority,
				RemovedCallback = afterRemovedCallback
			});
		}

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		/// <param name="priority"></param>
		/// <param name="afterRemovedCallback"></param>
		/// <returns></returns>
		public bool Add(string key, object value, TimeSpan slidingExpiration, CacheItemPriority priority, CacheEntryRemovedCallback afterRemovedCallback)
		{
			return _cache.Add(key, NullSafe(value), new CacheItemPolicy
			{
				SlidingExpiration = slidingExpiration,
				Priority = priority,
				RemovedCallback = afterRemovedCallback
			});
		}

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		///<param name="onUpdateCallback">the callback to be called before item is expired and removed from the cache.</param>
		///<returns>A boolean indicating whether the item was added succesfully to the cache or not.</returns>
		public bool Add(string key, object value, TimeSpan slidingExpiration, CacheEntryUpdateCallback onUpdateCallback)
		{
			_cache.Set(
				key,
				NullSafe(value),
				new CacheItemPolicy
				{
					SlidingExpiration = slidingExpiration,
					UpdateCallback = onUpdateCallback
				});
			return true;
		}

		/// <summary>
		/// Adds an item to the cache with the specified key.
		/// </summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		/// <param name="onUpdateCallback">the callback to be called before item is expired and removed from the cache.</param>
		/// <returns>A boolean indicating whether the item was added succesfully to the cache or not.</returns>
		public bool Add(string key, object value, UtcDateTime absoluteExpiration, CacheEntryUpdateCallback onUpdateCallback)
		{
			_cache.Set(
				key,
				NullSafe(value),
				new CacheItemPolicy
				{
					AbsoluteExpiration = absoluteExpiration.ToDateTime(),
					UpdateCallback = onUpdateCallback
				});
			return true;
		}

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		public void AddOrReplace(string key, object value)
		{
			_cache.Set(new CacheItem(key, NullSafe(value)), new CacheItemPolicy());
		}

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="absoluteExpiration">The date and time when the cached item must expire.</param>
		public void AddOrReplace(string key, object value, UtcDateTime absoluteExpiration)
		{
			_cache.Set(
				new CacheItem(key, NullSafe(value)),
				new CacheItemPolicy
				{
					AbsoluteExpiration = absoluteExpiration.ToDateTime()
				});
		}

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		public void AddOrReplace(string key, object value, TimeSpan slidingExpiration)
		{
			_cache.Set(
				new CacheItem(key, NullSafe(value)),
				new CacheItemPolicy
				{
					SlidingExpiration = slidingExpiration,
				});
		}

		///<summary>
		/// Adds an item if the key is not found or replaces the existing if found.
		///</summary>
		/// <param name="key">The unique key of the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="slidingExpiration">The duration after which the cached item must be flushed, if not touched.</param>
		/// <param name="onUpdateCallback">the callback to be called before item is expired and removed from the cache.</param>
		public void AddOrReplace(string key, object value, TimeSpan slidingExpiration, CacheEntryUpdateCallback onUpdateCallback)
		{
			_cache.Set(
				new CacheItem(key, NullSafe(value)),
				new CacheItemPolicy
				{
					SlidingExpiration = slidingExpiration,
					UpdateCallback = onUpdateCallback
				});
		}

		/// <summary>
		/// Gets an item, with the specified key, from the cache.
		/// </summary>
		/// <typeparam name="T">The type of the cache item that will be returned.</typeparam>
		/// <param name="key">The key under which the item was cached.</param>
		/// <returns>
		/// An object of type T, if it exists in the cache, otherwise null.
		/// </returns>
		public T Get<T>(string key)
		{
			var result = ReverseNullSafe(_cache.Get(key));
			return result is T ? (T)result : default(T);
		}

		/// <summary>
		/// Tries to get an item, with the specified key, from the cache.
		/// </summary>
		/// <typeparam name="T">The type of the cache item that will be returned.</typeparam>
		/// <param name="key">The key under which the item was cached.</param>
		/// <param name="result">The item from the cache if found.</param>
		/// <returns>
		/// A boolean indicating whether the key was found in the cache or not.
		/// </returns>
		public bool TryGet<T>(string key, out T result)
		{
			var value = _cache.Get(key);
			if (value == null)
			{
				result = default(T);
				return false;
			}

			var resultAsObject = ReverseNullSafe(value);
			result = resultAsObject is T ? (T)resultAsObject : default(T);
			return true;
		}

		/// <summary>
		/// Flushes an item, with the specified key, from the cache.
		/// </summary>
		/// <typeparam name="T">The type that the removed item will be cast to.</typeparam>
		/// <param name="key">Unique key of the item to flush.</param>
		/// <returns>The removed item.</returns>
		public T Remove<T>(string key)
		{
			return (T)_cache.Remove(key);
		}

		/// <summary>
		/// Flushes all cached items.
		/// </summary>
		public void Clear()
		{
			_cache.Trim(100);
		}

		/// <summary>
		/// Gets the number of items in the cache.
		/// </summary>
		/// <value></value>
		public long Count
		{
			get { return _cache.GetCount(); }
		}

		/// <summary>
		/// Gets enumerator for the cache.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)_cache).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_cache).GetEnumerator();
		}

		#endregion

		#region Implementation of IDisposable

		/// <summary>
		/// Disposes the object.
		/// </summary>
		/// <param name="disposing">True if called by
		/// the public Dispose method.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free unmanaged resources when explicitly called
				_cache.Dispose();
			}
		}

		/// <summary>
		/// Disposes the object.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		private static object NullSafe(object value)
		{
			return value ?? s_nullValue;
		}

		private static object ReverseNullSafe(object value)
		{
			return value == s_nullValue ? null : value;
		}
	}
}
