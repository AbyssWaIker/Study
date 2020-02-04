CREATE TABLE [dbo].[WrongAnswers] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [QuestionId]      INT           NOT NULL,
    [WrongAnswerText] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WrongAnswers_ToTable] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Questions] ([Id])
);

