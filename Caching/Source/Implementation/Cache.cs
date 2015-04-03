using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Interfaces;
using Providers;

namespace Implementation
{
	/// <summary>
	/// Responsible for caching items of a specific types.
	/// </summary>
	internal class Cache<TKey, TValue> : ICache<TKey, TValue>, IDisposable
	{
		private readonly TimeSpan _expiration;
		private readonly ExpirationPolicy _expirationPolicy;
		private readonly int _threshold;
		private readonly TimeSpan _pollingInterval;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly ITimer _timer;
		private readonly InterlockedInvocator _interlockedInvocator = new InterlockedInvocator();
		private readonly Lazy<ExpiryOptions<TKey, TValue>> _expiryOptionsLazy;
		private readonly ConcurrentDictionary<TKey, CacheItem<TKey, TValue>> _concurrentDictionary;
		private readonly ConcurrentQueue<Hook<TKey, TValue>> _usageQueue;

		/// <summary>
		/// Creates a new <see cref="Cache{TKey,TValue}"/> instance.
		/// </summary>
		/// <param name="expiration">The expiration for items in this cache. Must be <see cref="TimeSpan.Zero"/>, meaning no expiration, or positive. If <paramref name="expirationPolicy"/> is <see cref="Caching.ExpirationPolicy.None"/> the value must be <see cref="TimeSpan.Zero"/>.</param>
		/// <param name="expirationPolicy">The expiration policy for the cache.</param>
		/// <param name="threshold">The maximum number of items in the cache until the oldest items measured by usage is evicted to get under the threshold. Set it to <see cref="int.MaxValue"/> to not enforce a maximum size policy.</param>
		/// <param name="pollingInterval">The interval between scavenging based on expiration and maximum number items is performed.</param>
		/// <param name="timerProvider">The timer provider used by this cache instance.</param>
		/// <param name="dateTimeProvider">Provider of current date and time.</param>
		/// <param name="concurrentDictionary">The concurrent dictionary used for internal storage in the this cache instance.</param>
		/// <param name="usageQueue">The queue used for usage recordings, on which scavenging is later based.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="timerProvider"/>, <paramref name="dateTimeProvider"/>, <paramref name="concurrentDictionary"/> or <paramref name="usageQueue"/> is null.</exception>
		/// <exception cref="ArgumentException">Thrown if <paramref name="expiration"/> is negative, if <paramref name="threshold"/> or <paramref name="pollingInterval"/> is not positive, or if <paramref name="expirationPolicy"/> is <see cref="Caching.ExpirationPolicy.None"/> but <paramref name="expiration"/> is not <see cref="TimeSpan.Zero"/>.</exception>
		internal Cache(TimeSpan expiration, ExpirationPolicy expirationPolicy, int threshold, TimeSpan pollingInterval, ITimerProvider timerProvider, IDateTimeProvider dateTimeProvider, ConcurrentDictionary<TKey, CacheItem<TKey, TValue>> concurrentDictionary, ConcurrentQueue<Hook<TKey, TValue>> usageQueue)
		{
			if (timerProvider == null) throw new ArgumentNullException("timerProvider");
			if (dateTimeProvider == null) throw new ArgumentNullException("dateTimeProvider");
			if (concurrentDictionary == null) throw new ArgumentNullException("concurrentDictionary");
			if (usageQueue == null) throw new ArgumentNullException("usageQueue");
			if (expiration < TimeSpan.Zero) throw new ArgumentException("Value cannot be negative", "expiration");
			if (expirationPolicy == ExpirationPolicy.None && expiration != TimeSpan.Zero) throw new ArgumentException("Expiration must be Zero, when expiration policy is None", "expiration");
			if (threshold <= 0) throw new ArgumentException("Value must be positive", "threshold");
			if (pollingInterval <= TimeSpan.Zero) throw new ArgumentException("Value must be positive", "pollingInterval");
			_expiration = expiration;
			_expirationPolicy = expirationPolicy;
			_concurrentDictionary = concurrentDictionary;
			_dateTimeProvider = dateTimeProvider;
			_usageQueue = usageQueue;
			_threshold = threshold;
			_pollingInterval = pollingInterval;
			_expiryOptionsLazy = new Lazy<ExpiryOptions<TKey, TValue>>(
				() => new ExpiryOptions<TKey, TValue>(this),
				LazyThreadSafetyMode.ExecutionAndPublication
			);
			_timer = timerProvider.Create(x => x.SetInterval(pollingInterval).AutoReset().WhenElapsed(Scavenge));
			_timer.Start();
		}

		/// <summary>
		/// Gets the expiration for items in this cache.
		/// </summary>
		public TimeSpan Expiration
		{
			get { return _expiration; }
		}

		/// <summary>
		/// Gets the expiration policy for the cache.
		/// </summary>
		public ExpirationPolicy ExpirationPolicy
		{
			get { return _expirationPolicy; }
		}

		/// <summary>
		/// Gets the maximum number of items in the cache until the oldest items measured by usage is evicted to get under the threshold.
		/// </summary>
		public int Threshold
		{
			get { return _threshold; }
		}

		/// <summary>
		/// The interval between scavenging operations are performed.
		/// </summary>
		public TimeSpan PollingInterval
		{
			get { return _pollingInterval; }
		}

		/// <summary>
		/// Gets an item if key is found in the cache, or falls back to the value factory and adds the value to the cache.
		/// </summary>
		/// <param name="key">The key to find.</param>
		/// <param name="valueFactory">The delegate providing the value if key is not found in the cache.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="valueFactory"/> is null.</exception>
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			if (valueFactory == null) throw new ArgumentNullException("valueFactory");
			var isAdd = false;
			var item = _concurrentDictionary.GetOrAdd(key, k =>
			{
				isAdd = true;
				return new CacheItem<TKey, TValue>(key, valueFactory(key));
			});
			RecordUsage(item, isAdd);
			return item.Value;
		}

