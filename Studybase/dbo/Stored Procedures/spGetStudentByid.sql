CREATE PROCEDURE [dbo].[spGetStudentByid]
	
	@id int



AS
Begin

set nocount on

select * from dbo.Student where id=@id

end