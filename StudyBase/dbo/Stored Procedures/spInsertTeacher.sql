CREATE PROCEDURE [dbo].[spInsertTeacher]

	@name nvarchar(50),
	@UserName nvarchar(50),
	@Password nvarchar(50),
	@Position nvarchar(50),
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Teachers(Name,UserName,Password,Position)
	values(@name, @UserName, @Password, @Position);
	select @id = SCOPE_IDENTITY();

end