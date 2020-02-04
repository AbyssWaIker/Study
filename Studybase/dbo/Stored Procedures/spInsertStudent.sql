

CREATE PROCEDURE [dbo].[spInsertStudent]

	@name nvarchar(50),
	@groupid nvarchar(50),
	@username nvarchar(50),
	@StudentPassword nvarchar(50),
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Student(StudentName,StudentGroupid,username,StudentPassword)
	values(@name, @groupid, @username, @StudentPassword);
	select @id = SCOPE_IDENTITY();

end