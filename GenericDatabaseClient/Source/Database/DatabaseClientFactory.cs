using System;

namespace Database
{
	/// <summary>
	/// A factory to create instances of <see cref="IDatabaseClient"/>
	/// </summary>
	public class DatabaseClientFactory : IDatabaseClientFactory
	{
		/// <summary>
		/// Creates a new instance of <see cref="IDatabaseClient" /> using the specified connection string.
		/// </summary>
		/// <param name="connectionString">The connection string to use for the connection.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">connectionString cannot be empty or whitespace;connectionString</exception>
		public IDatabaseClient CreateDatabaseClient(string connectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("connectionString cannot be empty or whitespace", "connectionString");

			return new DatabaseClient(() => connectionString);
		}
	}
}