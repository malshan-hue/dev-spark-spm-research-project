CREATE TABLE [dbo].[Exercise]
(
	[ExerciseId] INT IDENTITY NOT NULL,
	[SubmoduleId] INT NOT NUlL,
	[Description] NVARCHAR(MAX),
	ProgressStatusEnum INT DEFAULT 1 NULL

	CONSTRAINT [Exercise_ExerciseId_Pk] PRIMARY KEY CLUSTERED ([ExerciseId]),
	CONSTRAINT [Exercise_SubmoduleId_FK] FOREIGN KEY ([SubmoduleId]) REFERENCES Submodule([SubmoduleId])
)
