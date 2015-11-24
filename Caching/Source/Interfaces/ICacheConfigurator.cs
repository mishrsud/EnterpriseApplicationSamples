using System;

namespace Smi.Caching.Interfaces
{
	/// <summary>
	/// Configures a cache with expiration, threshold and polling interval.
	/// </summary>
	public interface ICacheConfigurator
	{
		/// <summary>
		/// Sets the amount of time before items in the cache are expired.
		/// </summary>
		/// <param name="expiration">The amount of time before items in the cache are expired. Must be a positive value or <see cref="TimeSpan.Zero"/>.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="expiration"/> is negative.</exception>
		ICacheConfigurator WithExpiration(TimeSpan expiration);

		/// <summary>
		/// Sets the expiration policy used by the cache.
		/// </summary>
		/// <param name="expirationPolicy">The expiration policy used by the cache.</param>
		/// <remarks>Setting policy to <see cref="ExpirationPolicy.None"/> resets expiration to <see cref="TimeSpan.Zero"/>.</remarks>
		ICacheConfigurator WithExpirationPolicy(ExpirationPolicy expirationPolicy);

		/// <summary>
		/// Sets the maximum number of items in the cache before the ones not used for the longest time is removed.
		/// </summary>
		/// <param name="maxItems">The maximum number of items in the cache. Must be a positive value.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="maxItems"/> is not positive.</exception>
		ICacheConfigurator WithThreshold(int maxItems);

		/// <summary>
		/// Sets the interval between scavenging (based on expiration as well as threshold) of the cache is done.
		/// </summary>
		/// <param name="interval">The interval between scavenging operations. Must be a positive value.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="interval"/> is negative or <see cref="TimeSpan.Zero"/>.</exception>
		ICacheConfigurator WithPollingInterval(TimeSpan interval);
	}
}
