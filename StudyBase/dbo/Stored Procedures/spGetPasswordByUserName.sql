CREATE PROCEDURE [dbo].[spGetPasswordByUserName]
	@userName nvarchar(50),
	@StudentPassword nvarchar(50) output
AS
begin
	SELECT StudentPassword from dbo.Student where userName=@userName
end