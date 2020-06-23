CREATE TABLE [brm].[Document] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [FileName]     VARCHAR (50)     NULL,
    [FileData]     VARBINARY (MAX)  NULL,
    [CreatedDate]  DATETIME2 (7)    NOT NULL,
    [ModifiedDate] DATETIME2 (7)    NOT NULL,
    [ModifiedBy]   VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([Id] ASC)
);

