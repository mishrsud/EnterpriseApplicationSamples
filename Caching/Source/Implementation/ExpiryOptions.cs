using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smi.Caching.Interfaces;

namespace Smi.Caching.Implementation
{
	/// <summary>
	/// Provides the options accessible on cache item expiry, ie. to revive or replace the item. If no action is taken the item is removed from the cache.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	internal class ExpiryOptions<TKey, TValue> : IExpiryOptions<TValue>
	{
		private readonly ICache<TKey, TValue> _cache;
		private Action<CacheItem<TKey, TValue>> _expiryAction;

		/// <summary>
		/// Creates a new <see cref="ExpiryOptions{TKey,TValue}"/> instance.
		/// </summary>
		/// <param name="cache">The cache instance for this options instance.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="cache"/> is null.</exception>
		internal ExpiryOptions(ICache<TKey, TValue> cache)
		{
			if (cache == null) throw new ArgumentNullException("cache");
			_cache = cache;
		}

		/// <summary>
		/// Replaces the value in the cache and sets expiry to default expiry measured from the time of the call to this method.
		/// </summary>
		/// <param name="updateValueFactory">The delegate providing the update value.</param>
		public void Replace(Func<TValue, TValue> updateValueFactory)
		{
			if (updateValueFactory == null) throw new ArgumentNullException("updateValueFactory");
			_expiryAction = item => _cache.TryUpdate(item.Key, updateValueFactory(item.Value), item.Value);
		}

		/// <summary>
		/// Handles expiry if custom expiry action was set up for this instance.
		/// </summary>
		/// <param name="cacheItem">The cache item to handle expiry for.</param>
		/// <returns>True if expiry was handled by custom action, otherwise false.</returns>
		internal bool TryHandleExpiry(CacheItem<TKey, TValue> cacheItem)
		{
			var expiryAction = _expiryAction;
			if (expiryAction != null)
			{
				expiryAction(cacheItem);
				return true;
			}
			return false;
		}
	}
}
