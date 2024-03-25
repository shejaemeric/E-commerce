CREATE PROCEDURE dbo.ModifySessionLatestTime @shoppingSession int AS BEGIN
    UPDATE ShoppingSessions SET  Modified_At= GETDATE()  where Id = @shoppingSession;
 END;

 EXEC dbo.ModifySessionLatestTime 2;