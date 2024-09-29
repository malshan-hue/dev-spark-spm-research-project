CREATE PROCEDURE [dbo].[GetSubmoduleById]
	@submoduleId INT
	WITH ENCRYPTION
AS
BEGIN

    SELECT SM.*,
        JSON_QUERY(ISNULL((SELECT M.*,
            JSON_QUERY(ISNULL((SELECT C.* FROM Course C WHERE C.CourseId = M.CourseId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'Course'
        FROM Module M WHERE M.ModuleId = SM.ModuleId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'Module',
        JSON_QUERY(ISNULL(( SELECT T.*,
            JSON_QUERY(ISNULL((SELECT CS.* FROM CodeSnippet CS WHERE CS.TutorialId = T.TutorialId FOR JSON PATH), '[]')) AS 'CodeSnippets' 
        FROM Tutorial T WHERE T.SubmoduleId = SM.SubmoduleId FOR JSON PATH), '[]')) AS 'Tutorials',
        JSON_QUERY(ISNULL(( SELECT E.* FROM Exercise E WHERE E.SubmoduleId = SM.SubmoduleId FOR JSON PATH), '[]')) AS 'Exercises'
    FROM Submodule SM 
    WHERE SM.SubmoduleId = @submoduleId
    FOR JSON PATH

    IF EXISTS(SELECT SubmoduleId FROM Submodule WHERE ProgressStatusEnum = 1 AND SubmoduleId = @submoduleId)
    BEGIN
        
        UPDATE Submodule SET ProgressStatusEnum = 2 WHERE SubmoduleId = @submoduleId
        UPDATE Tutorial SET ProgressStatusEnum = 2 WHERE SubmoduleId = @submoduleId

    END

END