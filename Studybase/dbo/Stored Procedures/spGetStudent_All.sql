CREATE PROCEDURE [dbo].[spGetStudent_All]

AS
Begin

set nocount on

select Id, StudentName, StudentGroupid from dbo.Student

end