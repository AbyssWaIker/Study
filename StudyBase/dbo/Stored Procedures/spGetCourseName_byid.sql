CREATE PROCEDURE [dbo].[spGetCourseName_byid]
	@id int
AS

begin
set nocount on  
	SELECT TOP 1 Name from dbo.Courses
	where Id = @id
end