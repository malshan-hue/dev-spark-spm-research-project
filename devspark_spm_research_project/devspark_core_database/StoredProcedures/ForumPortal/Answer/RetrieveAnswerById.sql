CREATE PROCEDURE [dbo].[RetrieveAnswerById]
    @answerId INT,
    @jsonResult NVARCHAR(MAX) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT @jsonResult = (
            SELECT 
                [AnswerId],
                [QuestionId],
                [Explanation],
                [UserId],
                [DatePosted]
            FROM 
                [Answer]
            WHERE 
                [AnswerId] = @answerId
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    END TRY
    BEGIN CATCH
        SET @jsonResult = NULL;
    END CATCH
END
