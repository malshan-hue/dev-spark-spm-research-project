CREATE TABLE [dbo].[Folder]
(
	[Id] INT IDENTITY NOT NULL,
    [FolderTitle] NVARCHAR(255) NOT NULL,
    [UserId] INT NOT NULL

    CONSTRAINT [Folder_FolderId_PK] PRIMARY KEY CLUSTERED ([Id]), 
)