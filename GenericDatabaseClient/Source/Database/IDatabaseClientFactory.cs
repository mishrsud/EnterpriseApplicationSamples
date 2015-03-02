namespace Database
{
	/// <summary>
	/// A factory to create instances of <see cref="IDatabaseClient"/>
	/// </summary>
	public interface IDatabaseClientFactory
	{
		/// <summary>
		/// Creates a new instance of <see cref="IDatabaseClient"/> using the specified connection string.
		/// </summary>
		///<param name="connectionString">The connection string to use for the connection.</param>
		IDatabaseClient CreateDatabaseClient(string connectionString);
	}
}
