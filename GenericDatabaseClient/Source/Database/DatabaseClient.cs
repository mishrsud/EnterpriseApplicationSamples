using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Database
{
	public sealed class DatabaseClient : IDatabaseClient, IDatabaseClientAsync
	{
		//private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		private readonly Func<string> _connectionStringFactory;
		private SqlConnection _currentConnection;


		///<summary>
		/// Create a database client for the given database.
		///</summary>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="connectionStringFactory"/> is null.</exception>
		public DatabaseClient(Func<string> connectionStringFactory)
		{
			_connectionStringFactory = connectionStringFactory;
			if (connectionStringFactory == null) throw new ArgumentNullException("connectionStringFactory");
		}

		///<summary>
		/// Calls a stored procedure and returns the result as a <see cref="ISafeDataReader"/>
		///</summary>
		///<param name="procedureName">The name of the stored procedure to call</param>
		///<param name="parameters">The <see cref="SqlParameter"/>s to use in the call</param>
		///<returns>Returns a <see cref="ISafeDataReader"/> with the resulting rows</returns>
		/// <exception cref="T:Iit.OpenApi.Framework.Database.DataAccessException">The Database client failed to called the stored procedure.</exception>
		/// <exception cref="T:System.ArgumentNullException">The procedureName is null.</exception>
		public ISafeDataReader GetDataReaderFromSproc(string procedureName, SqlParameter[] parameters)
		{
			if (String.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

			return ExecuteSqlWithinTryCatch(() =>
			{
				var connection = GetConnection();
				using (var cmd = connection.CreateCommand())
				{
					BuildCommand(procedureName, parameters, cmd);
					var reader = ExecuteCommand(cmd, command => command.ExecuteReader());
					return new SafeDataReader(reader);
				}
			}, procedureName);
		}

		/// <summary>
		/// Calls a stored procedure and returns the result as a <see cref="ISafeDataReader" />
		/// </summary>
		/// <param name="procedureName">The name of the stored procedure to call</param>
		/// <param name="parameters">The <see cref="SqlParameter" />s to use in the call</param>
		/// <returns>
		/// Returns a <see cref="ISafeDataReader" /> with the resulting rows
		/// </returns>
		/// <exception cref="System.ArgumentNullException">procedureName</exception>
		public async Task<ISafeDataReaderAsync> GetDataReaderFromSprocAsync(string procedureName, SqlParameter[] parameters)
		{
			if (String.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

			return await ExecuteSqlWithinTryCatchAsync(async () =>
			{
				var connection = await GetConnectionAsync();
				using (var cmd = connection.CreateCommand())
				{
					BuildCommand(procedureName, parameters, cmd);
					var reader = await ExecuteCommandAsync(cmd, command => command.ExecuteReaderAsync());
					return new SafeDataReader(reader);
				}
			}, procedureName);
		}

		/// <summary>
		/// Calls a stored procedure and returns the result as a DataSet.
		/// </summary>
		/// <param name="procedureName">The name of the stored procedure to call</param>
		/// <param name="parameters">The <see cref="SqlParameter" />s to use in the call</param>
		/// <returns>
		/// Returns a DataSet with the resulting tables
		/// </returns>
		/// <exception cref="System.ArgumentNullException">procedureName</exception>
		[Obsolete("Use the GetDataReaderFromSproc methods instead.")]
		public DataSet GetDataSetFromSproc(string procedureName, SqlParameter[] parameters)
		{
			if (String.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

			var dataSet = new DataSet { Locale = CultureInfo.InvariantCulture };

			return ExecuteSqlWithinTryCatch(() =>
			{
				var connection = GetConnection();
				using (var cmd = connection.CreateCommand())
				{
					BuildCommand(procedureName, parameters, cmd);
					using (var adapter = new SqlDataAdapter(cmd))
					{
						adapter.Fill(dataSet);
					}
					return dataSet;
				}
			}, procedureName);
		}

		/// <summary>
		/// Takes name and parameters for a stored procedure, runs it and converts the result into a list of objects.
		/// </summary>
		/// <typeparam name="T">The type of the objects in the list.</typeparam>
		/// <param name="procedureName">The name of the stored procedure to call.</param>
		/// <param name="parameters">The parameters to pass to the stored procedure.</param>
		/// <param name="converter">The converter function to use to convert each row to an object in the list.</param>
		/// <returns>
		/// A list of objects each representing a row.
		/// </returns>
		public IList<T> StoredProcedureToList<T>(string procedureName, SqlParameter[] parameters, Func<IDataItem, T> converter)
		{
			var result = new List<T>();
			using (var reader = new DataEnumerator(GetDataReaderFromSproc(procedureName, parameters)))
			{
				result.AddRange(reader.Select(converter));
			}
			return result;
		}

		/// <summary>
		/// Executes a stored procedure without returning a result.
		/// </summary>
		///<param name="procedureName">The name of the stored procedure to call.</param>
		///<param name="parameters">The parameters to pass to the stored procedure.</param>
		public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
		{
			if (String.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");
			ExecuteSqlWithinTryCatch(() =>
			{
				var command = GetCommand(procedureName, parameters);
				return ExecuteCommand(command, cmd => cmd.ExecuteNonQuery());
			}, procedureName);
		}

		/// <summary>
		/// Executes a stored procedure and returns the scalar result.
		/// </summary>
		///<param name="procedureName">The name of the stored procedure to call.</param>
		///<param name="parameters">The parameters to pass to the stored procedure.</param>
		/// <typeparam name="T">The type returned from the stored procedure.</typeparam>
		/// <returns>The result returned by the stored procedure.</returns>
		public T ExecuteStoredProcedure<T>(string procedureName, SqlParameter[] parameters)
		{
			if (String.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");
			return ExecuteSqlWithinTryCatch(() =>
			{
				var command = GetCommand(procedureName, parameters);
				var result = ExecuteCommand(command, cmd => cmd.ExecuteScalar());
				return (T)Convert.ChangeType(result, typeof(T), CultureInfo.InvariantCulture);
			}, procedureName);
		}

		/// <summary>
		/// Executes the supplied <code>Func{T}</code> within a try/catch block and returns the result.
		/// The try/catch block is specialized for handling exceptions that can occur when accessing the db.
		/// </summary>
		/// <typeparam name="TReturn">The return type of the execute function.</typeparam>
		/// <param name="execute">The function to execute. It is expected that tyhe function accesses the db.</param>
		/// <param name="commandText">The text of the command to execute.</param>
		/// <returns>The result of executing the function.</returns>
		internal static TReturn ExecuteSqlWithinTryCatch<TReturn>(Func<TReturn> execute, string commandText)
		{
			try
			{
				return execute();
			}
			catch (Exception ex)
			{
				if (IsCriticalException(ex))
					throw;

				if (IsUnavailableException(ex))
				{
					var dueEx = new DatabaseUnavailableException("Database is currently unavailable!", ex);

					//Logger.WarnFormat(DatabaseLogEventId.DatabaseUnavailable, "Database unavailable: {0}", commandText, dueEx);

					throw dueEx;
				}

				throw new DataAccessException(string.Format(CultureInfo.InvariantCulture, "Failed to get data reader from SQL command '{0}'", commandText), ex);
			}
		}

		/// <summary>
		/// Executes the supplied <code>Func{T}</code> within a try/catch block and returns the result.
		/// The try/catch block is specialized for handling exceptions that can occur when accessing the db.
		/// </summary>
		/// <typeparam name="TReturn">The return type of the execute function.</typeparam>
		/// <param name="execute">The function to execute. It is expected that tyhe function accesses the db.</param>
		/// <param name="commandText">The text of the command to execute.</param>
		/// <returns>The result of executing the function.</returns>
		private static async Task<TReturn> ExecuteSqlWithinTryCatchAsync<TReturn>(Func<Task<TReturn>> execute, string commandText)
		{
			try
			{
				return await execute();
			}
			catch (Exception ex)
			{
				if (IsCriticalException(ex))
					throw;

				if (IsUnavailableException(ex))
				{
					var dueEx = new DatabaseUnavailableException("Database is currently unavailable!", ex);

					//Logger.WarnFormat(DatabaseLogEventId.DatabaseUnavailable, "Database unavailable: {0}", commandText, dueEx);

					throw dueEx;
				}

				throw new DataAccessException(string.Format(CultureInfo.InvariantCulture, "Failed to get data reader from SQL command '{0}'", commandText), ex);
			}
		}

		/// <summary>
		/// Determines whether [the specified ex] is critical.
		/// </summary>
		/// <param name="ex">The ex.</param>
		/// <returns>
		/// 	<c>true</c> if [the specified ex] is critical; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsCriticalException(Exception ex)
		{
			return (ex is NullReferenceException) || (ex is StackOverflowException) ||
				(ex is OutOfMemoryException) || (ex is ThreadAbortException) ||
				(ex is AccessViolationException);
		}

		/// <summary>
		/// Determines whether an exception is an indication of the database server being unavailable
		/// (timeout, shutting down etc.).
		/// Unavailability is considered a temporary problem.
		/// </summary>
		/// <param name="exception"></param>
		/// <returns></returns>
		private static bool IsUnavailableException(Exception exception)
		{
			var ex = exception as SqlException;
			if (ex != null)
			{
				//Timeout or Shutdown in progress
				if (ex.Number == -2 || ex.Number == 6005)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the current connection object.
		/// </summary>
		/// <returns>Current connection object.</returns>
		private SqlConnection GetConnection()
		{
			return _currentConnection ?? (_currentConnection = CreateConnection());
		}

		/// <summary>
		/// Gets the current connection object.
		/// </summary>
		/// <returns>Current connection object.</returns>
		private async Task<SqlConnection> GetConnectionAsync()
		{
			return _currentConnection ?? (_currentConnection = await CreateConnectionAsync());
		}

		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns></returns>
		public SqlConnection CreateConnection()
		{
			SqlConnection connection;
			SqlConnection tempConnection = null;
			try
			{
				tempConnection = new SqlConnection(_connectionStringFactory());
				tempConnection.Open();
				connection = tempConnection;
				tempConnection = null;
			}
			finally
			{
				if (tempConnection != null)
				{
					tempConnection.Dispose();
				}
			}

			return connection;
		}

		/// <summary>
		/// Creates the connection.
		/// </summary>
		/// <returns></returns>
		public async Task<SqlConnection> CreateConnectionAsync()
		{
			SqlConnection connection;
			SqlConnection tempConnection = null;
			try
			{
				tempConnection = new SqlConnection(_connectionStringFactory());
				await tempConnection.OpenAsync();
				connection = tempConnection;
				tempConnection = null;
			}
			finally
			{
				if (tempConnection != null)
				{
					tempConnection.Dispose();
				}
			}

			return connection;
		}

		/// <summary>
		/// Executes a <see cref="SqlCommand"/> and returns the result. Logs the time elapsed for executing the command.
		/// </summary>
		/// <typeparam name="TReturnType">The return type of the command.</typeparam>
		/// <param name="command">The command to execute.</param>
		/// <param name="execute">The function for executing the command. Use this to specify whether to execute by <code>ExecuteReader</code> or some other method.</param>
		/// <returns>The result of executing the command.</returns>
		private static TReturnType ExecuteCommand<TReturnType>(SqlCommand command, Func<SqlCommand, TReturnType> execute)
		{
			//Stopwatch time = null;
			//if (Logger.IsTraceEnabled) { time = Stopwatch.StartNew(); }

			var result = execute(command);

			//if (Logger.IsTraceEnabled && time != null)
			//{
			//	time.Stop();

			//	Logger.Trace(DatabaseLogEventId.DatabaseAccessTimeElapsedMeasured, fmh => fmh.Invoke("Successfully executed database command '{0}'. Parameters: {1}. Time elapsed: {2}.",
			//		command.CommandText,
			//		string.Join(",", MakeSqlParameterCollectionEnumerable(command.Parameters).Select(p => p.ParameterName + ": " + p.Value)),
			//		time.Elapsed));
			//}

			return result;
		}

		/// <summary>
		/// Asynchronously executes a <see cref="SqlCommand"/> and returns the result. Logs the time elapsed for executing the command.
		/// </summary>
		/// <typeparam name="TReturnType">The return type of the command.</typeparam>
		/// <param name="command">The command to execute.</param>
		/// <param name="execute">The function for executing the command. Use this to specify whether to execute by <code>ExecuteReader</code> or some other method.</param>
		/// <returns>The result of executing the command.</returns>
		private static async Task<TReturnType> ExecuteCommandAsync<TReturnType>(SqlCommand command, Func<SqlCommand, Task<TReturnType>> execute)
		{
			//Stopwatch time = null;
			//if (Logger.IsTraceEnabled) { time = Stopwatch.StartNew(); }

			var result = await execute(command);

			//if (Logger.IsTraceEnabled && time != null)
			//{
			//	time.Stop();

			//	//Logger.Trace(DatabaseLogEventId.DatabaseAccessTimeElapsedMeasured, fmh => fmh.Invoke("Successfully executed database command '{0}'. Parameters: {1}. Time elapsed: {2}.",
			//	//	command.CommandText,
			//	//	string.Join(",", MakeSqlParameterCollectionEnumerable(command.Parameters).Select(p => p.ParameterName + ": " + p.Value)),
			//	//	time.Elapsed));
			//}

			return result;
		}


		/// <summary>
		/// Convenience method for making <see cref="SqlParameterCollection"/> LINQable.
		/// </summary>
		/// <param name="coll">The SqlParameterCollection to make LINQable.</param>
		/// <returns>The contents of the SqlParameteterCollection in an <see cref="IEnumerable{SqlParameter}"/></returns>
		private static IEnumerable<SqlParameter> MakeSqlParameterCollectionEnumerable(SqlParameterCollection coll)
		{
			var parameters = new SqlParameter[coll.Count];
			coll.CopyTo(parameters, 0);
			return parameters;
		}


		private static void BuildCommand(string procedureName, SqlParameter[] parameters, SqlCommand cmd)
		{
			cmd.CommandText = procedureName;
			cmd.CommandType = CommandType.StoredProcedure;
			if (parameters != null)
				cmd.Parameters.AddRange(parameters);
		}

		private SqlCommand GetCommand(string procedureName, SqlParameter[] parameters)
		{
			var result = GetConnection().CreateCommand();
			result.CommandText = procedureName;
			result.CommandType = CommandType.StoredProcedure;
			if (parameters != null)
				result.Parameters.AddRange(parameters);
			return result;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">Indicates whether managed resources should be disposed.</param>
		public void Dispose(bool disposing)
		{
			if (!disposing || _currentConnection == null) return;

			_currentConnection.Dispose();
			_currentConnection = null;
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~DatabaseClient()
		{
			// YES, We need to have a destructor in this class (unmanaged resources)

			Dispose(false);
		}
	}
}
