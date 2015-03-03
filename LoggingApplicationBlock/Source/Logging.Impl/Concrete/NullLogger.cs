using System;

namespace Logging.Impl.Concrete
{
	/// <summary>
	/// Implementation of <see cref="ILogger"/> that ignores all messages.
	/// </summary>
	[Serializable]
	public class NullLogger : ILogger
	{
		private readonly string _name;

		/// <summary>
		/// Creates a new instance of a <see cref="NullLogger"/> with 
		/// name "Default".
		/// </summary>
		public NullLogger()
		{
			_name = "Default";
		}

		/// <summary>
		/// Creates a new instance of a <see cref="NullLogger"/>
		/// with a specific name.
		/// </summary>
		/// <param name="name">Name of the logger to create.</param>
		public NullLogger(string name)
		{
			_name = name;
		}

		/// <summary>
		/// Gets the name of this logger instance.
		/// </summary>
		/// <value></value>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Flushes all log entries to the underlying log store.
		/// </summary>
		public void Flush()
		{
		}

		#region Trace

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Trace(Enum eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Trace(int eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Trace(Enum eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Trace(int eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void TraceFormat(Enum eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void TraceFormat(int eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void TraceFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void TraceFormat(int eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void TraceFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void TraceFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void TraceFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void TraceFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Trace(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		#endregion

		#region Debug

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Debug(Enum eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Debug(int eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Debug(Enum eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Debug(int eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void DebugFormat(Enum eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void DebugFormat(int eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void DebugFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void DebugFormat(int eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void DebugFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void DebugFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void DebugFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void DebugFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Debug.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Debug(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		#endregion

		#region Info

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Info(Enum eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Info(int eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Info(Enum eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Info(int eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void InfoFormat(Enum eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void InfoFormat(int eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void InfoFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void InfoFormat(int eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void InfoFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void InfoFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void InfoFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void InfoFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Info.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Info(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		#endregion

		#region Warn

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Warn(Enum eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Warn(int eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Warn(Enum eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Warn(int eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void WarnFormat(Enum eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void WarnFormat(int eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void WarnFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void WarnFormat(int eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void WarnFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void WarnFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void WarnFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void WarnFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Warn.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Warn(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		#endregion

		#region Error

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Error(Enum eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Error(int eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Error(Enum eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Error(int eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void ErrorFormat(Enum eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void ErrorFormat(int eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void ErrorFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void ErrorFormat(int eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void ErrorFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void ErrorFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void ErrorFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void ErrorFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Error.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Error(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		#endregion

		#region Fatal

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Fatal(Enum eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		public void Fatal(int eventId, object message)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Fatal(Enum eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="message">The message object to log.</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		public void Fatal(int eventId, object message, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void FatalFormat(Enum eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args">the list of format arguments</param>
		public void FatalFormat(int eventId, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void FatalFormat(Enum eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args">the list of format arguments</param>
		public void FatalFormat(int eventId, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void FatalFormat(Enum eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="args"></param>
		public void FatalFormat(int eventId, IFormatProvider formatProvider, string format, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void FatalFormat(Enum eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="format">The format of the message object to log.<see cref="string.Format(string,object[])"/></param>
		/// <param name="exception">The exception to log.</param>
		/// <param name="args"></param>
		public void FatalFormat(int eventId, IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(Enum eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(int eventId, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(Enum eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack trace.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(int eventId, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(Enum eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		/// <summary>
		/// Ignores message.
		/// </summary>
		/// <param name="eventId">Id of the event being logged.</param>
		/// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
		/// <param name="formatMessageCallback">A callback used by the logger to obtain the message if log level is matched</param>
		/// <param name="exception">The exception to log, including its stack Fatal.</param>
		/// <remarks>
		/// Using this method avoids the cost of creating a message and evaluating message arguments
		/// that probably won't be logged due to loglevel settings.
		/// </remarks>
		public void Fatal(int eventId, IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
		{
			// NOP - no operation
		}

		#endregion

		#region IsXXXEnabled

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Trace"/> level.
		/// </summary>
		/// <value>Always returns <see langword="false" />.</value>
		public bool IsTraceEnabled
		{
			get { return false; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Debug"/> level.
		/// </summary>
		/// <value>Always returns <see langword="false" />.</value>
		public bool IsDebugEnabled
		{
			get { return false; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Error"/> level.
		/// </summary>
		/// <value>Always returns <see langword="false" />.</value>
		public bool IsErrorEnabled
		{
			get { return false; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Fatal"/> level.
		/// </summary>
		/// <value>Always returns <see langword="false" />.</value>
		public bool IsFatalEnabled
		{
			get { return false; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Info"/> level.
		/// </summary>
		/// <value>Always returns <see langword="false" />.</value>
		public bool IsInfoEnabled
		{
			get { return false; }
		}

		/// <summary>
		/// Checks if this logger is enabled for the <see cref="LogLevel.Warn"/> level.
		/// </summary>
		/// <value>Always returns <see langword="false" />.</value>
		public bool IsWarnEnabled
		{
			get { return false; }
		}

		#endregion
	}
}
