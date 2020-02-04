CREATE PROCEDURE [dbo].[spGetTopicsNumberByCourseid]
	
	@Courseid int
AS
Begin
select count(*) from dbo.Topics
where Courseid=@Courseid

end