CREATE PROCEDURE [dbo].[spGetPasswordByUserName]
	@userName nvarchar(50),
	@StudentPassword nvarchar(50) output
AS
begin
	SELECT TOP 1 StudentPassword from dbo.Student where userName=@userName
end