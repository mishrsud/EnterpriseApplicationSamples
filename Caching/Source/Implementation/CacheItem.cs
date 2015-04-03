using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Providers;

namespace Implementation
{
	/// <summary>
	/// Represent an item holding a value in <see cref="Cache{TKey,TValue}"/>.
	/// </summary>
	/// <typeparam name="TValue">The type the value this item can hold.</typeparam>
	/// <typeparam name="TKey"></typeparam>
	internal class CacheItem<TKey, TValue>
	{
		/// <summary>
		/// The key of this item.
		/// </summary>
		public TKey Key { get; private set; }

		/// <summary>
		/// The value of this item.
		/// </summary>
		public TValue Value { get; private set; }

		/// <summary>
		/// The current hook for this item.
		/// </summary>
		public Hook<TKey, TValue> Hook { get; set; }

		/// <summary>
		/// The last usage of this item.
		/// </summary>
		public UtcDateTime LastUsage { get; set; }

		/// <summary>
		/// Creates a new <see cref="CacheItem{TKey,TValue}"/> instance.
		/// </summary>
		/// <param name="key">The key of this item.</param>
		/// <param name="value">The value of this item.</param>
		internal CacheItem(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}
	}
}
