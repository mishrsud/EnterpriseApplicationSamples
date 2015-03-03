using System.Collections.Specialized;
using System.Diagnostics;

namespace Logging.Impl.Concrete
{
	/// <summary>
	/// Factory for creating <see cref="ILogger" /> instances that send 
	/// everything to the <see cref="System.Diagnostics.Trace"/> output stream.
	/// </summary>
	/// <remarks>
	/// Based on the version by Gilles Bayon, Mark Pollack and Erich Eichinger in Common.Logging
	/// </remarks>
	/// <seealso cref="AbstractSimpleLoggerFactory"/>
	/// <seealso cref="LogManager.FactoryAdapter"/>
	public class TraceLoggerFactory : AbstractSimpleLoggerFactory
	{
		private bool _useTraceSource;

		/// <summary>
		/// Whether to use <see cref="Trace"/>.<c>TraceXXXX(string,object[])</c> methods for logging
		/// or <see cref="TraceSource"/>.
		/// </summary>
		public bool UseTraceSource
		{
			get { return _useTraceSource; }
			set { _useTraceSource = value; }
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="TraceLoggerFactory"/> class using default settings.
		/// </summary>
		public TraceLoggerFactory()
			: base(null)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="TraceLoggerFactory"/> class.
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
		public TraceLoggerFactory(NameValueCollection properties)
			: base(properties)
		{
			if (properties != null)
				_useTraceSource = ArgumentHelpers.TryParse(false, properties["useTraceSource"]);
		}

		/// <summary>
		/// Creates a new <see cref="TraceLogger"/> instance.
		/// </summary>
		protected override ILogger CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
		{
			return new TraceLogger(_useTraceSource, name, level, showLevel, showDateTime, showLogName, dateTimeFormat);
		}
	}
}
