# GenericDatabaseClient
## Synopsis

A Generic database client that provides simplified access to SQL Server as a data source. Built with .NET 4.5.1 and with async APIs.

## Code Example
```csharp
// Create a connection string, e.g. reading the connection string from the app.config
Func<string> _connectionFunc = () => ConfigurationManager.AppSettings["DBConnection"];

// Instantiate DatabaseClient and call a stored proc that returns a scalar
using (var dbClient = new DatabaseClient(_connectionFunc))
{
	var parmeters = new SqlParameter[1];
	parmeters[0] = new SqlParameter("id", 2);
	var age = dbClient.ExecuteStoredProcedure<int>("GetAge", parmeters);
}

// Use DatabaseClient to call stored proc and obtain an IDataReader that can enumerate data asynchronously
using (var dbClient = new DatabaseClient(_connectionFunc))
{
	var parmeters = new SqlParameter[0];
	var reader = await dbClient.GetDataReaderFromSprocAsync("dbo.GetAllRecords", parmeters);
    // Use the reader to read records
}

// Map data returned from a stored proc to fill an object
string connectionsString = @"Data Source=.\SQLEXPRESS12;Initial Catalog=Local;Integrated Security=SSPI;";
using (var dbClient = new DatabaseClient(() => connectionsString))
{
	var parmeters = new SqlParameter[1];		
	parmeters[0] = new SqlParameter("@pk", 3);
	var testlist = dbClient
				.StoredProcedureToList(
					"dbo.testproc", 
					parmeters, 
					MapData());
	// Get the first item in the list
	var test = testlist.First();
}

// A Func delegate that maps data
private static Func<IDataItem, Test> MapData()
{
   return item => new Test() 
   					{
   						Id = item.GetField<int>("Id"), 
						FirstName = item.GetField<string>("FirstName")
					};
}

// The test object

public class Test
{
	public int Id {get;set;}
	public string FirstName {get;set;}
}

```
## API Reference

Coming Soon!

## Tests

**NOTE** MS SQL Express 2014 is required to run tests. Get your copy from here: http://www.microsoft.com/en-in/download/details.aspx?id=42299
IF YOU NAME THE INSTANCE ANYTHING OTHER THAN SQLEXPRESS, REMEMBER TO UPDATE THE app.config IN TEST

To run test, Rebuild the solution first to restore NuGet packages. The following NuGet packages are required:


|	Package Id						| Version			|
| --------------------------------|-----------------|
| Microsoft.ApplicationBlocks.Data 	| Version 2.0.0		|
| NUnit								| Version 2.6.4		|
| RhinoMocks						| Version 3.6.1		|

## Contributors

@sudhanshutheone.

## License

Open Source!
