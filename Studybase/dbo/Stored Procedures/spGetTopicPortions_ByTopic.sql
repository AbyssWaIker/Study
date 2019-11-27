CREATE PROCEDURE [dbo].[spGetTopicPortions_ByTopic]
	@Topicid int
AS

begin
set nocount on  
	SELECT  * from dbo.TopicPortions where Topicid = @Topicid
end