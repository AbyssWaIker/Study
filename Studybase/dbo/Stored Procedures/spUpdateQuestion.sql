CREATE PROCEDURE [dbo].[spUpdateQuestion]
	@id int,
	@QuestionText nvarchar(200),
	@CorrectAnswer nvarchar(50),
	@WrongAnswer1 nvarchar(50),
	@WrongAnswer2 nvarchar(50),
	@timetoanswer int
	
AS
begin

	set nocount on;
	update dbo.Questions  
	set QuestionText=@QuestionText, CorrectAnswer=@CorrectAnswer, WrongAnswer1=@WrongAnswer1, WrongAnswer2=@WrongAnswer2, timetoanswer=@timetoanswer
	where id=@id 
	
end