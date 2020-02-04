CREATE PROCEDURE [dbo].[spDeleteGroupToCourseRelationWithCourseID]
	@Courseid int
AS

begin
set nocount on  
	delete from dbo.StudentToCourse where Courseid = @Courseid
end