using System.Configuration;

namespace Logging
{
	public class LoggerFactoryElement : ConfigurationElement
	{
		/// <summary>
		/// Gets or sets the type of the <see cref="ILoggerFactory"/> to use.
		/// </summary>
		/// <value>The type of the factory.</value>
		[ConfigurationProperty("type", IsRequired = true)]
		public string FactoryType
		{
			get { return (string)this["type"]; }
			set { this["type"] = value; }
		}

		/// <summary>
		/// Gets arguments to pass into the constructor of the specified logger factory.
		/// </summary>
		/// <value>The arguments.</value>
		/// <remarks>
		/// The logger factory argument list in the configuration file allows implementation
		/// specific arguments to be specified.
		/// Arguments differ from logging implementation to logging implementation. NLog
		/// takes a specific set of arguments whereas log4net takes another.
		/// </remarks>
		[ConfigurationProperty("arguments", IsRequired = false, IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(NameValueConfigurationCollection), AddItemName = "add")]
		public NameValueConfigurationCollection Arguments
		{
			get { return (NameValueConfigurationCollection)this["arguments"]; }
		}
	}
}
