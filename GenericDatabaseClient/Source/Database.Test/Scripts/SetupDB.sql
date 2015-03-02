use DataBaseClientTest;
CREATE TABLE Persons(Id int,Age int);
GO
Insert into Persons values(1,1),(2,2),(3,3)
GO
CREATE PROCEDURE [GetAllRecords] 
AS SELECT * from Persons
GO
CREATE PROCEDURE GetCount 
AS SELECT count(*) from Persons
Go
CREATE PROCEDURE [UpdateAge]
	@age int ,
	@id int
AS
Update Persons Set Age= @age where Id= @id
GO
Create Procedure TestProcedure
As
select 4
Go
Create Procedure GetAge
@id int
As
select Age from Persons where Id=@id 
Go

