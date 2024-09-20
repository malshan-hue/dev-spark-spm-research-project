CREATE PROCEDURE [dbo].[UpdateFolder]
    @JsonData NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Extract values from JSON string using OPENJSON
        DECLARE @FolderId INT;
        DECLARE @FolderTitle NVARCHAR(255);

        -- Extract values using OPENJSON
        SELECT 
            @FolderId = [Id],
            @FolderTitle = [FolderTitle]
        FROM OPENJSON(@JsonData, '$')
        WITH (
            [Id] INT,
            [FolderTitle] NVARCHAR(255)
        );

        -- Update the folder record where FolderId matches
        UPDATE Folder
        SET
            FolderTitle = @FolderTitle
        WHERE Id = @FolderId;

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
