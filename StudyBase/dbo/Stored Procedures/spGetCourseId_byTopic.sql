CREATE PROCEDURE [dbo].[spGetCourseId_byTopic]
	@topicid int
AS

begin
set nocount on  
	SELECT Top 1 Courses.Id from dbo.Courses  
	inner join dbo.Topics on Topics.Courseid = Courses.id 
	where Topics.Id = @topicid
end