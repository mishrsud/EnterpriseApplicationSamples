using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
	public interface IDatabaseClient : IDisposable
	{
		///<summary>
		/// Calls a stored procedure and returns the result as a <see cref="ISafeDataReader"/>
		///</summary>
		///<param name="procedureName">The name of the stored procedure to call</param>
		///<param name="parameters">The <see cref="SqlParameter"/>s to use in the call</param>
		///<returns>Returns a <see cref="ISafeDataReader"/> with the resulting rows</returns>
		/// <exception cref="T:Iit.OpenApi.Framework.Database.DataAccessException">The Database client failed to called the stored procedure.</exception>
		/// <exception cref="T:System.ArgumentNullException">The procedureName is null.</exception>
		ISafeDataReader GetDataReaderFromSproc(string procedureName, SqlParameter[] parameters);

		///<summary>
		/// Calls a stored procedure and returns the result as a DataSet
		///</summary>
		///<param name="procedureName">The name of the stored procedure to call</param>
		///<param name="parameters">The <see cref="SqlParameter"/>s to use in the call</param>
		///<returns>Returns a DataSet with the resulting rows</returns>
		/// <exception cref="T:Iit.OpenApi.Framework.Database.DataAccessException">The Database client failed to called the stored procedure.</exception>
		/// <exception cref="T:System.ArgumentNullException">The procedureName is null.</exception>
		DataSet GetDataSetFromSproc(string procedureName, SqlParameter[] parameters);

		///<summary>
		/// Takes name and parameters for a stored procedure, runs it and converts the result into a list of objects.
		///</summary>
		///<param name="procedureName">The name of the stored procedure to call.</param>
		///<param name="parameters">The parameters to pass to the stored procedure.</param>
		///<param name="converter">The converter function to use to convert each row to an object in the list.</param>
		///<typeparam name="T">The type of the objects in the list.</typeparam>
		///<returns>A list of objects each representing a row.</returns>
		IList<T> StoredProcedureToList<T>(string procedureName, SqlParameter[] parameters, Func<IDataItem, T> converter);

		/// <summary>
		/// Executes a stored procedure without returning a result.
		/// </summary>
		///<param name="procedureName">The name of the stored procedure to call.</param>
		///<param name="parameters">The parameters to pass to the stored procedure.</param>
		void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters);

		/// <summary>
		/// Executes a stored procedure and returns the scalar result.
		/// </summary>
		///<param name="procedureName">The name of the stored procedure to call.</param>
		///<param name="parameters">The parameters to pass to the stored procedure.</param>
		/// <typeparam name="T">The type returned from the stored procedure.</typeparam>
		/// <returns>The result returned by the stored procedure.</returns>
		T ExecuteStoredProcedure<T>(string procedureName, SqlParameter[] parameters);
	}
}
