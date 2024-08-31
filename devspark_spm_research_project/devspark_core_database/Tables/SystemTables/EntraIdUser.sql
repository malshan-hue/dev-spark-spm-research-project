CREATE TABLE [dbo].[EntraIdUser]
(
	[UserId] INT NOT NULL,
    [Id] NVARCHAR(255) NOT NULL,
    [AccountEnabled] BIT DEFAULT 0 NOT NULL,
    [DisplayName] NVARCHAR(255) NULL,
    [GivenName] NVARCHAR(255) NULL,
    [Surname] NVARCHAR(255) NULL,
    [UserPrincipalName] NVARCHAR(255) NULL,
    [MailNickname] NVARCHAR(255) NULL,
    [Mail] NVARCHAR(255) NULL,
    [MobilePhone] NVARCHAR(50) NULL,
    [OfficeLocation] NVARCHAR(255) NULL,
    [StreetAddress] NVARCHAR(255) NULL,
    [City] NVARCHAR(100) NULL,
    [State] NVARCHAR(100) NULL,
    [Country] NVARCHAR(100) NULL,
    [PostalCode] NVARCHAR(20) NULL,
    [JobTitle] NVARCHAR(255) NULL,
    [Department] NVARCHAR(255) NULL,
    [CompanyName] NVARCHAR(255) NULL

    CONSTRAINT [EntraIdUser_UserId_FK] FOREIGN KEY ([UserId]) REFERENCES DevSparkUser([UserId])
)
