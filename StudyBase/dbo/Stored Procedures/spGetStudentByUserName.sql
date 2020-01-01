CREATE PROCEDURE [dbo].[spGetStudentByUserName]
	
	@userName nvarchar(50)

AS
Begin

set nocount on

select * from dbo.Student where userName=@userName

end