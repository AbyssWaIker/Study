CREATE PROCEDURE [dbo].[spGetTopicByid]


	@id int
	
AS
Begin

set nocount on

select TOP 1 * from dbo.Topics where id=@id

end