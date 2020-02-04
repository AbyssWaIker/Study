CREATE PROCEDURE [dbo].[spCheckifTeacherUsernameIsFree]
	@userName nvarchar(50),
	@get int = 0 output
AS
begin
	if not exists (select 1 from Teachers where userName = @userName) select @get=1
else select @get=0
end