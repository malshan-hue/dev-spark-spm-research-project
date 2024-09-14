CREATE TABLE [dbo].[CodeSnippet]
(
	[CodeSnippetId] INT IDENTITY NOT NULL,
	[TutorialId] INT NOT NULL,
	[Language] NVARCHAR(100),
	[Code] NVARCHAR(MAX)

	CONSTRAINT [CodeSnippet_CodeSnippetId_Pk] PRIMARY KEY CLUSTERED ([CodeSnippetId]),
	CONSTRAINT [CodeSnippet_TutorialId_FK] FOREIGN KEY ([TutorialId]) REFERENCES Tutorial([TutorialId])
)
