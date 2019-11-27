CREATE PROCEDURE [dbo].[spGetTopicByid]


	@id int
	
AS
Begin

set nocount on

select * from dbo.Topic where id=@id

end