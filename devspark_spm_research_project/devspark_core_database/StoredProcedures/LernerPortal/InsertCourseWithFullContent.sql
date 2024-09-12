CREATE PROCEDURE [dbo].[InsertCourseWithFullContent]
	@jsonString NVARCHAR(MAX) = '',
	@executionStatus BIT OUT
AS
BEGIN
	
	BEGIN TRANSACTION;

	BEGIN TRY

		INSERT INTO [Course]([UserId], [CourseName], [CourseContent], [AreaOfStudyEnum], [CurrentStatusEnum], [YearsOfExperience], [AchivingLevelEnum], [StudyPeriodEnum], [CreatedDateTime])
		SELECT [UserId], [CourseName], [CourseContent], [AreaOfStudyEnum], [CurrentStatusEnum], [YearsOfExperience], [AchivingLevelEnum], [StudyPeriodEnum], GETUTCDATE()
		FROM OPENJSON(@jsonString, '$')
		WITH(
			[UserId] INT,
			[CourseName] NVARCHAR(MAX),
			[CourseContent] NVARCHAR(MAX),
			[AreaOfStudyEnum] INT,
			[CurrentStatusEnum] INT,
			[YearsOfExperience] INT,
			[AchivingLevelEnum] INT,
			[StudyPeriodEnum] INT
		);

		COMMIT TRANSACTION;
		SET @executionStatus = 1;

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION;
		SET @executionStatus = 0;
        THROW;

    END CATCH
END