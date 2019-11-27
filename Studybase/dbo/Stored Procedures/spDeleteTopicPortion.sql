CREATE PROCEDURE [dbo].[spDeleteTopicPortion]
	@id int
AS

begin
set nocount on  
	delete from dbo.TopicPortions where id = @id
end