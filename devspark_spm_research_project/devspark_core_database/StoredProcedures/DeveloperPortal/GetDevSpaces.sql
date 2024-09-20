﻿CREATE PROCEDURE [dbo].[GetDevSpaces]
AS
BEGIN
    SELECT F.*,
	JSON_QUERY(ISNULL((SELECT Files.* FROM Files WHERE Files.FolderId = F.Id FOR JSON PATH), '[]')) AS 'Files'
FROM [Folder] F
FOR JSON PATH
END
