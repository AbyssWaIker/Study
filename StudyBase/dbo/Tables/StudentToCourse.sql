CREATE TABLE [dbo].[StudentToCourse] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [Groupid]  INT NOT NULL,
    [Courseid] INT NOT NULL,
    [Access]   INT DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StudentToCourse_Course] FOREIGN KEY ([Courseid]) REFERENCES [dbo].[Courses] ([Id]),
    CONSTRAINT [FK_StudentToCourse_Group] FOREIGN KEY ([Groupid]) REFERENCES [dbo].[Groups] ([Id])
);

