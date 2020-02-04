CREATE TABLE [dbo].[Courses] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [TeacherId] INT           NOT NULL,
    [Name]      NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_ToTable] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teachers] ([Id])
);

