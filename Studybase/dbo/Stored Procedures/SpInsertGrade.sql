CREATE PROCEDURE [dbo].[SpInsertGrade]

	@studentid int,
	@Topicid int,
	@QuestionAnsweredCorrectly int,
	@QuestionAnswered int,
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Grades(studentid, Topicid, QuestionAnsweredCorrectly, QuestionAnswered)
	values(@studentid, @Topicid, @QuestionAnsweredCorrectly, @QuestionAnswered);
	select @id = SCOPE_IDENTITY();

end