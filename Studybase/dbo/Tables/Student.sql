CREATE TABLE [dbo].[Student] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [StudentName]     NVARCHAR (50) NOT NULL,
    [StudentGroupid]  INT           NOT NULL,
    [userName]        NVARCHAR (50) NOT NULL,
    [StudentPassword] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Student_ToGroup] FOREIGN KEY ([StudentGroupid]) REFERENCES [dbo].[Groups] ([Id])
);

