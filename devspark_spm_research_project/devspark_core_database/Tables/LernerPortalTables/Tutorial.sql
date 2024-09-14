CREATE TABLE [dbo].[Tutorial]
(
	[TutorialId] INT IDENTITY NOT NULL,
	[SubmoduleId] INT NOT NULL,
	[Title] NVARCHAR(MAX) NULL,
	[Content] NVARCHAR(MAX) NULL,
	ProgressStatusEnum INT DEFAULT 1 NULL

	CONSTRAINT [Tutorial_TutorialId_Pk] PRIMARY KEY CLUSTERED ([TutorialId]),
	CONSTRAINT [Tutorial_SubmoduleId_FK] FOREIGN KEY ([SubmoduleId]) REFERENCES Submodule([SubmoduleId])
)
