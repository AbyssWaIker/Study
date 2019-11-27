CREATE PROCEDURE [dbo].[spDeleteTopic]
	@id int
AS

begin
set nocount on  
	delete from dbo.Topic where id = @id
end