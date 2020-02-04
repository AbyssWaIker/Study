CREATE PROCEDURE [dbo].[spGetWrongAnswers_byQuestionID]
	@QuestionId int
AS

begin
set nocount on  
	SELECT  * from dbo.WrongAnswers where QuestionId = @QuestionId
end