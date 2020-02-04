CREATE PROCEDURE [dbo].[spGetCoursesByteacher]

	@TeacherId int

AS
Begin

set nocount on

select * from dbo.Courses where TeacherId=@TeacherId

end