CREATE PROCEDURE [dbo].[spInsertQuestion]
	@TopicId int,
	@QuestionText nvarchar(200),
	@CorrectAnswer nvarchar(50),
	@WrongAnswer1 nvarchar(50),
	@WrongAnswer2 nvarchar(50),
	@timetoanswer int,
	@id int = 0 out
AS
begin
	set nocount on;
	insert into dbo.Questions(TopicId,QuestionText,CorrectAnswer,WrongAnswer1,WrongAnswer2,timetoanswer)
	values(@TopicId,@QuestionText,@CorrectAnswer,@WrongAnswer1,@WrongAnswer2, @timetoanswer);
	select @id = SCOPE_IDENTITY();

end