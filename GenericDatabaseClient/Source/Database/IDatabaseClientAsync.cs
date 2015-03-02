using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Database
{
	/// <summary>
	/// Interface for the asynchronous database client.
	/// </summary>
	public interface IDatabaseClientAsync : IDisposable
	{
		///<summary>
		/// Calls a stored procedure and returns the result as a <see cref="ISafeDataReader"/>
		///</summary>
		///<param name="procedureName">The name of the stored procedure to call</param>
		///<param name="parameters">The <see cref="SqlParameter"/>s to use in the call</param>
		///<returns>Returns a <see cref="ISafeDataReader"/> with the resulting rows</returns>
		/// <exception cref="T:Iit.OpenApi.Framework.Database.DataAccessException">The Database client failed to called the stored procedure.</exception>
		/// <exception cref="T:System.ArgumentNullException">The procedureName is null.</exception>
		Task<ISafeDataReaderAsync> GetDataReaderFromSprocAsync(string procedureName, SqlParameter[] parameters);

		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns></returns>
		Task<SqlConnection> CreateConnectionAsync();
	}
}
