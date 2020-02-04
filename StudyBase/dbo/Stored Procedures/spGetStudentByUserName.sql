CREATE PROCEDURE [dbo].[spGetStudentByUserName]
	
	@userName nvarchar(50)

AS
Begin

set nocount on

select TOP 1 * from dbo.Student where userName=@userName

end