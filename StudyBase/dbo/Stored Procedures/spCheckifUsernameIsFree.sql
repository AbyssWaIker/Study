CREATE PROCEDURE [dbo].[spCheckifUsernameIsFree]
	@userName nvarchar(50),
	@get int = 0 output
AS
begin
	if not exists (select 1 from Student where userName = @userName) select @get=1
else select @get=0
end