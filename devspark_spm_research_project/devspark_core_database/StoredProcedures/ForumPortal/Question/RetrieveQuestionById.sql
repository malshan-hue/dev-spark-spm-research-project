CREATE PROCEDURE [dbo].[RetrieveQuestionById]
    @questionId INT,
    @jsonResult NVARCHAR(MAX) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT @jsonResult = (
            SELECT 
                [QuestionId], 
                [Title], 
                [Description], 
                [UserId]
            FROM 
                [Question]
            WHERE 
                [QuestionId] = @questionId
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    END TRY
    BEGIN CATCH
         
        SET @jsonResult = NULL;
    END CATCH
END

