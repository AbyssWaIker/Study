CREATE PROCEDURE [dbo].[spGetCoursesAll]

AS
Begin

set nocount on

select * from dbo.Courses

end