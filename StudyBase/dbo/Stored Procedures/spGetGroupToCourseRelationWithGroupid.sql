CREATE PROCEDURE [dbo].[spGetGroupToCourseRelationWithGroupid]
	@Groupid int
AS

begin
set nocount on  
	select * from dbo.StudentToCourse where Groupid = @Groupid
end