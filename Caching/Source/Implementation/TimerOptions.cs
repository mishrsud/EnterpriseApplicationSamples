using System;
using System.Collections.Generic;
using Interfaces;

namespace Implementation
{
	/// <summary>
	/// Resposible for providing a fluent API to configure timers.
	/// </summary>
	public class TimerOptions : ITimerOptions
	{
		private IList<Action<System.Timers.Timer>> _timerActions = new List<Action<System.Timers.Timer>>();

		/// <summary>
		/// Configures the action to be called when the timer elapses.
		/// </summary>
		/// <param name="action">The action to be called.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="action"/> is null.</exception>
		public ITimerOptions WhenElapsed(Action action)
		{
			if (action == null) throw new ArgumentNullException("action");
			_timerActions.Add(timer => timer.Elapsed += (sender, args) => action());
			return this;
		}

		/// <summary>
		/// Configures the interval until timer elapses.
		/// </summary>
		/// <param name="interval">The interval before elapse.</param>
		public ITimerOptions SetInterval(TimeSpan interval)
		{
			if (interval.TotalMilliseconds < 1) throw new ArgumentOutOfRangeException("interval");

			_timerActions.Add(timer => timer.Interval = interval.TotalMilliseconds);
			return this;
		}

		/// <summary>
		/// Configures the timer to auto reset, meaning it will restart the timer immediately after elapse.
		/// </summary>
		public ITimerOptions AutoReset()
		{
			_timerActions.Add(timer => timer.AutoReset = true);
			return this;
		}

		/// <summary>
		/// Builds a timer with the specified options.
		/// </summary>
		internal System.Timers.Timer Build()
		{
			var timer = new System.Timers.Timer
			{
				AutoReset = false,
				Enabled = false
			};
			foreach (var timerAction in _timerActions)
			{
				timerAction(timer);
			}
			return timer;
		}

		/// <summary>
		/// Disposes the object.
		/// </summary>
		/// <param name="disposing">True if called by the public Dispose method.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_timerActions != null)
				{
					// This is done so delegates in the _timerActions list are disposed. Otherwise they hang around forever! (observed on test environment with a memory profiler)
					_timerActions.Clear();
					_timerActions = null;
				}
				// free unmanaged resources when explicitly called
			}
		}

		/// <summary>
		/// Disposes the object.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
