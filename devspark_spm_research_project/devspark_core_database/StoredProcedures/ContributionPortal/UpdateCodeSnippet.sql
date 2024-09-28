CREATE PROCEDURE UpdateCodeSnippet
    @Id INT,
    @jsonString NVARCHAR(MAX),
    @executionStatus BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Declare variables to hold parsed JSON data
        DECLARE @Title NVARCHAR(255), @Language NVARCHAR(100), @Code NVARCHAR(MAX), 
                @Description NVARCHAR(MAX), @Tags NVARCHAR(255);

        -- Parse JSON string into variables
        SELECT @Title = JSON_VALUE(@jsonString, '$.Title'),
               @Language = JSON_VALUE(@jsonString, '$.Language'),
               @Code = JSON_VALUE(@jsonString, '$.Code'),
               @Description = JSON_VALUE(@jsonString, '$.Description'),
               @Tags = JSON_VALUE(@jsonString, '$.Tags');

        -- Update the corresponding record
        UPDATE CodeSnippetLibrary
        SET [Title] = @Title,
            [Language] = @Language,
            [Code] = @Code,
            [Description] = @Description,
            [Tags] = @Tags
        WHERE Id = @Id;

        SET @executionStatus = 1;  -- Indicate success
    END TRY
    BEGIN CATCH
        SET @executionStatus = 0;  -- Indicate failure
    END CATCH
END;
