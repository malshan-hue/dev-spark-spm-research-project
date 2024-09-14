CREATE TABLE [dbo].[Course]
(
	[CourseId] INT IDENTITY NOT NULL,
	[UserId] INT NOT NULL,
	[CourseName] NVARCHAR(MAX),
	[CourseContent] NVARCHAR(MAX),
	[AreaOfStudyEnum] INT NULL,
	[CurrentStatusEnum] INT NULL,
	[YearsOfExperience] INT NULL,
	[AchivingLevelEnum] INT NULL,
	[StudyPeriodEnum] INT NULL,
	[CreatedDateTime] DATETIME NULL,
	ProgressStatusEnum INT DEFAULT 1 NULL


	CONSTRAINT [Course_Id_Pk] PRIMARY KEY CLUSTERED ([CourseId])
)
