using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smi.Caching.Interfaces
{
	/// <summary>
	/// Resposible for creating <see cref="ITimer"/> instances.
	/// </summary>
	public interface ITimerProvider
	{
		/// <summary>
		/// Creates a new <see cref="ITimer"/> instance using the specfied options.
		/// </summary>
		/// <param name="optionsAction">The action to configure the timer.</param>
		ITimer Create(Action<ITimerOptions> optionsAction);

		/// <summary>
		/// Creates a new <see cref="ITimer"/> instance using the specfied updateInterval and callback.
		/// Optionally start the timer immediately.
		/// </summary>
		/// <param name="updateInterval">The update interval.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="autoStart">if set to <c>true</c> [start timer immediately].</param>
		ITimer Create(TimeSpan updateInterval, Action callback, bool autoStart);
	}
}
