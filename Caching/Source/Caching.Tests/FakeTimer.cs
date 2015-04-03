using System;
using Interfaces;

namespace Caching.Tests
{
	/// <summary>
	/// Fake implementation of <see cref="ITimer"/> for testing purposes. Provides mechanism to manually triggering elapse.
	/// </summary>
	public class FakeTimer : ITimer
	{
		private readonly Func<Action> _elapseActionProvider;

		/// <summary>
		/// Creates a new <see cref="FakeTimer"/> instance.
		/// </summary>
		/// <param name="elapseActionProvider">Provider of elapse action.</param>
		public FakeTimer(Func<Action> elapseActionProvider)
		{
			if (elapseActionProvider == null) throw new ArgumentNullException("elapseActionProvider");
			_elapseActionProvider = elapseActionProvider;
		}

		/// <summary>
		/// Starts the timer.
		/// </summary>
		public virtual void Start()
		{
		}

		/// <summary>
		/// Stops the timer.
		/// </summary>
		public virtual void Stop()
		{
		}

		/// <summary>
		/// Resets the timer, meaning it will start over from 0.
		/// </summary>
		public virtual void Reset()
		{
		}

		/// <summary>
		/// Triggers elapse of the timer resulting of a call to the elapse action.
		/// </summary>
		public virtual void TriggerElapse()
		{
			var action = _elapseActionProvider();
			if (action != null)
				action();
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
			}
		}
	}
}
