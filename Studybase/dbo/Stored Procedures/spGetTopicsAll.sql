﻿CREATE PROCEDURE [dbo].[spGetTopicsAll]



AS
Begin

set nocount on

select * from dbo.Topics

end