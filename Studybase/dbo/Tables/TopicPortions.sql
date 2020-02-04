CREATE TABLE [dbo].[TopicPortions] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Topicid]          INT            NOT NULL,
    [TopicPortionName] NVARCHAR (50)  NOT NULL,
    [TopicPortionText] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TopicPortions_Topicid] FOREIGN KEY ([Topicid]) REFERENCES [dbo].[Topics] ([Id])
);

