CREATE TABLE [dbo].[Answer]
(
	[AnswerId] INT IDENTITY NOT NULL,
    [QuestionId] INT NOT NULL,
    [Explanation] NVARCHAR(MAX) NOT NULL,
    [UserId] NVARCHAR(255) NULL,
    [DatePosted] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT [Answer_AnswerId_PK] PRIMARY KEY CLUSTERED ([AnswerId]),
    CONSTRAINT [Answer_QuestionId_FK] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question]([QuestionId]) ON DELETE CASCADE,
    
)
