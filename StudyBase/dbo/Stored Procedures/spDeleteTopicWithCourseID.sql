CREATE PROCEDURE [dbo].[spDeleteTopicWithCourseID]
	@Courseid int
AS

begin
set nocount on  
	delete from dbo.Topics where Courseid = @Courseid
end