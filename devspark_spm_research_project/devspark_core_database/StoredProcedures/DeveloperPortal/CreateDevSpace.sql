CREATE PROCEDURE [dbo].[CreateDevSpace]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Insert data into Folder table
        INSERT INTO [Folder] ([FolderTitle], [UserId])
        SELECT [FolderTitle], [UserId]
        FROM OPENJSON(@jsonString, '$')
        WITH (
            [FolderTitle] NVARCHAR(255),
            [UserId] INT
        );

        -- Get the ID of the newly inserted folder
        DECLARE @FolderId INT = SCOPE_IDENTITY();
        
        -- Insert data into FileModel table
        INSERT INTO [Files] ([FolderId], [FileTitle], [Language], [Extension], [CodeSnippet], [UserId])
        SELECT @FolderId, [FileTitle], [Language], [Extension], [CodeSnippet], [UserId]
        FROM OPENJSON(@jsonString, '$.Files')
        WITH (
            [FileTitle] NVARCHAR(255),
            [Language] NVARCHAR(50),
            [Extension] NVARCHAR(50),
            [CodeSnippet] NVARCHAR(MAX),
            [UserId] INT
        );
        
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