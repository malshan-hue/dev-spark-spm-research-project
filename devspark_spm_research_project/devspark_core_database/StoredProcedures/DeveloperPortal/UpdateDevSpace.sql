CREATE PROCEDURE [dbo].[UpdateDevSpace]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Extract FolderId from the JSON string
        DECLARE @FolderId INT;
        SELECT @FolderId = [Id]
        FROM OPENJSON(@jsonString, '$')
        WITH (
            [Id] INT
        );

        -- Insert only the new file into the Files table
        INSERT INTO [Files] ([FolderId], [FileTitle], [Language], [Extension], [CodeSnippet])
        SELECT @FolderId, [FileTitle], [Language], [Extension], [CodeSnippet]
        FROM OPENJSON(@jsonString, '$.Files')
        WITH (
            [FileTitle] NVARCHAR(255),
            [Language] NVARCHAR(50),
            [Extension] NVARCHAR(50),
            [CodeSnippet] NVARCHAR(MAX),
            [IsNew] BIT
        )
        WHERE [IsNew] = 1; -- Only insert files flagged as new

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
