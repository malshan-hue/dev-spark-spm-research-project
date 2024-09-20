CREATE TABLE [dbo].[Module]
(
	[ModuleId] INT IDENTITY NOT NULL,
	[CourseId] INT NOT NULL,
	[Title] NVARCHAR(MAX) NULL,
	[Description] NVARCHAR(MAX) NULL,
	ProgressStatusEnum INT DEFAULT 1 NULL

	CONSTRAINT [Module_ModuleId_Pk] PRIMARY KEY CLUSTERED ([ModuleId]),
	CONSTRAINT [Module_CourseId_FK] FOREIGN KEY ([CourseId]) REFERENCES Course([CourseId])
)
