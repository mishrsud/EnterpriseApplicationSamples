using System;
using System.Configuration;

namespace Logging.Impl
{
	public class LoggingConfigurationSection : ConfigurationSection, ILoggingConfigurationSection
	{
		/// <summary>
		/// Constant string with name path of the logging configuration section.
		/// </summary>
		public static readonly string LoggingSectionPath = "loggingApplicationBlock/logging";

		/// <summary>
		/// Gets or sets the <see cref="ILoggerFactory"/> that the <see cref="LogManager"/>
		/// uses to return instances of loggers.
		/// </summary>
		/// <value></value>
		[ConfigurationProperty("loggerFactory", IsRequired = false)]
		public LoggerFactoryElement LoggerFactory
		{
			get { return (LoggerFactoryElement)this["loggerFactory"]; }
			set { this["loggerFactory"] = value; }
		}

		/// <summary>
		/// Constructs an instance of <see cref="LoggingConfigurationSection"/> initialized
		/// with configuration values read from the &lt;loggingApplicationBlock/logging&gt; section.
		/// </summary>
		/// <returns></returns>
		public static LoggingConfigurationSection GetSection()
		{
			return GetSection(LoggingSectionPath);
		}

		internal static LoggingConfigurationSection GetSection(string sectionPath)
		{
			LoggingConfigurationSection section;
			try
			{
				section = ConfigurationManager.GetSection(sectionPath) as LoggingConfigurationSection;
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(ex.Message, ex);
			}
			return section;
		}
	}
}
