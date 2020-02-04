CREATE PROCEDURE [dbo].[spDeleteWrongQuestionAnswer]
	@id int
AS

begin
set nocount on  
	delete from dbo.WrongAnswers where id = @id
end