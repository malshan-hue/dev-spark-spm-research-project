CREATE PROCEDURE [dbo].[InsertQuestion]
    @jsonString NVARCHAR(MAX) = '',
    @executionStatus BIT OUT
AS
BEGIN
    SET @executionStatus = 0;

    BEGIN TRY
        INSERT INTO [Question]([Title], [Description]  )
        SELECT [Title], [Description] 
        FROM OPENJSON(@jsonString, '$')
        WITH(
            [Title] NVARCHAR(255),
            [Description] NVARCHAR(MAX)
             
        );

        SET @executionStatus = 1;
    END TRY
    BEGIN CATCH
        
        SET @executionStatus = 0;
    END CATCH
END
