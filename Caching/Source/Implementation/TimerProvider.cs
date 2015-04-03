using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Interfaces;

namespace Implementation
{
	/// <summary>
	/// Resposible for creating <see cref="ITimer"/> instances.
	/// </summary>
	public class TimerProvider : ITimerProvider
	{
		/// <summary>
		/// Creates a new <see cref="ITimer"/> instance using the specfied options.
		/// </summary>
		/// <param name="optionsAction">The action to configure the timer.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="optionsAction"/> is null.</exception>
		public ITimer Create(Action<ITimerOptions> optionsAction)
		{
			if (optionsAction == null) throw new ArgumentNullException("optionsAction");

			var options = new TimerOptions();
			optionsAction(options);
			return new Timer(options.Build());
		}

		/// <summary>
		/// Creates a new <see cref="ITimer"/> instance using the specfied updateInterval and callback.
		/// Optionally start the timer immediately.
		/// </summary>
		/// <param name="updateInterval">The update interval.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="autoStart">if set to <c>true</c> [start timer immediately].</param>
		public ITimer Create(TimeSpan updateInterval, Action callback, bool autoStart)
		{
			if (callback == null) throw new ArgumentNullException("callback");

			var timerOptions = new TimerOptions();
			timerOptions.SetInterval(updateInterval);
			timerOptions.WhenElapsed(callback);
			timerOptions.AutoReset();

			var timer = new Timer(timerOptions.Build());
			if (autoStart) timer.Start();

			return timer;
		}
	}
}
