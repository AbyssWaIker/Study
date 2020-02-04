CREATE PROCEDURE [dbo].[spGetGroupToCourseRelationWithCourseID]
	@Courseid int
AS

begin
set nocount on  
	select * from dbo.StudentToCourse where Courseid = @Courseid
end