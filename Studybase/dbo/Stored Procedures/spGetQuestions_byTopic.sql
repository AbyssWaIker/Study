CREATE PROCEDURE [dbo].[spGetQuestions_byTopic]
	@Topicid int
AS

begin
set nocount on  
	SELECT  * from dbo.Questions where Topicid = @Topicid
end