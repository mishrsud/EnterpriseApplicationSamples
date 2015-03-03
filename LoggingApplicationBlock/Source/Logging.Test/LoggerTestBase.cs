using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Test
{
	[TestClass]
	public abstract class LoggerTestBase
	{
		public abstract ILogger LogObject { get; }

		[TestMethod, TestCategory("Unit")]
		public void Logging_WithNullParameters_ShouldBeAllowed()
		{
			ILogger log = LogObject;

			Assert.IsNotNull(log);

			log.Trace(TestLogEventId.TestEventId, (object)null);
			log.Trace(TestLogEventId.TestEventId, (object)null, null);
			log.TraceFormat(TestLogEventId.TestEventId, log.GetType().FullName + ": trace statement");
			log.Trace(TestLogEventId.TestEventId, log.GetType().FullName + ": trace statement w/ exception",
					  new ArgumentException("exception message"));
			try
			{
				new ArgumentException("exception message");
			}
			catch (ArgumentException e)
			{
				log.Trace(TestLogEventId.TestEventId, log.GetType().FullName + ": trace statement w/ exception", e);
			}

			log.Debug(TestLogEventId.TestEventId, null);
			log.Debug(TestLogEventId.TestEventId, (object)null, null);
			log.Debug(TestLogEventId.TestEventId, log.GetType().FullName + ": debug statement");
			try
			{
				new ArgumentException("exception message");
			}
			catch (ArgumentException e)
			{
				log.Debug(TestLogEventId.TestEventId, log.GetType().FullName + ": debug statement w/ exception", e);
			}

			log.Info(TestLogEventId.TestEventId, null);
			log.Info(TestLogEventId.TestEventId, (object)null, null);
			log.Info(TestLogEventId.TestEventId, log.GetType().FullName + ": info statement");
			try
			{
				throw new ArgumentException("exception message");
			}
			catch (ArgumentException e)
			{
				log.Info(TestLogEventId.TestEventId, log.GetType().FullName + ": info statement w/ exception", e);
			}

			log.Warn(TestLogEventId.TestEventId, null);
			log.Warn(TestLogEventId.TestEventId, (object)null, null);
			log.Warn(TestLogEventId.TestEventId, log.GetType().FullName + ": warn statement");
			try
			{
				throw new ArgumentException("exception message");
			}
			catch (ArgumentException e)
			{
				log.Warn(TestLogEventId.TestEventId, log.GetType().FullName + ": warn statement w/ exception", e);
			}

			log.Error(TestLogEventId.TestEventId, null);
			log.Error(TestLogEventId.TestEventId, (object)null, null);
			log.Error(TestLogEventId.TestEventId, log.GetType().FullName + ": error statement");
			try
			{
				throw new ArgumentException("exception message");
			}
			catch (ArgumentException e)
			{
				log.Error(TestLogEventId.TestEventId, log.GetType().FullName + ": error statement w/ exception", e);
			}


			log.Fatal(TestLogEventId.TestEventId, null);
			log.Fatal(TestLogEventId.TestEventId, (object)null, null);
			log.Fatal(TestLogEventId.TestEventId, log.GetType().FullName + ": fatal statement");
			try
			{
				throw new ArgumentException("exception message");
			}
			catch (ArgumentException e)
			{
				log.Fatal(TestLogEventId.TestEventId, log.GetType().FullName + ": fatal statement w/ exception", e);
			}
		}

	}
}
