CREATE PROCEDURE dbo.spGetTopicNameByid
	@id int
	
AS
Begin

set nocount on

select topicName from dbo.Topic where id=@id

end