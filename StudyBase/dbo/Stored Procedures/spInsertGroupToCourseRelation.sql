CREATE PROCEDURE [dbo].[spInsertGroupToCourseRelation]
	@Courseid int,
	@Groupid int,
	@id int = 0 output 
AS

begin
set nocount on  
	insert into dbo.StudentToCourse(Courseid, Groupid) 
	values(@Courseid,@Groupid);
	select @id = SCOPE_IDENTITY();
end