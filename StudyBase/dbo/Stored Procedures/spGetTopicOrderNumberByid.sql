CREATE PROCEDURE dbo.[spGetTopicOrderNumberByid]
	@id int
	
AS
Begin

set nocount on

select TOP 1 TopicOrderNumber from dbo.Topics where id=@id

end