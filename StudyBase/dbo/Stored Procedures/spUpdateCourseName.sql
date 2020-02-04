CREATE PROCEDURE [dbo].[spUpdateCourseName]
	@Name NVARCHAR(50),
	@id int 
AS

begin
	set nocount on;
	update dbo.Courses 
	set Name = @Name
	where id = @id;
end