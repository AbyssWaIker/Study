CREATE PROCEDURE [dbo].[SpDeleteGradeWithTopic]
	@Topicid int
AS

begin
set nocount on  
	delete from dbo.Grades where Topicid = @Topicid
end