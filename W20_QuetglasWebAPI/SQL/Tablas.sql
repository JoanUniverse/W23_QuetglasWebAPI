CREATE TABLE [dbo].[Messages] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [PlayerId]    NVARCHAR (128) NOT NULL,
    [Message]     NVARCHAR (256) NULL,
    [MessageTime] DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




CREATE TABLE [dbo].[Players] (
    [Id]        NVARCHAR (128) NOT NULL,
    [Name]      NVARCHAR (256) NOT NULL,
    [Email]     NVARCHAR (256) NOT NULL,
    [BirthDay]  DATETIME2 (7)  NOT NULL,
    [LastLogin] DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Players_To_AspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);