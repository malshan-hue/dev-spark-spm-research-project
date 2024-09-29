CREATE TABLE [dbo].[Question]
(
    [QuestionId] INT IDENTITY NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [UserId] NVARCHAR(255) NULL,
    [DatePosted] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT [Question_QuestionId_PK] PRIMARY KEY CLUSTERED ([QuestionId]),
   
)
