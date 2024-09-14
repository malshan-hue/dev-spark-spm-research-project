CREATE PROCEDURE [dbo].[InsertCourseWithFullContent]
	@jsonString NVARCHAR(MAX) = '',
	@executionStatus BIT OUT
AS
BEGIN
	
	BEGIN TRANSACTION;

	BEGIN TRY

		DECLARE @courseId INT;
        DECLARE @moduleIds TABLE(Id INT, title NVARCHAR(MAX));
        DECLARE @submoduleIds TABLE(Id INT, title NVARCHAR(MAX));
        DECLARE @tutorialIds TABLE(Id INT, title NVARCHAR(MAX));

        -- insert the course
        INSERT INTO [Course]([UserId], [CourseName], [CourseContent], [AreaOfStudyEnum], [CurrentStatusEnum], [YearsOfExperience], [AchivingLevelEnum], [StudyPeriodEnum], [CreatedDateTime])
        SELECT [UserId], [CourseName], [CourseContent], [AreaOfStudyEnum], [CurrentStatusEnum], [YearsOfExperience], [AchivingLevelEnum], [StudyPeriodEnum], GETUTCDATE()
        FROM OPENJSON(@jsonString, '$') WITH(
            [UserId] INT,
            [CourseName] NVARCHAR(MAX),
            [CourseContent] NVARCHAR(MAX),
            [AreaOfStudyEnum] INT,
            [CurrentStatusEnum] INT,
            [YearsOfExperience] INT,
            [AchivingLevelEnum] INT,
            [StudyPeriodEnum] INT
        );
        SET @courseId = SCOPE_IDENTITY();
        -- end insert course

        -- insert modules
        INSERT INTO [Module]([CourseId], [Title], [Description])
        OUTPUT INSERTED.ModuleId, INSERTED.Title INTO @moduleIds(Id, Title)
        SELECT @courseId, ModuleJson.[Title], ModuleJson.[Description]
        FROM OPENJSON(@jsonString, '$.Modules') WITH (
            [Title] NVARCHAR(MAX),
            [Description] NVARCHAR(MAX)
        ) AS ModuleJson;
        -- end insert modules

        -- insert submodules
        INSERT INTO [Submodule]([ModuleId], [Title], [Content])
        OUTPUT INSERTED.SubmoduleId, INSERTED.Title INTO @submoduleIds(Id, Title)
        SELECT (SELECT Id FROM @moduleIds WHERE title = ModuleJson.[Title]), SubmoduleJson.[Title], SubmoduleJson.[Content]
        FROM OPENJSON(@jsonString, '$.Modules') WITH (
            [Title] NVARCHAR(MAX),
            [Description] NVARCHAR(MAX),
            Submodules NVARCHAR(MAX) AS JSON
        ) AS ModuleJson
        CROSS APPLY OPENJSON(ModuleJson.Submodules) WITH (
            [Title] NVARCHAR(MAX),
            [Content] NVARCHAR(MAX)
        ) AS SubmoduleJson
        -- end insert submodules

        -- insert exercises
        INSERT INTO Exercise([SubmoduleId], [Description])
        SELECT (SELECT Id FROM @submoduleIds WHERE title = SubmoduleJson.[Title]), ExerciseJson.[Description]
        FROM OPENJSON(@jsonString, '$.Modules') WITH (
            [Title] NVARCHAR(MAX),
            [Description] NVARCHAR(MAX),
            Submodules NVARCHAR(MAX) AS JSON
        ) AS ModuleJson
        CROSS APPLY OPENJSON(ModuleJson.Submodules) WITH (
            [Title] NVARCHAR(MAX),
            [Content] NVARCHAR(MAX),
            Exercises NVARCHAR(MAX) AS JSON
        ) AS SubmoduleJson
        CROSS APPLY OPENJSON(SubmoduleJson.Exercises) WITH (
            [Description] NVARCHAR(MAX)
        ) AS ExerciseJson;
        -- end insert exercises

        -- insert tutorials
        INSERT INTO Tutorial([SubmoduleId], [Title], [Content])
        OUTPUT INSERTED.TutorialId, INSERTED.Title INTO @tutorialIds(Id, Title)
        SELECT (SELECT Id FROM @submoduleIds WHERE title = SubmoduleJson.[Title]), TutorialJson.[Title] AS TutorialTitle, TutorialJson.[Content] AS TutorialContent
        FROM OPENJSON(@jsonString, '$.Modules') WITH (
            [Title] NVARCHAR(MAX),
            [Description] NVARCHAR(MAX),
            Submodules NVARCHAR(MAX) AS JSON
        ) AS ModuleJson
        CROSS APPLY OPENJSON(ModuleJson.Submodules) WITH (
            [Title] NVARCHAR(MAX),
            [Content] NVARCHAR(MAX),
            Tutorials NVARCHAR(MAX) AS JSON
        ) AS SubmoduleJson
        CROSS APPLY OPENJSON(SubmoduleJson.Tutorials) WITH (
            [Title] NVARCHAR(MAX),
            [Content] NVARCHAR(MAX)
        ) AS TutorialJson;
        -- end insert tutorials

        -- insert codesnippets
        INSERT INTO CodeSnippet([TutorialId], [Language], [Code])
        SELECT (SELECT Id FROM @tutorialIds WHERE title = TutorialJson.[Title]), CodeSnippetJson.[Language], CodeSnippetJson.[Code]
        FROM OPENJSON(@jsonString, '$.Modules') WITH (
            [Title] NVARCHAR(MAX),
            [Description] NVARCHAR(MAX),
            Submodules NVARCHAR(MAX) AS JSON
        ) AS ModuleJson
        CROSS APPLY OPENJSON(ModuleJson.Submodules) WITH (
            [Title] NVARCHAR(MAX),
            [Content] NVARCHAR(MAX),
            Tutorials NVARCHAR(MAX) AS JSON
        ) AS SubmoduleJson
        CROSS APPLY OPENJSON(SubmoduleJson.Tutorials) WITH (
            [Title] NVARCHAR(MAX),
            [Content] NVARCHAR(MAX),
            CodeSnippets NVARCHAR(MAX) AS JSON
        ) AS TutorialJson
        CROSS APPLY OPENJSON(TutorialJson.CodeSnippets) WITH (
            [Language] NVARCHAR(100),
            [Code] NVARCHAR(MAX)
        ) AS CodeSnippetJson;
        -- end insert codesnippets

		COMMIT TRANSACTION;
		SET @executionStatus = 1;

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION;
		SET @executionStatus = 0;
        THROW;

    END CATCH
END