using System;

namespace Logging
{
	/// <summary>
	/// Models a generic logger factory that can be injected into a generic log manager
	/// </summary>
	public interface ILoggerFactory
	{
		/// <summary>
		/// GetField an <see cref="ILogger" /> instance by type.
		/// </summary>
		/// <param name="type">The <see cref="Type">type</see> to use for the logger</param>
		/// <returns>An <see cref="ILogger"/> instance.</returns>
		ILogger GetLogger(Type type);

		/// <summary>
		/// GetField an <see cref="ILogger" /> instance by name.
		/// </summary>
		/// <param name="name">The name of the logger</param>
		/// <returns>An <see cref="ILogger"/> instance.</returns>
		ILogger GetLogger(string name);

		/// <summary>
		/// Gets a logger using the type of the calling class.
		/// </summary>
		/// <remarks>
		/// This method needs to inspect the StackTrace in order to determine the calling 
		/// class. This of course comes with a performance penalty, most common use cases would use a static field of this type
		/// </remarks>
		/// <seealso cref="GetLogger(Type)"/>
		ILogger GetCurrentClassLogger();
	}
}
