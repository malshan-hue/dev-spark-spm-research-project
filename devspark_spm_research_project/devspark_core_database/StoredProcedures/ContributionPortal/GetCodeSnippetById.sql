CREATE PROCEDURE GetCodeSnippetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Retrieve the single code snippet and return it as JSON
        SELECT Id, Title, Language, Code, Description, Tags
        FROM CodeSnippetLibrary
        WHERE Id = @Id
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
    END TRY
    BEGIN CATCH
        -- Handle exceptions if needed
    END CATCH
END;
