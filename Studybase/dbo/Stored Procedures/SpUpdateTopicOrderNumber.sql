CREATE PROCEDURE [dbo].[SpUpdateTopicOrderNumber]
	
	@TopicOrderNumber int,
	@id int 
AS

begin
	set nocount on;
	update dbo.Topic 
	set TopicOrderNumber = @TopicOrderNumber 
	where id = @id;
end