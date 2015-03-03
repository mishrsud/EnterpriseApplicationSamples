using System;
using System.Collections.Specialized;
using Logging.Impl;
using Logging.Impl.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Test
{
	[TestClass]
	public class ConsoleOutLoggerTest : SimpleLoggingTestBase
	{
		public ConsoleOutLoggerTest()
		{
			NameValueCollection properties = GetProperties();

			// set Adapter
			LogManager.FactoryAdapter = new ConsoleOutLoggerFactory(properties);
			DefaultLogInstance = LogManager.GetLogger(GetType().FullName);
		}

		public override Type LoggerType
		{
			get { return typeof(ConsoleOutLogger); }
		}

		protected override void CheckLog(ILogger log)
		{
			Assert.IsNotNull(log);
			Assert.IsInstanceOfType(log, typeof(ConsoleOutLogger));

			// Can we call level checkers with no exceptions?
			// Note that everything is hard-coded to be disabled for NullLogger
			Assert.IsTrue(log.IsDebugEnabled);
			Assert.IsTrue(log.IsInfoEnabled);
			Assert.IsTrue(log.IsWarnEnabled);
			Assert.IsTrue(log.IsErrorEnabled);
			Assert.IsTrue(log.IsFatalEnabled);
		}
	}
}
