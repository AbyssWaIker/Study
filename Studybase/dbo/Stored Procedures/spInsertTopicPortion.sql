



 CREATE PROCEDURE dbo.spInsertTopicPortion
	@TopicId int,
	@TopicPortionName nvarchar(50),
	@TopicPortionText nvarchar(max),
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.TopicPortions(Topicid, TopicPortionName, TopicPortionText)
	values(@TopicId, @TopicPortionName, @TopicPortionText);
	select @id = SCOPE_IDENTITY();

end