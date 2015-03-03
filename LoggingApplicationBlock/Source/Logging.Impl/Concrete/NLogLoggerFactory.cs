using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using NLog.Config;

namespace Logging.Impl.Concrete
{
	public class NLogLoggerFactory : AbstractCachingLoggerFactory 
	{
		/// <summary>
		/// Creates a new instance of the <see cref="NLogLoggerFactory" />.
		/// </summary>
		/// <param name="properties">
		/// A <see cref="NameValueCollection" /> with configuration values for the <see cref="NLog.Logger" />.
		/// </param>
		public NLogLoggerFactory(NameValueCollection properties)
			: base(true)
		{
			var configType = String.Empty;
			var configFile = String.Empty;

			if (properties != null)
			{
				if (properties["configType"] != null)
				{
					configType = properties["configType"].ToUpperInvariant();
				}

				if (properties["configFile"] != null)
				{
					configFile = properties["configFile"];
					if (configFile.StartsWith("~/", StringComparison.OrdinalIgnoreCase) || configFile.StartsWith("~\\", StringComparison.OrdinalIgnoreCase))
					{
						configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/", configFile.Substring(2));
					}
				}

				if (configType == "FILE")
				{
					if (string.IsNullOrWhiteSpace(configFile))
					{
						throw new ConfigurationErrorsException("Configuration property 'configFile' must be set for NLog configuration of type 'FILE'.");
					}

					if (!File.Exists(configFile))
					{
						throw new ConfigurationErrorsException("NLog configuration file '" + configFile + "' does not exist");
					}
				}
			}
			switch (configType)
			{
				case "INLINE":
					break;
				case "FILE":
					NLog.LogManager.Configuration = new XmlLoggingConfiguration(configFile);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Creates an instance of an <see cref="NLog.Logger" /> with a specific name.
		/// </summary>
		/// <param name="name">The name of the logger.</param>
		/// <returns></returns>
		protected override ILogger CreateLogger(string name)
		{
			var logger = NLog.LogManager.GetLogger(name, typeof(NLogWrapper)) as NLogWrapper;
			return new NLogLogger(logger);
		}
	}
}
