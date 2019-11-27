CREATE PROCEDURE [dbo].[spDeleteQuestion]
	@id int
AS

begin
set nocount on  
	delete from dbo.Questions where id=@id
end