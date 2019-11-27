CREATE TABLE [dbo].[Topic] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [topicName]        NVARCHAR (50) NOT NULL,
    [TopicOrderNumber] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

