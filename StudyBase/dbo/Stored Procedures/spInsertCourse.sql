CREATE PROCEDURE dbo.spInsertCourse
	@TeacherId nvarchar(50),
	@Name nvarchar(50),
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Courses(TeacherId, Name)
	values(@TeacherId, @Name);
	select @id = SCOPE_IDENTITY();

end