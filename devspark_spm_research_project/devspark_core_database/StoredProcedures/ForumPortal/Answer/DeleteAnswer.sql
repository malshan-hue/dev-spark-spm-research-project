CREATE PROCEDURE [dbo].[DeleteAnswer]
    @AnswerId INT,
    @executionStatus BIT OUT
AS
BEGIN
    SET @executionStatus = 0;

    BEGIN TRY
        DELETE FROM [Answer]
        WHERE [AnswerId] = @AnswerId;

        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
        
        SET @executionStatus = 0;
    END CATCH
END
