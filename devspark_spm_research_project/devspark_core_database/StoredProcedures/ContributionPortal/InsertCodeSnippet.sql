CREATE PROCEDURE [dbo].[InsertCodeSnippet]
	@jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN

	INSERT INTO [CodeSnippetLibrary] ([Title], [Language], [Code], [Description], [Tags])
	SELECT [Title], [Language], [Code], [Description], [Tags]
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[Title] NVARCHAR(225), 
		[Language] NVARCHAR(100), 
		[Code] NVARCHAR(MAX), 
		[Description] NVARCHAR(MAX), 
		[Tags] NVARCHAR(225)
	);

	SET @executionStatus = 1;

END