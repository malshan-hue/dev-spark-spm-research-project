CREATE PROCEDURE [dbo].[CreateNewFolder]
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
