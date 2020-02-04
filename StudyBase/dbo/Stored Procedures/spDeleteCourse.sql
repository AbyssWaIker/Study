CREATE PROCEDURE [dbo].[spDeleteCourse]
	@id int
AS

begin
set nocount on  
	delete from dbo.Courses where id = @id
end