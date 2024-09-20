CREATE PROCEDURE [dbo].[UpdateFile]
    @JsonData NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Extract values from JSON string using OPENJSON
        DECLARE @FileId INT;
        DECLARE @FileTitle NVARCHAR(255);

        -- Extract values using OPENJSON
        SELECT 
            @FileId = [Id],
            @FileTitle = [FileTitle]
        FROM OPENJSON(@JsonData, '$')
        WITH (
            [Id] INT,
            [FileTitle] NVARCHAR(255)
        );

        -- Update the file record where FileId matches
        UPDATE Files
        SET
            FileTitle = @FileTitle
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