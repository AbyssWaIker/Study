CREATE TABLE [dbo].[WrongAnswers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [QuestionId] INT NOT NULL, 
    [WrongAnswerText] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_WrongAnswers_ToTable] FOREIGN KEY ([QuestionId]) REFERENCES [Questions]([id])
)
