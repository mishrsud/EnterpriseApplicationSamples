using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Logging.Impl.Concrete
{
	/// <summary>
	/// Logger sending everything to the trace output stream using <see cref="System.Diagnostics.Trace"/>.
	/// </summary>
	/// <remarks>
	/// Based on the version by Gilles Bayon and Erich Eichinger in Common.Logging.
	/// </remarks>
	/// <seealso cref="LogManager.FactoryAdapter"/>
	[Serializable]
	public class TraceLogger : AbstractSimpleLogger, IDeserializationCallback
	{
		private readonly bool _useTraceSource;
		[NonSerialized]
		private TraceSource _traceSource;

		/// <summary>
		/// Used to defer message formatting until it is really needed.
		/// </summary>
		/// <remarks>
		/// This class also improves performance when multiple 
		/// <see cref="TraceListener"/>s are configured.
		/// </remarks>
		private sealed class FormatOutputMessage
		{
			private readonly TraceLogger _outer;
			private readonly int _eventId;
			private readonly LogLevel _level;
			private readonly object _message;
			private readonly Exception _ex;

			/// <summary>
			/// Constructs a new instance of the <see cref="FormatOutputMessage"/> for a trace logger.
			/// </summary>
			/// <param name="outer"></param>
			/// <param name="eventId"></param>
			/// <param name="level"></param>
			/// <param name="message"></param>
			/// <param name="ex"></param>
			public FormatOutputMessage(TraceLogger outer, int eventId, LogLevel level, object message, Exception ex)
			{
				_outer = outer;
				_eventId = eventId;
				_level = level;
				_message = message;
				_ex = ex;
			}

			/// <summary>
			/// Returns a <see cref="System.String"/> of the formatted log event.
			/// </summary>
			/// <returns>
			/// A <see cref="System.String"/> that represents this instance.
			/// </returns>
			public override string ToString()
			{
				var sb = new StringBuilder();
				_outer.FormatOutput(sb, _eventId, _level, _message, _ex);
				return sb.ToString();
			}
		}

		/// <summary>
		/// Creates a new TraceLogger instance.
		/// </summary>
		/// <param name="useTraceSource">whether to use <see cref="TraceSource"/> or <see cref="Trace"/> for logging.</param>
		/// <param name="logName">the name of this logger</param>
		/// <param name="logLevel">the default log level to use</param>
		/// <param name="showLevel">Include the current log level in the log message.</param>
		/// <param name="showDateTime">Include the current time in the log message.</param>
		/// <param name="showLogName">Include the instance name in the log message.</param>
		/// <param name="dateTimeFormat">The date and time format to use in the log message.</param>
		public TraceLogger(bool useTraceSource, string logName, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
			: base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
		{
			_useTraceSource = useTraceSource;
			if (_useTraceSource)
			{
				_traceSource = new TraceSource(logName, Map2SourceLevel(logLevel));
			}
		}

		/// <summary>
		/// Determines if the given log level is currently enabled.
		/// checks <see cref="TraceSource.Switch"/> if <see cref="TraceLoggerFactory.UseTraceSource"/> is true.
		/// </summary>
		protected override bool IsLevelEnabled(LogLevel level)
		{
			if (!_useTraceSource)
			{
				return base.IsLevelEnabled(level);
			}
			return _traceSource.Switch.ShouldTrace(Map2TraceEventType(level));
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
			var msg = new FormatOutputMessage(this, eventId, level, message, exception);
			if (_traceSource != null)
			{
				_traceSource.TraceEvent(Map2TraceEventType(level), 0, "{0}", msg);
			}
			else
			{
				switch (level)
				{
					case LogLevel.Info:
						System.Diagnostics.Trace.TraceInformation("{0}", msg);
						break;
					case LogLevel.Warn:
						System.Diagnostics.Trace.TraceWarning("{0}", msg);
						break;
					case LogLevel.Error:
					case LogLevel.Fatal:
						System.Diagnostics.Trace.TraceError("{0}", msg);
						break;
					default:
						System.Diagnostics.Trace.WriteLine(msg);
						break;
				}
			}
		}

		private static TraceEventType Map2TraceEventType(LogLevel logLevel)
		{
			switch (logLevel)
			{
				case LogLevel.Trace:
					return TraceEventType.Verbose;
				case LogLevel.Debug:
					return TraceEventType.Verbose;
				case LogLevel.Info:
					return TraceEventType.Information;
				case LogLevel.Warn:
					return TraceEventType.Warning;
				case LogLevel.Error:
					return TraceEventType.Error;
				case LogLevel.Fatal:
					return TraceEventType.Critical;
				default:
					return 0;
			}
		}

		private static SourceLevels Map2SourceLevel(LogLevel logLevel)
		{
			switch (logLevel)
			{
				case LogLevel.All:
				case LogLevel.Trace:
					return SourceLevels.All;
				case LogLevel.Debug:
					return SourceLevels.Verbose;
				case LogLevel.Info:
					return SourceLevels.Information;
				case LogLevel.Warn:
					return SourceLevels.Warning;
				case LogLevel.Error:
					return SourceLevels.Error;
				case LogLevel.Fatal:
					return SourceLevels.Critical;
				default:
					return SourceLevels.Off;
			}
		}

		/// <summary>
		/// Called after deserialization completed.
		/// </summary>
		public virtual void OnDeserialization(object sender)
		{
			if (_useTraceSource)
			{
				_traceSource = new TraceSource(Name, Map2SourceLevel(CurrentLogLevel));
			}
		}
	}
}
