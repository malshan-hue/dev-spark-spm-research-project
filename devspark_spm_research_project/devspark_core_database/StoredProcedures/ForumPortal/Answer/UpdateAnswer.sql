CREATE PROCEDURE [dbo].[UpdateAnswer]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    SET @executionStatus = 0;

    BEGIN TRY
        -- Open the JSON and map its data to variables
        UPDATE [Answer]
        SET [Explanation] = jsonData.[Explanation]
        FROM OPENJSON(@jsonString, '$')
        WITH(
            [AnswerId] INT,
            [Explanation] NVARCHAR(MAX)
        ) AS jsonData
        WHERE [Answer].[AnswerId] = jsonData.[AnswerId];  -- Correct reference to jsonData

        -- Set execution status to success
        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
        -- Handle any errors by setting execution status to failure
        SET @executionStatus = 0;
    END CATCH
END
