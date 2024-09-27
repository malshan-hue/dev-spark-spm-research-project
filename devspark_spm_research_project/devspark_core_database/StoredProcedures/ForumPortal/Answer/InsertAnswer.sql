CREATE PROCEDURE [dbo].[InsertAnswer]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    SET @executionStatus = 0;

    BEGIN TRY
        INSERT INTO [Answer]([QuestionId], [Explanation], [UserId])
        SELECT [QuestionId], [Explanation], [UserId]
        FROM OPENJSON(@jsonString, '$')
        WITH(
            [QuestionId] INT,
            [Explanation] NVARCHAR(MAX),
            [UserId] INT
        );

        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
         
        SET @executionStatus = 0;
    END CATCH
END