		/// <summary>
		/// Adds an item to the cache, if the key is not found, or updates the value if it is.
		/// </summary>
		/// <param name="key">The key to find.</param>
		/// <param name="addValueFactory">The delegate providing the value if the key is not found.</param>
		/// <param name="updateValueFactory">The delegate providing the update value if the key is found.</param>
		/// <returns>The value after add or update was performed.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="addValueFactory"/> or <paramref name="updateValueFactory"/> is null.</exception>
		public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (addValueFactory == null) throw new ArgumentNullException("addValueFactory");
			if (updateValueFactory == null) throw new ArgumentNullException("updateValueFactory");
			var result = _concurrentDictionary.AddOrUpdate(key, x => CreateCacheItem(key, addValueFactory),
											  (x, item) => CreateCacheItem(key, y => updateValueFactory(key, item.Value)));
			RecordUsage(result, true);
			return result.Value;
		}

		/// <summary>
		/// Tries to update an item in the cache with a new value, if the comparison value matches.
		/// </summary>
		/// <param name="key">The key for the item to be updated.</param>
		/// <param name="newValue">The new value for if the comparison value matches.</param>
		/// <param name="comparisonValue">The value to compare the existing value with.</param>
		/// <returns>True if key was found, comparison value was equal to existing and update succeeded, otherwise false.</returns>
		public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
		{
			CacheItem<TKey, TValue> existing;
			if (!_concurrentDictionary.TryGetValue(key, out existing) || !Equals(comparisonValue, existing.Value))
				return false;
			var cacheItem = CreateCacheItem(key, x => newValue);
			if (_concurrentDictionary.TryUpdate(key, cacheItem, existing))
			{
				RecordUsage(cacheItem, true);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Tries to get an item from the cache.
		/// </summary>
		/// <param name="key">The key to find.</param>
		/// <param name="value">The value if found.</param>
		/// <returns>True if the item was found, otherwise false.</returns>
		public bool TryGet(TKey key, out TValue value)
		{
			CacheItem<TKey, TValue> cacheItem;
			var isFound = _concurrentDictionary.TryGetValue(key, out cacheItem);
			if (isFound && cacheItem != null)
			{
				RecordUsage(cacheItem, false);
				value = cacheItem.Value;
				return true;
			}
			value = default(TValue);
			return false;
		}


		/// <summary>
		/// Clears all items in the cache.
		/// </summary>
		public void Clear()
		{
			_concurrentDictionary.Clear();
		}

		/// <summary>
		/// Sets the action called when items in the cache expires.
		/// </summary>
		/// <param name="expiryAction">The action to be called on expiry.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="expiryAction"/> is null.</exception>
		public void OnExpiry(Action<IExpiryOptions<TValue>> expiryAction)
		{
			if (expiryAction == null) throw new ArgumentNullException("expiryAction");
			expiryAction(_expiryOptionsLazy.Value);
		}

		private void RecordUsage(CacheItem<TKey, TValue> item, bool isAddOrUpdate)
		{
			if (item == null) throw new ArgumentNullException("item");
			// Except for sliding expiration we will only record usage on add or update
			if (!isAddOrUpdate && _expirationPolicy != ExpirationPolicy.Sliding)
				return;
			var hook = new Hook<TKey, TValue>(item);
			item.Hook = hook;
			item.LastUsage = _dateTimeProvider.UtcDateTimeNow;
			_usageQueue.Enqueue(hook);
		}

		private void Scavenge()
		{
			_interlockedInvocator.TryInvoke(() =>
			{
				Hook<TKey, TValue> hook;
				CacheItem<TKey, TValue> cacheItem = null;

				// Remove expired
				if (_expiration > TimeSpan.Zero)
				{
					var expiry = _dateTimeProvider.UtcDateTimeNow.Subtract(_expiration);
					while (TryDequeueIf(x => (cacheItem = x.CacheItem) == null || cacheItem.LastUsage < expiry, out hook))
					{
						if (cacheItem != null)
							RemoveItem(cacheItem);
					}
				}

				// Remove excess
				while (_concurrentDictionary.Count > _threshold && _usageQueue.TryDequeue(out hook))
				{
					cacheItem = hook.CacheItem;
					if (cacheItem != null)
						RemoveItem(cacheItem);
				}
			});
		}

		private bool TryDequeueIf(Predicate<Hook<TKey, TValue>> predicate, out Hook<TKey, TValue> dequeued)
		{
			if (predicate == null) throw new ArgumentNullException("predicate");
			return _usageQueue.TryPeek(out dequeued) && predicate(dequeued) && _usageQueue.TryDequeue(out dequeued);
		}


		private void RemoveItem(CacheItem<TKey, TValue> cacheItem)
		{
			if (cacheItem == null) throw new ArgumentNullException("cacheItem");

			if (_threshold == int.MaxValue && _expiryOptionsLazy.Value.TryHandleExpiry(cacheItem))
				return;

			if (_concurrentDictionary.TryRemove(cacheItem.Key, out cacheItem))
				cacheItem.Hook = null;
		}

		private static CacheItem<TKey, TValue> CreateCacheItem(TKey key, Func<TKey, TValue> valueFactory)
		{
			return new CacheItem<TKey, TValue>(key, valueFactory(key));
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposes the instance.
		/// </summary>
		/// <param name="disposing">Boolean indicating whether managed resources should be disposed or not.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_timer.Dispose();
			}
		}
	}
}
