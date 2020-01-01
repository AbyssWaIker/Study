CREATE TABLE [dbo].[Student] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [StudentName]     NVARCHAR (50) NOT NULL,
    [StudentGroup]    NVARCHAR (50) NOT NULL,
    [userName]        NVARCHAR (50) NOT NULL,
    [StudentPassword] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

