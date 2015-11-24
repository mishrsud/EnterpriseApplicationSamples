using System;
using System.Collections.Concurrent;
using Smi.Caching.Interfaces;
using Smi.Caching.Providers;

namespace Smi.Caching.Implementation
{
	/// <summary>
	/// Configures a cache with expiration, threshold and polling interval.
	/// </summary>
	internal class CacheConfigurator<TKey, TValue> : ICacheConfigurator
	{
		private TimeSpan _expiration = TimeSpan.Zero;
		private ExpirationPolicy _expirationPolicy = ExpirationPolicy.None;
		private int _threshold = int.MaxValue;
		private TimeSpan _pollingInterval = TimeSpan.FromMilliseconds(500);

		/// <summary>
		/// Sets the amount of time before items in the cache are expired.
		/// </summary>
		/// <param name="expiration">The amount of time before items in the cache are expired. Must be a positive value or <see cref="TimeSpan.Zero"/>.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="expiration"/> is negative.</exception>
		public ICacheConfigurator WithExpiration(TimeSpan expiration)
		{
			if (expiration < TimeSpan.Zero)
				throw new ArgumentException("Value cannot be negative", "expiration");
			_expiration = expiration;
			return this;
		}

		/// <summary>
		/// Sets the expiration policy used by the cache.
		/// </summary>
		/// <param name="expirationPolicy">The expiration policy used by the cache.</param>
		/// <remarks>Setting policy to <see cref="ExpirationPolicy.None"/> resets expiration to <see cref="TimeSpan.Zero"/>.</remarks>
		public ICacheConfigurator WithExpirationPolicy(ExpirationPolicy expirationPolicy)
		{
			_expirationPolicy = expirationPolicy;
			return this;
		}

		/// <summary>
		/// Sets the maximum number of items in the cache before the ones not used for the longest time is removed.
		/// </summary>
		/// <param name="maxItems">The maximum number of items in the cache. Must be a positive value.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="maxItems"/> is not positive.</exception>
		public ICacheConfigurator WithThreshold(int maxItems)
		{
			if (maxItems <= 0)
				throw new ArgumentException("Value must be positive", "maxItems");
			_threshold = maxItems;
			return this;
		}

		/// <summary>
		/// Sets the interval between scavenging (based on expiration as well as threshold) of the cache is done.
		/// </summary>
		/// <param name="interval">The interval between scavenging operations. Must be a positive value.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="interval"/> is negative or <see cref="TimeSpan.Zero"/>.</exception>
		public ICacheConfigurator WithPollingInterval(TimeSpan interval)
		{
			if (interval <= TimeSpan.Zero)
				throw new ArgumentException("Value must be positive", "interval");
			_pollingInterval = interval;
			return this;
		}

		/// <summary>
		/// Creates a <see cref="CacheItem{TKey,TValue}"/> instance based on the settings in the configurator and the parameters passed to this method.
		/// </summary>
		/// <param name="timerProvider">The timer provider used by the cache being returned.</param>
		/// <param name="dateTimeProvider">Provider of current date and time.</param>
		internal Cache<TKey, TValue> CreateCache(ITimerProvider timerProvider, IDateTimeProvider dateTimeProvider)
		{
			if (timerProvider == null) throw new ArgumentNullException("timerProvider");
			if (dateTimeProvider == null) throw new ArgumentNullException("dateTimeProvider");

			var expiration = _expirationPolicy == ExpirationPolicy.None ? TimeSpan.Zero : _expiration;
			return new Cache<TKey, TValue>(
				expiration,
				_expirationPolicy,
				_threshold,
				_pollingInterval,
				timerProvider,
				dateTimeProvider,
				new ConcurrentDictionary<TKey, CacheItem<TKey, TValue>>(),
				new ConcurrentQueue<Hook<TKey, TValue>>()
			);
		}
	}
}
