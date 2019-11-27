CREATE PROCEDURE dbo.spInsertTopic
	@TopicName nvarchar(50),
	@TopicOrderNumber int,
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Topic(topicName, TopicOrderNumber)
	values(@TopicName, @TopicOrderNumber);
	select @id = SCOPE_IDENTITY();

end