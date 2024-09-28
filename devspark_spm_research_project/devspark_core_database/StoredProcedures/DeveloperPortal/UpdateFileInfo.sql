CREATE PROCEDURE [dbo].[UpdateFileInfo]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Extract values from JSON string using OPENJSON
        DECLARE @FileId INT;
        DECLARE @FileTitle NVARCHAR(255);
        DECLARE @Language NVARCHAR(50);
        DECLARE @Extension NVARCHAR(50);
        DECLARE @CodeSnippet NVARCHAR(MAX);

        -- Extract values using OPENJSON
        SELECT 
            @FileId = [Id],
            @FileTitle = [FileTitle],
            @Language = [Language],
            @Extension = [Extension],
            @CodeSnippet = [CodeSnippet]
        FROM OPENJSON(@jsonString, '$')
        WITH (
            [Id] INT,
            [FileTitle] NVARCHAR(255),
            [Language] NVARCHAR(50),
            [Extension] NVARCHAR(50),
            [CodeSnippet] NVARCHAR(MAX)
        );

        -- Update the file record where FileId matches
        UPDATE Files
        SET
            FileTitle = @FileTitle,
            Language = @Language,
            Extension = @Extension,
            CodeSnippet = @CodeSnippet
        WHERE Id = @FileId;

        -- Commit the transaction
        COMMIT TRANSACTION;

        -- Set execution status to success
        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
        -- Rollback transaction on error
        ROLLBACK TRANSACTION;

        -- Set execution status to failure
        SET @executionStatus = 0;
    END CATCH
END
