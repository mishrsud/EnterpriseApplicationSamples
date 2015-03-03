using System;

namespace Logging.Impl.Concrete
{
	public class NLogWrapper : NLog.Logger
	{
		private void LogWithType(LogEntry logEntry)
		{
			Log(typeof(NLog.Logger), logEntry);
		}

		#region Debug() overloads

		public void Debug(int eventID, string message)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, null, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void DebugException(int eventID, string message, Exception exception)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, null, exception);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, args, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, args, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Object arg1, System.Object arg2)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { arg1, arg2 }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Object arg1, System.Object arg2, System.Object arg3)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { arg1, arg2, arg3 }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, IFormatProvider formatProvider, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Debug(int eventID, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Debug, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		#endregion

		#region Error() overloads

		public void Error(int eventID, string message)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, null, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void ErrorException(int eventID, string message, Exception exception)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, null, exception);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, args, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, args, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Object arg1, System.Object arg2)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { arg1, arg2 }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Object arg1, System.Object arg2, System.Object arg3)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { arg1, arg2, arg3 }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, IFormatProvider formatProvider, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Error(int eventID, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Error, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		#endregion

		#region Fatal() overloads

		public void Fatal(int eventID, string message)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, null, null);
			LogWithType(logEntry); ;
		}

		public void Fatal(int eventID, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void FatalException(int eventID, string message, Exception exception)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, null, exception);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, args, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, args, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Object arg1, System.Object arg2)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { arg1, arg2 }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Object arg1, System.Object arg2, System.Object arg3)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { arg1, arg2, arg3 }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, IFormatProvider formatProvider, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Fatal(int eventID, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Fatal, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		#endregion

		#region Info() overloads

		public void Info(int eventID, string message)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, null, null);
			LogWithType(logEntry); ;
		}

		public void Info(int eventID, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void InfoException(int eventID, string message, Exception exception)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, null, exception);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, args, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, args, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Object arg1, System.Object arg2)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { arg1, arg2 }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Object arg1, System.Object arg2, System.Object arg3)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { arg1, arg2, arg3 }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, IFormatProvider formatProvider, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Info(int eventID, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Info, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		#endregion

		#region Trace() overloads

		public void Trace(int eventID, string message)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, null, null);
			LogWithType(logEntry); ;
		}

		public void Trace(int eventID, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void TraceException(int eventID, string message, Exception exception)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, null, exception);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, args, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, args, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Object arg1, System.Object arg2)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { arg1, arg2 }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Object arg1, System.Object arg2, System.Object arg3)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { arg1, arg2, arg3 }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, IFormatProvider formatProvider, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Trace(int eventID, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Trace, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		#endregion

		#region Warn() overloads

		public void Warn(int eventID, string message)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, null, null);
			LogWithType(logEntry); ;
		}

		public void Warn(int eventID, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void WarnException(int eventID, string message, Exception exception)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, null, exception);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, args, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, args, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Object arg1, System.Object arg2)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { arg1, arg2 }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Object arg1, System.Object arg2, System.Object arg3)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { arg1, arg2, arg3 }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Boolean argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Char argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Byte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.String argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Int32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Int64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Single argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Double argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Decimal argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.Object argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.SByte argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.UInt32 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, IFormatProvider formatProvider, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, formatProvider, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		public void Warn(int eventID, string message, System.UInt64 argument)
		{
			LogEntry logEntry = new LogEntry(eventID, NLog.LogLevel.Warn, Name, null, message, new object[] { argument }, null);
			LogWithType(logEntry);
		}

		#endregion

		public void Log(int eventID, NLog.LogLevel level, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, level, Name, null, "{0}", new object[] { obj }, null);
			LogWithType(logEntry);
		}

		public void Log(int eventID, NLog.LogLevel level, string message)
		{
			LogEntry logEntry = new LogEntry(eventID, level, Name, null, message, new object[] { }, null);
			LogWithType(logEntry);
		}

		public void Log(int eventID, NLog.LogLevel level, IFormatProvider formatProvider, object obj)
		{
			LogEntry logEntry = new LogEntry(eventID, level, Name, formatProvider, "{0}", new object[] { }, null);
			LogWithType(logEntry);
		}

		public void Log(int eventID, NLog.LogLevel level, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, level, Name, null, message, args, null);
			LogWithType(logEntry);
		}

		public void Log(int eventID, NLog.LogLevel level, IFormatProvider formatProvider, string message, params object[] args)
		{
			LogEntry logEntry = new LogEntry(eventID, level, Name, formatProvider, message, args, null);
			LogWithType(logEntry);
		}

		public void LogException(int eventID, NLog.LogLevel level, string message, Exception exception)
		{
			LogEntry logEntry = new LogEntry(eventID, level, Name, null, message, new object[0], exception);
			LogWithType(logEntry);
		}
	}
}
