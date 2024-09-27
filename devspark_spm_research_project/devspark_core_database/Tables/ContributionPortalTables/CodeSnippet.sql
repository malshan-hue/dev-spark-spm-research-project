﻿CREATE TABLE [dbo].[CodeSnippetLibrary]
(
	[Id] INT PRIMARY KEY CLUSTERED IDENTITY(1,1),
    [UserId] INT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Language] NVARCHAR(100) NOT NULL,
    [Code] NVARCHAR(MAX) NOT NULL,
    [Description] NVARCHAR(MAX),
    [Tags] NVARCHAR(255) NOT NULL,
)
