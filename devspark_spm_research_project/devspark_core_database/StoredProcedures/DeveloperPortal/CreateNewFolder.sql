CREATE PROCEDURE [dbo].[CreateNewFolder]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Insert data into Folder table
        INSERT INTO [Folder] ([FolderTitle])
        SELECT [FolderTitle]
        FROM OPENJSON(@jsonString, '$')
        WITH (
            [FolderTitle] NVARCHAR(255)
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
