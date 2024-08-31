CREATE PROCEDURE [dbo].[CreateDevSpace]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Insert data into Folder table
        INSERT INTO [Folder] ([FolderTitle])
        SELECT [FolderTitle]
        FROM OPENJSON(@jsonString, '$.Folders')
        WITH (
            [FolderTitle] NVARCHAR(255)
        );

        -- Get the ID of the newly inserted folder
        DECLARE @FolderId INT = SCOPE_IDENTITY();
        
        -- Insert data into FileModel table
        INSERT INTO [FileModel] ([FolderId], [FileTitle], [Language], [CodeSnippet])
        SELECT [@FolderId], [FileTitle], [Language], [CodeSnippet]
        FROM OPENJSON(@jsonString, '$.Files')
        WITH (
            [FileTitle] NVARCHAR(255),
            [Language] NVARCHAR(50),
            [CodeSnippet] NVARCHAR(555)
        );
        
        -- Commit the transaction
        COMMIT;
        
        -- Set execution status to success
        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
        -- Rollback transaction on error
        ROLLBACK;
        
        -- Set execution status to failure
        SET @executionStatus = 0;
    END CATCH
END