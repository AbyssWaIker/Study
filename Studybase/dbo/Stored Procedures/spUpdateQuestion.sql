CREATE PROCEDURE [dbo].[spUpdateQuestion]
	@id int,
	@QuestionText nvarchar(200),
	@CorrectAnswer nvarchar(50),
	@timetoanswer int
	
AS
begin

	set nocount on;
	update dbo.Questions  
	set QuestionText=@QuestionText, CorrectAnswer=@CorrectAnswer, timetoanswer=@timetoanswer
	where id=@id 
	
end