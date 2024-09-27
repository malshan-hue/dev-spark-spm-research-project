CREATE PROCEDURE [dbo].[DeleteFile]
    @FileId INT,
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Delete associated files
        DELETE FROM Files WHERE Id = @FileId;

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
