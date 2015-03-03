namespace Logging.Impl
{
	public interface ILoggingConfigurationSection
	{
		/// <summary>
		/// Gets or sets the <see cref="ILoggerFactory"/> that the <see cref="LogManager"/>
		/// uses to return instances of loggers.
		/// </summary>
		LoggerFactoryElement LoggerFactory { get; set; }
	}
}
