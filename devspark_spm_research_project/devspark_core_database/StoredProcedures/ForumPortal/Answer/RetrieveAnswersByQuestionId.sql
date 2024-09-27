CREATE PROCEDURE [dbo].[RetrieveAnswersByQuestionId]
    @questionId INT,
    @jsonResult NVARCHAR(MAX) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT @jsonResult = (
            SELECT 
                [AnswerId], 
                [Explanation], 
                [UserId], 
                [QuestionId]
            FROM 
                [Answer]
            WHERE 
                [QuestionId] = @questionId
            ORDER BY 
                [DatePosted] DESC
            FOR JSON PATH
        )
    END TRY
    BEGIN CATCH
         
        SET @jsonResult = NULL;
    END CATCH
END
