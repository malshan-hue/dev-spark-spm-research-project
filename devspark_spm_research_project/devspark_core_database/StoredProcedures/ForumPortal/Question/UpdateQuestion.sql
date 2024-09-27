CREATE PROCEDURE [dbo].[UpdateQuestion]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    SET @executionStatus = 0;

    BEGIN TRY
        UPDATE [Question]
        SET [Title] = jsonData.[Title], [Description] = jsonData.[Description]
        FROM OPENJSON(@jsonString, '$')
        WITH(
            [QuestionId] INT,
            [Title] NVARCHAR(255),
            [Description] NVARCHAR(MAX)
        ) AS jsonData
        WHERE [Question].[QuestionId] = jsonData.[QuestionId];

        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
       
        SET @executionStatus = 0;
    END CATCH
END
