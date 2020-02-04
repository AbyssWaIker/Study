CREATE PROCEDURE [dbo].[spGetGroups_All]

AS
Begin

set nocount on

select * from dbo.Groups

end