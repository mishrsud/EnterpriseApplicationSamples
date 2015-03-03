using System;
using System.Globalization;
using System.Text;

namespace Logging.Impl
{
	/// <summary>
	/// Abstract base class for simple loggers
	/// </summary>
	[Serializable]
	public abstract class AbstractSimpleLogger : AbstractLogger
	{
		private readonly bool _showLevel;
		private readonly bool _showDateTime;
		private readonly bool _showLogName;
		private readonly string _dateTimeFormat;
		private readonly bool _hasDateTimeFormat;
		private LogLevel _currentLogLevel;

		[NonSerialized]
		private readonly IDateTimeProvider _dateTimeProvider;

		#region Properties

		/// <summary>
		/// Include the current log level in the log message.
		/// </summary>
		public bool ShowLevel
		{
			get { return _showLevel; }
		}

		/// <summary>
		/// Include the current time in the log message.
		/// </summary>
		public bool ShowDateTime
		{
			get { return _showDateTime; }
		}

		/// <summary>
		/// Include the instance name in the log message.
		/// </summary>
		public bool ShowLogName
		{
			get { return _showLogName; }
		}

		/// <summary>
		/// The current logging threshold. Messages recieved that are beneath this threshold will not be logged.
		/// </summary>
		public LogLevel CurrentLogLevel
		{
			get { return _currentLogLevel; }
			set { _currentLogLevel = value; }
		}

		/// <summary>
		/// The date and time format to use in the log message.
		/// </summary>
		public string DateTimeFormat
		{
			get { return _dateTimeFormat; }
		}

		/// <summary>
		/// Determines Whether <see cref="DateTimeFormat"/> is set.
		/// </summary>
		public bool HasDateTimeFormat
		{
			get { return _hasDateTimeFormat; }
		}


		#endregion

		/// <summary>
		/// Creates and initializes a the simple logger.
		/// </summary>
		/// <param name="name">The name, usually type name of the calling class, of the logger.</param>
		/// <param name="logLevel">The current logging threshold. Messages recieved that are beneath this threshold will not be logged.</param>
		/// <param name="showLevel">Include level in the log message.</param>
		/// <param name="showDateTime">Include the current time in the log message.</param>
		/// <param name="showLogName">Include the instance name in the log message.</param>
		/// <param name="dateTimeFormat">The date and time format to use in the log message.</param>
		protected AbstractSimpleLogger(string name, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
			: base(name)
		{
			_currentLogLevel = logLevel;
			_showLevel = showLevel;
			_showDateTime = showDateTime;
			_showLogName = showLogName;
			_dateTimeFormat = dateTimeFormat;
			_hasDateTimeFormat = !string.IsNullOrEmpty(_dateTimeFormat);
			_dateTimeProvider = new DateTimeProvider();
		}

		/// <summary>
		/// Appends the formatted message to the specified <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="builder">the <see cref="StringBuilder"/> that receíves the formatted message.</param>
		/// <param name="eventId">Id of the event.</param>
		/// <param name="level">The event <see cref="LogLevel">level</see></param>
		/// <param name="message">Message to log.</param>
		/// <param name="exception">Optional exception.</param>
		protected virtual void FormatOutput(StringBuilder builder, int eventId, LogLevel level, object message, Exception exception)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			// Append date-time if so configured
			if (_showDateTime)
			{
				if (_hasDateTimeFormat)
				{
					builder.Append(_dateTimeProvider.UtcDateTimeNow.ToString(_dateTimeFormat, CultureInfo.InvariantCulture));
				}
				else
				{
					builder.Append(_dateTimeProvider.UtcDateTimeNow);
				}

				builder.Append(" ");
			}

			if (_showLevel)
			{
				// Append a readable representation of the log level
				builder.Append(("[" + level.ToString().ToUpper(CultureInfo.InvariantCulture) + "]").PadRight(8));
			}

			// Append the name of the log instance if so configured
			if (_showLogName)
			{
				builder.Append(Name).Append(" - ");
			}

			// Append the event id
			builder.Append(eventId + " - ");


			// Append the message
			builder.Append(message);

			// Append exception if not null
			if (exception != null)
			{
				builder.Append(Environment.NewLine).AppendFormat("{0} {1}", exception.GetType().FullName, exception.Message);

				// Append stack trace if not null
				builder.Append(Environment.NewLine).Append(exception.StackTrace);
			}
		}

		/// <summary>
		/// Determines if the given log level is currently enabled.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		protected virtual bool IsLevelEnabled(LogLevel level)
		{
			var iLevel = (int)level;
			var iCurrentLogLevel = (int)_currentLogLevel;

			// return iLevel.CompareTo(iCurrentLogLevel); better ???
			return (iLevel >= iCurrentLogLevel);
		}

		#region ILog Members

		/// <summary>
		/// Returns <see langword="true" /> if the current <see cref="LogLevel" /> is greater than or
		/// equal to <see cref="LogLevel.Trace" />. If it is, all messages will be sent to <see cref="Console.Out" />.
		/// </summary>
		public override bool IsTraceEnabled
		{
			get { return IsLevelEnabled(LogLevel.Trace); }
		}

		/// <summary>
		/// Returns <see langword="true" /> if the current <see cref="LogLevel" /> is greater than or
		/// equal to <see cref="LogLevel.Debug" />. If it is, all messages will be sent to <see cref="Console.Out" />.
		/// </summary>
		public override bool IsDebugEnabled
		{
			get { return IsLevelEnabled(LogLevel.Debug); }
		}

		/// <summary>
		/// Returns <see langword="true" /> if the current <see cref="LogLevel" /> is greater than or
		/// equal to <see cref="LogLevel.Info" />. If it is, only messages with a <see cref="LogLevel" /> of
		/// <see cref="LogLevel.Info" />, <see cref="LogLevel.Warn" />, <see cref="LogLevel.Error" />, and 
		/// <see cref="LogLevel.Fatal" /> will be sent to <see cref="Console.Out" />.
		/// </summary>
		public override bool IsInfoEnabled
		{
			get { return IsLevelEnabled(LogLevel.Info); }
		}


		/// <summary>
		/// Returns <see langword="true" /> if the current <see cref="LogLevel" /> is greater than or
		/// equal to <see cref="LogLevel.Warn" />. If it is, only messages with a <see cref="LogLevel" /> of
		/// <see cref="LogLevel.Warn" />, <see cref="LogLevel.Error" />, and <see cref="LogLevel.Fatal" /> 
		/// will be sent to <see cref="Console.Out" />.
		/// </summary>
		public override bool IsWarnEnabled
		{
			get { return IsLevelEnabled(LogLevel.Warn); }
		}

		/// <summary>
		/// Returns <see langword="true" /> if the current <see cref="LogLevel" /> is greater than or
		/// equal to <see cref="LogLevel.Error" />. If it is, only messages with a <see cref="LogLevel" /> of
		/// <see cref="LogLevel.Error" /> and <see cref="LogLevel.Fatal" /> will be sent to <see cref="Console.Out" />.
		/// </summary>
		public override bool IsErrorEnabled
		{
			get { return IsLevelEnabled(LogLevel.Error); }
		}

		/// <summary>
		/// Returns <see langword="true" /> if the current <see cref="LogLevel" /> is greater than or
		/// equal to <see cref="LogLevel.Fatal" />. If it is, only messages with a <see cref="LogLevel" /> of
		/// <see cref="LogLevel.Fatal" /> will be sent to <see cref="Console.Out" />.
		/// </summary>
		public override bool IsFatalEnabled
		{
			get { return IsLevelEnabled(LogLevel.Fatal); }
		}

		#endregion
	}
}
