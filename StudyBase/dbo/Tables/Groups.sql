CREATE TABLE [dbo].[Groups] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [GroupName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

