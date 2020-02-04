CREATE PROCEDURE [dbo].[spGetTeachersNumber]

AS
Begin
select count(*) from dbo.Teachers

end