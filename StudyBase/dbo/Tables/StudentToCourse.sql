CREATE TABLE [dbo].[StudentToCourse]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Studentid] INT NOT NULL, 
    [Courseid] INT NOT NULL, 
    CONSTRAINT [FK_StudentToCourse_Student] FOREIGN KEY ([Studentid]) REFERENCES [Student]([id]), 
    CONSTRAINT [FK_StudentToCourse_Course] FOREIGN KEY ([Courseid]) REFERENCES [Courses]([id])
)
