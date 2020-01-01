CREATE PROCEDURE [dbo].[spGetStudentByid]
	
	@id int



AS
Begin

set nocount on

select Id, StudentName, StudentGroup from dbo.Student where id=@id

end