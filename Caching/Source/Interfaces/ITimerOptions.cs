using System;

namespace Smi.Caching.Interfaces
{
	/// <summary>
	/// Resposible for providing a fluent API to configure timers.
	/// </summary>
	public interface ITimerOptions : IDisposable
	{
		/// <summary>
		/// Configures the action to be called when the timer elapses.
		/// </summary>
		/// <param name="action">The action to be called.</param>
		ITimerOptions WhenElapsed(Action action);

		/// <summary>
		/// Configures the interval until timer elapses.
		/// </summary>
		/// <param name="interval">The interval before elapse.</param>
		ITimerOptions SetInterval(TimeSpan interval);

		/// <summary>
		/// Configures the timer to auto reset, meaning it will restart the timer immediately after elapse.
		/// </summary>
		ITimerOptions AutoReset();
	}
}
