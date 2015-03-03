using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Logging.Impl.Concrete;

namespace Logging.Impl
{
	public static class LogManager
	{
		private static volatile ILoggerFactory _adapter;
		private static volatile ILoggingConfigurationSection _config;
		private static readonly object s_adapterConfigLock = new object();
		private const string SECURITY_LOGGER = "SecurityLogger";

		/// <summary>
		/// Resets the <see cref="LogManager"/> to it's default state.
		/// </summary>
		/// <remarks>
		/// Clears the <see cref="ConfigurationSection"/> and <see cref="FactoryAdapter"/>
		/// properties forcing a reload of these.
		/// </remarks>
		public static void Reset()
		{
			lock (s_adapterConfigLock)
			{
				SetAdaptor(null);
				SetConfig(null);
			}
		}

		/// <summary>
		/// Gets or sets the logger factory adapter.
		/// </summary>
		/// <remarks>
		/// If no custom adapter has been specified the <see cref="LogManager"/> builds a
		/// factory from the &lt;loggingApplicationBlock/logging/loggerFactory&gt; section, if one has been
		/// specified otherwise it defaults to the <see cref="NullLoggerFactory"/>.
		/// </remarks>
		/// <value>The logger factory adapter.</value>
		public static ILoggerFactory FactoryAdapter
		{
			get
			{
				if (_adapter == null)
				{
					lock (s_adapterConfigLock)
					{
						if (_adapter == null)
						{
							SetAdaptor(BuildLoggerFactory());
						}
					}
				}
				return _adapter;
			}
			set
			{
				if (value == null) throw new ArgumentNullException("value");

				SetAdaptor(value);
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="ILoggingConfigurationSection">config</see>
		/// that the <see cref="LogManager"/> will use to build the default logger factory.
		/// </summary>
		/// <remarks>
		/// If not specified it uses the default <see cref="LoggingConfigurationSection"/>.
		/// </remarks>
		public static ILoggingConfigurationSection ConfigurationSection
		{
			get
			{
				if (_config == null)
				{
					lock (s_adapterConfigLock)
					{
						if (_config == null)
						{
							SetConfig(LoggingConfigurationSection.GetSection());
						}
					}
				}
				return _config;
			}
			set
			{
				if (value == null) throw new ArgumentNullException("value");

				SetConfig(value);
			}
		}

		/// <summary>
		/// Gets the logger by calling <see cref="ILoggerFactory.GetLogger(Type)"/>
		/// on the currently configured <see cref="FactoryAdapter"/> using the type of the calling class.
		/// </summary>
		/// <remarks>
		/// This method needs to inspect the <see cref="StackTrace"/> in order to determine the calling 
		/// class. This of course comes with a performance penalty, thus you shouldn't call it too
		/// often in your application.
		/// </remarks>
		/// <seealso cref="GetLogger(Type)"/>
		/// <returns>the logger instance obtained from the current <see cref="FactoryAdapter"/></returns>
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ILogger GetCurrentClassLogger()
		{
			// NOTE: Could use the C# 5.0 CallerMemberName feature if we didn't need the actual class name ...

			var frame = new StackFrame(1, false);
			return FactoryAdapter.GetLogger(frame.GetMethod().DeclaringType);
		}

		/// <summary>
		/// Gets the logger by calling <see cref="ILoggerFactory.GetLogger(Type)"/>
		/// on the currently configured <see cref="FactoryAdapter"/> using the specified type.
		/// </summary>
		/// <returns>the logger instance obtained from the current <see cref="FactoryAdapter"/></returns>
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ILogger GetSecurityLogger()
		{

			return FactoryAdapter.GetLogger(SECURITY_LOGGER); //typeof(T));
		}

		/// <summary>
		/// Gets the logger by calling <see cref="ILoggerFactory.GetLogger(Type)"/>
		/// on the currently configured <see cref="FactoryAdapter"/> using the specified type.
		/// </summary>
		/// <returns>the logger instance obtained from the current <see cref="FactoryAdapter"/></returns>
		public static ILogger GetLogger<T>(T type)
		{
			return FactoryAdapter.GetLogger(type.GetType()); //typeof(T));
		}

		/// <summary>
		/// Gets the logger by calling <see cref="ILoggerFactory.GetLogger(Type)"/>
		/// on the currently configured <see cref="FactoryAdapter"/> using the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>the logger instance obtained from the current <see cref="FactoryAdapter"/></returns>
		public static ILogger GetLogger(Type type)
		{
			return FactoryAdapter.GetLogger(type);
		}

		/// <summary>
		/// Gets the logger by calling <see cref="ILoggerFactory.GetLogger(string)"/>
		/// on the currently configured <see cref="FactoryAdapter"/> using the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>the logger instance obtained from the current <see cref="FactoryAdapter"/></returns>
		public static ILogger GetLogger(string name)
		{
			return FactoryAdapter.GetLogger(name);
		}

		/// <summary>
		/// Log message by LogLevel.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="logLevel">The log level.</param>
		/// <param name="eventId">The event id.</param>
		/// <param name="message">The message.</param>
		/// <exception cref="System.ArgumentNullException">logger</exception>
		public static void LogByLevel(ILogger logger, LogLevel logLevel, Enum eventId, object message)
		{
			if (logger == null) throw new ArgumentNullException("logger");

			switch (logLevel)
			{
				case LogLevel.Trace:
					logger.Trace(eventId, message);
					break;

				case LogLevel.Debug:
					logger.Debug(eventId, message);
					break;

				case LogLevel.All:
				case LogLevel.Info:
					logger.Info(eventId, message);
					break;

				case LogLevel.Warn:
					logger.Warn(eventId, message);
					break;

				case LogLevel.Error:
					logger.Error(eventId, message);
					break;

				case LogLevel.Fatal:
					logger.Fatal(eventId, message);
					break;

				// Don't handle LogLevel.Off
			}
		}

		/// <summary>
		/// Log message by LogLevel including an exception object.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="logLevel">The log level.</param>
		/// <param name="eventId">The event id.</param>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		/// <exception cref="System.ArgumentNullException">logger</exception>
		public static void LogByLevel(ILogger logger, LogLevel logLevel, Enum eventId, object message, Exception exception)
		{
			if (logger == null) throw new ArgumentNullException("logger");
			if (exception == null) throw new ArgumentNullException("exception");

			switch (logLevel)
			{
				case LogLevel.Trace:
					logger.Trace(eventId, message, exception);
					break;

				case LogLevel.Debug:
					logger.Debug(eventId, message, exception);
					break;

				case LogLevel.All:
				case LogLevel.Info:
					logger.Info(eventId, message, exception);
					break;

				case LogLevel.Warn:
					logger.Warn(eventId, message, exception);
					break;

				case LogLevel.Error:
					logger.Error(eventId, message, exception);
					break;

				case LogLevel.Fatal:
					logger.Fatal(eventId, message, exception);
					break;

				// Don't handle LogLevel.Off
			}
		}

		/// <summary>
		/// Builds the logger factory specified in the openApi/logging configuration section.
		/// </summary>
		/// <remarks>
		/// If no configuration is specified, the method defaults to the <see cref="NullLoggerFactory"/>.
		/// </remarks>
		/// <returns>A factory adapter instance. Is never <c>null</c>.</returns>
		private static ILoggerFactory BuildLoggerFactory()
		{
			Type factoryType;

			var config = ConfigurationSection;
			if (config == null || config.LoggerFactory == null)
			{
				var message = string.Format(CultureInfo.InvariantCulture, "No logging configuration section <{0}> found - suppressing logging output", LoggingConfigurationSection.LoggingSectionPath);
				Trace.WriteLine(message);
				return new NullLoggerFactory();
			}

			var factoryTypeString = config.LoggerFactory.FactoryType;

			try
			{
				switch (factoryTypeString.ToUpperInvariant())
				{
					case "CONSOLE":
						factoryType = typeof(ConsoleOutLoggerFactory);
						break;
					case "TRACE":
						factoryType = typeof(TraceLoggerFactory);
						break;
					case "NULL":
						factoryType = typeof(NullLoggerFactory);
						break;
					default:
						factoryType = Type.GetType(factoryTypeString, true, false);
						break;
				}
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException("Unable to create type '" + factoryTypeString + "'", ex);
			}

			return BuildLoggerFactory(factoryType, ConvertArguments(config.LoggerFactory.Arguments));
		}

		/// <summary>
		/// Builds a <see cref="ILoggerFactory"/> instance from the specified type
		/// using <see cref="Activator"/>.
		/// </summary>
		/// <param name="factoryType"><see cref="Type"/> of factory to create.</param>
		/// <param name="arguments">A <see cref="NameValueCollection"/> containing logger factory constructor arguments.</param>
		/// <returns>the <see cref="ILoggerFactory"/> instance. Is never <c>null</c></returns>
		private static ILoggerFactory BuildLoggerFactory(Type factoryType, ICollection arguments)
		{
			if (!typeof(ILoggerFactory).IsAssignableFrom(factoryType))
				throw new ConfigurationErrorsException(
					string.Format(CultureInfo.InvariantCulture, "Specified factory type does not implement {0}.  Check implementation of class {1}",
					typeof(ILoggerFactory).FullName,
					factoryType.AssemblyQualifiedName));

			try
			{
				if (arguments != null && arguments.Count > 0)
					return (ILoggerFactory)Activator.CreateInstance(factoryType, arguments);

				return (ILoggerFactory)Activator.CreateInstance(factoryType);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(
					string.Format(CultureInfo.InvariantCulture, "Unable to create instance of type {0}. Possible explanation is lack of zero- and single argument name/value collection constructors",
					factoryType.FullName), ex);
			}
		}

		/// <summary>
		/// Sets the adaptor. Can be null.
		/// </summary>
		/// <param name="factory">The factory.</param>
		private static void SetAdaptor(ILoggerFactory factory)
		{
			lock (s_adapterConfigLock)
			{
				_adapter = factory;
			}
		}

		/// <summary>
		/// Sets the config. Can be null.
		/// </summary>
		/// <param name="config">The config.</param>
		private static void SetConfig(ILoggingConfigurationSection config)
		{
			lock (s_adapterConfigLock)
			{
				_config = config;
			}
		}

		/// <summary>
		/// Copies arguments from a <see cref="NameValueConfigurationCollection"/> to 
		/// a <see cref="NameValueCollection"/>.
		/// </summary>
		/// <param name="arguments">The arguments.</param>
		private static NameValueCollection ConvertArguments(NameValueConfigurationCollection arguments)
		{
			var collection = new NameValueCollection(arguments.Count);
			foreach (var element in arguments.AllKeys.Select(key => arguments[key]))
			{
				collection.Add(element.Name, element.Value);
			}
			return collection;
		}
	}
}
