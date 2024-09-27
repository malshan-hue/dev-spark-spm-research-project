CREATE TABLE [dbo].[Files]
(
	[Id] INT NOT NULL PRIMARY KEY,
    [FolderId] INT NOT NULL,
    [FileTitle] NVARCHAR(255) NOT NULL,
    [Language] NVARCHAR(50),
    [CodeSnippet] NVARCHAR(555),
    FOREIGN KEY (FolderId) REFERENCES Folder(Id)
)

