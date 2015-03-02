using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subtext.Scripting;
using Assert = NUnit.Framework.Assert;

namespace Database.Test
{
	[TestClass]
	public class DatabaseClientTest
	{
		private readonly Func<string> _connectionFunc;
		public DatabaseClientTest()
		{
			_connectionFunc = () => ConfigurationManager.AppSettings["DBConnection"];
		}

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void SetUpDatabase(TestContext testContext)
		{
			var connectionString = GetConnectionString();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var createDbStmt = GetTextFromEmbededFile("Database.Test.Scripts.CreateDB.sql");
				using (var cmd = new SqlCommand(createDbStmt, connection))
				{
					cmd.ExecuteNonQuery();
				}

				var queryString = GetTextFromEmbededFile("Database.Test.Scripts.SetupDB.sql");
				var runner = new SqlScriptRunner(queryString);

				using (var transaction = connection.BeginTransaction())
				{
					runner.Execute(transaction);
					transaction.Commit();
				}
			}
		}

		[ClassCleanup]
		public static void ResetDatabase()
		{
			var connectionString = GetConnectionString();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var queryString = GetTextFromEmbededFile("Database.Test.Scripts.DropDatabase.sql");
				using (var cmd = new SqlCommand(queryString, connection))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void GetDataReader_ValidProcedureNameAndParameter_ShouldReturnISafeDataReader()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				var reader = dbClient.GetDataReaderFromSproc("dbo.GetAllRecords", parmeters);
				Assert.IsNotNull(reader);
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void GetDataReader_InValidProcedureName_ShouldThrowException()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				Assert.Throws<DataAccessException>(
					() => dbClient.GetDataReaderFromSproc("InvalidProcedureName", parmeters));
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void ExecuteStoredProcedureGeneric_ValidProcedureName_ShouldReturnExpectedResult()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				var result = dbClient.ExecuteStoredProcedure<int>("TestProcedure", parmeters);
				Assert.AreEqual(result, 4);
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void ExecuteStoredProcedureGeneric_InValidProcedureName_ShouldThrowException()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				Assert.Throws<DataAccessException>(
					() => dbClient.ExecuteStoredProcedure<int>("InvalidProcedureName", parmeters));
			}

		}

		[TestMethod, TestCategory("Integration")]
		public void ExecuteStoredProcedure_ValidProcedureName_ShouldReturnExpectedResult()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var age = new Random().Next(1, 60);
				var parmeters = new SqlParameter[2];
				parmeters[0] = new SqlParameter("age", age);
				parmeters[1] = new SqlParameter("id", 2);
				dbClient.ExecuteStoredProcedure("UpdateAge", parmeters);

				parmeters = new SqlParameter[1];
				parmeters[0] = new SqlParameter("id", 2);
				var updatedAge = dbClient.ExecuteStoredProcedure<int>("GetAge", parmeters);
				Assert.AreEqual(age, updatedAge);
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void ExecuteStoredProcedure_InValidProcedureName_ShouldThrowException()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				Assert.Throws<DataAccessException>(
					() => dbClient.ExecuteStoredProcedure("InvalidProcedureName", parmeters));
			}
		}

		[TestMethod, TestCategory("Integration"), TestCategory("SlowAsADog")]
		public void GetDataSet_ValidProcedureName_ShouldReturnExpectedValue()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				var ds = dbClient.GetDataSetFromSproc("GetAllRecords", parmeters);
				Assert.AreEqual(ds.Tables.Count, 1);
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void GetDataSet_InValidProcedureName_ShouldThrowException()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				Assert.Throws<DataAccessException>(() => dbClient.GetDataSetFromSproc("InvalidProcedureName", parmeters));
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void StoredProcedureToList_ValidParameters_ShouldReturnExpectedResult()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				var items = dbClient.StoredProcedureToList("GetAllRecords", parmeters,
					arg => new TestData(arg.GetField<int>("Age"), arg.GetField<int>("Id")));
				var rowsCount = dbClient.ExecuteStoredProcedure<int>("GetCount", parmeters);

				Assert.AreEqual(items.Count, rowsCount);
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void StoredProcedureToList_InValidProcedureName_ShouldThrowException()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				Assert.Throws<DataAccessException>(() => dbClient.StoredProcedureToList("InvalidProcedureName", parmeters,
					arg => 4));
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void CreateConnectionAsync_WithValidConnection_CreatesConnection()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				Assert.DoesNotThrow(async () => await dbClient.CreateConnectionAsync());
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void CreateConnectionAsync_WithInvalidConnection_ThrowsSqlException()
		{
			using (var dbClient = new DatabaseClient(() => @"Data Source=.\SQLEXPRESSDOESNOTEXIST;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;"))
			{
				Assert.Throws<SqlException>(async () => await dbClient.CreateConnectionAsync());
			}
		}

		[TestMethod, TestCategory("Integration")]
		public async Task GetDataReaderFromSprocAsync_ValidProcedureNameAndParameter_ShouldReturnISafeDataReader()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				var reader = await dbClient.GetDataReaderFromSprocAsync("dbo.GetAllRecords", parmeters);
				Assert.IsInstanceOf<ISafeDataReader>(reader, "The object returned by is not an ISafeDataReader");
			}
		}

		[TestMethod, TestCategory("Integration")]
		public void GetDataReaderFromSprocAsync_InvalidProcedureNameAndParameter_ShouldThrowDataAccessException()
		{
			using (var dbClient = new DatabaseClient(_connectionFunc))
			{
				var parmeters = new SqlParameter[0];
				Assert.Throws<DataAccessException>(async () => await dbClient.GetDataReaderFromSprocAsync("dbo.DoesNotExist", parmeters));
			}
		}

		private static string GetTextFromEmbededFile(string fileName)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = fileName;
			var result = string.Empty;

			using (var stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null) return result;
				var reader = new StreamReader(stream);
				result = reader.ReadToEnd();

			}

			return result;
		}

		private static string GetConnectionString()
		{
			return ConfigurationManager.AppSettings["ServerConnection"];
		}

		internal class TestData
		{
			private readonly int _age;
			private readonly int _id;

			public TestData(int age, int id)
			{
				_age = age;
				_id = id;
			}

			public int GetAge()
			{
				return _age;
			}

			public int GetId()
			{
				return _id;
			}
		}
	}
}
