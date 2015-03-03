using System;

namespace Logging
{
	/// <summary>
	/// A Simple logging facade over logging APIs
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Gets the name of this logger instance.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		bool IsTraceEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="System.Diagnostics.Debug"/> level.
		/// </summary>
		bool IsDebugEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Error"/> level.
		/// </summary>
		bool IsErrorEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		bool IsFatalEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Info"/> level.
		/// </summary>
		bool IsInfoEnabled { get; }

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		bool IsWarnEnabled { get; }

		/// <summary>
		/// Flushes all log entries to the underlying log store.
		/// </summary>
		void Flush();

		#region Trace

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Trace(Enum eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Trace(int eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Trace(Enum eventId, object message, Exception exception);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Trace"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Trace(int eventId, object message, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void TraceFormat(Enum eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void TraceFormat(int eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void TraceFormat(Enum eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void TraceFormat(int eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void TraceFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void TraceFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void TraceFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void TraceFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Trace(Enum eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Trace"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Trace(int eventId, Action<FormatMessageHandler> formatMessageCallback);

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
		void Trace(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Trace(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Trace(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Trace(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Trace(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Trace(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		#endregion

		#region Debug

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Debug(Enum eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Debug(int eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Debug(Enum eventId, object message, Exception exception);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Debug"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Debug(int eventId, object message, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void DebugFormat(Enum eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void DebugFormat(int eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void DebugFormat(Enum eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void DebugFormat(int eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void DebugFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void DebugFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void DebugFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void DebugFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Debug(Enum eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Debug(int eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Debug(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Debug"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Debug(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Debug(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Debug(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Debug(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Debug(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		#endregion

		#region Info

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Info(Enum eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Info(int eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Info(Enum eventId, object message, Exception exception);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Info"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Info(int eventId, object message, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void InfoFormat(Enum eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void InfoFormat(int eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void InfoFormat(Enum eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void InfoFormat(int eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void InfoFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void InfoFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void InfoFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void InfoFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Info(Enum eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Info(int eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Info(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Info"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Info(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Info(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Info(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Info(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Info(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		#endregion

		#region Warn

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Warn(Enum eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Warn(int eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Warn(Enum eventId, object message, Exception exception);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Warn"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Warn(int eventId, object message, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void WarnFormat(Enum eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void WarnFormat(int eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void WarnFormat(Enum eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void WarnFormat(int eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void WarnFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void WarnFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void WarnFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void WarnFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Warn(Enum eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Warn(int eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Warn(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Warn"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Warn(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Warn(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Warn(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Warn(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Warn(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		#endregion

		#region Error

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Error(Enum eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Error(int eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Error(Enum eventId, object message, Exception exception);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Error"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Error(int eventId, object message, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void ErrorFormat(Enum eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void ErrorFormat(int eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void ErrorFormat(Enum eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void ErrorFormat(int eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void ErrorFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void ErrorFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void ErrorFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void ErrorFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Error(Enum eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Error(int eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Error(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Error"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Error(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Error(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Error(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Error(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Error(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		#endregion

		#region Fatal

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Fatal(Enum eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		void Fatal(int eventId, object message);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Fatal(Enum eventId, object message, Exception exception);

		/// <summary>
		/// Log a message object with the <see cref="LogLevel.Fatal"/> level including
		/// the stack trace of the <see cref="Exception"/> passed
		/// as a parameter.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Fatal(int eventId, object message, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void FatalFormat(Enum eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args">the list of format arguments</param>
		void FatalFormat(int eventId, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void FatalFormat(Enum eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		void FatalFormat(int eventId, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void FatalFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="args"></param>
		void FatalFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void FatalFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/> </param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		void FatalFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Fatal(Enum eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		void Fatal(int eventId, Action<FormatMessageHandler> formatMessageCallback);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Fatal(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		/// <summary>
		/// Log a message with the <see cref="LogLevel.Fatal"/> level using a callback to obtain the message
		/// </summary>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments 
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		void Fatal(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Fatal(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Fatal(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

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
		void Fatal(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

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
		void Fatal(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

		#endregion
	}
}
