-- ALTER FUNCTION dbo.getLatestUserSession(@userId int)
-- RETURNS TABLE AS
-- RETURN (
--     SELECT TOP 1 * FROM dbo.ShoppingSessions AS C WHERE C.UserId = @userId ORDER BY C.Modified_At DESC
-- )
-- GO

SELECT * FROM dbo.getLatestUserSession (2);