CREATE PROCEDURE [dbo].[SpUpdateTopicOrderNumber]
	
	@TopicOrderNumber int,
	@id int 
AS

begin
	set nocount on;
	update dbo.Topics 
	set TopicOrderNumber = @TopicOrderNumber 
	where id = @id;
end