CREATE PROCEDURE [dbo].[spUpdateTopicPortion]
	@id int,
	@TopicPortionName nvarchar(50),
	@TopicPortionText nvarchar(max)
AS

begin
	set nocount on;
	update dbo.TopicPortions
	set TopicPortionName = @TopicPortionName, TopicPortionText=@TopicPortionText
	where id = @id;
end