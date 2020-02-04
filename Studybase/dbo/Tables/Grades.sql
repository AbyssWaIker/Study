CREATE TABLE [dbo].[Grades] (
    [Id]                        INT IDENTITY (1, 1) NOT NULL,
    [studentid]                 INT NOT NULL,
    [Topicid]                   INT NOT NULL,
    [QuestionAnsweredCorrectly] INT NOT NULL,
    [QuestionAnswered]          INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Grades_TopicID] FOREIGN KEY ([Topicid]) REFERENCES [dbo].[Topics] ([Id]),
    CONSTRAINT [FK_Grades_Tostudentid] FOREIGN KEY ([studentid]) REFERENCES [dbo].[Student] ([Id])
);

