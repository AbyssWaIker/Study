

CREATE PROCEDURE [dbo].[spInsertStudent]

	@name nvarchar(50),
	@group nvarchar(50),
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Student(StudentName,StudentGroup)
	values(@name, @group);
	select @id = SCOPE_IDENTITY();

end