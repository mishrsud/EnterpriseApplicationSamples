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
			_logger.Info(1000, "Test");

			MethodThatThrows();
			Console.WriteLine("Press any key to exit");
			Console.ReadLine();
		}

		static void MethodThatThrows()
		{
			try
			{
				throw new Exception("SHIT");
			}
			catch (Exception ex)
			{
				_logger.Error(5000, "Something", ex);
			}
		}
	}
}
