CREATE PROCEDURE [dbo].[spInsertWrongAnswerToQuestion]
	@QuestionId int,
	@WrongAnswerText nvarchar(50),
	@id int = 0 out
AS

begin
	set nocount on;
	insert into dbo.WrongAnswers(QuestionId,WrongAnswerText)
	values(@QuestionId,@WrongAnswerText);
	select @id = SCOPE_IDENTITY();
end