CREATE PROCEDURE [dbo].[spGetTopicsByCourse]

	@Courseid int

AS
Begin

set nocount on

select * from dbo.Topics where Courseid = @Courseid
ORDER By TopicOrderNumber ASC

end