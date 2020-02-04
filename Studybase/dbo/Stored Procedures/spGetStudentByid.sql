CREATE PROCEDURE [dbo].[spGetStudentByid]
	
	@id int



AS
Begin

set nocount on

select TOP 1 Id, StudentName, StudentGroupid from dbo.Student where id=@id

end