CREATE PROCEDURE [dbo].[spGetStudent_All]

AS
Begin

set nocount on

select Id, StudentName, StudentGroup from dbo.Student

end