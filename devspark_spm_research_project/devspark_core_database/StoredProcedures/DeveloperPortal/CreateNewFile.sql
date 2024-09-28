CREATE PROCEDURE [dbo].[CreateNewFile]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
      
        -- Insert data into FileModel table
        INSERT INTO [Files] ([FolderId], [FileTitle], [Language], [Extension], [CodeSnippet], [UserId])
        SELECT [FolderId], [FileTitle], [Language], [Extension], [CodeSnippet], [UserId]
        FROM OPENJSON(@jsonString, '$')
        WITH (
            [FolderId] INT,
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