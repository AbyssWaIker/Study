CREATE TABLE [dbo].[Questions] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [topicid]       INT            NOT NULL,
    [questiontext]  NVARCHAR (200) NOT NULL,
    [correctAnswer] NVARCHAR (50)  NOT NULL,
    [timeToAnswer]  INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Questions_ToTopicid] FOREIGN KEY ([topicid]) REFERENCES [dbo].[Topic] ([Id])
);

