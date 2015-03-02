using System;
using System.Runtime.Serialization;

namespace Database
{
	/// <summary>
	/// Exception thrown if the database is deemed unavailable during a database call.
	/// Unavailable can be a timeout or that the database server is shutting down.
	/// </summary>
	[Serializable]
	public class DatabaseUnavailableException : DataAccessException
	{
		/// <summary>
		/// Creates a new instance of a <see cref="DatabaseUnavailableException"/>.
		/// </summary>
		public DatabaseUnavailableException()
		{
		}

		/// <summary>
		/// Creates a new instance of a <see cref="DatabaseUnavailableException"/>.
		/// </summary>
		/// <param name="message"></param>
		public DatabaseUnavailableException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a new instance of a <see cref="DatabaseUnavailableException"/>.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public DatabaseUnavailableException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>
		/// Creates a new instance of a <see cref="DatabaseUnavailableException"/>.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected DatabaseUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
