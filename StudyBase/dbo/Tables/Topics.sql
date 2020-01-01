CREATE TABLE [dbo].[Topic] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Courseid] INT NOT NULL, 
    [TopicOrderNumber] INT NOT NULL, 
    [topicName] NVARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Topic_ToCourse] FOREIGN KEY ([Courseid]) REFERENCES [Courses]([id])
);

