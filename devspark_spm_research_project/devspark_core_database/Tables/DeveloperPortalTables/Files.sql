CREATE TABLE [dbo].[Files]
(
	[Id] INT IDENTITY NOT NULL,
    [FolderId] INT NOT NULL,
    [FileTitle] NVARCHAR(255) NOT NULL,
    [Language] NVARCHAR(50),
    [Extension] NVARCHAR(50),
    [CodeSnippet] NVARCHAR(MAX),
    [IsNew] BIT NOT NULL DEFAULT 0,
    [UserId] INT NOT NULL
     
	CONSTRAINT [Files_FilesId_PK] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [Files_FolderId_FK] FOREIGN KEY ([FolderId]) REFERENCES [Folder]([Id]) 
)
