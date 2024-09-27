CREATE PROCEDURE [dbo].[GetCourseProgressByCourseId]
	@courseId INT = 0
	WITH ENCRYPTION
AS
BEGIN

	SELECT	C.CourseId,

            -- total counts
            (SELECT COUNT(*) FROM [Module] WHERE [Module].CourseId = C.CourseId) AS 'TotalModuleCount',
            (SELECT COUNT(*) FROM [Submodule] WHERE [Submodule].ModuleId IN ( SELECT [Module].ModuleId FROM [Module] WHERE [Module].CourseId = C.CourseId)) AS 'TotalSubModuleCount',
            (SELECT COUNT(*) FROM [Tutorial] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Tutorial].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Module].CourseId = C.CourseId) AS 'TotalTutorialCount',
            (SELECT COUNT(*) FROM [Exercise] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Exercise].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Module].CourseId = C.CourseId) AS 'TotalExerciseCount',

            -- not started
			(SELECT COUNT(*) FROM [Module] WHERE [Module].CourseId = C.CourseId AND [Module].ProgressStatusEnum = 1) AS 'NotStartedModuleCount',
            (SELECT COUNT(*) FROM [Submodule] WHERE [Submodule].ProgressStatusEnum = 1 AND [Submodule].ModuleId IN ( SELECT [Module].ModuleId FROM [Module] WHERE [Module].CourseId = C.CourseId)) AS 'NotStartedSubModuleCount',
            (SELECT COUNT(*) FROM [Tutorial] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Tutorial].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Tutorial].ProgressStatusEnum = 1 AND [Module].CourseId = C.CourseId) AS 'NotStartedTutorialCount',
            (SELECT COUNT(*) FROM [Exercise] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Exercise].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Exercise].ProgressStatusEnum = 1 AND [Module].CourseId = C.CourseId) AS 'NotStartedExerciseCount',

            -- in progress
            (SELECT COUNT(*) FROM [Module] WHERE [Module].CourseId = C.CourseId AND [Module].ProgressStatusEnum = 2) AS 'InProgressModuleCount',
            (SELECT COUNT(*) FROM [Submodule] WHERE [Submodule].ProgressStatusEnum = 2 AND [Submodule].ModuleId IN ( SELECT [Module].ModuleId FROM [Module] WHERE [Module].CourseId = C.CourseId)) AS 'InProgressSubModuleCount',
            (SELECT COUNT(*) FROM [Tutorial] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Tutorial].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Tutorial].ProgressStatusEnum = 2 AND [Module].CourseId = C.CourseId) AS 'InProgressTutorialCount',
            (SELECT COUNT(*) FROM [Exercise] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Exercise].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Exercise].ProgressStatusEnum = 2 AND [Module].CourseId = C.CourseId) AS 'InProgressExerciseCount',

            -- completed
            (SELECT COUNT(*) FROM [Module] WHERE [Module].CourseId = C.CourseId AND [Module].ProgressStatusEnum = 3) AS 'CompletedModuleCount',
            (SELECT COUNT(*) FROM [Submodule] WHERE [Submodule].ProgressStatusEnum = 3 AND [Submodule].ModuleId IN ( SELECT [Module].ModuleId FROM [Module] WHERE [Module].CourseId = C.CourseId)) AS 'CompletedSubModuleCount',
            (SELECT COUNT(*) FROM [Tutorial] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Tutorial].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Tutorial].ProgressStatusEnum = 3 AND [Module].CourseId = C.CourseId) AS 'CompletedTutorialCount',
            (SELECT COUNT(*) FROM [Exercise] INNER JOIN [Submodule] ON [Submodule].SubmoduleId = [Exercise].SubmoduleId INNER JOIN [Module] ON [Module].ModuleId = [Submodule].ModuleId WHERE [Exercise].ProgressStatusEnum = 3 AND [Module].CourseId = C.CourseId) AS 'CompletedExerciseCount'

    FROM Course C  
    WHERE C.CourseId = @courseId
    FOR JSON PATH

END
