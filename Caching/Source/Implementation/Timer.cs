using System;
using Smi.Caching.Interfaces;

namespace Smi.Caching.Implementation
{
	/// <summary>
	/// Responsible for triggering a timed event.
	/// </summary>
	internal class Timer : ITimer
	{
		private readonly System.Timers.Timer _innerTimer;

		/// <summary>
		/// Creates a new <see cref="Timer"/> instance.
		/// </summary>
		/// <param name="innerTimer">The inner timer to use to trigger events.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="innerTimer"/> is null.</exception>
		internal Timer(System.Timers.Timer innerTimer)
		{
			if (innerTimer == null) throw new ArgumentNullException("innerTimer");
			_innerTimer = innerTimer;
		}

		/// <summary>
		/// Starts the timer.
		/// </summary>
		public void Start()
		{
			_innerTimer.Start();
		}

		/// <summary>
		/// Stops the timer.
		/// </summary>
		public void Stop()
		{
			_innerTimer.Stop();
		}

		/// <summary>
		/// Resets the timer, meaning it will start over from 0.
		/// </summary>
		public void Reset()
		{
			_innerTimer.Stop();
			_innerTimer.Start();
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
				_innerTimer.Dispose();
			}
		}
	}
}
