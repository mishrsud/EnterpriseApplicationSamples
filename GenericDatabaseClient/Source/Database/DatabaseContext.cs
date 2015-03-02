using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace Database
{
	///<summary>
	/// Responsible for holding information about the database we are using.
	///</summary>
	[Serializable]
	public class DatabaseContext : IEquatable<DatabaseContext>
	{
		private const string DefaultAppName = "DatabaseClient";

		private readonly string _database;
		private readonly string _serverName;
		private readonly string _failoverServer;
		private readonly string _appName;

		///<summary>
		/// Constructor.
		///</summary>
		///<param name="serverName">The name of the database server to connect to.</param>
		///<param name="database">The name of the database.</param>
		///<exception cref="ArgumentNullException">Raised if any of the arguments are null.</exception>
		public DatabaseContext(string serverName, string database)
			: this(serverName, database, DefaultAppName)
		{
		}

		///<summary>
		/// Constructor.
		///</summary>
		///<param name="serverName">The name of the database server to connect to.</param>
		///<param name="database">The name of the database.</param>
		///<param name="appName">The name of the application connecting to the database. Used for logging etc.</param>
		///<exception cref="ArgumentNullException">Raised if any of the arguments are null.</exception>
		public DatabaseContext(string serverName, string database, string appName)
			: this(serverName, null, database, appName)
		{
		}

		///<summary>
		/// Constructor.
		///</summary>
		///<param name="serverName">The name of the database server to connect to.</param>
		///<param name="failoverServer">The failover server to use. Set to null or empty string if no failover server should be used.</param>
		///<param name="database">The name of the database.</param>
		///<param name="appName">The name of the application connecting to the database. Used for logging etc.</param>
		///<exception cref="ArgumentNullException">Raised if any of the arguments are null.</exception>
		public DatabaseContext(string serverName, string failoverServer, string database, string appName)
		{
			if (serverName == null) throw new ArgumentNullException("serverName");
			if (database == null) throw new ArgumentNullException("database");
			if (appName == null) throw new ArgumentNullException("appName");
			_serverName = serverName;
			_failoverServer = failoverServer;
			_database = database;
			_appName = appName;
		}

		/// <summary>
		/// Creates a new <see cref="DatabaseContext"/> instance.
		/// </summary>
		/// <param name="connectionStringBuilder">The connection string builder containing the database information for this instance.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="connectionStringBuilder"/> is null.</exception>
		/// <exception cref="ArgumentException">Thrown if <see cref="SqlConnectionStringBuilder.DataSource"/> or <see cref="SqlConnectionStringBuilder.InitialCatalog"/> of <paramref name="connectionStringBuilder"/> is null or whitespace.</exception>
		public DatabaseContext(SqlConnectionStringBuilder connectionStringBuilder)
		{
			if (connectionStringBuilder == null) throw new ArgumentNullException("connectionStringBuilder");
			if (string.IsNullOrWhiteSpace(connectionStringBuilder.DataSource)) throw new ArgumentException("DataSource cannot be null or whitespace", "connectionStringBuilder");
			if (string.IsNullOrWhiteSpace(connectionStringBuilder.InitialCatalog)) throw new ArgumentException("InitialCatalog cannot be null or whitespace", "connectionStringBuilder");
			_serverName = connectionStringBuilder.DataSource;
			_database = connectionStringBuilder.InitialCatalog;
			_failoverServer = string.IsNullOrWhiteSpace(connectionStringBuilder.FailoverPartner)
								? null
								: connectionStringBuilder.FailoverPartner;
			_appName = string.IsNullOrWhiteSpace(connectionStringBuilder.ApplicationName)
						? DefaultAppName
						: connectionStringBuilder.ApplicationName;
		}

		///<summary>
		/// The server name.
		///</summary>
		public string ServerName
		{
			get { return _serverName; }
		}

		///<summary>
		/// The failover server to use.
		///</summary>
		public string FailoverServer
		{
			get { return _failoverServer; }
		}

		///<summary>
		/// The database name.
		///</summary>
		public string Database
		{
			get { return _database; }
		}

		/// <summary>
		/// The application name for the connection.
		/// </summary>
		public string AppName
		{
			get { return _appName; }
		}

		///<summary>
		/// Creates a connection string based on the values in this <see cref="DatabaseContext"/> instance.
		///</summary>
		///<returns>A connection string.</returns>
		public string GetConnectionString()
		{
			var builder = new StringBuilder("Trusted_Connection=Yes;persist security info=False;");
			builder.AppendFormat("App={0};", _appName);
			builder.AppendFormat("Initial Catalog={0};", _database);
			builder.AppendFormat("Data Source={0};", _serverName);
			if (!string.IsNullOrWhiteSpace(_failoverServer))
				builder.AppendFormat("Failover Partner={0};", _failoverServer);
			return builder.ToString();
		}

		///<summary>
		/// Makes a description with settings in the current instance of <see cref="DatabaseContext"/>.
		///</summary>
		///<returns>A string with the description.</returns>
		public string Describe()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("ServerName={0};", ServerName);
			builder.AppendFormat("FailoverServer={0};", FailoverServer);
			builder.AppendFormat("Database={0};", Database);
			builder.AppendFormat("AppName={0};", AppName);
			return builder.ToString();
		}

		#region IEquatable<DatabaseInfo> Members

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(DatabaseContext other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._database, _database) &&
				Equals(other._serverName, _serverName) &&
				Equals(other._failoverServer, _failoverServer) &&
				Equals(other._appName, _appName);
		}

		#endregion

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(DatabaseContext)) return false;
			return Equals((DatabaseContext)obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			unchecked
			{
				int result = (_database != null ? _database.GetHashCode() : 0);
				result = (result * 397) ^ (_serverName != null ? _serverName.GetHashCode() : 0);
				result = (result * 397) ^ (_failoverServer != null ? _failoverServer.GetHashCode() : 0);
				result = (result * 397) ^ (_appName != null ? _appName.GetHashCode() : 0);
				return result;
			}
		}

		/// <summary>
		/// Equals operator override.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(DatabaseContext left, DatabaseContext right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Not equals operator override.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(DatabaseContext left, DatabaseContext right)
		{
			return !Equals(left, right);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ServerName: {0}, FailoverServer: {1}, Database: {2}, AppName: {3}", ServerName, FailoverServer, Database, AppName);
		}
	}
}
