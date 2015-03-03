using System;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Logging.Impl.Concrete
{
	/// <summary>
	/// No operation factory logger implementation of <see cref="ILoggerFactory"/>.
	/// Responsible for creating <see cref="NullLogger"/> instances.
	/// </summary>
	public class NullLoggerFactory : ILoggerFactory
	{
		private static readonly ILogger s_logger = new NullLogger();

		/// <summary>
		/// Constructs an instance of a <see cref="NullLoggerFactory"/>.
		/// </summary>
		public NullLoggerFactory()
		{
		}

		/// <summary>
		/// Constructs an instance of a <see cref="NullLoggerFactory"/>
		/// with custom property settings.
		/// </summary>
		/// <param name="properties">
		/// A <see cref="NameValueCollection"/> of custom logger properties.
		/// </param>
		public NullLoggerFactory(NameValueCollection properties)
		{
		}

		#region ILoggerFactory Members

		/// <summary>
		/// GetField an <see cref="ILogger"/> instance by type.
		/// </summary>
		/// <param name="type">The <see cref="Type">type</see> to use for the logger</param>
		/// <returns>An <see cref="ILogger"/> instance.</returns>
		public ILogger GetLogger(Type type)
		{
			return s_logger;
		}

		/// <summary>
		/// GetField an <see cref="ILogger"/> instance by name.
		/// </summary>
		/// <param name="name">The name of the logger</param>
		/// <returns>An <see cref="ILogger"/> instance.</returns>
		public ILogger GetLogger(string name)
		{
			return s_logger;
		}

		/// <summary>
		/// Gets a logger using the type of the calling class.
		/// </summary>
		/// <remarks>
		/// This method needs to inspect the <see cref="StackTrace"/> in order to determine the calling 
		/// class. This of course comes with a performance penalty, thus you shouldn't call it too
		/// often in your application.
		/// </remarks>
		/// <seealso cref="ILoggerFactory.GetLogger(System.Type)"/>
		public ILogger GetCurrentClassLogger()
		{
			return s_logger;
		}

		#endregion
	}
}
