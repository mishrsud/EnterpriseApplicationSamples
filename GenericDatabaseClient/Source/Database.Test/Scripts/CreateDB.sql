DECLARE @dbname nvarchar(128)
SET @dbname = N'DataBaseClientTest'

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
    Alter database [DataBaseClientTest] set single_user with rollback immediate
    DROP DATABASE [DataBaseClientTest]
	CREATE DATABASE DataBaseClientTest
END
ELSE
BEGIN
    CREATE DATABASE DataBaseClientTest
END