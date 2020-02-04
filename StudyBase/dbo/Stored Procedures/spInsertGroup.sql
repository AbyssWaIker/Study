CREATE PROCEDURE dbo.spInsertGroup
	@Name nvarchar(50),
	@id int = 0 output 
AS

begin
	set nocount on;
	insert into dbo.Groups(GroupName)
	values(@Name);
	select @id = SCOPE_IDENTITY();
end