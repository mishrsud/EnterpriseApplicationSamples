using System;
using Smi.Caching.Interfaces;
using Smi.Caching.Providers;

namespace Smi.Caching.Implementation
{
	/// <summary>
	/// Responsible for providing <see cref="ICache{TKey,TValue}"/> instances.
	/// </summary>
	public class CacheFactory : ICacheFactory
	{
		private readonly ITimerProvider _timerProvider;
		private readonly IDateTimeProvider _dateTimeProvider;

		/// <summary>
		/// Creates a new <see cref="CacheFactory"/> instance.
		/// </summary>
		/// <param name="timerProvider">The timer provider used by the cache instances being returned.</param>
		/// <param name="dateTimeProvider">Provider of current date and time.</param>
		public CacheFactory(ITimerProvider timerProvider, IDateTimeProvider dateTimeProvider)
		{
			if (timerProvider == null) throw new ArgumentNullException("timerProvider");
			if (dateTimeProvider == null) throw new ArgumentNullException("dateTimeProvider");
			_timerProvider = timerProvider;
			_dateTimeProvider = dateTimeProvider;
		}

		/// <summary>
		/// Creates a <see cref="ICache{TKey,TValue}"/> instance with default settings.
		/// </summary>
		/// <typeparam name="TKey">The type of the cache key.</typeparam>
		/// <typeparam name="TValue">The type of the cache value.</typeparam>
		public ICache<TKey, TValue> Create<TKey, TValue>()
		{
			return Create<TKey, TValue>(c => { });
		}

		/// <summary>
		/// Creates a <see cref="ICache{TKey,TValue}"/> instance based on the settings provided by the configurator action.
		/// </summary>
		/// <param name="configuratorAction">The action to which the configurator instance is passed to configure the cache.</param>
		/// <typeparam name="TKey">The type of the cache key.</typeparam>
		/// <typeparam name="TValue">The type of the cache value.</typeparam>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="configuratorAction"/> is null.</exception>
		public ICache<TKey, TValue> Create<TKey, TValue>(Action<ICacheConfigurator> configuratorAction)
		{
			if (configuratorAction == null) throw new ArgumentNullException("configuratorAction");
			var configurator = new CacheConfigurator<TKey, TValue>();
			configuratorAction(configurator);
			return configurator.CreateCache(_timerProvider, _dateTimeProvider);
		}
	}
}
