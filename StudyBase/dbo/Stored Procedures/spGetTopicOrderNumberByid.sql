CREATE PROCEDURE dbo.[spGetTopicOrderNumberByid]
	@id int
	
AS
Begin

set nocount on

select TopicOrderNumber from dbo.Topic where id=@id

end