CREATE PROCEDURE [dbo].[spUpdateCourseToGroupRealtion]
	@Access int,
	@id int 
AS

begin
	set nocount on;
	update dbo.StudentToCourse
	set Access = @Access
	where id = @id;
end