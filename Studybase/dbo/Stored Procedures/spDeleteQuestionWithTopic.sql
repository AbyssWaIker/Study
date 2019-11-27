CREATE PROCEDURE [dbo].[spDeleteQuestionWithTopic]
	@Topicid int
AS

begin
set nocount on  
	delete from dbo.Questions where Topicid = @Topicid
end