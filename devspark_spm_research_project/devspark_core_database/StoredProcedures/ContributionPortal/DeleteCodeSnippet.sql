CREATE PROCEDURE DeleteCodeSnippet
    @Id INT,
    @executionStatus BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Delete the record with the given Id
        DELETE FROM CodeSnippetLibrary
        WHERE Id = @Id;

        SET @executionStatus = 1;  -- Indicate success
    END TRY
    BEGIN CATCH
        SET @executionStatus = 0;  -- Indicate failure
    END CATCH
END;
