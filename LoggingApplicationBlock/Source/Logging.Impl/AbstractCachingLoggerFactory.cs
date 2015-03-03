using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Logging.Impl
{
	/// <summary>
	/// An implementation of <see cref="ILoggerFactory"/> that caches loggers created by the factory.
	/// </summary>
	/// <remarks>
	/// For a concrete implementation, override <see cref="CreateLogger"/>.
	/// </remarks>
	public abstract class AbstractCachingLoggerFactory : ILoggerFactory
	{
		private readonly Hashtable _loggers;

		/// <summary>
		/// Creates a new instance of the caching logger factory.
		/// </summary>
		/// <param name="caseSensitiveCache">
		/// If <c>true</c> loggers will be stored in the cache with case sensitivity.
		/// </param>
		protected AbstractCachingLoggerFactory(bool caseSensitiveCache)
		{
			_loggers = (caseSensitiveCache) ? new Hashtable() : CollectionsUtil.CreateCaseInsensitiveHashtable();
		}

		/// <summary>
		/// Flushes the logger cache.
		/// </summary>
		protected void ClearCache()
		{
			lock (_loggers)
			{
				_loggers.Clear();
			}
		}

		/// <summary>
		/// Creates an instance of a logger with a specific name.
		/// </summary>
		/// <param name="name">The name of the logger.</param>
		/// <remarks>
		/// Derived factories need to implement this method to create the actual logger instance.
		/// </remarks>
		protected abstract ILogger CreateLogger(string name);

		#region ILoggerFactory Members

		/// <summary>
		/// GetField an <see cref="ILogger"/> instance by type.
		/// </summary>
		/// <param name="type">The <see cref="Type">type</see> to use for the logger</param>
		/// <returns>An <see cref="ILogger"/> instance.</returns>
		public ILogger GetLogger(Type type)
		{
			if (type == null) throw new ArgumentNullException("type");
			return GetLoggerInternal(type.FullName);
		}

		/// <summary>
		/// GetField an <see cref="ILogger"/> instance by name.
		/// </summary>
		/// <param name="name">The name of the logger</param>
		/// <returns>An <see cref="ILogger"/> instance.</returns>
		public ILogger GetLogger(string name)
		{
			if (name == null) throw new ArgumentNullException("name");
			return GetLoggerInternal(name);
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ILogger GetCurrentClassLogger()
		{
			var frame = new StackFrame(1, false);
			return GetLogger(frame.GetMethod().DeclaringType);
		}

		#endregion

		#region GetLoggerInternal

		/// <summary>
		/// GetField or create a <see cref="ILogger" /> instance by name.
		/// </summary>
		/// <param name="name">Usually a <see cref="Type" />'s Name or FullName property.</param>
		/// <returns>
		/// An <see cref="ILogger" /> instance either obtained from the internal cache or created by a call to <see cref="CreateLogger"/>.
		/// </returns>
		private ILogger GetLoggerInternal(string name)
		{
			var logger = _loggers[name] as ILogger;
			if (logger == null)
			{
				lock (_loggers)
				{
					logger = _loggers[name] as ILogger;
					if (logger == null)
					{
						logger = CreateLogger(name);
						if (logger == null)
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "{0} returned null on creating logger instance for name {1}", GetType().FullName, name));
						}
					}
				}
			}
			return logger;
		}

		#endregion
	}
}
