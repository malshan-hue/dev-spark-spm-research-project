CREATE PROCEDURE [dbo].[RetrieveAnswers]
    @QuestionId INT
AS
BEGIN
    SELECT [AnswerId], [Explanation], [UserId], [DatePosted]
    FROM [Answer]
    WHERE [QuestionId] = @QuestionId
    ORDER BY [DatePosted] ASC;
END
