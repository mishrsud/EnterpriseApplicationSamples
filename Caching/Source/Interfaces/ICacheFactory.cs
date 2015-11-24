using System;

namespace Smi.Caching.Interfaces
{
	/// <summary>
	/// Responsible for providing <see cref="ICache{TKey,TValue}"/> instances.
	/// </summary>
	public interface ICacheFactory
	{
		/// <summary>
		/// Creates a <see cref="ICache{TKey,TValue}"/> instance with default settings.
		/// </summary>
		/// <typeparam name="TKey">The type of the cache key.</typeparam>
		/// <typeparam name="TValue">The type of the cache value.</typeparam>
		ICache<TKey, TValue> Create<TKey, TValue>();

		/// <summary>
		/// Creates a <see cref="ICache{TKey,TValue}"/> instance based on the settings provided by the configurator action.
		/// </summary>
		/// <param name="configuratorAction">The action to which the configurator instance is passed to configure the cache.</param>
		/// <typeparam name="TKey">The type of the cache key.</typeparam>
		/// <typeparam name="TValue">The type of the cache value.</typeparam>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="configuratorAction"/> is null.</exception>
		ICache<TKey, TValue> Create<TKey, TValue>(Action<ICacheConfigurator> configuratorAction);
	}
}
