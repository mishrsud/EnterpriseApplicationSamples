using System;
using System.Threading;

namespace Implementation
{
	/// <summary>
	/// Guarantees that an action is only invoked by one thread by using a <see cref="SpinLock"/> to synchronize threads.
	/// </summary>
	public class InterlockedInvocator
	{
		private readonly object _syncRoot = new object();

		/// <summary>
		/// Invokes the action passed to this method, if other threads are not already during invocation of an action. 
		/// If another thread already obtained the lock, the call on the current thread will simply exit without invoking the action.
		/// </summary>
		/// <param name="action">The action to invoke.</param>
		/// <returns>True if lock was obtained and invocation succeeded, otherwise false.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
		public bool TryInvoke(Action action)
		{
			if (action == null) throw new ArgumentNullException("action");
			if (Monitor.TryEnter(_syncRoot))
			{
				try
				{
					action();
					return true;
				}
				finally
				{
					Monitor.Exit(_syncRoot);
				}
			}
			return false;
		}
	}
}
