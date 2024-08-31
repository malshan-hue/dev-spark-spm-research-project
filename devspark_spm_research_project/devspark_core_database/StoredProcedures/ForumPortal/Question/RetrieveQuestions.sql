CREATE PROCEDURE [dbo].[RetrieveQuestions]
AS
BEGIN
    SELECT [QuestionId], [Title], [Description], [UserId], [DatePosted]
    FROM [Question]
    ORDER BY [DatePosted] DESC;
END
