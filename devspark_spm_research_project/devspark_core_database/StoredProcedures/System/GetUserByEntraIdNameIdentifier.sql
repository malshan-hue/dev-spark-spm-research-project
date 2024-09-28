CREATE PROCEDURE [dbo].[GetUserByEntraIdNameIdentifier]
	@userObjectidentifier NVARCHAR(100)
	WITH ENCRYPTION
AS
BEGIN

	SELECT EU.*,
		JSON_QUERY(ISNULL((SELECT DU.* FROM DevSparkUser DU WHERE DU.UserId = EU.UserId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'DevSparkUser'
	FROM EntraIdUser EU
	WHERE  EU.Id = @userObjectidentifier
	FOR JSON PATH

END
