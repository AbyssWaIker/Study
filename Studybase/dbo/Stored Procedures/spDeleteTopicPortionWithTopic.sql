CREATE PROCEDURE [dbo].[spDeleteTopicPortionWithTopic]
	@Topicid int
AS

begin
set nocount on  
	delete from dbo.TopicPortions where Topicid = @Topicid
end