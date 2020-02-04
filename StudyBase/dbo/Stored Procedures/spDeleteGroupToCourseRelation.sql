CREATE PROCEDURE [dbo].[spDeleteGroupToCourseRelation]
	@id int
AS

begin
set nocount on  
	delete from dbo.StudentToCourse where id = @id
end