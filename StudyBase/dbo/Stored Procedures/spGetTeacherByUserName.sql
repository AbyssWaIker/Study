CREATE PROCEDURE [dbo].[spGetTeacherByUserName]
	
	@userName nvarchar(50)

AS
Begin

set nocount on

select TOP 1 * from dbo.Teachers where userName=@userName

end