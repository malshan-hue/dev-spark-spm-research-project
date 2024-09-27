CREATE PROCEDURE [dbo].[RetrieveQuestions]
AS
BEGIN
    SELECT 
        [QuestionId], 
        [Title], 
        STRING_ESCAPE([Description], 'json') AS [Description], 
        [DatePosted]
    FROM 
        [Question]
    ORDER BY 
        [DatePosted] DESC
    FOR JSON AUTO;


END
