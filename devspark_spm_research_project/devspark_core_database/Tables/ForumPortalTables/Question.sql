CREATE TABLE [dbo].[Question]
(
    [QuestionId] INT IDENTITY NOT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [UserId] INT NULL,
    [DatePosted] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT [Question_QuestionId_PK] PRIMARY KEY CLUSTERED ([QuestionId]),
    CONSTRAINT [Question_UserId_FK] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId])
)
