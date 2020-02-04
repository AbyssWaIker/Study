CREATE PROCEDURE [dbo].[spDeleteTopic]
	@id int
AS

begin
set nocount on  
	delete from dbo.Topics where id = @id
end