using System;
using System.Collections.Specialized;
using System.Reflection;
using Logging.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Test
{
	[TestClass]
	public abstract class SimpleLoggingTestBase : LoggerTestBase
	{
		private static int s_count;

		private ILogger _defaultLogInstance;

		// ReSharper disable ConvertToAutoProperty
		public ILogger DefaultLogInstance
		// ReSharper restore ConvertToAutoProperty
		{
			get { return _defaultLogInstance; }
			set { _defaultLogInstance = value; }
		}

		protected static NameValueCollection GetProperties()
		{
			var properties = new NameValueCollection();
			properties["showDateTime"] = "true";
			if ((s_count % 2) == 0)
			{
				properties["dateTimeFormat"] = "yyyy/MM/dd HH:mm:ss:fff";
			}
			s_count++;
			return properties;
		}

		public override ILogger LogObject
		{
			get { return DefaultLogInstance; }
		}

		public abstract Type LoggerType { get; }

		[TestMethod, TestCategory("Unit")]
		public void SimpleLogger_DefaultSettings_ShouldBeApplied()
		{
			CheckLog(LogObject);
		}

		[TestMethod, TestCategory("Unit")]
		public void SimpleLogger_FromNamedLog_CanCallIsEnabled()
		{
			var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName);
			CanCallIsEnabled(log);
		}

		[TestMethod, TestCategory("Unit")]
		public void SimpleLogger_FromTypeLog_CanCallIsEnabled()
		{
			var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
			CanCallIsEnabled(log);
		}

		[TestMethod, TestCategory("Unit")]
		public void SimpleLogger_FromNamedLog_CanLogMessage()
		{
			var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName);
			CanLogMessage(log);
		}

		[TestMethod, TestCategory("Unit")]
		public void SimpleLogger_FromTypeLog_CanLogMessage()
		{
			var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
			CanLogMessage(log);
		}

		[TestMethod, TestCategory("Unit")]
		public void SimpleLogger_FromNamedLog_CanLogMessageWithException()
		{
			var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.FullName);
			CanLogMessageWithException(log);
		}

		[TestMethod, TestCategory("Unit")]
		public void SimpleLogger_FromTypeLog_CanLogMessageWithException()
		{
			var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
			CanLogMessageWithException(log);
		}

		protected virtual void CanCallIsEnabled(ILogger log)
		{
			if (log == null) throw new ArgumentNullException("log");
			bool b = log.IsTraceEnabled;
			b = log.IsDebugEnabled;
			b = log.IsErrorEnabled;
			b = log.IsFatalEnabled;
			b = log.IsInfoEnabled;
			b = log.IsWarnEnabled;
		}

		protected virtual void CanLogMessage(ILogger log)
		{
			if (log == null) throw new ArgumentNullException("log");
			log.TraceFormat(TestLogEventId.TestEventId, "Hi");
			log.Debug(TestLogEventId.TestEventId, "Hi");
			log.Info(TestLogEventId.TestEventId, "Hi");
			log.Warn(TestLogEventId.TestEventId, "Hi");
			log.Error(TestLogEventId.TestEventId, "Hi");
			log.Fatal(TestLogEventId.TestEventId, "Hi");
		}

		protected virtual void CanLogMessageWithException(ILogger log)
		{
			if (log == null) throw new ArgumentNullException("log");
			log.Trace(TestLogEventId.TestEventId, m => m("Hi {0}", "dude"));
			log.Debug(TestLogEventId.TestEventId, m => m("Hi {0}", "dude"), new ArithmeticException());
			log.Info(TestLogEventId.TestEventId, m => m("Hi {0}", "dude"), new ArithmeticException());
			log.Warn(TestLogEventId.TestEventId, m => m("Hi {0}", "dude"), new ArithmeticException());
			log.Error(TestLogEventId.TestEventId, m => m("Hi {0}", "dude"), new ArithmeticException());
			log.Fatal(TestLogEventId.TestEventId, m => m("Hi {0}", "dude"), new ArithmeticException());
		}

		/// <summary>
		/// Basic sanity checks of default values for a log implementation.
		/// </summary>
		/// <param name="log">A log implementation</param>
		protected abstract void CheckLog(ILogger log);
	}
}
