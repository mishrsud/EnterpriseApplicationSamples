using System;

namespace Logging.Impl.Concrete
{
	public class NLogLogger : AbstractLogger
	{
		private readonly NLogWrapper _logger;
		private readonly static Type _declaringType = typeof(AbstractLogger);

		/// <summary>
		/// Creates a new instance of the <see cref="NLogLogger"/> class.
		/// </summary>
		/// <param name="logger">
		/// An instance of <see cref="NLog.Logger"/> to performs the actual logging.
		/// </param>
		protected internal NLogLogger(NLogWrapper logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Actually sends the message to the underlying log system.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="level">The level of this log event.</param>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">Optional exception to log.</param>
		protected override void WriteInternal(int eventId, LogLevel level, object message, Exception exception)
		{
			var nLoglevel = GetNLogLevel(level);
			if (exception != null)
			{
				var msg = message == null ? "<NULL>" : message.ToString();
				_logger.LogException(eventId, nLoglevel, msg, exception);
			}
			else
			{
				var logEntry = new LogEntry(eventId, nLoglevel, _logger.Name, null, "{0}", new[] { message }, exception);
				_logger.Log(_declaringType, logEntry);
			}
		}

		/// <summary>
		/// Flushes all log entries to the underlying log store.
		/// </summary>
		public override void Flush()
		{
			_logger.Factory.Flush();
			base.Flush();
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public override bool IsTraceEnabled
		{
			get { return _logger.IsTraceEnabled; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public override bool IsDebugEnabled
		{
			get { return _logger.IsDebugEnabled; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public override bool IsInfoEnabled
		{
			get { return _logger.IsInfoEnabled; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public override bool IsWarnEnabled
		{
			get { return _logger.IsWarnEnabled; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public override bool IsErrorEnabled
		{
			get { return _logger.IsErrorEnabled; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <value></value>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public override bool IsFatalEnabled
		{
			get { return _logger.IsFatalEnabled; }
		}

		/// <summary>
		/// Converts the Open API <see cref="LogLevel"/> into an NLog <see cref="NLog.LogLevel">log level</see>.
		/// </summary>
		/// <param name="logLevel"></param>
		/// <returns></returns>
		private static NLog.LogLevel GetNLogLevel(LogLevel logLevel)
		{
			switch (logLevel)
			{
				case LogLevel.All:
					return NLog.LogLevel.Trace;
				case LogLevel.Trace:
					return NLog.LogLevel.Trace;
				case LogLevel.Debug:
					return NLog.LogLevel.Debug;
				case LogLevel.Info:
					return NLog.LogLevel.Info;
				case LogLevel.Warn:
					return NLog.LogLevel.Warn;
				case LogLevel.Error:
					return NLog.LogLevel.Error;
				case LogLevel.Fatal:
					return NLog.LogLevel.Fatal;
				case LogLevel.Off:
					return NLog.LogLevel.Off;
				default:
					throw new ArgumentOutOfRangeException("logLevel", logLevel, "unknown log level");
			}
		}
	}
}
