CREATE PROCEDURE [dbo].[InsertUser]
	@jsonString NVARCHAR(MAX) = '',
	@executionStatus BIT OUT
AS
BEGIN

	INSERT INTO [User]([FirstName],[LastName],[PersonalEmail])
	SELECT [FirstName],[LastName],[PersonalEmail]
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[FirstName] NVARCHAR(100),
		[LastName] NVARCHAR(100),
		[PersonalEmail] NVARCHAR(100)
	);

	SET @executionStatus = 1;
END
