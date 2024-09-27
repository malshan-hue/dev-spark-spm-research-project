CREATE PROCEDURE [dbo].[DeleteQuestion]
    @QuestionId INT,
    @executionStatus BIT OUT
AS
BEGIN
    SET @executionStatus = 0;

    BEGIN TRY
        DELETE FROM [Question]
        WHERE [QuestionId] = @QuestionId;

        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
         
        SET @executionStatus = 0;
    END CATCH
END

