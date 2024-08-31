CREATE PROCEDURE [dbo].[InsertUser]
	@jsonString NVARCHAR(MAX) = '',
	@executionStatus BIT OUT
AS
BEGIN
	
	BEGIN TRANSACTION;

	BEGIN TRY

		DECLARE @devsparkUserId INT;

		INSERT INTO [DevSparkUser]([FirstName],[LastName],[PersonalEmail], [Password], [PasswordSalt])
		SELECT [FirstName],[LastName],[PersonalEmail], [Password], [PasswordSalt]
		FROM OPENJSON(@jsonString, '$.DevSparkUser')
		WITH(
			[FirstName] NVARCHAR(100),
			[LastName] NVARCHAR(100),
			[PersonalEmail] NVARCHAR(100),
			[Password] NVARCHAR(100),
			[PasswordSalt] NVARCHAR(100)
		);

		SET @devsparkUserId = SCOPE_IDENTITY();

		INSERT INTO [EntraIdUser]([UserId],[Id],[AccountEnabled],[DisplayName],[GivenName],[Surname],[UserPrincipalName],[MailNickname],[Mail],[MobilePhone],[OfficeLocation],[StreetAddress],[City], [State], [Country], [PostalCode], [JobTitle], [Department], [CompanyName])
		SELECT @devsparkUserId,[Id],[AccountEnabled],[DisplayName],[GivenName],[Surname],[UserPrincipalName],[MailNickname],[Mail],[MobilePhone],[OfficeLocation],[StreetAddress],[City], [State], [Country], [PostalCode], [JobTitle], [Department], [CompanyName]
			FROM OPENJSON(@jsonString, '$')
		WITH(
			[Id] NVARCHAR(255),
			[AccountEnabled] BIT,
			[DisplayName] NVARCHAR(255),
			[GivenName] NVARCHAR(255),
			[Surname] NVARCHAR(255) ,
			[UserPrincipalName] NVARCHAR(255) ,
			[MailNickname] NVARCHAR(255) ,
			[Mail] NVARCHAR(255) ,
			[MobilePhone] NVARCHAR(50) ,
			[OfficeLocation] NVARCHAR(255) ,
			[StreetAddress] NVARCHAR(255) ,
			[City] NVARCHAR(100) ,
			[State] NVARCHAR(100) ,
			[Country] NVARCHAR(100) ,
			[PostalCode] NVARCHAR(20) ,
			[JobTitle] NVARCHAR(255) ,
			[Department] NVARCHAR(255) ,
			[CompanyName] NVARCHAR(255) 
		);

		COMMIT TRANSACTION;
		SET @executionStatus = 1;

	END TRY
	BEGIN CATCH

		ROLLBACK TRANSACTION;
		SET @executionStatus = 0;
        THROW;

    END CATCH
END
