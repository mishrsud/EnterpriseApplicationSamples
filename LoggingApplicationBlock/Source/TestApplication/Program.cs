using System;
using Logging.Impl;

namespace TestApplication
{
	class Program
	{
		private static Logging.ILogger _logger = LogManager.GetCurrentClassLogger();

		static void Main(string[] args)
		{
			_logger = LogManager.GetCurrentClassLogger();
			_logger.Info(LogEventId.InformationOnly, "Test");

			MethodThatThrows();
			Console.WriteLine("Press any key to exit");
			Console.ReadLine();
		}

		static void MethodThatThrows()
		{
			try
			{
				throw new Exception("Something bad happened");
			}
			catch (Exception ex)
			{
				_logger.Error(LogEventId.UnhandledException, "Something bad happened", ex);
			}
		}
	}
}
