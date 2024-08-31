CREATE TABLE [dbo].[Answer]
(
	[AnswerId] INT IDENTITY NOT NULL,
    [QuestionId] INT NOT NULL,
    [Explanation] NVARCHAR(MAX) NOT NULL,
    [UserId] INT NOT NULL,
    [DatePosted] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT [Answer_AnswerId_PK] PRIMARY KEY CLUSTERED ([AnswerId]),
    CONSTRAINT [Answer_QuestionId_FK] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question]([QuestionId]),
    CONSTRAINT [Answer_UserId_FK] FOREIGN KEY ([UserId]) REFERENCES [dbo].[DevSparkUser]([UserId])
)
