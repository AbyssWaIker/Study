CREATE PROCEDURE [dbo].[spUpdateTopicName]
	@topicName NVARCHAR(50),
	@id int 
AS

begin
	set nocount on;
	update dbo.Topics 
	set topicName = @topicName
	where id = @id;
end