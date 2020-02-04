CREATE PROCEDURE [dbo].[spGetGroupName]

	@id int

AS
Begin

set nocount on

select TOP 1 GroupName from dbo.Groups where id=@id

end