using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Impl.Concrete
{
	/// <summary>
	/// Sends log messages to <see cref="Console.Out" />.
	/// </summary>
	/// <remarks>
	/// Based on the version by Gilles Bayon in Common.Logging. 
	/// </remarks>
	[Serializable]
	public class ConsoleOutLogger : AbstractSimpleLogger
	{
		/// <summary>
		/// Creates and initializes a logger that writes messages to <see cref="Console.Out" />.
		/// </summary>
		/// <param name="name">The name, usually type name of the calling class, of the logger.</param>
		/// <param name="logLevel">The current logging threshold. Messages recieved that are beneath this threshold will not be logged.</param>
		/// <param name="showLevel">Include the current log level in the log message.</param>
		/// <param name="showDateTime">Include the current time in the log message.</param>
		/// <param name="showLogName">Include the instance name in the log message.</param>
		/// <param name="dateTimeFormat">The date and time format to use in the log message.</param>
		public ConsoleOutLogger(string name, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
			: base(name, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
		{
		}

		/// <summary>
		/// Do the actual logging by constructing the log message using a <see cref="StringBuilder" /> then
		/// sending the output to <see cref="Console.Out" />.
		/// </summary>
		/// <param name="eventId">Id of the event</param>
		/// <param name="level">The <see cref="LogLevel" /> of the message.</param>
		/// <param name="message">The log message.</param>
		/// <param name="exception">An optional <see cref="Exception" /> associated with the message.</param>
		protected override void WriteInternal(int eventId, LogLevel level, object message, Exception exception)
		{
			// Use a StringBuilder for better performance
			var sb = new StringBuilder();
			FormatOutput(sb, eventId, level, message, exception);

			// Print to the appropriate destination
			Console.Out.WriteLine(sb.ToString());
		}
	}
}
