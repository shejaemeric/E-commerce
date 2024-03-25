CREATE PROCEDURE dbo.deleteShoppingSession(@shoppingSessionId int,@actionPeformerId int,@referenceId varchar(250)) 
AS
BEGIN

INSERT INTO CartItems_Archive (
      [Archive_Id]
      ,[Quantity]
      ,[Created_At]
      ,[Modified_At]
      ,[Product_Id]
      ,[ShoppingSession_Id]
      ,[Action]
      ,[Peformed_At]
      ,[PeformedById]
      ,[Record_Type]
      ,[Reference_Id]
)
SELECT 
        [Id]
      ,[Quantity]
      ,[Created_At]
      ,[Modified_At]
      ,[ProductId]
      ,[ShoppingSessionId],
        'DELETE' Actions,
        GETDATE() Peformed_At,
        @actionPeformerId,
        'delete' Record_Type
        ,@referenceId 
 FROM  CartItems WHERE ShoppingSessionId = @shoppingSessionId


 INSERT INTO ShoppingSessions_Archive (
      [Archive_Id]
      ,[Total]
      ,[Created_At]
      ,[Modified_At]
      ,[UserId]
      ,[Action]
      ,[Peformed_At]
      ,[PeformedById]
      ,[Record_Type]
      ,[Reference_Id]
 ) SELECT         
        [Id]
      ,[Total]
      ,[Created_At]
      ,[Modified_At]
      ,[UserId],
    'DELETE' Actions,
    GETDATE() Peformed_At,
    @actionPeformerId,
    'delete' Record_Type
    ,@referenceId  from ShoppingSessions WHERE Id = @shoppingSessionId
END;

-- EXEC dbo.deleteShoppingSession @shoppingSessionId = 4,@actionPeformerId = 1,@referenceId ='126326gewttyghsdghhvhgsvdhjjhdshj';
-- DELETE FROM CartItems WHERE ShoppingSessionId = 3;
-- DELETE FROM ShoppingSessions WHERE Id = 3;
