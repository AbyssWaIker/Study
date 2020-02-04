CREATE PROCEDURE [dbo].[spGetCourseName_byTopic]
	@topicid int
AS

begin
set nocount on  
	SELECT TOP 1 Name from dbo.Courses  
	inner join dbo.Topics on Topics.Courseid = Courses.id 
	where Topics.Id = @topicid
end