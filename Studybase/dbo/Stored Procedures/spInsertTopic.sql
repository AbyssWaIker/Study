CREATE PROCEDURE dbo.spInsertTopic
	@TopicName nvarchar(50),
	@TopicOrderNumber int,
	@Courseid int,
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Topics(topicName, TopicOrderNumber, Courseid)
	values(@TopicName, @TopicOrderNumber, @Courseid);
	select @id = SCOPE_IDENTITY();

end