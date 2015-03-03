using System;
using System.Globalization;

namespace Logging.Impl
{
	/// <summary>
	/// The basic implementation common to logging adapters
	/// </summary>
	/// <remarks>Inspired by Common Logging 2.0 Erich Eichinger</remarks>
	[Serializable]
	public abstract class AbstractLogger : ILogger
	{
		private readonly string _name;
		private WriteHandler _writeInternal;

		#region FormatMessageCallbackFormattedMessage

		private sealed class FormatMessageCallbackFormattedMessage
		{
			private volatile string _cachedMessage;

			private readonly IFormatProvider _formatProvider;
			private readonly Action<FormatMessageHandler> _formatMessageCallback;

			public FormatMessageCallbackFormattedMessage(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
			{
				_formatProvider = formatProvider;
				_formatMessageCallback = formatMessageCallback;
			}

			public override string ToString()
			{
				if (_cachedMessage == null)
				{
					if (_formatMessageCallback != null) _formatMessageCallback(FormatMessage);
					// FormatMessage ensures _cachedMessage isn't null
				}

				return _cachedMessage;
			}

			private string FormatMessage(string format, params object[] args)
			{
				// Catch user-made errors in Exception messages (ie. missing parameters etc.,)
				try
				{
					_cachedMessage = string.Format(_formatProvider, format, args);
				}
				catch (Exception)
				{
					_cachedMessage = "YOU HAVE PARAMETER MISMATCH IN YOUR EXCEPTION. Args: " + args.Length + ", Msg: " + format;
				}
				return _cachedMessage;
			}
		}

		#endregion

		#region StringFormatFormattedMessage

		private sealed class StringFormatFormattedMessage
		{
			private volatile string _cachedMessage;

			private readonly IFormatProvider _formatProvider;
			private readonly string _message;
			private readonly object[] _args;

			public StringFormatFormattedMessage(IFormatProvider formatProvider, string message, params object[] args)
			{
				_formatProvider = formatProvider;
				_message = message;
				_args = args;
			}

