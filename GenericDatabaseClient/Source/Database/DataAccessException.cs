using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
	/// <summary>
	/// Describes an exception encountered during data access
	/// </summary>
	[Serializable]
	[ExcludeFromCodeCoverage]
	public class DataAccessException : Exception
	{
		/// <summary>
		/// The database info.
		/// </summary>
		/// <value>The database info.</value>
		public DatabaseContext DatabaseContext { get; private set; }

		/// <summary>
		/// The CMD text.
		/// </summary>
		/// <value>The CMD text.</value>
		public string CmdText { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAccessException"/> class.
		/// </summary>
		public DataAccessException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAccessException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public DataAccessException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAccessException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="innerException">The inner exception.</param>
		public DataAccessException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAccessException" /> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="DatabaseContext">The database info.</param>
		/// <param name="cmdText">The CMD text.</param>
		/// <param name="innerException">The inner exception.</param>
		public DataAccessException(string message, DatabaseContext databaseContext, string cmdText, Exception innerException)
			: base(message, innerException)
		{
			DatabaseContext = databaseContext;
			CmdText = cmdText;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAccessException"/> class.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
		protected DataAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private static string FormatExceptionText(string message, DatabaseContext databaseContext, string commandText)
		{
			var exceptionText = String.Format(CultureInfo.InvariantCulture,
									"{0}.\r\n" +
									"\tDB server name: {1}\r\n" +
									"\tDatabase name: {2}\r\n" +
									"\tSQL command: {3}\r\n",
									message,
									databaseContext.ServerName,
									databaseContext.Database,
									commandText
								);
			return exceptionText;
		}

		/// <summary>
		/// Creates the with context info message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="databaseContext">The database info.</param>
		/// <param name="cmdText">The CMD text.</param>
		/// <param name="innerException">The inner exception.</param>
		/// <returns>The <see cref="DataAccessException"/> object.</returns>
		/// <exception cref="System.ArgumentNullException">databaseInfo cannot be null</exception>
		public static DataAccessException CreateWithContextInfoMessage(string message, DatabaseContext databaseContext, string cmdText, Exception innerException)
		{
			if (databaseContext == null) throw new ArgumentNullException("databaseInfo cannot be null", innerException);

			var contextMessage = FormatExceptionText(message, databaseContext, cmdText);
			return new DataAccessException(contextMessage, databaseContext, cmdText, innerException);
		}
	}
}
