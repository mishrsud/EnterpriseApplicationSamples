using System;

namespace Implementation
{
	/// <summary>
	/// Represents a hook to a cache item in the usage queue.
	/// </summary>
	/// <remarks>
	/// The hook uses a simple mechanism of mutual recognition, so that the cache item also has to point back to this hook for them to be connected.
	/// When a cache item is used and new hook for is added to the end of the usage queue, all other hooks referencing it are thus automatically 
	/// being disconnected and will become orphan. The hook is using a <see cref="WeakReference"/> to reference the cache item, so that it does not 
	/// hold on the instance in any way by itself.
	/// </remarks>
	/// <typeparam name="TKey">The key type for the cache.</typeparam>
	/// <typeparam name="TValue">The value type for the cache.</typeparam>
	internal class Hook<TKey, TValue>
	{
		private readonly WeakReference _cacheItemWeakReference;

		/// <summary>
		/// Creates a new <see cref="Hook{TKey,TValue}"/> instance.
		/// </summary>
		/// <param name="cacheItem">The cache item referenced by this hook.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="cacheItem"/> is null.</exception>
		public Hook(CacheItem<TKey, TValue> cacheItem)
		{
			if (cacheItem == null) throw new ArgumentNullException("cacheItem");
			_cacheItemWeakReference = new WeakReference(cacheItem);
		}

		/// <summary>
		/// Gets the cache item for this hook.
		/// </summary>
		/// <remarks>
		/// Mutual recognition is used for evaluation, so if the the cache item does not reference this hook instance, null is returned.
		/// </remarks>
		public CacheItem<TKey, TValue> CacheItem
		{
			get
			{
				if (!_cacheItemWeakReference.IsAlive || _cacheItemWeakReference.Target == null)
					return null;
				var cacheItem = (CacheItem<TKey, TValue>)_cacheItemWeakReference.Target;
				return cacheItem.Hook == this ? cacheItem : null;
			}
		}
	}
}