			public override string ToString()
			{
				if (_cachedMessage == null)
				{
					// Catch user-made errors in Exception messages (ie. missing parameters etc.,)
					try
					{
						_cachedMessage = string.Format(_formatProvider, _message, _args);
					}
					catch (Exception)
					{
						_cachedMessage = "YOU HAVE PARAMETER MISMATCH IN YOUR EXCEPTION. Args: " + _args.Length + ", Msg: " + _message;
					}
				}

				return _cachedMessage;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Creates a new logger instance with the name "Default"
		/// using <see cref="WriteInternal"/> for writing log events 
		/// to the underlying log system.
		/// </summary>
		protected AbstractLogger() : this("Default") { }

		/// <summary>
		/// Creates a new logger instance using <see cref="WriteInternal"/> for 
		/// writing log events to the underlying log system.
		/// </summary>
		/// <param name="name">Unique name of the logger</param>
		/// <seealso cref="GetWriteHandler"/>
		protected AbstractLogger(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

			_name = name;
		}

		#endregion

		/// <summary>
		/// Holds the method for writing a message to the log system.
		/// </summary>
		private WriteHandler Write
		{
			get { return _writeInternal ?? (_writeInternal = GetWriteHandler() ?? WriteInternal); }
		}

		#region Write Handler Members

		/// <summary>
		/// Represents a method responsible for writing a message to the log system.
		/// </summary>
		protected delegate void WriteHandler(int eventId, LogLevel level, object message, Exception exception);

		/// <summary>
		/// Override this method to use a different method than <see cref="WriteInternal"/> 
		/// for writing log events to the underlying log system.
		/// </summary>
		/// <remarks>
		/// Usually you don't need to override thise method. The default implementation returns
		/// <c>null</c> to indicate that the default handler <see cref="WriteInternal"/> should be 
		/// used.
		/// </remarks>
		protected virtual WriteHandler GetWriteHandler()
		{
			return null;
		}

		/// <summary>
		/// Actually sends the message to the underlying log system.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="level">The level of this log event.</param>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">Optional exception to log.</param>
		protected abstract void WriteInternal(int eventId, LogLevel level, object message, Exception exception);

		#endregion

		#region Log Level Properties

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public abstract bool IsTraceEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public abstract bool IsDebugEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public abstract bool IsInfoEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public abstract bool IsWarnEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public abstract bool IsErrorEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <remarks>
		/// Override this in your derived class to comply with the underlying logging system
		/// </remarks>
		public abstract bool IsFatalEnabled { get; }

		#endregion

		#region Name

		/// <summary>
		/// Gets the name of this logger instance.
		/// </summary>
		public string Name { get { return _name; } }

		#endregion

		#region Trace

		/// <summary>
		/// Flushes all log entries to the underlying log store.
		/// </summary>
		public virtual void Flush()
		{
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Trace(Enum eventId, object message)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Trace(int eventId, object message)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public virtual void Trace(Enum eventId, object message, Exception exception)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, message, exception);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public virtual void Trace(int eventId, object message, Exception exception)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, message, exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void TraceFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void TraceFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void TraceFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void TraceFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void TraceFormat(Enum eventId, string format, params object[] args)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void TraceFormat(int eventId, string format, params object[] args)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void TraceFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void TraceFormat(int eventId, string format, Exception exception, params object[] args)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Trace(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Trace(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public virtual void Trace(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public virtual void Trace(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Trace(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Trace(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public virtual void Trace(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsTraceEnabled)
				Write(EnumToValue(eventId), LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public virtual void Trace(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsTraceEnabled)
				Write(eventId, LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		#endregion

		#region Debug

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Debug(Enum eventId, object message)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Debug(int eventId, object message)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level including
		/// the stack Debug of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		public virtual void Debug(Enum eventId, object message, Exception exception)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, message, exception);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level including
		/// the stack Debug of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		public virtual void Debug(int eventId, object message, Exception exception)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, message, exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void DebugFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void DebugFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void DebugFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void DebugFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void DebugFormat(Enum eventId, string format, params object[] args)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void DebugFormat(int eventId, string format, params object[] args)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void DebugFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void DebugFormat(int eventId, string format, Exception exception, params object[] args)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Debug(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Debug(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		public virtual void Debug(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		public virtual void Debug(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Debug(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Debug(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		public virtual void Debug(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsDebugEnabled)
				Write(EnumToValue(eventId), LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		public virtual void Debug(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsDebugEnabled)
				Write(eventId, LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		#endregion

		#region Info

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Info(Enum eventId, object message)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Info(int eventId, object message)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level including
		/// the stack Info of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		public virtual void Info(Enum eventId, object message, Exception exception)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, message, exception);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level including
		/// the stack Info of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		public virtual void Info(int eventId, object message, Exception exception)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, message, exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void InfoFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void InfoFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void InfoFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void InfoFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void InfoFormat(Enum eventId, string format, params object[] args)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void InfoFormat(int eventId, string format, params object[] args)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void InfoFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void InfoFormat(int eventId, string format, Exception exception, params object[] args)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Info(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Info(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		public virtual void Info(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		public virtual void Info(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Info(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Info(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		public virtual void Info(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsInfoEnabled)
				Write(EnumToValue(eventId), LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		public virtual void Info(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsInfoEnabled)
				Write(eventId, LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		#endregion

		#region Warn

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Warn(Enum eventId, object message)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Warn(int eventId, object message)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level including
		/// the stack Warn of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		public virtual void Warn(Enum eventId, object message, Exception exception)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, message, exception);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level including
		/// the stack Warn of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		public virtual void Warn(int eventId, object message, Exception exception)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, message, exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Warnrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void WarnFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Warnrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void WarnFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Warnrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void WarnFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Warnrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void WarnFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void WarnFormat(Enum eventId, string format, params object[] args)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void WarnFormat(int eventId, string format, params object[] args)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void WarnFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void WarnFormat(int eventId, string format, Exception exception, params object[] args)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Warn(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Warn(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		public virtual void Warn(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		public virtual void Warn(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Warn(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Warn(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		public virtual void Warn(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsWarnEnabled)
				Write(EnumToValue(eventId), LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		public virtual void Warn(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsWarnEnabled)
				Write(eventId, LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		#endregion

		#region Error

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Error(Enum eventId, object message)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Error(int eventId, object message)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level including
		/// the stack Error of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		public virtual void Error(Enum eventId, object message, Exception exception)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, message, exception);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level including
		/// the stack Error of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		public virtual void Error(int eventId, object message, Exception exception)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, message, exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Errorrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void ErrorFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Errorrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void ErrorFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Errorrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void ErrorFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Errorrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void ErrorFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void ErrorFormat(Enum eventId, string format, params object[] args)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void ErrorFormat(int eventId, string format, params object[] args)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void ErrorFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void ErrorFormat(int eventId, string format, Exception exception, params object[] args)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Error(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Error(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		public virtual void Error(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		public virtual void Error(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Error(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Error(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		public virtual void Error(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsErrorEnabled)
				Write(EnumToValue(eventId), LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		public virtual void Error(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsErrorEnabled)
				Write(eventId, LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		#endregion

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Fatal(Enum eventId, object message)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public virtual void Fatal(int eventId, object message)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, message, null);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level including
		/// the stack Fatal of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		public virtual void Fatal(Enum eventId, object message, Exception exception)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, message, exception);
		}

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level including
		/// the stack Fatal of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		public virtual void Fatal(int eventId, object message, Exception exception)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, message, exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void FatalFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		public virtual void FatalFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void FatalFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public virtual void FatalFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void FatalFormat(Enum eventId, string format, params object[] args)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		public virtual void FatalFormat(int eventId, string format, params object[] args)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void FatalFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public virtual void FatalFormat(int eventId, string format, Exception exception, params object[] args)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Fatal(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Fatal(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		public virtual void Fatal(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		public virtual void Fatal(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(CultureInfo.CurrentCulture, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Fatal(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		public virtual void Fatal(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		public virtual void Fatal(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsFatalEnabled)
				Write(EnumToValue(eventId), LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		public virtual void Fatal(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			if (IsFatalEnabled)
				Write(eventId, LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
		}

		/// <summary>
		/// Converts an enum value to an <see cref="Int32"/>.
		/// </summary>
		/// <param name="value">The <see cref="Enum"/> value.</param>
		/// <returns>An <see cref="Int32"/> value-</returns>
		protected static int EnumToValue(Enum value)
		{
			return Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}
	}
}
