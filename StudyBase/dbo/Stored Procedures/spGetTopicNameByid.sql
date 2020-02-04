CREATE PROCEDURE dbo.spGetTopicNameByid
	@id int
	
AS
Begin

set nocount on

select TOP 1 topicName from dbo.Topics where id=@id

end