CREATE PROCEDURE [dbo].[spDeleteWrongQuestionAnswerWithQuestionID]
	@QuestionId int
AS

begin
set nocount on  
	delete from dbo.WrongAnswers where QuestionId = @QuestionId
end