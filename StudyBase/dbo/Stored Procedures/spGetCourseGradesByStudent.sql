CREATE PROCEDURE [dbo].[spGetCourseGradesByStudent]
	@studentid int,
	@courseid int
AS

begin
set nocount on  
	SELECT  * from dbo.Grades 
	where (studentid = @studentid and topicid = ANY(select Id from dbo.Topics where Courseid=@courseid))
end