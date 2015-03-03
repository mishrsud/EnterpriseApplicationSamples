using System;
using System.Collections.Specialized;

namespace Logging.Impl.Concrete
{
	/// <summary>
	/// Factory for creating <see cref="ILogger" /> instances that write data to <see cref="Console.Out" />.
	/// </summary>
	/// <seealso cref="AbstractSimpleLoggerFactory"/>
	/// <seealso cref="LogManager.FactoryAdapter"/>
	/// <seealso cref="LoggingSection"/>
	public class ConsoleOutLoggerFactory : AbstractSimpleLoggerFactory
	{
		/// <summary>
		/// Creates a new instance of a <see cref="ConsoleOutLoggerFactory"/> using default settings.
		/// </summary>
		public ConsoleOutLoggerFactory() : base(null) { }

		/// <summary>
		/// Creates a new instance of a <see cref="ConsoleOutLoggerFactory"/>.
		/// </summary>
		/// <remarks>
		/// Looks for level, showDateTime, showLogName, dateTimeFormat items from 
		/// <paramref name="properties" /> for use when the GetLogger methods are called.
		/// <see cref="LoggingSection"/> for more information on how to use the 
		/// standard .NET application configuraiton file (App.config/Web.config) 
		/// to configure this adapter.
		/// </remarks>
		/// <param name="properties">
		/// The name value collection, typically specified by the user in 
		/// a configuration section named common/logging.
		/// </param>
		public ConsoleOutLoggerFactory(NameValueCollection properties) : base(properties) { }

		/// <summary>
		/// Creates a new <see cref="ConsoleOutLogger"/> instance.
		/// </summary>
		protected override ILogger CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
		{
			return new ConsoleOutLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat);
		}
	}
}
