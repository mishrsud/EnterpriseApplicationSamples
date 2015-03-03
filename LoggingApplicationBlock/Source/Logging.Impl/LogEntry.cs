using System;
using NLog;

namespace Logging.Impl
{
	internal class LogEntry : LogEventInfo
	{
		public LogEntry(
			int eventId,
			NLog.LogLevel level,
			string loggerName,
			IFormatProvider formatProvider,
			string message,
			object[] parameters,
			Exception exception)
			: base(level, loggerName, formatProvider, message, parameters, exception)
		{
			// set event-specific context parameter
			// this context parameter can be retrieved in logging layouts using ${event-context:EventID}
			this.Properties["EventID"] = eventId;
		}
	}
}
