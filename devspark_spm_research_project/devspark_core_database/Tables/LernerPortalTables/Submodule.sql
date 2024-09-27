CREATE TABLE [dbo].[Submodule]
(
	[SubmoduleId] INT IDENTITY NOT NULL,
	[ModuleId] INT NOT NULL,
	[Title] NVARCHAR(MAX) NULL,
	[Content] NVARCHAR(MAX) NULL,
	ProgressStatusEnum INT DEFAULT 1 NULL

	CONSTRAINT [Submodule_SubmoduleId_Pk] PRIMARY KEY CLUSTERED ([SubmoduleId]),
	CONSTRAINT [Submodule_ModuleId_FK] FOREIGN KEY ([ModuleId]) REFERENCES Module([ModuleId])
)
