using System;

namespace Smi.Caching.Interfaces
{
	/// <summary>
	/// Resposible for triggering a timed event.
	/// </summary>
	public interface ITimer : IDisposable
	{
		/// <summary>
		/// Starts the timer.
		/// </summary>
		void Start();

		/// <summary>
		/// Stops the timer.
		/// </summary>
		void Stop();

		/// <summary>
		/// Resets the timer, meaning it will start over from 0.
		/// </summary>
		void Reset();
	}
}
