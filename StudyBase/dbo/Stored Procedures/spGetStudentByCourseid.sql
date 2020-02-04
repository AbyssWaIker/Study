CREATE PROCEDURE [dbo].[spGetStudentByCourseid]
	
	@Courseid int
AS
Begin

set nocount on

select Student.Id, Student.StudentName, Student.StudentGroupid from dbo.Student
inner JOIN StudentToCourse on StudentToCourse.Groupid=Student.StudentGroupid
where StudentToCourse.Courseid=@Courseid and StudentToCourse.Access = 1

end