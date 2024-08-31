CREATE PROCEDURE [dbo].[UpdateAnswer]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    SET @executionStatus = 0;

    BEGIN TRY
        UPDATE [Answer]
        SET [Explanation] = jsonData.[Body]
        FROM OPENJSON(@jsonString, '$')
        WITH(
            [AnswerId] INT,
            [Body] NVARCHAR(MAX)
        ) AS jsonData
        WHERE [Answer].[AnswerId] = jsonData.[AnswerId];

        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
        
        SET @executionStatus = 0;
    END CATCH
END
