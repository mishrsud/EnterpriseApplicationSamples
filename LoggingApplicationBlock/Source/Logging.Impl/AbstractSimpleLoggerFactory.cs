using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Impl
{
	/// <summary>
	/// Base factory implementation for creating simple <see cref="ILogger" /> instances.
	/// </summary>
	/// <remarks>Default settings are LogLevel.All, showDateTime = true, showLogName = true, and no DateTimeFormat.
	/// The keys in the NameValueCollection to configure this adapter are the following
	/// <list type="bullet">
	///     <item>level</item>
	///     <item>showDateTime</item>
	///     <item>showLogName</item>
	///     <item>dateTimeFormat</item>
	/// </list>
	/// </remarks>
	/// <seealso cref="LogManager.FactoryAdapter"/>
	public abstract class AbstractSimpleLoggerFactory : AbstractCachingLoggerFactory
	{
		private LogLevel _level;
		private bool _showLevel;
		private bool _showDateTime;
		private bool _showLogName;
		private string _dateTimeFormat;

		/// <summary>
		/// Initializes a new instance of the <see cref="AbstractSimpleLoggerFactory"/> class.
		/// </summary>
		/// <remarks>
		/// Looks for level, showDateTime, showLogName, dateTimeFormat items from 
		/// <paramref name="properties" /> for use when the GetLogger methods are called.
		/// <see cref="LoggingSection"/> for more information on how to use the 
		/// standard .NET application configuraiton file (App.config/Web.config) 
		/// to configure this adapter.
		/// </remarks>
		/// <param name="properties">The name value collection, typically specified by the user in 
		/// a configuration section named common/logging.</param>
		protected AbstractSimpleLoggerFactory(NameValueCollection properties)
			: base(true)
		{
			const bool parseLevelIgnoreCase = true;
			_level = ArgumentHelpers.TryParseEnum(LogLevel.All, ArgumentHelpers.GetValue(properties, "level"), parseLevelIgnoreCase);
			_showDateTime = ArgumentHelpers.TryParse(true, ArgumentHelpers.GetValue(properties, "showDateTime"));
			_showLogName = ArgumentHelpers.TryParse(true, ArgumentHelpers.GetValue(properties, "showLogName"));
			_showLevel = ArgumentHelpers.TryParse(true, ArgumentHelpers.GetValue(properties, "showLevel"));
			_dateTimeFormat = ArgumentHelpers.GetValue(properties, "dateTimeFormat", String.Empty);
		}

		/// <summary>
		/// The default <see cref="LogLevel"/> to use when creating new <see cref="ILogger"/> instances.
		/// </summary>
		public LogLevel Level
		{
			get { return _level; }
			set { _level = value; }
		}

		/// <summary>
		/// The default setting to use when creating new <see cref="ILogger"/> instances.
		/// </summary>
		public bool ShowLevel
		{
			get { return _showLevel; }
			set { _showLevel = value; }
		}

		/// <summary>
		/// The default setting to use when creating new <see cref="ILogger"/> instances.
		/// </summary>
		public bool ShowDateTime
		{
			get { return _showDateTime; }
			set { _showDateTime = value; }
		}

		/// <summary>
		/// The default setting to use when creating new <see cref="ILogger"/> instances.
		/// </summary>
		public bool ShowLogName
		{
			get { return _showLogName; }
			set { _showLogName = value; }
		}

		/// <summary>
		/// The default setting to use when creating new <see cref="ILogger"/> instances.
		/// </summary>
		public string DateTimeFormat
		{
			get { return _dateTimeFormat; }
			set { _dateTimeFormat = value; }
		}

		/// <summary>
		/// Create the specified logger instance
		/// </summary>
		protected override ILogger CreateLogger(string name)
		{
			return CreateLogger(name, _level, _showLevel, _showDateTime, _showLogName, _dateTimeFormat);
		}

		/// <summary>
		/// Derived factories need to implement this method to create the
		/// actual logger instance.
		/// </summary>
		/// <returns>a new logger instance. Must never be <c>null</c>!</returns>
		protected abstract ILogger CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat);

	}
}
