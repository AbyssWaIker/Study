CREATE PROCEDURE [dbo].[spGetGradesByStudent]
	@studentid int
AS

begin
set nocount on  
	SELECT  * from dbo.Grades where studentid = @studentid
end