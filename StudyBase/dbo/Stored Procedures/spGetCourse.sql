CREATE PROCEDURE [dbo].[spGetCourse]

	@id int

AS
Begin

set nocount on

select TOP 1 * from dbo.Courses where id=@id

end