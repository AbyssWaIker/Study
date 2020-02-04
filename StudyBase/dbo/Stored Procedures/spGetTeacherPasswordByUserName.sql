CREATE PROCEDURE [dbo].[spGetTeacherPasswordByUserName]
	@userName nvarchar(50),
	@Password nvarchar(50) output
AS
begin
	SELECT TOP 1 Password from dbo.Teachers where userName=@userName
end