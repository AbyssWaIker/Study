CREATE TABLE [dbo].[Courses]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TeacherId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Courses_ToTable] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers]([id])
)
