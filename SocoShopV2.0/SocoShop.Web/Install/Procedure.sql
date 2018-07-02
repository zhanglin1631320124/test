



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductCollectIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductCollectIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_ProductCollect WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductPhotoCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductPhotoCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_Product SET [PhotoCount]=[PhotoCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_Product SET [PhotoCount]=[PhotoCount]-1 WHERE [ID] = @id


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StatisticsOrderArea]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_StatisticsOrderArea]
@condition nvarchar(200)
AS 
	IF @condition=''''
		SELECT (substring(RegionID, 0,charindex(''|'',RegionID,5)+1)) AS RegionID ,Count(*)  AS Count FROM SocoShop_Order  Group BY  (substring(RegionID, 0,charindex(''|'',RegionID,5)+1))
	ELSE 
		EXEC(''SELECT (substring(RegionID, 0,charindex(''''|'''',RegionID,5)+1)) AS RegionID,Count(*)  AS Count FROM SocoShop_Order WHERE ''+@condition+''  Group BY (substring(RegionID, 0,charindex(''''|'''',RegionID,5)+1))'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StatisticsOrderStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_StatisticsOrderStatus]
@condition nvarchar(200)
AS 
	IF @condition=''''
		SELECT OrderStatus,Count(*)  AS Count FROM SocoShop_Order  Group BY OrderStatus
	ELSE 
		EXEC(''SELECT OrderStatus,Count(*)  AS Count FROM SocoShop_Order WHERE ''+@condition+''  Group BY OrderStatus'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_DeleteProductPhoto]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ProductPhoto WHERE [ID] IN(''+@strID+'')'')


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StatisticsSaleTotal]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_StatisticsSaleTotal]
@condition nvarchar(200),
@dateType int
AS	
	IF @dateType=1	
		BEGIN
			IF @condition=''''
				SELECT Day(AddDate) As Day,Count(*) AS OrderCount,Sum([ProductMoney]-[FavorableMoney]+[ShippingMoney]+[OtherMoney]) As OrderMoney FROM SocoShop_Order WHERE OrderStatus=6 Group BY Day(AddDate)
			ELSE 
				EXEC(''SELECT Day(AddDate) As Day,Count(*) AS OrderCount,Sum([ProductMoney]-[FavorableMoney]+[ShippingMoney]+[OtherMoney]) As OrderMoney FROM SocoShop_Order WHERE OrderStatus=6 AND ''+@condition+'' Group BY Day(AddDate)'')
		END 
	ELSE 
		BEGIN
			IF @condition=''''
				SELECT Month(AddDate) As Month,Count(*) AS OrderCount,Sum([ProductMoney]-[FavorableMoney]+[ShippingMoney]+[OtherMoney]) As OrderMoney FROM SocoShop_Order WHERE OrderStatus=6 Group BY Month(AddDate)
			ELSE 
				EXEC(''SELECT Month(AddDate) As Month,Count(*) AS OrderCount,Sum([ProductMoney]-[FavorableMoney]+[ShippingMoney]+[OtherMoney]) As OrderMoney FROM SocoShop_Order WHERE OrderStatus=6 AND ''+@condition+'' Group BY Month(AddDate)'')
		END 



' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateOrderDetail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateOrderDetail]
@id int,
@buyCount int
AS 
	UPDATE SocoShop_OrderDetail Set [BuyCount]=@buyCount WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StatisticsOrderCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_StatisticsOrderCount]
@condition nvarchar(200),
@dateType int
AS	
	IF @dateType=1	
		BEGIN
			IF @condition=''''
				SELECT Day(AddDate) As Day,Count(*) AS Count FROM SocoShop_Order Group BY Day(AddDate)
			ELSE 
				EXEC(''SELECT Day(AddDate) As Day,Count(*) AS Count FROM SocoShop_Order WHERE ''+@condition+'' Group BY Day(AddDate)'')
		END 
	ELSE 
		BEGIN
			IF @condition=''''
				SELECT Month(AddDate) As Month,Count(*) AS Count FROM SocoShop_Order Group BY Month(AddDate)
			ELSE 
				EXEC(''SELECT Month(AddDate) As Month,Count(*) AS Count FROM SocoShop_Order WHERE ''+@condition+'' Group BY Month(AddDate)'')
		END 
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrderDetail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteOrderDetail]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_OrderDetail WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrderDetailByOrderID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteOrderDetailByOrderID]
@strOrderID nvarchar(800)
AS 
	IF @strOrderID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_OrderDetail WHERE [OrderID] IN(''+@strOrderID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdmin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteAdmin]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	

		EXEC (''DELETE FROM SocoShop_Admin WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteSendMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteSendMessage]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_SendMessage WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_SendMessage WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductReplyByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteProductReplyByProductID]
@strProductID nvarchar(800)
AS 
	IF @strProductID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ProductReply WHERE [ProductID] IN(''+@strProductID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadSendMessageIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadSendMessageIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_SendMessage WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserOrderCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserOrderCount]
@userID int
AS 

SELECT (SELECT COUNT([ID]) FROM SocoShop_Order WHERE [OrderStatus]=1 AND [UserID]=@userID) ,(SELECT COUNT([ID]) FROM SocoShop_Order  WHERE [UserID]=@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeOrderProductBuyCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROCEDURE [dbo].[SocoShop_ChangeOrderProductBuyCount]
@strID nvarchar(800),
@buyCount int
AS 
	IF @strID=''''
		RETURN
	EXEC(''UPDATE SocoShop_OrderDetail SET [BuyCount]=''+@buyCount+'' WHERE [ID] IN(''+@strID+'')'')




' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdRecordByAdID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_DeleteAdRecordByAdID]
@strAdID nvarchar(200),
@userID int
AS 
	IF @strAdID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_AdRecord WHERE [AdID] IN(''+@strAdID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_AdRecord WHERE [AdID] IN(''+@strAdID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserEmailByMoneyUsed]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserEmailByMoneyUsed]
@condition nvarchar(800)
AS 
	EXEC(''SELECT [Email] FROM(SELECT [Email],ISNULL((SELECT Sum(ProductMoney-FavorableMoney+ShippingMoney+OtherMoney-CouponMoney)  FROM SocoShop_Order WHERE  OrderStatus=6 AND [UserID]=SocoShop_User.[ID]),0) AS MoneyUsed  FROM SocoShop_User) AS TEMP ''+@condition)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteProductClass]
@id int
AS 
	 DECLARE @temp int
	 SELECT @temp=COUNT(*) FROM SocoShop_ProductClass WHERE [FatherID]=@id 
	 IF @temp=0
	 	DELETE FROM SocoShop_ProductClass WHERE [ID]=@id
		
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadCount]
@tableName nvarchar(2000),
@condition nvarchar(2000) -- 查询条件 (注意: 不要加 where) 
AS 
	DECLARE @strSQL  nvarchar(4000) 
        --查询总数
	IF @condition ='''' 
		SET @strSQL = ''SELECT COUNT(*) FROM ''+ @tableName 	
	ELSE 
		SET @strSQL = ''SELECT COUNT(*) FROM ''+ @tableName +'' WHERE ''+@condition 
	EXEC (@strSQL)





' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_IsProductInCart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_IsProductInCart]
@productID int,
@productName nvarchar(200),
@userID int
AS 
	SELECT Count(*) FROM SocoShop_Cart WHERE [ProductID]=@productID AND [ProductName]=@productName AND [UserID]=@userID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteMemberPrice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteMemberPrice]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_MemberPrice WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteCart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteCart]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_Cart WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_Cart WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadCart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadCart]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[ProductID],[ProductName],[BuyCount],[FatherID],[RandNumber],[UserID],[UserName] FROM SocoShop_Cart WHERE [ID]=@id
	ELSE
		SELECT [ID],[ProductID],[ProductName],[BuyCount],[FatherID],[RandNumber],[UserID],[UserName] FROM SocoShop_Cart WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadCartAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadCartAllList]
AS
	SELECT [ID],[ProductID],[ProductName],[BuyCount],[FatherID],[RandNumber],[UserID],[UserName] FROM SocoShop_Cart 

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserCouponIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserCouponIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_UserCoupon WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadCartIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadCartIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_Cart WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateCart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateCart]
@strID nvarchar(200),
@buyCount int
AS 
	IF @strID=''''
		RETURN
	EXEC(''UPDATE SocoShop_Cart Set [BuyCount]=''+@buyCount+'' WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteMemberPriceByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteMemberPriceByProductID]
@strProductID nvarchar(800)
AS 
	IF @strProductID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_MemberPrice WHERE [ProductID] IN(''+@strProductID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserAccountRecordByUserID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteUserAccountRecordByUserID]
@strUserID nvarchar(800)
AS 
	IF @strUserID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_UserAccountRecord WHERE [UserID] IN(''+@strUserID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAttributeIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAttributeIDList]
@strID nvarchar(400),
@adminID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_Attribute WHERE [ID] IN(''+@strID+'') AND [AdminID]=''+@adminID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrderAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteOrderAction]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_OrderAction WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeVoteItemCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeVoteItemCount]
@strID nvarchar(200),
@action nvarchar(10)
AS 
	IF @strID=''''
		RETURN
	IF @action=''Plus''
		EXEC(''UPDATE SocoShop_VoteItem SET [VoteCount]=[VoteCount]+1 WHERE [ID] IN(''+ @strID+'')'')
	ELSE
		EXEC(''UPDATE SocoShop_VoteItem SET [VoteCount]=[VoteCount]-1 WHERE [ID] IN(''+ @strID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserFriend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserFriend]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_UserFriend WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_UserFriend WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeUserGrade]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeUserGrade]
@strID nvarchar(800),
@toStatus int,
@adminID int
AS 
	IF @strID=''''
		RETURN	
	IF @adminID=0
		EXEC(''UPDATE SocoShop_UserGrade SET [ID]=''+ @toStatus +'' WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC(''UPDATE SocoShop_UserGrade SET [ID]=''+ @toStatus +'' WHERE [ID] IN(''+@strID+'') AND [AdminID]=''+@adminID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteStandard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteStandard]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Standard WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdminByGroupID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteAdminByGroupID]
@strGroupID nvarchar(800)
AS 
	IF @strGroupID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Admin WHERE [GroupID] IN(''+@strGroupID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserFriendIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserFriendIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_UserFriend WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrderByNumber]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_ReadOrderByNumber]
@orderNumber nvarchar(50),
@userID int
AS 
	IF @userID=0
		SELECT [ID],[OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName] FROM SocoShop_Order WHERE [OrderNumber]=@orderNumber
	ELSE
		SELECT [ID],[OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName] FROM SocoShop_Order WHERE [OrderNumber]=@orderNumber AND [UserID]=@userID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateUserPhoto]  
@userID int,
@photo nvarchar(200)
AS
	SET NOCOUNT ON;
	UPDATE SocoShop_User SET [Photo] = @photo WHERE [ID] = @userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrderActionByOrderID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteOrderActionByOrderID]
@strOrderID nvarchar(800)
AS 
	IF @strOrderID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_OrderAction WHERE [OrderID] IN(''+@strOrderID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteReceiveMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteReceiveMessage]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_ReceiveMessage WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_ReceiveMessage WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserPassword]
@id int,
@password nvarchar(50)
AS 
		UPDATE SocoShop_User Set [UserPassword]=@password WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteFlash]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteFlash]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Flash WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteLink]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteLink]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Link WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserAddress]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_UserAddress WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_UserAddress WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadReceiveMessageIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadReceiveMessageIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_ReceiveMessage WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StatisticsUserStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_StatisticsUserStatus]
@condition nvarchar(200)
AS 
	IF @condition=''''
		SELECT Status,Count(*)  AS Count FROM SocoShop_User  Group BY Status
	ELSE 
		EXEC(''SELECT Status,Count(*)  AS Count FROM SocoShop_User WHERE ''+@condition+''  Group BY Status'')' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadFlashPhotoByFlashID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadFlashPhotoByFlashID]
@strFlashID nvarchar(100),
@userID int
AS 
	IF @strFlashID=''''
		RETURN	
	IF @userID=0
		EXEC (''SELECT FileName FROM SocoShop_FlashPhoto WHERE [FlashID] IN(''+@strFlashID+'')'')
	ELSE
		EXEC (''SELECT FileName FROM SocoShop_FlashPhoto WHERE [FlashID] IN(''+@strFlashID+'') AND [AdminID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StatisticsUserCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_StatisticsUserCount]
@condition nvarchar(200),
@dateType int
AS	
	IF @dateType=1	
		BEGIN
			IF @condition=''''
				SELECT Day(RegisterDate) As Day,Count(*) AS Count FROM SocoShop_User Group BY Day(RegisterDate)
			ELSE 
				EXEC(''SELECT Day(RegisterDate) As Day,Count(*) AS Count FROM SocoShop_User WHERE ''+@condition+'' Group BY Day(RegisterDate)'')
		END 
	ELSE 
		BEGIN
			IF @condition=''''
				SELECT Month(RegisterDate) As Month,Count(*) AS Count FROM SocoShop_User Group BY Month(RegisterDate)
			ELSE 
				EXEC(''SELECT Month(RegisterDate) As Month,Count(*) AS Count FROM SocoShop_User WHERE ''+@condition+'' Group BY Month(RegisterDate)'')
		END ' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserAddressIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserAddressIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_UserAddress WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserAddressByUserID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteUserAddressByUserID]
@strUserID nvarchar(800)
AS 
	IF @strUserID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_UserAddress WHERE [UserID] IN(''+@strUserID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteFlashPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteFlashPhoto]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_FlashPhoto WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteCoupon]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Coupon WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUploadByRecordID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteUploadByRecordID]
@tableID int,
@strRecordID nvarchar(2000)
AS 
	IF @strRecordID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Upload WHERE [TableID]=''+@tableID+'' AND [RecordID] IN(''+@strRecordID +'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteTaobaoProductClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_DeleteTaobaoProductClass]
AS 
	DELETE SocoShop_ProductClass WHERE TaobaoID>0

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeUserPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeUserPassword]
@id int,
@oldPassword nvarchar(50),
@newPassword nvarchar(50)
AS
	UPDATE SocoShop_User SET [UserPassword]=@newPassword WHERE [ID]=@id AND [UserPassword]=@oldPassword
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteFlashPhotoByFlashID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteFlashPhotoByFlashID]
@strFlashID nvarchar(800)
AS 
	IF @strFlashID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_FlashPhoto WHERE [FlashID] IN(''+@strFlashID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_CheckUserLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[SocoShop_CheckUserLogin]
@loginName nvarchar(100),
@loginPass nvarchar(100)
AS
SELECT [ID],[Status] FROM SocoShop_User WHERE [UserName]=@loginName AND [UserPassword]=@loginPass


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteStandardRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteStandardRecord]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_StandardRecord WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateUserLogin]
@id int,
@lastLoginDate datetime,
@lastLoginIP nvarchar(40)
AS 
	UPDATE SocoShop_User SET [LastLoginDate]=@lastLoginDate,[LastLoginIP]=@lastLoginIP,[LoginTimes]=[LoginTimes]+1 WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdminLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteAdminLog]
@strID nvarchar(800),
@adminID int
AS 
	IF @strID=''''
		RETURN	
	IF @adminID=0
		EXEC (''DELETE FROM SocoShop_AdminLog WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_AdminLog WHERE [ID] IN(''+@strID+'') AND [AdminID]=''+@adminID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductClassAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductClassAllList]
AS
	SELECT [ID],[FatherID],[OrderID],[ClassName],[Keywords],[Description],[TaobaoID] FROM SocoShop_ProductClass ORDER BY [OrderID] ASC,ID ASC
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserMessage]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_UserMessage WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_UserMessage WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ResetUserPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ResetUserPassword]
@id int,
@userPassword nvarchar(50)
AS
   UPDATE SocoShop_USER SET [UserPassword]=@userPassword WHERE [ID]=@id' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteProductComment]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_ProductComment WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_ProductComment WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserMessageByUserID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteUserMessageByUserID]
@strUserID nvarchar(800)
AS 
	IF @strUserID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_UserMessage WHERE [UserID] IN(''+@strUserID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteStandardRecordByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_DeleteStandardRecordByProductID]
@strProductID nvarchar(800)
AS 
	IF @strProductID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_StandardRecord WHERE [ProductID] IN(''+@strProductID+'') AND len([GroupTag])=0'')	
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	DECLARE @id nvarchar(20)
	SET @seprate='',''
	SET @strList=@strProductID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				SET @id=LEFT(@strList,@i-1)
				EXEC (''DELETE FROM SocoShop_StandardRecord WHERE '''',''''+[GroupTag]+'''','''' LIKE ''''%,''+@id+'',%'''''')	
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--drop proc SocoShop_UpdateUserPersonal
CREATE PROCEDURE [dbo].[SocoShop_ReadUserPassword]
@id int,
@password nvarchar(50)
AS 
		SET NOCOUNT ON;
		SELECT [ID] FROM SocoShop_User WHERE [ID]=@id AND [UserPassword] = @password
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAttributeRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteAttributeRecord]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_AttributeRecord WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserMessageIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserMessageIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_UserMessage WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUploadByRecordID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUploadByRecordID]
@tableID int,
@strRecordID nvarchar(800)
AS 
	IF @strRecordID=''''
		RETURN	
	EXEC (''SELECT [ID],[TableID],[ClassID],[RecordID],[UploadName],[OtherFile],[Size],[FileType],[RandomNumber],[Date],[IP] FROM SocoShop_Upload WHERE [TableID]=''+@tableID+'' AND [RecordID] IN(''+@strRecordID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductCommentByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteProductCommentByProductID]
@strProductID nvarchar(800)
AS 
	IF @strProductID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ProductComment WHERE [ProductID] IN(''+@strProductID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserApply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserApply]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_UserApply WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_UserApply WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUploadByClassID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteUploadByClassID]
@tableID int,
@strClassID nvarchar(800)
AS 
	IF @strClassID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Upload WHERE [TableID]=''+@tableID+'' AND [ClassID] IN(''+@strClassID +'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductCommentIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductCommentIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_ProductComment WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_CheckUserName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_CheckUserName]
@userName nvarchar(100)
AS 
   SELECT [ID] FROM  SocoShop_User WHERE [UserName] =@userName

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserAccountRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserAccountRecord]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_UserAccountRecord WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_UserAccountRecord WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserRecharge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserRecharge]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_UserRecharge WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_UserRecharge WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadPageList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadPageList]
@tableName varchar(2000),--多表的话用  Table1 Inner JOIN Table2 ON Table1.Field=Table2.Field
@fields varchar(2000),
@pageSize int, 
@currentPage int,   
@fieldName nvarchar(50), 
@orderType bit,  -- 设置排序类型, 0降序  1升序
@condition varchar(max) -- 查询条件 (注意: 不要加 where) 
AS 
	DECLARE @strSQL varchar(max) 
	DECLARE @strTmp varchar(110)
	DECLARE @strOrder varchar(400) 
	DECLARE @whereCondition varchar(max)
	DECLARE @andCondition varchar(max)
	--条件处理
	IF @condition=''''
		BEGIN
			SET @whereCondition=''''
			SET @andCondition=''''
		END
	ELSE
		BEGIN
			SET @whereCondition='' WHERE ''+@condition
			SET @andCondition='' AND ''+@condition
		END
		
	--多表连接字段处理
	DECLARE @noPrefixFieldName nvarchar(50)
    IF CHARINDEX(''.'',@fieldName)>0
		SET @noPrefixFieldName=SUBSTRING(@fieldName,CHARINDEX(''.'',@fieldName)+1,LEN(@fieldName))
	ELSE
		SET @noPrefixFieldName=@fieldName
	--排序处理
	IF @orderType = 0 
		BEGIN 
			SET @strTmp = ''< (SELECT MIN'' 
			SET @strOrder = '' ORDER BY '' + REPLACE(@fieldName,'','','' DESC ,'') +'' DESC''
		END 
	ELSE 
		BEGIN 
			SET @strTmp = ''> (SELECT MAX''
			SET @strOrder = '' ORDER BY '' + REPLACE(@fieldName,'','','' ASC ,'') +'' ASC''
		END 
	--分页	
	IF CHARINDEX('','',@fieldName)=0
		BEGIN		
			IF @currentPage = 1 
				SET @strSQL = ''SELECT TOP '' + CAST(@pageSize AS nvarchar(10)) +'' ''+@fields+ '' FROM '' +@tableName + @whereCondition + '' '' + @strOrder 

			ELSE 
				SET @strSQL = ''SELECT TOP '' + CAST(@pageSize AS nvarchar(10)) +'' ''+@fields+ '' FROM ''+ @tableName + '' WHERE '' + @fieldName + @strTmp + ''('' + @noPrefixFieldName + '') FROM (SELECT TOP '' + CAST((@currentPage-1)*@pageSize AS nvarchar(10)) +'' ''+ @fieldName + '' FROM '' + @tableName + @whereCondition+ @strOrder + '') AS tblTmp) '' + @andCondition + '' '' + @strOrder 
		END
	ELSE
		BEGIN
			IF @currentPage = 1 
				SET @strSQL = ''SELECT TOP '' + CAST(@pageSize AS nvarchar(10)) +'' ''+@fields+ '' FROM '' +@tableName + @whereCondition + @strOrder
			ELSE 
				SET @strSQL = ''SELECT ''+@fields + '' FROM (SELECT ROW_NUMBER() OVER(''+@strOrder+'' ) AS [RowID],''+@fields+'' FROM(SELECT ''+@fields+'' FROM ''+@tableName+@whereCondition+'' ) AS TEMP1)AS TEMP WHERE [RowID] BETWEEN ''+CAST((@currentPage-1)*@pageSize+1 AS nvarchar(10))+'' AND ''+CAST(@currentPage*@pageSize AS nvarchar(10))
		END
	EXEC (@strSQL)









' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteArticle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteArticle]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Article WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUploadByClassID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUploadByClassID]
@tableID int,
@strClassID nvarchar(800)
AS 
	IF @strClassID=''''
		RETURN	
	EXEC (''SELECT [ID],[TableID],[ClassID],[RecordID],[UploadName],[OtherFile],[Size],[FileType],[RandomNumber],[Date],[IP] FROM SocoShop_Upload WHERE [TableID]=''+@tableID+'' AND [ClassID] IN(''+@strClassID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserRechargeIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserRechargeIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_UserRecharge WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrderFavorableProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[SocoShop_DeleteOrderFavorableProduct]
@id int
AS 	
	DELETE FROM SocoShop_OrderDetail WHERE [ID] =id


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAttributeRecordByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteAttributeRecordByProductID]
@strProductID nvarchar(800)
AS 
	IF @strProductID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_AttributeRecord WHERE [ProductID] IN(''+@strProductID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductBrand]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteProductBrand]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ProductBrand WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserCoupon]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_UserCoupon WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_UserCoupon WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductReply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteProductReply]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_ProductReply WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_ProductReply WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadMemberPriceByStrProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadMemberPriceByStrProduct]
@strProductID nvarchar(4000)
AS
	IF @strProductID=''''
		RETURN
	EXEC(''SELECT [ProductID],[GradeID],[Price] FROM SocoShop_MemberPrice WHERE [ProductID] IN(''+@strProductID+'')'')


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCollectCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCollectCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_Product SET [CollectCount]=[CollectCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_Product SET [CollectCount]=[CollectCount]-1 WHERE [ID] = @id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteShipping]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteShipping]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Shipping WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UnionUpdateProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE  PROCEDURE [dbo].[SocoShop_UnionUpdateProduct]
@productIDList nvarchar(4000),
@marketPrice decimal(18,2),
@weight int,
@sendPoint int,
@totalStorageCount int,
@lowerCount int,
@upperCount int
AS 
	IF @productIDList=''''
		RETURN
	DECLARE @sql nvarchar(4000)	
    SET @sql=''UPDATE SocoShop_Product Set Name=[Name] ''
	IF @marketPrice!=-2
		SET @sql=@sql+'', [MarketPrice]=''+Cast(@marketPrice AS nvarchar(50))
	IF @weight!=-2
		SET @sql=@sql+'', [Weight]=''+ Cast(@weight AS nvarchar(50))
	IF @sendPoint!=-2
		SET @sql=@sql+'', [SendPoint]=''+ Cast(@sendPoint AS nvarchar(50))
	IF @totalStorageCount!=-2
		SET @sql=@sql+'', [TotalStorageCount]=''+ Cast(@totalStorageCount AS nvarchar(50))
	IF @lowerCount!=-2
		SET @sql=@sql+'', [LowerCount]=''+ Cast(@lowerCount AS nvarchar(50))
	IF @upperCount!=-2
		SET @sql=@sql+'', [UpperCount]=''+ Cast(@upperCount AS nvarchar(50))
	SET @sql=@sql+''  WHERE [ID]IN (''+@productIDList+'')''
	EXEC (@sql)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserCouponByCouponID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteUserCouponByCouponID]
@strCouponID nvarchar(800)
AS 
	IF @strCouponID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_UserCoupon WHERE [CouponID] IN(''+@strCouponID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserGradeName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SocoShop_ReadUserGradeName]
@strID nvarchar(10)
AS
	SET NOCOUNT ON;
	EXEC(''SELECT [Name] FROM SocoShop_UserGrade WHERE [ID] IN (''+@strID+'') ORDER BY [ID]'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserGrade]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUserGrade]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_UserGrade WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StatisticsSaleStop]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_StatisticsSaleStop]	
@productIDList varchar(800)
AS 
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	CREATE TABLE #temp(		
		[OrderNumber] [nvarchar](50) ,
		[ProductID] [int] ,
		[BuyCount] [int] ,
		[AddDate] [DateTime] ,
	) 
	IF SUBSTRING(@productIDList,LEN(@productIDList),1)!='',''
		SET @productIDList=@productIDList+'',''
	DECLARE @i int
	SET @productIDList=RTRIM(LTRIM(@productIDList))
	SET @i=CHARINDEX(@seprate,@productIDList)
	WHILE @i>=1
		BEGIN
			INSERT INTO #temp([OrderNumber],[ProductID],[BuyCount],[AddDate] ) SELECT TOP 1 [OrderNumber],[ProductID],[BuyCount],[AddDate]  FROM SocoShop_Order INNER JOIN  SocoShop_OrderDetail ON SocoShop_Order.[ID]=SocoShop_OrderDetail.[OrderID] WHERE ProductID=CONVERT(int,LEFT(@productIDList,@i-1)) Order BY AddDate DESC
			SET @productIDList=SUBSTRING(@productIDList,@i+1,LEN(@productIDList)-@i)
			SET @i=CHARINDEX(@seprate,@productIDList)
		END
	SELECT * FROM #temp
	DROP TABLE #temp

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadArticleListByArticleID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SocoShop_ReadArticleListByArticleID]
@strArticleID NVARCHAR(200)
AS
EXEC(''SELECT  [ID],[Title]  FROM  [SocoShop_Article] WHERE [ID] IN(''+@strArticleID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadMemberPriceByProductGrade]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadMemberPriceByProductGrade]
@strProductID nvarchar(4000),
@gradeID int
AS
	IF @strProductID=''''
		RETURN
	EXEC(''SELECT [ProductID],[GradeID],[Price] FROM SocoShop_MemberPrice WHERE [ProductID] IN(''+@strProductID+'') AND [GradeID]=''+@gradeID)



' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadFlashJsNameList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadFlashJsNameList]
@strID nvarchar(100),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''SELECT FileName FROM SocoShop_Flash WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''SELECT FileName FROM SocoShop_Flash WHERE [ID] IN(''+@strID+'') AND [AdminID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProduct]
@id int,
@name nvarchar(500),
@spelling nvarchar(1000),
@color nvarchar(50),
@fontStyle nvarchar(50),
@productNumber nvarchar(50),
@classID nvarchar(500),
@brandID int,
@marketPrice decimal(18,2),
@weight int,
@sendPoint int,
@photo nvarchar(100),
@keywords nvarchar(200),
@summary ntext,
@introduction ntext,
@remark ntext,
@isSpecial int,
@isNew int,
@isHot int,
@isSale int,
@isTop int,
@accessory nvarchar(500),
@relationProduct nvarchar(500),
@relationArticle nvarchar(500),
@allowComment int,
@totalStorageCount int,
@lowerCount int,
@upperCount int,
@attributeClassID int,
@standardType int
AS 
	UPDATE SocoShop_Product Set [Name]=@name,[Spelling]=@spelling,[Color]=@color,[FontStyle]=@fontStyle,[ProductNumber]=@productNumber,[ClassID]=@classID,[BrandID]=@brandID,[MarketPrice]=@marketPrice,[Weight]=@weight,[SendPoint]=@sendPoint,[Photo]=@photo,[Keywords]=@keywords,[Summary]=@summary,[Introduction]=@introduction,[Remark]=@remark,[IsSpecial]=@isSpecial,[IsNew]=@isNew,[IsHot]=@isHot,[IsSale]=@isSale,[IsTop]=@isTop,[Accessory]=@accessory,[RelationProduct]=@relationProduct,[RelationArticle]=@relationArticle,[AllowComment]=@allowComment,[TotalStorageCount]=@totalStorageCount,[LowerCount]=@lowerCount,[UpperCount]=@upperCount,[AttributeClassID]=@attributeClassID,[StandardType]=@standardType WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteThemeActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteThemeActivity]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ThemeActivity WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteVoteRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteVoteRecord]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_VoteRecord WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadShippingRegionByShipping]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadShippingRegionByShipping]
@strShippingID nvarchar(200)
AS
IF @strShippingID=''''
	RETURN
EXEC(''SELECT [ID],[Name],[ShippingID],[RegionID],[FixedMoeny],[FirstMoney],[AgainMoney],[OneMoeny],[AnotherMoeny] FROM SocoShop_ShippingRegion WHERE [ShippingID] IN(''+@strShippingID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdminLogByAdminID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteAdminLogByAdminID]
@strAdminID nvarchar(800)
AS 
	IF @strAdminID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_AdminLog WHERE [AdminID] IN(''+@strAdminID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteVoteRecordByItemID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteVoteRecordByItemID]
@strItemID nvarchar(800)
AS 
	IF @strItemID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_VoteRecord WHERE [ItemID] IN(''+@strItemID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteTags]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteTags]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_Tags WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_Tags WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteEmailSendRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteEmailSendRecord]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_EmailSendRecord WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeOrderDetailOrderID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeOrderDetailOrderID]
@oldOrderID int,
@newOrderID int
AS 
	UPDATE SocoShop_OrderDetail SET [OrderID]=@newOrderID WHERE [OrderID]=@oldOrderID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteTagsByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteTagsByProductID]
@strProductID nvarchar(800)
AS 
	IF @strProductID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Tags WHERE [ProductID] IN(''+@strProductID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProductClass]
@id int,
@fatherID int,
@orderID int,
@className nvarchar(50),
@keywords nvarchar(200),
@description ntext
AS 	
	UPDATE SocoShop_ProductClass SET [FatherID]=@FatherID,[OrderID]=@orderID,[ClassName]=@ClassName,[Keywords]=@Keywords, [Description]=@Description WHERE [ID]=@id   

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUserApplyByUserID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteUserApplyByUserID]
@strUserID nvarchar(800)
AS 
	IF @strUserID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_UserApply WHERE [UserID] IN(''+@strUserID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteVoteRecordByVoteID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteVoteRecordByVoteID]
@strVoteID nvarchar(800)
AS 
	IF @strVoteID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_VoteRecord WHERE [VoteID] IN(''+@strVoteID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadTagsIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadTagsIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_Tags WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrderGiftPack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[SocoShop_DeleteOrderGiftPack]
@orderID int,
@randNumber nvarchar(800)
AS 
	DELETE FROM SocoShop_OrderDetail WHERE [OrderID]=@orderID AND [RandNumber]=@randNumber


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteFavorableActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteFavorableActivity]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_FavorableActivity WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteProduct]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Product WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeOrderGiftPackBuyCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROCEDURE [dbo].[SocoShop_ChangeOrderGiftPackBuyCount]
@orderID int,
@randNumber nvarchar(800),
@buyCount int
AS 
	UPDATE SocoShop_OrderDetail SET [BuyCount]=@buyCount WHERE [OrderID]=@orderID  AND [RandNumber]=@randNumber



' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCommentAgainstCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCommentAgainstCount]
@strID nvarchar(1000),
@action nvarchar(10)
AS 
	IF @strID=''''
		RETURN
	IF @action=''Plus''
		EXEC(''UPDATE SocoShop_ProductComment SET Against=Against+1 WHERE [ID] IN(''+ @strID+'')'')
	ELSE
		EXEC(''UPDATE SocoShop_ProductComment SET Against=Against-1 WHERE [ID] IN(''+ @strID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCommentSupportCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCommentSupportCount]
@strID nvarchar(1000),
@action nvarchar(10)
AS 
	IF @strID=''''
		RETURN
	IF @action=''Plus''
		EXEC(''UPDATE SocoShop_ProductComment SET Support=Support+1 WHERE [ID] IN(''+ @strID+'')'')
	ELSE
		EXEC(''UPDATE SocoShop_ProductComment SET Support=Support-1 WHERE [ID] IN(''+ @strID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteUser]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_User WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchProductList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchProductList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Name],[Spelling],[Color],[FontStyle],[ProductNumber],[ClassID],[BrandID],[MarketPrice],[Weight],[SendPoint],[Photo],[Keywords],[Summary],[IsSpecial],[IsNew],[IsHot],[IsSale],[IsTop],[Accessory],[RelationProduct],[RelationArticle],[ViewCount],[AllowComment],[CommentCount],[SumPoint],[PerPoint],[PhotoCount],[CollectCount],[TotalStorageCount],[OrderCount],[SendCount],[ImportActualStorageCount],[ImportVirtualStorageCount],[LowerCount],[UpperCount],[AttributeClassID],[StandardType],[AddDate] FROM SocoShop_Product 
	ELSE
		EXEC(''SELECT [ID],[Name],[Spelling],[Color],[FontStyle],[ProductNumber],[ClassID],[BrandID],[MarketPrice],[Weight],[SendPoint],[Photo],[Keywords],[Summary],[IsSpecial],[IsNew],[IsHot],[IsSale],[IsTop],[Accessory],[RelationProduct],[RelationArticle],[ViewCount],[AllowComment],[CommentCount],[SumPoint],[PerPoint],[PhotoCount],[CollectCount],[TotalStorageCount],[OrderCount],[SendCount],[ImportActualStorageCount],[ImportVirtualStorageCount],[LowerCount],[UpperCount],[AttributeClassID],[StandardType],[AddDate] FROM SocoShop_Product WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangProductIsNew]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangProductIsNew]
@id int,
@status int
AS
	UPDATE SocoShop_Product SET [IsNew]=@status WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchEmailContentList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchEmailContentList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Title] FROM SocoShop_EmailContent 
	ELSE
		EXEC(''SELECT [ID],[Title] FROM SocoShop_EmailContent WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangProductIsTop]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangProductIsTop]
@id int,
@status int
AS
	UPDATE SocoShop_Product SET [IsTop]=@status WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteAd]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Ad WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangProductIsSale]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangProductIsSale]
@id int,
@status int
AS
	UPDATE SocoShop_Product SET [IsSale]=@status WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductReplyByCommentID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteProductReplyByCommentID]
@strCommentID nvarchar(800)
AS 
	IF @strCommentID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ProductReply WHERE [CommentID] IN(''+@strCommentID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangProductIsHot]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangProductIsHot]
@id int,
@status int
AS
	UPDATE SocoShop_Product SET [IsHot]=@status WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteGiftPack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteGiftPack]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_GiftPack WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeUserSafeCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeUserSafeCode]
@id int,
@safeCode nvarchar(200),
@findDate datetime
AS 
	UPDATE SocoShop_User set SafeCode=@safeCode,FindDate=@findDate WHERE [ID]=@id' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeTagsStatu]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeTagsStatu]
@strID nvarchar(500),
@toStatus int,
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC(''UPDATE SocoShop_Tags SET [IsTop]=''+ @toStatus +'' WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC(''UPDATE SocoShop_Tags SET [IsTop]=''+ @toStatus +'' WHERE [ID] IN(''+@strID+'') AND [AdminID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_OnSaleProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_OnSaleProduct]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''UPDATE SocoShop_Product SET [IsSale]=1 WHERE [ID] IN(''+@strID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_CheckEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_CheckEmail]
@email nvarchar(100)
AS 
   SELECT [ID] FROM  SocoShop_User WHERE [Email] =@email

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrderCommonProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[SocoShop_DeleteOrderCommonProduct]
@id int,
@productID int
AS 
	DELETE FROM SocoShop_OrderDetail WHERE [FatherID]=@productID AND [OrderID]=(SELECT [OrderID] FROM SocoShop_OrderDetail WHERE [ID]=@id)
	DELETE FROM SocoShop_OrderDetail WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductCollectByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_DeleteProductCollectByProductID]
@strProductID nvarchar(200),
@userID int
AS 
	IF @strProductID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_ProductCollect WHERE [ProductID] IN(''+@strProductID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_ProductCollect WHERE [ProductID] IN(''+@strProductID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdminGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteAdminGroup]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_AdminGroup WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_DeleteAdRecord]
@strID nvarchar(100),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_AdRecord WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_AdRecord WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ExistFavorableActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SocoShop_ExistFavorableActivity]
@name nvarchar(100)
AS
	SET NOCOUNT ON;
	SELECT [ID] FROM SocoShop_FavorableActivity WHERE [Name] = @name
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProduct]
@id int
AS 
	SELECT [ID],[Name],[Spelling],[Color],[FontStyle],[ProductNumber],[ClassID],[BrandID],[MarketPrice],[Weight],[SendPoint],[Photo],[Keywords],[Summary],[Introduction],[Remark],[IsSpecial],[IsNew],[IsHot],[IsSale],[IsTop],[Accessory],[RelationProduct],[RelationArticle],[ViewCount],[AllowComment],[CommentCount],[SumPoint],[PerPoint],[PhotoCount],[CollectCount],[TotalStorageCount],[OrderCount],[SendCount],[ImportActualStorageCount],[ImportVirtualStorageCount],[LowerCount],[UpperCount],[AttributeClassID],[StandardType],[AddDate] FROM SocoShop_Product WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangProductAllowComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangProductAllowComment]
@id int,
@status int
AS
	UPDATE SocoShop_Product SET [AllowComment]=@status WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAdminLogByGroupID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteAdminLogByGroupID]
@strGroupID nvarchar(800)
AS 
	IF @strGroupID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_AdminLog WHERE [GroupID] IN(''+@strGroupID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_OffSaleProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_OffSaleProduct]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''UPDATE SocoShop_Product SET [IsSale]=0 WHERE [ID] IN(''+@strID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeUserStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_ChangeUserStatus]
@strID nvarchar(800),
@status int 
AS 
	IF @strID=''''
		RETURN	
	EXEC (''UPDATE SocoShop_User SET [Status]=''+@status+'' WHERE [ID] IN(''+@strID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAttributeClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteAttributeClass]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_AttributeClass WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteBookingProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteBookingProduct]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_BookingProduct WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_BookingProduct WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCommentStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCommentStatus]
@strID nvarchar(100),
@status int
AS 
	IF @strID=''''
		RETURN		
	EXEC (''UPDATE SocoShop_ProductComment SET [Status]=''+@status+'' WHERE [ID] IN (''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangProductIsSpecial]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangProductIsSpecial]
@id int,
@status int
AS
	UPDATE SocoShop_Product SET IsSpecial=@status WHERE [ID]=@id


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteGift]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteGift]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Gift WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadBookingProductIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadBookingProductIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_BookingProduct WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductOrderCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create PROCEDURE [dbo].[SocoShop_ChangeProductOrderCount]
@strProductID nvarchar(200),
@changeCount int
AS 
	IF @strProductID=''''
		RETURN
    EXEC(''UPDATE SocoShop_Product SET [OrderCount]=[OrderCount]+''+@changeCount+'' WHERE [ID] IN(''+ @strProductID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeUserProductCollectCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeUserProductCollectCount]
@strID nvarchar(100),
@action nvarchar(10)
AS 
	IF @strID=''''
		RETURN	
	BEGIN
	IF @action=''Plus''
		EXEC (''UPDATE SocoShop_User SET [ProductCollect]=[ProductCollect]+1 WHERE [ID] IN (''+@strID+'')'')
	ELSE
		EXEC (''UPDATE SocoShop_User SET [ProductCollect]=[ProductCollect]-1 WHERE [ID] IN (''+@strID+'')'') 
	END' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateBookingProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateBookingProduct]
@id int,
@isHandler int,
@handlerDate datetime,
@handlerAdminID int,
@handlerAdminName nvarchar(50),
@handlerNote nvarchar(100)
AS 
	UPDATE SocoShop_BookingProduct Set [IsHandler]=@isHandler,[HandlerDate]=@handlerDate,[HandlerAdminID]=@handlerAdminID,[HandlerAdminName]=@handlerAdminName,[HandlerNote]=@handlerNote WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteVoteItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteVoteItem]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_VoteItem WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAttribute]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteAttribute]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Attribute WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductViewCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductViewCount]
@productID int,
@changeCount int 
AS 
	UPDATE SocoShop_Product SET [ViewCount]=[ViewCount]+@changeCount WHERE [ID] = @productID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveUpProductClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_MoveUpProductClass]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_ProductClass WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_ProductClass WHERE [OrderID]<@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] DESC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_ProductClass SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_ProductClass SET [OrderID]=@orderID WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddOrder]
@orderNumber nvarchar(50),
@isActivity int,
@orderStatus int,
@orderNote nvarchar(50),
@productMoney decimal(15,2),
@balance decimal(15,2),
@favorableMoney decimal(15,2),
@otherMoney decimal(15,2),
@couponMoney decimal(15,2),
@consignee nvarchar(50),
@regionID nvarchar(50),
@address nvarchar(100),
@zipCode nvarchar(50),
@tel nvarchar(50),
@email nvarchar(50),
@mobile nvarchar(50),
@shippingID int,
@shippingDate datetime,
@shippingNumber nvarchar(50),
@shippingMoney decimal(15,2),
@payKey nvarchar(50),
@payName nvarchar(50),
@payDate datetime,
@isRefund int,
@favorableActivityID int,
@giftID int,
@invoiceTitle nvarchar(100),
@invoiceContent nvarchar(200),
@userMessage nvarchar(500),
@addDate datetime,
@iP nvarchar(40),
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_Order([OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName]) VALUES(@orderNumber,@isActivity,@orderStatus,@orderNote,@productMoney,@balance,@favorableMoney,@otherMoney,@couponMoney,@consignee,@regionID,@address,@zipCode,@tel,@email,@mobile,@shippingID,@shippingDate,@shippingNumber,@shippingMoney,@payKey,@payName,@payDate,@isRefund,@favorableActivityID,@giftID,@invoiceTitle,@invoiceContent,@userMessage,@addDate,@iP,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveDownProductClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_MoveDownProductClass]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_ProductClass WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_ProductClass WHERE [OrderID]>@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] ASC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_ProductClass SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_ProductClass SET [OrderID]=@orderID WHERE [ID]=@tempID
		END' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCommentCountAndRank]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCommentCountAndRank]
@id int,
@rank int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_Product SET [CommentCount]=[CommentCount]+1,[SumPoint]=[SumPoint]+@rank WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_Product SET [CommentCount]=[CommentCount]-1,[SumPoint]=[SumPoint]-@rank WHERE [ID] = @id
	UPDATE SocoShop_Product SET [PerPoint]=(CASE [CommentCount] WHEN 0 THEN 0 ELSE CAST([SumPoint] as decimal(15,2))/CAST([CommentCount]  as decimal(15,2)) END) WHERE [ID] = @id

		' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateOrder]
@id int,
@orderStatus int,
@orderNote nvarchar(50),
@productMoney decimal(15,2),
@balance decimal(15,2),
@favorableMoney decimal(15,2),
@otherMoney decimal(15,2),
@couponMoney decimal(15,2),
@consignee nvarchar(50),
@regionID nvarchar(50),
@address nvarchar(100),
@zipCode nvarchar(50),
@tel nvarchar(50),
@email nvarchar(50),
@mobile nvarchar(50),
@shippingID int,
@shippingDate datetime,
@shippingNumber nvarchar(50),
@shippingMoney decimal(15,2),
@payKey nvarchar(50),
@payName nvarchar(50),
@payDate datetime,
@isRefund int,
@favorableActivityID int,
@giftID int,
@invoiceTitle nvarchar(100),
@invoiceContent nvarchar(200),
@userMessage nvarchar(500)
AS 
	UPDATE SocoShop_Order Set [OrderStatus]=@orderStatus,[OrderNote]=@orderNote,[ProductMoney]=@productMoney,[Balance]=@balance,[FavorableMoney]=@favorableMoney,[OtherMoney]=@otherMoney,[CouponMoney]=@couponMoney,[Consignee]=@consignee,[RegionID]=@regionID,[Address]=@address,[ZipCode]=@zipCode,[Tel]=@tel,[Email]=@email,[Mobile]=@mobile,[ShippingID]=@shippingID,[ShippingDate]=@shippingDate,[ShippingNumber]=@shippingNumber,[ShippingMoney]=@shippingMoney,[PayKey]=@payKey,[PayName]=@payName,[PayDate]=@payDate,[IsRefund]=@isRefund,[FavorableActivityID]=@favorableActivityID,[GiftID]=@giftID,[InvoiceTitle]=@invoiceTitle,[InvoiceContent]=@invoiceContent,[UserMessage]=@userMessage WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductPhotoByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteProductPhotoByProductID]
@strProductID nvarchar(800)
AS 
	IF @strProductID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ProductPhoto WHERE [ProductID] IN(''+@strProductID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteOrder]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_Order WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_Order WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteAttributeByAttributeClassID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteAttributeByAttributeClassID]
@strAttributeClassID nvarchar(800)
AS 
	IF @strAttributeClassID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Attribute WHERE [AttributeClassID] IN(''+@strAttributeClassID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadOrder]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName] FROM SocoShop_Order WHERE [ID]=@id
	ELSE
		SELECT [ID],[OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName] FROM SocoShop_Order WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrderIDList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadOrderIDList]
@strID nvarchar(400),
@userID int
AS
	IF @strID=''''
		RETURN	
	EXEC(''SELECT [ID] FROM SocoShop_Order WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteVoteItemByVoteID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteVoteItemByVoteID]
@strVoteID nvarchar(800)
AS 
	IF @strVoteID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_VoteItem WHERE [VoteID] IN(''+@strVoteID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchOrderList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchOrderList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName] FROM SocoShop_Order 
	ELSE
		EXEC(''SELECT [ID],[OrderNumber],[IsActivity],[OrderStatus],[OrderNote],[ProductMoney],[Balance],[FavorableMoney],[OtherMoney],[CouponMoney],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Email],[Mobile],[ShippingID],[ShippingDate],[ShippingNumber],[ShippingMoney],[PayKey],[PayName],[PayDate],[IsRefund],[FavorableActivityID],[GiftID],[InvoiceTitle],[InvoiceContent],[UserMessage],[AddDate],[IP],[UserID],[UserName] FROM SocoShop_Order WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteVote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteVote]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_Vote WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteShippingRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteShippingRegion]
@strID nvarchar(800)
AS 
	IF @strID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ShippingRegion WHERE [ID] IN(''+@strID+'')'')
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteProductCollect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_DeleteProductCollect]
@strID nvarchar(800),
@userID int
AS 
	IF @strID=''''
		RETURN	
	IF @userID=0
		EXEC (''DELETE FROM SocoShop_ProductCollect WHERE [ID] IN(''+@strID+'')'')
	ELSE
		EXEC (''DELETE FROM SocoShop_ProductCollect WHERE [ID] IN(''+@strID+'') AND [UserID]=''+@userID)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteShippingRegionByShippingID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteShippingRegionByShippingID]
@strShippingID nvarchar(800)
AS 
	IF @strShippingID=''''
		RETURN	
	EXEC (''DELETE FROM SocoShop_ShippingRegion WHERE [ShippingID] IN(''+@strShippingID+'')'')

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteMenu]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteMenu]
@id int
AS 
	 DECLARE @temp int
	 SELECT @temp=COUNT(*) FROM SocoShop_Menu WHERE [FatherID]=@id 
	 IF @temp=0
	 	DELETE FROM SocoShop_Menu WHERE [ID]=@id
		
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveDownMenu]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_MoveDownMenu]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_Menu WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_Menu WHERE [OrderID]>@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] ASC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_Menu SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_Menu SET [OrderID]=@orderID WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveUpMenu]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_MoveUpMenu]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_Menu WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_Menu WHERE [OrderID]<@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] DESC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_Menu SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_Menu SET [OrderID]=@orderID WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadMenuAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadMenuAllList]
AS
	SELECT [ID],[FatherID],[OrderID],[MenuName],[MenuImage],[URL],[Date],[IP] FROM SocoShop_Menu ORDER BY [OrderID] ASC,ID ASC
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateMenu]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateMenu]
@id int,
@fatherID int,
@orderID int,
@menuName nvarchar(50),
@menuImage int,
@uRL nvarchar(50)
AS 
	UPDATE SocoShop_Menu Set [FatherID]=@fatherID,[OrderID]=@orderID,[MenuName]=@menuName,[MenuImage]=@menuImage,[URL]=@uRL WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddMenu]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddMenu]
@fatherID int,
@orderID int,
@menuName nvarchar(50),
@menuImage int,
@uRL nvarchar(50),
@date datetime,
@iP nvarchar(50)
AS 
	INSERT INTO SocoShop_Menu([FatherID],[OrderID],[MenuName],[MenuImage],[URL],[Date],[IP]) VALUES(@fatherID,@orderID,@menuName,@menuImage,@uRL,@date,@iP)	
		SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductBrandCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeProductBrandCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_ProductBrand SET [ProductCount]=[ProductCount]+1 WHERE [ID] IN (SELECT [BrandID] FROM SocoShop_Product WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_ProductBrand SET [ProductCount]=[ProductCount]-1 WHERE [ID] IN (SELECT [BrandID] FROM SocoShop_Product WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductBrandCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeProductBrandCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_ProductBrand SET [ProductCount]=[ProductCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_ProductBrand SET [ProductCount]=[ProductCount]-1 WHERE [ID] = @id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductBrandOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeProductBrandOrder]
@action nvarchar(100),
@id int
AS 
	DECLARE @tempID int
	SET @tempID= 0
	DECLARE @tempOrder int
	SET @tempOrder= 0
	DECLARE @needChange bit 
	SET @needChange=0
	DECLARE @orderID int
	SET @orderID=0
	SELECT @orderID=[OrderID] FROM SocoShop_ProductBrand WHERE [ID]= @id
	IF @orderID=0
		RETURN			
	IF @action = ''Up''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_ProductBrand WHERE [OrderID]<@orderID ORDER BY [OrderID] DESC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @action = ''Down''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_ProductBrand WHERE [OrderID]>@orderID ORDER BY [OrderID] ASC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @needChange=1
		BEGIN
		UPDATE SocoShop_ProductBrand SET [OrderID]=@tempOrder  WHERE [ID]=@id
		UPDATE SocoShop_ProductBrand SET [OrderID]=@orderID  WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductBrand]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProductBrand]
@id int,
@name nvarchar(100),
@logo nvarchar(100),
@url nvarchar(200),
@description ntext,
@isTop int
AS 
	UPDATE SocoShop_ProductBrand Set [Name]=@name,[Logo]=@logo,[Url]=@url,[Description]=@description,[IsTop]=@isTop WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductBrandAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductBrandAllList]
AS
	SELECT [ID],[Name],[Logo],[Url],[Description],[OrderID],[IsTop],[ProductCount] FROM SocoShop_ProductBrand ORDER BY [OrderID]
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddProductBrand]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddProductBrand]
@name nvarchar(100),
@logo nvarchar(100),
@url nvarchar(200),
@description ntext, 
@orderID int, 
@isTop int, 
@productCount int
AS 
	DECLARE @maxID int
	SELECT @maxID=MAX([OrderID]) FROM SocoShop_ProductBrand
	IF @maxID IS NULL	 
		SET @orderID= 1
        ELSE
		SET @orderID= @maxID+ 1
	INSERT INTO SocoShop_ProductBrand([Name],[Logo],[Url],[Description],[OrderID],[IsTop],[ProductCount]) VALUES(@name,@logo,@url,@description,@orderID,@isTop,@productCount)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAttribute]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateAttribute]
@id int,
@name nvarchar(50),
@attributeClassID int,
@inputType int,
@inputValue nvarchar(200)
AS 
	UPDATE SocoShop_Attribute Set [Name]=@name,[AttributeClassID]=@attributeClassID,[InputType]=@inputType,[InputValue]=@inputValue WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAttribute]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAttribute]
@name nvarchar(50),
@attributeClassID int, 
@inputType int, 
@inputValue nvarchar(200),
@orderID int
AS 
	DECLARE @maxID int
	SELECT @maxID=MAX([OrderID]) FROM SocoShop_Attribute
	IF @maxID IS NULL	 
		SET @orderID= 1
        ELSE
		SET @orderID= @maxID+ 1
	INSERT INTO SocoShop_Attribute([Name],[AttributeClassID],[InputType],[InputValue],[OrderID]) VALUES(@name,@attributeClassID,@inputType,@inputValue,@orderID)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAttributeOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeAttributeOrder]
@action nvarchar(100),
@id int
AS 
	DECLARE @tempID int
	SET @tempID= 0
	DECLARE @tempOrder int
	SET @tempOrder= 0
	DECLARE @needChange bit 
	SET @needChange=0
	DECLARE @orderID int
	SET @orderID=0
	SELECT @orderID=[OrderID] FROM SocoShop_Attribute WHERE [ID]= @id
	IF @orderID=0
		RETURN			
	IF @action = ''Up''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_Attribute WHERE [OrderID]<@orderID ORDER BY [OrderID] DESC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @action = ''Down''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_Attribute WHERE [OrderID]>@orderID ORDER BY [OrderID] ASC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @needChange=1
		BEGIN
		UPDATE SocoShop_Attribute SET [OrderID]=@tempOrder  WHERE [ID]=@id
		UPDATE SocoShop_Attribute SET [OrderID]=@orderID  WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAttributeClassCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeAttributeClassCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_AttributeClass SET [AttributeCount]=[AttributeCount]+1 WHERE [ID] IN (SELECT [AttributeClassID] FROM SocoShop_Attribute WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_AttributeClass SET [AttributeCount]=[AttributeCount]-1 WHERE [ID] IN (SELECT [AttributeClassID] FROM SocoShop_Attribute WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAttributeAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAttributeAllList]
AS
	SELECT [ID],[Name],[AttributeClassID],[InputType],[InputValue],[OrderID] FROM SocoShop_Attribute ORDER BY [OrderID]
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddSendMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddSendMessage]
@title nvarchar(100),
@content ntext,
@date datetime,
@toUserID ntext,
@toUserName ntext,
@userID int,
@userName nvarchar(50),
@isAdmin int
AS 
	INSERT INTO SocoShop_SendMessage([Title],[Content],[Date],[ToUserID],[ToUserName],[UserID],[UserName],[IsAdmin]) VALUES(@title,@content,@date,@toUserID,@toUserName,@userID,@userName,@isAdmin)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateSendMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateSendMessage]
@id int,
@title nvarchar(100),
@content ntext
AS 
	UPDATE SocoShop_SendMessage Set [Title]=@title,[Content]=@content WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchSendMessageList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchSendMessageList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Title],[Content],[Date],[ToUserID],[ToUserName],[UserID],[UserName],[IsAdmin] FROM SocoShop_SendMessage 
	ELSE
		EXEC(''SELECT [ID],[Title],[Content],[Date],[ToUserID],[ToUserName],[UserID],[UserName],[IsAdmin] FROM SocoShop_SendMessage WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadSendMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadSendMessage]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[Title],[Content],[Date],[ToUserID],[ToUserName],[UserID],[UserName],[IsAdmin] FROM SocoShop_SendMessage WHERE [ID]=@id
	ELSE
		SELECT [ID],[Title],[Content],[Date],[ToUserID],[ToUserName],[UserID],[UserName],[IsAdmin] FROM SocoShop_SendMessage WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAttributeRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAttributeRecord]
@attributeID int,
@productID int,
@value nvarchar(100)
AS 
	INSERT INTO SocoShop_AttributeRecord([AttributeID],[ProductID],[Value]) VALUES(@attributeID,@productID,@value)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAttributeRecordByProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAttributeRecordByProduct]
@productID int
AS
	SELECT [AttributeID],[ProductID],[Value] FROM SocoShop_AttributeRecord WHERE [ProductID]=@productID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchReceiveMessageList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchReceiveMessageList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Title],[Content],[Date],[IsRead],[IsAdmin],[FromUserID],[FromUserName],[UserID],[UserName] FROM SocoShop_ReceiveMessage 
	ELSE
		EXEC(''SELECT [ID],[Title],[Content],[Date],[IsRead],[IsAdmin],[FromUserID],[FromUserName],[UserID],[UserName] FROM SocoShop_ReceiveMessage WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadReceiveMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadReceiveMessage]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[Title],[Content],[Date],[IsRead],[IsAdmin],[FromUserID],[FromUserName],[UserID],[UserName] FROM SocoShop_ReceiveMessage WHERE [ID]=@id
	ELSE
		SELECT [ID],[Title],[Content],[Date],[IsRead],[IsAdmin],[FromUserID],[FromUserName],[UserID],[UserName] FROM SocoShop_ReceiveMessage WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateReceiveMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateReceiveMessage]
@id int,
@isRead int
AS 
	UPDATE SocoShop_ReceiveMessage Set [IsRead]=@isRead WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddReceiveMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddReceiveMessage]
@title nvarchar(200),
@content ntext,
@date datetime,
@isRead int,
@isAdmin int,
@fromUserID int,
@fromUserName nvarchar(50),
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_ReceiveMessage([Title],[Content],[Date],[IsRead],[IsAdmin],[FromUserID],[FromUserName],[UserID],[UserName]) VALUES(@title,@content,@date,@isRead,@isAdmin,@fromUserID,@fromUserName,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserMore]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserMore]
@id int
AS 
	SELECT * FROM
(SELECT [ID],[UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID] FROM SocoShop_User WHERE [ID]=@id) As Temp1,
(SELECT ISNULL(Sum(Money),0) AS MoneyLeft,ISNULL(Sum(Point),0) AS PointLeft FROM SocoShop_UserAccountRecord WHERE UserID=@id) AS TEMP2,
(SELECT  ISNULL(Sum(ProductMoney-FavorableMoney+ShippingMoney+OtherMoney-CouponMoney),0) AS MoneyUsed FROM SocoShop_Order WHERE UserID=@id AND OrderStatus=6 ) AS TEMP3








' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadPreNextOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadPreNextOrder]
@id int
AS
SELECT [ID] FROM
(
	(SELECT TOP 1 [ID] FROM SocoShop_Order WHERE [ID]>@id ORDER BY ID ASC)
	 UNION ALL 
	(SELECT TOP 1 [ID] FROM SocoShop_Order WHERE [ID]<@id ORDER BY ID DESC)
) AS TEMP 

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveUpRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_MoveUpRegion]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_Region WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_Region WHERE [OrderID]<@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] DESC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_Region SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_Region SET [OrderID]=@orderID WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveDownRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_MoveDownRegion]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_Region WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_Region WHERE [OrderID]>@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] ASC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_Region SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_Region SET [OrderID]=@orderID WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadRegionAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadRegionAllList]
AS
	SELECT [ID],[FatherID],[OrderID],[RegionName] FROM SocoShop_Region ORDER BY [OrderID] ASC,ID ASC
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteRegion]
@id int
AS 
	 DECLARE @temp int
	 SELECT @temp=COUNT(*) FROM SocoShop_Region WHERE [FatherID]=@id 
	 IF @temp=0
	 	DELETE FROM SocoShop_Region WHERE [ID]=@id
		
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateRegion]
@id int,
@fatherID int,
@orderID int,
@regionName nvarchar(50)
AS 
	UPDATE SocoShop_Region Set [FatherID]=@fatherID,[OrderID]=@orderID,[RegionName]=@regionName WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddRegion]
@fatherID int,
@orderID int,
@regionName nvarchar(50)
AS 
	INSERT INTO SocoShop_Region([FatherID],[OrderID],[RegionName]) VALUES(@fatherID,@orderID,@regionName)	
		SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadMemberPriceByProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadMemberPriceByProduct]
@productID int
AS
	SELECT [ProductID],[GradeID],[Price] FROM SocoShop_MemberPrice WHERE [ProductID]=@productID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteMemberPriceByBatchEdit]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SocoShop_DeleteMemberPriceByBatchEdit]
@productID int,
@gradeID int
AS
	SET NOCOUNT ON;
	DELETE FROM SocoShop_MemberPrice WHERE [productID] = @productID AND [GradeID] = @gradeID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadMemberPriceListByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadMemberPriceListByProductID]
@productID int
AS 
    IF @productID=0
		SELECT [ID],[Name],NULL AS [Price] FROM SocoShop_UserGrade
    ELSE
    BEGIN
		SELECT SocoShop_UserGrade.[ID],SocoShop_UserGrade.[Name],[Price]  FROM SocoShop_UserGrade INNER JOIN SocoShop_MemberPrice ON SocoShop_UserGrade.[ID]=SocoShop_MemberPrice.[GradeID] WHERE [ProductID]=@productID 
		UNION ALL
		SELECT [ID],[Name],NULL AS [Price] FROM SocoShop_UserGrade WHERE [ID] NOT IN(SELECT SocoShop_UserGrade.[ID] FROM SocoShop_UserGrade INNER JOIN SocoShop_MemberPrice ON SocoShop_UserGrade.[ID]=SocoShop_MemberPrice.[GradeID] WHERE [ProductID]=@productID )
	END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddMemberPrice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddMemberPrice]
@productID int,
@gradeID int,
@price decimal(15,2)
AS 
	INSERT INTO SocoShop_MemberPrice([ProductID],[GradeID],[Price]) VALUES(@productID,@gradeID,@price)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAttributeClassCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeAttributeClassCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_AttributeClass SET [AttributeCount]=[AttributeCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_AttributeClass SET [AttributeCount]=[AttributeCount]-1 WHERE [ID] = @id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAttributeClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAttributeClass]
@name nvarchar(50),
@attributeCount int
AS 
	INSERT INTO SocoShop_AttributeClass([Name],[AttributeCount]) VALUES(@name,@attributeCount)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAttributeClassAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAttributeClassAllList]
AS
	SELECT [ID],[Name],[AttributeCount] FROM SocoShop_AttributeClass 
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAttributeClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateAttributeClass]
@id int,
@name nvarchar(50),
@attributeCount int
AS 
	UPDATE SocoShop_AttributeClass Set [Name]=@name,[AttributeCount]=@attributeCount WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadLatestOrderAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE  PROCEDURE [dbo].[SocoShop_ReadLatestOrderAction]
@orderID int,
@endOrderStatus int 
AS 
	SELECT top 1 [ID],[OrderID],[OrderOperate],[StartOrderStatus],[EndOrderStatus],[Note],[IP],[Date],[AdminID],[AdminName] FROM SocoShop_OrderAction WHERE [OrderID]=@orderID AND [EndOrderStatus]= @endOrderStatus AND [OrderOperate]!=8  Order By ID DESC



' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrderAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadOrderAction]
@id int
AS 
	SELECT [ID],[OrderID],[OrderOperate],[StartOrderStatus],[EndOrderStatus],[Note],[IP],[Date],[AdminID],[AdminName] FROM SocoShop_OrderAction WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddOrderAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddOrderAction]
@orderID int,
@orderOperate int,
@startOrderStatus int,
@endOrderStatus int,
@note nvarchar(500),
@iP nvarchar(40),
@date datetime,
@adminID int,
@adminName nvarchar(50)
AS 
	INSERT INTO SocoShop_OrderAction([OrderID],[OrderOperate],[StartOrderStatus],[EndOrderStatus],[Note],[IP],[Date],[AdminID],[AdminName]) VALUES(@orderID,@orderOperate,@startOrderStatus,@endOrderStatus,@note,@iP,@date,@adminID,@adminName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrderActionByOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadOrderActionByOrder]
@orderID int
AS
	SELECT [ID],[OrderID],[OrderOperate],[StartOrderStatus],[EndOrderStatus],[Note],[IP],[Date],[AdminID],[AdminName] FROM SocoShop_OrderAction WHERE [OrderID]=@orderID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_NoHandlerStatistics]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_NoHandlerStatistics] 
AS
SELECT 
(SELECT Count(1) FROM SocoShop_ProductComment WHERE Status=1) As UnHanderProductCommentCount,
(SELECT Count(1) FROM SocoShop_BookingProduct WHERE IsHandler=0) As UnHanderBookingProductCount,
(SELECT Count(1) FROM SocoShop_User WHERE Status=1) As UnActiveUserCount,
(SELECT Count(1) FROM SocoShop_User WHERE Status=3) As FreezeUserCount,
(SELECT Count(1) FROM SocoShop_UserMessage WHERE IsHandler=0) As UnHanderUserMessageCount,
(SELECT Count(1) FROM SocoShop_UserApply WHERE Status=1) As UnHanderUserApplyCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=1) As UnPayOrderCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=2) As UnCheckOrderCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=4) As UnShippingOrderCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=5) As UnReceiveOrderCount

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserApply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserApply]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[Number],[Money],[UserNote],[Status],[ApplyDate],[ApplyIP],[AdminNote],[UpdateDate],[UpdateAdminID],[UpdateAdminName],[UserID],[UserName] FROM SocoShop_UserApply WHERE [ID]=@id
	ELSE
		SELECT [ID],[Number],[Money],[UserNote],[Status],[ApplyDate],[ApplyIP],[AdminNote],[UpdateDate],[UpdateAdminID],[UpdateAdminName],[UserID],[UserName] FROM SocoShop_UserApply WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserApply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserApply]
@number nvarchar(50),
@money decimal(15,2),
@userNote nvarchar(4000),
@status int,
@applyDate datetime,
@applyIP nvarchar(40),
@adminNote nvarchar(500),
@updateDate datetime,
@updateAdminID int,
@updateAdminName nvarchar(50),
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_UserApply([Number],[Money],[UserNote],[Status],[ApplyDate],[ApplyIP],[AdminNote],[UpdateDate],[UpdateAdminID],[UpdateAdminName],[UserID],[UserName]) VALUES(@number,@money,@userNote,@status,@applyDate,@applyIP,@adminNote,@updateDate,@updateAdminID,@updateAdminName,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserApply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserApply]
@id int,
@status int,
@adminNote nvarchar(500),
@updateDate datetime,
@updateAdminID int,
@updateAdminName nvarchar(50)
AS 
	UPDATE SocoShop_UserApply Set [Status]=@status,[AdminNote]=@adminNote,[UpdateDate]=@updateDate,[UpdateAdminID]=@updateAdminID,[UpdateAdminName]=@updateAdminName WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserCouponByOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_ReadUserCouponByOrder]
@orderID int
AS 
		SELECT SocoShop_UserCoupon.[ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName],[Money],[UseMinAmount] 
 FROM SocoShop_UserCoupon INNER JOIN  SocoShop_Coupon 
ON SocoShop_UserCoupon.[CouponID]=SocoShop_Coupon.[ID] 
 WHERE [OrderID]=@orderID
	
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserCouponCanUse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserCouponCanUse]
@userID int 
AS
SELECT SocoShop_UserCoupon.[ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName],[Money],[UseMinAmount]
FROM  SocoShop_UserCoupon INNER JOIN  SocoShop_Coupon 
ON SocoShop_UserCoupon.[CouponID]=SocoShop_Coupon.[ID] 
WHERE [IsUse]=0 AND [UserID]=@userID
 AND  [UseStartDate]<=getdate() AND [UseEndDate]>=getdate()


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadCoupon]
@id int
AS 
	SELECT [ID],[Name],[Money],[UseMinAmount],[UseStartDate],[UseEndDate] FROM SocoShop_Coupon WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateCoupon]
@id int,
@name nvarchar(50),
@money decimal(18,2),
@useMinAmount decimal(18,2),
@useStartDate datetime,
@useEndDate datetime
AS 
	UPDATE SocoShop_Coupon Set [Name]=@name,[Money]=@money,[UseMinAmount]=@useMinAmount,[UseStartDate]=@useStartDate,[UseEndDate]=@useEndDate WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchCouponList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchCouponList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Name],[Money],[UseMinAmount],[UseStartDate],[UseEndDate] FROM SocoShop_Coupon 
	ELSE
		EXEC(''SELECT [ID],[Name],[Money],[UseMinAmount],[UseStartDate],[UseEndDate] FROM SocoShop_Coupon WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddCoupon]
@name nvarchar(50),
@money decimal(18,2),
@useMinAmount decimal(18,2),
@useStartDate datetime,
@useEndDate datetime
AS 
	INSERT INTO SocoShop_Coupon([Name],[Money],[UseMinAmount],[UseStartDate],[UseEndDate]) VALUES(@name,@money,@useMinAmount,@useStartDate,@useEndDate)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserIndexStatistics]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UserIndexStatistics] 
@userID int
AS
SELECT 
(SELECT Count(1) FROM SocoShop_ProductCollect WHERE UserID=@userID) As ProductCollectCount,
(SELECT Count(1) FROM SocoShop_ProductComment WHERE UserID=@userID) As ProductCommentCount,
(SELECT Count(1) FROM SocoShop_ProductReply WHERE UserID=@userID) As ProductReplyCount,
(SELECT Count(1) FROM SocoShop_UserMessage WHERE UserID=@userID) As UserMessageCount,
(SELECT Count(1) FROM SocoShop_UserFriend WHERE UserID=@userID) As UserFriendCount,
(SELECT Count(1) FROM SocoShop_Order WHERE UserID=@userID) As OrderCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=2 AND UserID=@userID) As UnCheckOrderCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=4 AND UserID=@userID) As UnShippingOrderCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=5 AND UserID=@userID) As UnReceiveOrderCount,
(SELECT Count(1) FROM SocoShop_Order WHERE OrderStatus=6 AND UserID=@userID) As FinishOrderCount


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserFriendByFriendID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserFriendByFriendID]
@friendID int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[FriendID],[FriendName],[UserID],[UserName] FROM SocoShop_UserFriend WHERE [FriendID]=@friendID
	ELSE
		SELECT [ID],[FriendID],[FriendName],[UserID],[UserName] FROM SocoShop_UserFriend WHERE [FriendID]=@friendID AND [UserID]=@userID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchUserFriendList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchUserFriendList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[FriendID],[FriendName],[UserID],[UserName] FROM SocoShop_UserFriend 
	ELSE
		EXEC(''SELECT [ID],[FriendID],[FriendName],[UserID],[UserName] FROM SocoShop_UserFriend WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserFriend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserFriend]
@friendID int,
@friendName nvarchar(50),
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_UserFriend([FriendID],[FriendName],[UserID],[UserName]) VALUES(@friendID,@friendName,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserFriend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserFriend]
@id int,
@friendName nvarchar(50)
AS 
	UPDATE SocoShop_UserFriend Set [FriendName]=@friendName WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserFriend]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserFriend]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[FriendID],[FriendName],[UserID],[UserName] FROM SocoShop_UserFriend WHERE [ID]=@id
	ELSE
		SELECT [ID],[FriendID],[FriendName],[UserID],[UserName] FROM SocoShop_UserFriend WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddCart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddCart]
@productID int,
@productName nvarchar(200),
@buyCount int,
@fatherID int,
@randNumber nvarchar(50),
@giftPackID int,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_Cart([ProductID],[ProductName],[BuyCount],[FatherID],[RandNumber],[GiftPackID],[UserID],[UserName]) VALUES(@productID,@productName,@buyCount,@fatherID,@randNumber,@giftPackID,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ClearCart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_ClearCart]
@userID int 
AS
	DELETE FROM SocoShop_Cart WHERE [UserID]=@userID 
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadCartListByUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_ReadCartListByUser]
@userID int 
AS
	SELECT [ID],[ProductID],[ProductName],[BuyCount],[FatherID],[RandNumber],[GiftPackID],[UserID],[UserName] FROM SocoShop_Cart WHERE [UserID]=@userID 

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadFlashPhotoByFlash]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadFlashPhotoByFlash]
@flashID int
AS
	SELECT [ID],[FlashID],[Title],[FileName],[URL],[OrderID],[Date] FROM SocoShop_FlashPhoto WHERE [FlashID]=@flashID ORDER BY [OrderID]
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateFlashPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateFlashPhoto]
@id int,
@flashID int,
@title nvarchar(100),
@fileName nvarchar(100),
@uRL nvarchar(100)
AS 
	UPDATE SocoShop_FlashPhoto Set [FlashID]=@flashID,[Title]=@title,[FileName]=@fileName,[URL]=@uRL WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeFlashCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeFlashCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 

	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_Flash SET [PhotoCount]=[PhotoCount]+1 WHERE [ID] IN (SELECT [FlashID] FROM SocoShop_FlashPhoto WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_Flash SET [PhotoCount]=[PhotoCount]-1 WHERE [ID] IN (SELECT [FlashID] FROM SocoShop_FlashPhoto WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeFlashPhotoOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeFlashPhotoOrder]
@action nvarchar(100),
@id int
AS 
	DECLARE @tempID int
	SET @tempID= 0
	DECLARE @tempOrder int
	SET @tempOrder= 0
	DECLARE @needChange bit 
	SET @needChange=0
	DECLARE @orderID int
	SET @orderID=0
	SELECT @orderID=[OrderID] FROM SocoShop_FlashPhoto WHERE [ID]= @id
	IF @orderID=0
		RETURN			
	IF @action = ''Up''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_FlashPhoto WHERE [OrderID]<@orderID ORDER BY [OrderID] DESC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @action = ''Down''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_FlashPhoto WHERE [OrderID]>@orderID ORDER BY [OrderID] ASC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @needChange=1
		BEGIN
		UPDATE SocoShop_FlashPhoto SET [OrderID]=@tempOrder  WHERE [ID]=@id
		UPDATE SocoShop_FlashPhoto SET [OrderID]=@orderID  WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadFlashPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadFlashPhoto]
@id int
AS 
	SELECT [ID],[FlashID],[Title],[FileName],[URL],[OrderID],[Date] FROM SocoShop_FlashPhoto WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddFlashPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddFlashPhoto]
@flashID int, 
@title nvarchar(100),
@fileName nvarchar(100),
@uRL nvarchar(100),
@orderID int, 
@date datetime
AS 
	DECLARE @maxID int
	SELECT @maxID=MAX([OrderID]) FROM SocoShop_FlashPhoto
	IF @maxID IS NULL	 
		SET @orderID= 1
        ELSE
		SET @orderID= @maxID+ 1
	INSERT INTO SocoShop_FlashPhoto([FlashID],[Title],[FileName],[URL],[OrderID],[Date]) VALUES(@flashID,@title,@fileName,@uRL,@orderID,@date)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateStandard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateStandard]
@id int,
@name nvarchar(50),
@displayTye int,
@valueList nvarchar(500),
@photoList nvarchar(500)
AS 
	UPDATE SocoShop_Standard Set [Name]=@name,[DisplayTye]=@displayTye,[ValueList]=@valueList,[PhotoList]=@photoList WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadStandardAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadStandardAllList]
AS
	SELECT [ID],[Name],[DisplayTye],[ValueList],[PhotoList] FROM SocoShop_Standard 
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddStandard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddStandard]
@name nvarchar(50),
@displayTye int,
@valueList nvarchar(500),
@photoList nvarchar(500)
AS 
	INSERT INTO SocoShop_Standard([Name],[DisplayTye],[ValueList],[PhotoList]) VALUES(@name,@displayTye,@valueList,@photoList)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddGift]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddGift]
@name nvarchar(50),
@photo nvarchar(100),
@description ntext
AS 
	INSERT INTO SocoShop_Gift([Name],[Photo],[Description]) VALUES(@name,@photo,@description)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadGift]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadGift]
@id int
AS 
	SELECT [ID],[Name],[Photo],[Description] FROM SocoShop_Gift WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchGiftList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchGiftList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Name],[Photo],[Description] FROM SocoShop_Gift 
	ELSE
		EXEC(''SELECT [ID],[Name],[Photo],[Description] FROM SocoShop_Gift WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateGift]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateGift]
@id int,
@name nvarchar(50),
@photo nvarchar(100),
@description ntext
AS 
	UPDATE SocoShop_Gift Set [Name]=@name,[Photo]=@photo,[Description]=@description WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddStandardRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddStandardRecord]
@productID int,
@standardIDList nvarchar(500),
@valueList nvarchar(500),
@groupTag nvarchar(500)
AS 
	INSERT INTO SocoShop_StandardRecord([ProductID],[StandardIDList],[ValueList],[GroupTag]) VALUES(@productID,@standardIDList,@valueList,@groupTag)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadStandardRecordByProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadStandardRecordByProduct]
@productID int,
@standardType int
AS
	IF @standardType=1 
		SELECT [ProductID],[StandardIDList],[ValueList],[GroupTag] FROM SocoShop_StandardRecord WHERE [ProductID]=@productID AND len([GroupTag])=0
	ELSE
		SELECT [ProductID],[StandardIDList],[ValueList],[GroupTag] FROM SocoShop_StandardRecord WHERE '',''+[GroupTag]+'','' LIKE ''%,''+CAST(@productID AS nvarchar(50))+'',%''


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductStandardType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SocoShop_UpdateProductStandardType]
@strID nvarchar(200),
@standardType int,
@id int
AS 
	DECLARE @groupTag nvarchar(200)
	SELECT TOP 1 @groupTag=[GroupTag] FROM SocoShop_StandardRecord WHERE ProductID=@id
	IF @groupTag!=''''
		EXEC(''UPDATE SocoShop_Product SET [StandardType]=0 WHERE ID in(''+@groupTag+'') AND ID !=''+@id)
	IF @standardType=2 AND @strID!=''''
		EXEC(''UPDATE SocoShop_Product SET [StandardType]=2 WHERE ID in(''+@strID+'')'')


	

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddOrderDetail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddOrderDetail]
@orderID int,
@productID int,
@productName nvarchar(200),
@productWeight decimal(18,2),
@sendPoint int,
@productPrice decimal(18,2),
@buyCount int,
@fatherID int,
@randNumber nvarchar(50),
@giftPackID int
AS 
	INSERT INTO SocoShop_OrderDetail([OrderID],[ProductID],[ProductName],[ProductWeight],[SendPoint],[ProductPrice],[BuyCount],[FatherID],[RandNumber],[GiftPackID]) VALUES(@orderID,@productID,@productName,@productWeight,@sendPoint,@productPrice,@buyCount,@fatherID,@randNumber,@giftPackID)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrderDetail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadOrderDetail]
@id int
AS 
	SELECT [ID],[OrderID],[ProductID],[ProductName],[ProductWeight],[SendPoint],[ProductPrice],[BuyCount],[FatherID],[RandNumber],[GiftPackID] FROM SocoShop_OrderDetail WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrderDetailByOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadOrderDetailByOrder]
@orderID int
AS
	SELECT [ID],[OrderID],[ProductID],[ProductName],[ProductWeight],[SendPoint],[ProductPrice],[BuyCount],[FatherID],[RandNumber],[GiftPackID] FROM SocoShop_OrderDetail WHERE [OrderID]=@orderID ORDER BY  [GiftPackID] DESC,[FatherID] ASC

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadOrderDetailByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadOrderDetailByProductID]
@productID int
AS
	SELECT [ID],[OrderID],[ProductID],[ProductName],[ProductWeight],[SendPoint],[ProductPrice],[BuyCount],[FatherID],[RandNumber],[GiftPackID] FROM SocoShop_OrderDetail WHERE [ProductID]=@productID 


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductOrderCountByOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductOrderCountByOrder]
@orderID int,
@changeAction nvarchar(10)
AS 
declare orderCursor cursor for select ProductID,BuyCount FROM SocoShop_OrderDetail WHERE [OrderID]=@orderID
open orderCursor
declare @productID int ,@buyCount int
fetch next from orderCursor into @productID,@buyCount
while(@@fetch_status=0)
  begin
	IF @changeAction=''Plus''
		UPDATE SocoShop_Product SET [OrderCount]=[OrderCount]+@buyCount WHERE [ID] = @productID
	ELSE
		UPDATE SocoShop_Product SET [OrderCount]=[OrderCount]-@buyCount WHERE [ID] = @productID
	fetch next from orderCursor into @productID,@buyCount
  end
close orderCursor
deallocate orderCursor



' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductSendCountByOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductSendCountByOrder]
@orderID int,
@changeAction nvarchar(10)
AS 
declare orderCursor cursor for select ProductID,BuyCount FROM SocoShop_OrderDetail WHERE [OrderID]=@orderID
open orderCursor
declare @productID int ,@buyCount int
fetch next from orderCursor into @productID,@buyCount
while(@@fetch_status=0)
  begin
	IF @changeAction=''Plus''
		UPDATE SocoShop_Product SET [SendCount]=[SendCount]+@buyCount WHERE [ID] = @productID
	ELSE
		UPDATE SocoShop_Product SET [SendCount]=[SendCount]-@buyCount WHERE [ID] = @productID
	fetch next from orderCursor into @productID,@buyCount
  end
close orderCursor
deallocate orderCursor
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateFlash]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateFlash]
@id int,
@title nvarchar(50),
@introduce ntext,
@width int,
@height int
AS 
	UPDATE SocoShop_Flash Set [Title]=@title,[Introduce]=@introduce,[Width]=@width,[Height]=@height WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadFlash]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadFlash]
@id int
AS 
	SELECT [ID],[Title],[Introduce],[Width],[Height],[PhotoCount] FROM SocoShop_Flash WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddFlash]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddFlash]
@title nvarchar(50),
@introduce ntext,
@width int,
@height int,
@photoCount int
AS 
	INSERT INTO SocoShop_Flash([Title],[Introduce],[Width],[Height],[PhotoCount]) VALUES(@title,@introduce,@width,@height,@photoCount)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeFlashCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeFlashCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_Flash SET [PhotoCount]=[PhotoCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_Flash SET [PhotoCount]=[PhotoCount]-1 WHERE [ID] = @id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAdCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeAdCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_Ad SET [ClickCount]=[ClickCount]+1 WHERE [ID] IN (SELECT [AdID] FROM SocoShop_AdRecord WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_Ad SET [ClickCount]=[ClickCount]-1 WHERE [ID] IN (SELECT [AdID] FROM SocoShop_AdRecord WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAdRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAdRecord]
@adID int,
@iP nvarchar(40),
@date datetime,
@page nvarchar(100),
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_AdRecord([AdID],[IP],[Date],[Page],[UserID],[UserName]) VALUES(@adID,@iP,@date,@page,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAdminLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateAdminLog]
@id int,
@action nvarchar(200)
AS 
	UPDATE SocoShop_AdminLog Set [Action]=@action WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAdminLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAdminLog]
@id int,
@adminID int
AS 
	IF @adminID=0
		SELECT [ID],[GroupID],[Action],[IP],[AddDate],[AdminID],[AdminName] FROM SocoShop_AdminLog WHERE [ID]=@id
	ELSE
		SELECT [ID],[GroupID],[Action],[IP],[AddDate],[AdminID],[AdminName] FROM SocoShop_AdminLog WHERE [ID]=@id AND [AdminID]=@adminID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAdminLog]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAdminLog]
@groupID int,
@action nvarchar(200),
@iP nvarchar(40),
@addDate datetime,
@adminID int,
@adminName nvarchar(50)
AS 
	INSERT INTO SocoShop_AdminLog([GroupID],[Action],[IP],[AddDate],[AdminID],[AdminName]) VALUES(@groupID,@action,@iP,@addDate,@adminID,@adminName)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAd]
@id int
AS 
	SELECT [ID],[Title],[Introduction],[AdClass],[Display],[Width],[Height],[Url],[StartDate],[EndDate],[Remark],[ClickCount],[IsEnabled] FROM SocoShop_Ad WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAdCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeAdCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_Ad SET [ClickCount]=[ClickCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_Ad SET [ClickCount]=[ClickCount]-1 WHERE [ID] = @id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAd]
@title nvarchar(100),
@introduction ntext,
@adClass int,
@display ntext,
@width int,
@height int,
@url nvarchar(200),
@startDate datetime,
@endDate datetime,
@remark nvarchar(200),
@clickCount int,
@isEnabled int
AS 
	INSERT INTO SocoShop_Ad([Title],[Introduction],[AdClass],[Display],[Width],[Height],[Url],[StartDate],[EndDate],[Remark],[ClickCount],[IsEnabled]) VALUES(@title,@introduction,@adClass,@display,@width,@height,@url,@startDate,@endDate,@remark,@clickCount,@isEnabled)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateAd]
@id int,
@title nvarchar(100),
@introduction ntext,
@adClass int,
@display ntext,
@width int,
@height int,
@url nvarchar(200),
@startDate datetime,
@endDate datetime,
@remark nvarchar(200),
@isEnabled int
AS 
	UPDATE SocoShop_Ad Set [Title]=@title,[Introduction]=@introduction,[AdClass]=@adClass,[Display]=@display,[Width]=@width,[Height]=@height,[Url]=@url,[StartDate]=@startDate,[EndDate]=@endDate,[Remark]=@remark,[IsEnabled]=@isEnabled WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadMoneyLeftBeforID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadMoneyLeftBeforID]
@id int,
@userID int 
AS 
	SELECT Sum(Money) AS MoneyLeft FROM SocoShop_UserAccountRecord WHERE UserID=@userID AND [ID]<@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserAccountRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserAccountRecord]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[Money],[Point],[Date],[IP],[Note],[UserID],[UserName] FROM SocoShop_UserAccountRecord WHERE [ID]=@id
	ELSE
		SELECT [ID],[Money],[Point],[Date],[IP],[Note],[UserID],[UserName] FROM SocoShop_UserAccountRecord WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadPointLeftBeforID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadPointLeftBeforID]
@id int,
@userID int 
AS 
	SELECT Sum(Point) AS PointLeft FROM SocoShop_UserAccountRecord WHERE UserID=@userID AND [ID]<@id' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserAccountRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserAccountRecord]
@id int,
@note nvarchar(50)
AS 
	UPDATE SocoShop_UserAccountRecord Set [Note]=@note WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserAccountRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserAccountRecord]
@money decimal(15,2),
@point int,
@date datetime,
@iP nvarchar(50),
@note nvarchar(50),
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_UserAccountRecord([Money],[Point],[Date],[IP],[Note],[UserID],[UserName]) VALUES(@money,@point,@date,@iP,@note,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserAccountRecordListByUserID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserAccountRecordListByUserID]
@userID int
AS 
		SELECT [ID],[Money],[Point],[Date],[IP],[Note],[UserID],[UserName] FROM SocoShop_UserAccountRecord WHERE [UserID]=@userID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductReply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProductReply]
@id int,
@content ntext
AS 
	UPDATE SocoShop_ProductReply Set [Content]=@content WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCommentCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCommentCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_ProductComment SET [ReplyCount]=[ReplyCount]+1 WHERE [ID] IN (SELECT [CommentID] FROM SocoShop_ProductReply WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_ProductComment SET [ReplyCount]=[ReplyCount]-1 WHERE [ID] IN (SELECT [CommentID] FROM SocoShop_ProductReply WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddProductReply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddProductReply]
@productID int,
@commentID int,
@content ntext,
@userIP nvarchar(40),
@postDate datetime,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_ProductReply([ProductID],[CommentID],[Content],[UserIP],[PostDate],[UserID],[UserName]) VALUES(@productID,@commentID,@content,@userIP,@postDate,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductReply]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductReply]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[ProductID],[CommentID],[Content],[UserIP],[PostDate],[UserID],[UserName] FROM SocoShop_ProductReply WHERE [ID]=@id
	ELSE
		SELECT [ID],[ProductID],[CommentID],[Content],[UserIP],[PostDate],[UserID],[UserName] FROM SocoShop_ProductReply WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_TaobaoProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_TaobaoProduct]
@name nvarchar(500),
@spelling nvarchar(1000),
@classID nvarchar(500),
@marketPrice decimal(18,2),
@photo nvarchar(100),
@introduction ntext,
@summary ntext,
@totalStorageCount int,
@addDate datetime,
@taobaoID bigint 
AS 
	IF exists(SELECT [ID] FROM SocoShop_Product WHERE [TaobaoID]=@taobaoID)
		UPDATE SocoShop_Product Set [Name]=@name,[Spelling]=@spelling,[ClassID]=@classID,[MarketPrice]=@marketPrice,[Photo]=@photo,[Introduction]=@introduction,[Summary]=@summary,[TotalStorageCount]=@totalStorageCount WHERE [TaobaoID]=@taobaoID
	ELSE
		INSERT INTO SocoShop_Product([Name],[Spelling],[Color],[FontStyle],[ProductNumber],[ClassID],[BrandID],[MarketPrice],[Weight],[SendPoint],[Photo],[Keywords],[Summary],[Introduction],[Remark],[IsSpecial],[IsNew],[IsHot],[IsSale],[IsTop],[Accessory],[RelationProduct],[RelationArticle],[ViewCount],[AllowComment],[CommentCount],[SumPoint],[PerPoint],[PhotoCount],[CollectCount],[TotalStorageCount],[OrderCount],[SendCount],[ImportActualStorageCount],[ImportVirtualStorageCount],[LowerCount],[UpperCount],[AttributeClassID],[StandardType],[AddDate],[TaobaoID]) 
							 VALUES(@name,@spelling,'''','''','''',@classID,0,@marketPrice,0,0,@photo,'''',@summary,@introduction,'''',0,0,0,1,0,'''','''','''',0,1,0,0,0,0,0,@totalStorageCount,0,0,0,0,0,0,'''',0,@addDate,@taobaoID)


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddProduct]
@name nvarchar(500),
@spelling nvarchar(1000),
@color nvarchar(50),
@fontStyle nvarchar(50),
@productNumber nvarchar(50),
@classID nvarchar(500),
@brandID int,
@marketPrice decimal(18,2),
@weight int,
@sendPoint int,
@photo nvarchar(100),
@keywords nvarchar(200),
@summary ntext,
@introduction ntext,
@remark ntext,
@isSpecial int,
@isNew int,
@isHot int,
@isSale int,
@isTop int,
@accessory nvarchar(500),
@relationProduct nvarchar(500),
@relationArticle nvarchar(500),
@viewCount int,
@allowComment int,
@commentCount int,
@sumPoint int,
@perPoint decimal(15,2),
@photoCount int,
@collectCount int,
@totalStorageCount int,
@orderCount int,
@sendCount int,
@importActualStorageCount int,
@importVirtualStorageCount int,
@lowerCount int,
@upperCount int,
@attributeClassID int,
@standardType int,
@addDate datetime
AS 
	INSERT INTO SocoShop_Product([Name],[Spelling],[Color],[FontStyle],[ProductNumber],[ClassID],[BrandID],[MarketPrice],[Weight],[SendPoint],[Photo],[Keywords],[Summary],[Introduction],[Remark],[IsSpecial],[IsNew],[IsHot],[IsSale],[IsTop],[Accessory],[RelationProduct],[RelationArticle],[ViewCount],[AllowComment],[CommentCount],[SumPoint],[PerPoint],[PhotoCount],[CollectCount],[TotalStorageCount],[OrderCount],[SendCount],[ImportActualStorageCount],[ImportVirtualStorageCount],[LowerCount],[UpperCount],[AttributeClassID],[StandardType],[AddDate]) VALUES(@name,@spelling,@color,@fontStyle,@productNumber,@classID,@brandID,@marketPrice,@weight,@sendPoint,@photo,@keywords,@summary,@introduction,@remark,@isSpecial,@isNew,@isHot,@isSale,@isTop,@accessory,@relationProduct,@relationArticle,@viewCount,@allowComment,@commentCount,@sumPoint,@perPoint,@photoCount,@collectCount,@totalStorageCount,@orderCount,@sendCount,@importActualStorageCount,@importVirtualStorageCount,@lowerCount,@upperCount,@attributeClassID,@standardType,@addDate)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductCoverPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateProductCoverPhoto]
@id int,
@photo nvarchar(100)
AS 
	UPDATE SocoShop_Product SET [Photo]=@photo WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAdminGroupCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeAdminGroupCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_AdminGroup SET [AdminCount]=[AdminCount]+1 WHERE [ID] IN (SELECT [GroupID] FROM SocoShop_Admin WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_AdminGroup SET [AdminCount]=[AdminCount]-1 WHERE [ID] IN (SELECT [GroupID] FROM SocoShop_Admin WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAdmin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAdmin]
@id int
AS
		SELECT [ID],[Name],[Email],[GroupID],[Password],[LastLoginIP],[LastLoginDate],[LoginTimes],[NoteBook],[IsCreate] FROM SocoShop_Admin WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAdmin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateAdmin]
@id int,
@name nvarchar(50),
@email nvarchar(50),
@groupID int,
@noteBook ntext
AS 
	UPDATE SocoShop_Admin Set [Name]=@name,[Email]=@email,[GroupID]=@groupID,[NoteBook]=@noteBook WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAdminPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeAdminPassword]
@id int,
@oldPassword nvarchar(50),
@newPassword nvarchar(50)
AS
	UPDATE SocoShop_Admin SET [Password]=@newPassword WHERE [ID]=@id AND [Password]=@oldPassword
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_CheckAdminLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_CheckAdminLogin]
@loginName nvarchar(50),
@loginPass nvarchar(50)
AS 
		SELECT [ID],[Name],[GroupID] FROM SocoShop_Admin WHERE [Name]=@loginName AND [Password]=@loginPass

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAdmin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAdmin]
@name nvarchar(50),
@email nvarchar(50),
@groupID int,
@password nvarchar(50),
@lastLoginIP nvarchar(40),
@lastLoginDate datetime,
@loginTimes int,
@noteBook ntext,
@isCreate int
AS 
	INSERT INTO SocoShop_Admin([Name],[Email],[GroupID],[Password],[LastLoginIP],[LastLoginDate],[LoginTimes],[NoteBook],[IsCreate]) VALUES(@name,@email,@groupID,@password,@lastLoginIP,@lastLoginDate,@loginTimes,@noteBook,@isCreate)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAdminPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateAdminPassword]
@id int,
@password nvarchar(50)
AS 
		UPDATE SocoShop_Admin Set [Password]=@password WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAdminLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateAdminLogin]
@id int,
@lastLoginDate datetime,
@lastLoginIP nvarchar(40)
AS 
	UPDATE SocoShop_Admin SET [LastLoginDate]=@lastLoginDate,[LastLoginIP]=@lastLoginIP,[LoginTimes]=[LoginTimes]+1 WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductComment]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[ProductID],[Title],[Content],[UserIP],[PostDate],[Support],[Against],[Status],[Rank],[ReplyCount],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_ProductComment WHERE [ID]=@id
	ELSE
		SELECT [ID],[ProductID],[Title],[Content],[UserIP],[PostDate],[Support],[Against],[Status],[Rank],[ReplyCount],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_ProductComment WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchProductCommentList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchProductCommentList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[ProductID],[Title],[Content],[UserIP],[PostDate],[Support],[Against],[Status],[Rank],[ReplyCount],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_ProductComment 
	ELSE
		EXEC(''SELECT [ID],[ProductID],[Title],[Content],[UserIP],[PostDate],[Support],[Against],[Status],[Rank],[ReplyCount],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_ProductComment WHERE ''+ @condition)


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductCommentSupport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateProductCommentSupport]
@commentID int,
@support int

AS 
	UPDATE SocoShop_ProductComment Set [Support]=@support WHERE [ID]=@commentID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductCommentIndex]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadProductCommentIndex]
@firstProductID int,
@secondProductID int,
@thirdProductID int
AS
SELECT TOP 1 [ProductID],[content] FROM SocoShop_ProductComment where [ProductID]=@firstProductID
Union All SELECT TOP 1 [ProductID],[content] FROM SocoShop_ProductComment where [ProductID]=@secondProductID
Union All SELECT TOP 1 [ProductID],[content] FROM SocoShop_ProductComment where [ProductID]=@thirdProductID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddProductComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddProductComment]
@productID int,
@title nvarchar(500),
@content ntext,
@userIP nvarchar(50),
@postDate datetime,
@support int,
@against int,
@status int,
@rank int,
@replyCount int,
@adminReplyContent nvarchar(500),
@adminReplyDate datetime,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_ProductComment([ProductID],[Title],[Content],[UserIP],[PostDate],[Support],[Against],[Status],[Rank],[ReplyCount],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName]) VALUES(@productID,@title,@content,@userIP,@postDate,@support,@against,@status,@rank,@replyCount,@adminReplyContent,@adminReplyDate,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductCommentByTime]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SocoShop_ReadProductCommentByTime]
@userIP nvarchar(50),        
@userID int,
@productID int,
@time datetime
as
		SELECT [ID] FROM SocoShop_ProductComment WHERE [ProductID]=@productID AND ([UserID]=@userID OR [UserIP]=@userIP) AND [PostDate]>@time
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProductComment]
@id int,
@status int,
@adminReplyContent nvarchar(500),
@adminReplyDate datetime
AS 
	UPDATE SocoShop_ProductComment Set [Status]=@status,[AdminReplyContent]=@adminReplyContent,[AdminReplyDate]=@adminReplyDate WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCommentCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCommentCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_ProductComment SET [ReplyCount]=[ReplyCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_ProductComment SET [ReplyCount]=[ReplyCount]-1 WHERE [ID] = @id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCommentCountAndRankByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCommentCountAndRankByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		DECLARE @productID int
		DECLARE @rank int 
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					SELECT @productID=[ProductID],@rank=[Rank] FROM SocoShop_ProductComment WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1))
					IF @action=''Plus''
						UPDATE SocoShop_Product SET [CommentCount]=[CommentCount]+1,[SumPoint]=[SumPoint]+@rank WHERE [ID] =@productID						
					ELSE
						UPDATE SocoShop_Product SET [CommentCount]=[CommentCount]-1,[SumPoint]=[SumPoint]-@rank WHERE [ID] =@productID
					UPDATE SocoShop_Product SET [PerPoint]=(CASE [CommentCount] WHEN 0 THEN 0 ELSE CAST([SumPoint] as decimal(15,2))/CAST([CommentCount]  as decimal(15,2)) END) WHERE [ID] = @productID
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserRechargeByNumber]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserRechargeByNumber]
@number nvarchar(50),
@userID int
AS 
	IF @userID=0
		SELECT [ID],[Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName] FROM SocoShop_UserRecharge WHERE [Number]=@number
	ELSE
		SELECT [ID],[Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName] FROM SocoShop_UserRecharge WHERE [Number]=@number AND [UserID]=@userID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchUserRechargeList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchUserRechargeList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName] FROM SocoShop_UserRecharge 
	ELSE
		EXEC(''SELECT [ID],[Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName] FROM SocoShop_UserRecharge WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserRecharge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserRecharge]
@id int,
@isFinish int
AS 
	UPDATE SocoShop_UserRecharge Set [IsFinish]=@isFinish WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserRecharge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserRecharge]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName] FROM SocoShop_UserRecharge WHERE [ID]=@id
	ELSE
		SELECT [ID],[Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName] FROM SocoShop_UserRecharge WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserRecharge]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserRecharge]
@number nvarchar(50),
@money decimal(18,2),
@payKey nvarchar(50),
@payName nvarchar(50),
@rechargeDate datetime,
@rechargeIP nvarchar(50),
@isFinish int,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_UserRecharge([Number],[Money],[PayKey],[PayName],[RechargeDate],[RechargeIP],[IsFinish],[UserID],[UserName]) VALUES(@number,@money,@payKey,@payName,@rechargeDate,@rechargeIP,@isFinish,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateShipping]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateShipping]
@id int,
@name nvarchar(100),
@description ntext,
@isEnabled int,
@shippingType int,
@firstWeight int,
@againWeight int
AS 
	UPDATE SocoShop_Shipping Set [Name]=@name,[Description]=@description,[IsEnabled]=@isEnabled,[ShippingType]=@shippingType,[FirstWeight]=@firstWeight,[AgainWeight]=@againWeight WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddShipping]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddShipping]
@name nvarchar(100),
@description ntext, 
@isEnabled int, 
@shippingType int, 
@firstWeight int, 
@againWeight int, 
@orderID int
AS 
	DECLARE @maxID int
	SELECT @maxID=MAX([OrderID]) FROM SocoShop_Shipping
	IF @maxID IS NULL	 
		SET @orderID= 1
        ELSE
		SET @orderID= @maxID+ 1
	INSERT INTO SocoShop_Shipping([Name],[Description],[IsEnabled],[ShippingType],[FirstWeight],[AgainWeight],[OrderID]) VALUES(@name,@description,@isEnabled,@shippingType,@firstWeight,@againWeight,@orderID)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadShippingAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadShippingAllList]
AS
	SELECT [ID],[Name],[Description],[IsEnabled],[ShippingType],[FirstWeight],[AgainWeight],[OrderID] FROM SocoShop_Shipping ORDER BY [OrderID]
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeShippingOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeShippingOrder]
@action nvarchar(100),
@id int
AS 
	DECLARE @tempID int
	SET @tempID= 0
	DECLARE @tempOrder int
	SET @tempOrder= 0
	DECLARE @needChange bit 
	SET @needChange=0
	DECLARE @orderID int
	SET @orderID=0
	SELECT @orderID=[OrderID] FROM SocoShop_Shipping WHERE [ID]= @id
	IF @orderID=0
		RETURN			
	IF @action = ''Up''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_Shipping WHERE [OrderID]<@orderID ORDER BY [OrderID] DESC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @action = ''Down''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_Shipping WHERE [OrderID]>@orderID ORDER BY [OrderID] ASC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @needChange=1
		BEGIN
		UPDATE SocoShop_Shipping SET [OrderID]=@tempOrder  WHERE [ID]=@id
		UPDATE SocoShop_Shipping SET [OrderID]=@orderID  WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductPhotoByProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductPhotoByProduct]
@productID int
AS
	SELECT [ID],[ProductID],[Name],[Photo] FROM SocoShop_ProductPhoto WHERE [ProductID]=@productID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddProductPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddProductPhoto]
@productID int,
@name nvarchar(50),
@photo nvarchar(100)
AS 
	INSERT INTO SocoShop_ProductPhoto([ProductID],[Name],[Photo]) VALUES(@productID,@name,@photo)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductPhotoCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ChangeProductPhotoCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_Product SET [PhotoCount]=[PhotoCount]+1 WHERE [ID] IN (SELECT [ProductID] FROM SocoShop_ProductPhoto WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_Product SET [PhotoCount]=[PhotoCount]-1 WHERE [ID] IN (SELECT [ProductID] FROM SocoShop_ProductPhoto WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END



' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProductPhoto]
@id int,
@productID int,
@name nvarchar(50),
@photo nvarchar(100)
AS 
	UPDATE SocoShop_ProductPhoto Set [ProductID]=@productID,[Name]=@name,[Photo]=@photo WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductPhoto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductPhoto]
@id int
AS 
	SELECT [ID],[ProductID],[Name],[Photo] FROM SocoShop_ProductPhoto WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchProductPhotoList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchProductPhotoList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[ProductID],[Name],[Photo] FROM SocoShop_ProductPhoto 
	ELSE
		EXEC(''SELECT [ID],[ProductID],[Name],[Photo] FROM SocoShop_ProductPhoto WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateShippingRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateShippingRegion]
@id int,
@name nvarchar(100),
@regionID nvarchar(500),
@fixedMoeny decimal(18,2),
@firstMoney decimal(18,2),
@againMoney decimal(18,2),
@oneMoeny decimal(18,2),
@anotherMoeny decimal(18,2)
AS 
	UPDATE SocoShop_ShippingRegion Set [Name]=@name,[RegionID]=@regionID,[FixedMoeny]=@fixedMoeny,[FirstMoney]=@firstMoney,[AgainMoney]=@againMoney,[OneMoeny]=@oneMoeny,[AnotherMoeny]=@anotherMoeny WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadShippingRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadShippingRegion]
@id int
AS 
	SELECT [ID],[Name],[ShippingID],[RegionID],[FixedMoeny],[FirstMoney],[AgainMoney],[OneMoeny],[AnotherMoeny] FROM SocoShop_ShippingRegion WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddShippingRegion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddShippingRegion]
@name nvarchar(100),
@shippingID int,
@regionID nvarchar(500),
@fixedMoeny decimal(18,2),
@firstMoney decimal(18,2),
@againMoney decimal(18,2),
@oneMoeny decimal(18,2),
@anotherMoeny decimal(18,2)
AS 
	INSERT INTO SocoShop_ShippingRegion([Name],[ShippingID],[RegionID],[FixedMoeny],[FirstMoney],[AgainMoney],[OneMoeny],[AnotherMoeny]) VALUES(@name,@shippingID,@regionID,@fixedMoeny,@firstMoney,@againMoney,@oneMoeny,@anotherMoeny)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddEmailSendRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddEmailSendRecord]
@title nvarchar(200),
@content ntext,
@isSystem int,
@emailList ntext,
@openEmailList ntext,
@isStatisticsOpendEmail int,
@sendStatus int,
@note nvarchar(500),
@addDate datetime,
@sendDate datetime
AS 
	INSERT INTO SocoShop_EmailSendRecord([Title],[Content],[IsSystem],[EmailList],[OpenEmailList],[IsStatisticsOpendEmail],[SendStatus],[Note],[AddDate],[SendDate]) VALUES(@title,@content,@isSystem,@emailList,@openEmailList,@isStatisticsOpendEmail,@sendStatus,@note,@addDate,@sendDate)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchEmailSendRecordList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchEmailSendRecordList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Title],[Content],[IsSystem],[EmailList],[OpenEmailList],[IsStatisticsOpendEmail],[SendStatus],[Note],[AddDate],[SendDate] FROM SocoShop_EmailSendRecord 
	ELSE
		EXEC(''SELECT [ID],[Title],[Content],[IsSystem],[EmailList],[OpenEmailList],[IsStatisticsOpendEmail],[SendStatus],[Note],[AddDate],[SendDate] FROM SocoShop_EmailSendRecord WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadEmailSendRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadEmailSendRecord]
@id int
AS 
	SELECT [ID],[Title],[Content],[IsSystem],[EmailList],[OpenEmailList],[IsStatisticsOpendEmail],[SendStatus],[Note],[AddDate],[SendDate] FROM SocoShop_EmailSendRecord WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SaveEmailSendRecordStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_SaveEmailSendRecordStatus]
@id int,
@sendStatus int,
@sendDate datetime
AS 
	UPDATE SocoShop_EmailSendRecord Set [SendStatus]=@sendStatus,[SendDate]=@sendDate WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_RecordOpenedEmailRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_RecordOpenedEmailRecord]
@email nvarchar(100),
@id int
AS 
	DECLARE @openEmailList nvarchar(400)
	DECLARE @emailList nvarchar(400)
	SELECT @openEmailList=[OpenEmailList],@emailList=[EmailList] FROM SocoShop_EmailSendRecord WHERE [ID]=@id
	IF CHARINDEX('',''+@email+'','','',''+@openEmailList)=0 AND CHARINDEX(@email,@emailList)>0
		UPDATE SocoShop_EmailSendRecord SET [OpenEmailList]=[OpenEmailList]+ @email+'','' WHERE [ID]=@id



' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductFatherID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProductFatherID]
@taobaoID bigint,
@systemID int
AS 
	UPDATE SocoShop_ProductClass SET FatherID=@systemID where FatherID=@taobaoID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddProductClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddProductClass]
@fatherID int,
@orderID int,
@className nvarchar(50),
@keywords nvarchar(200),
@description ntext,
@taobaoID bigint
AS 
	INSERT INTO SocoShop_ProductClass([FatherID],[OrderID],[ClassName],[Keywords],[Description],[TaobaoID]) VALUES(@fatherID,@orderID,@className,@keywords,@description,@taobaoID)	
		SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserGrade]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserGrade]
@name nvarchar(50),
@minMoney decimal(15,2),
@maxMoney decimal(15,2),
@discount decimal(15,2)
AS 
	INSERT INTO SocoShop_UserGrade([Name],[MinMoney],[MaxMoney],[Discount]) VALUES(@name,@minMoney,@maxMoney,@discount)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserGradeAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserGradeAllList]
AS
	SELECT [ID],[Name],[MinMoney],[MaxMoney],[Discount] FROM SocoShop_UserGrade 
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserGrade]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserGrade]
@id int,
@name nvarchar(50),
@minMoney decimal(15,2),
@maxMoney decimal(15,2),
@discount decimal(15,2)
AS 
	UPDATE SocoShop_UserGrade Set [Name]=@name,[MinMoney]=@minMoney,[MaxMoney]=@maxMoney,[Discount]=@discount WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadFavorableActivityByDateTime]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_ReadFavorableActivityByDateTime]
@startDate DateTime,
@endDate DateTime,
@id int
AS 
	IF @id=0
		SELECT TOP 1 [ID],[Name],[Photo],[Content],[StartDate],[EndDate],[UserGrade],[OrderProductMoney],[RegionID],[ShippingWay],[ReduceWay],[ReduceMoney],[ReduceDiscount],[GiftID] FROM SocoShop_FavorableActivity WHERE ([StartDate]<=@startDate AND [EndDate]>@startDate) OR ([StartDate]>=@startDate AND [StartDate]<@endDate)
	ELSE
		SELECT TOP 1 [ID],[Name],[Photo],[Content],[StartDate],[EndDate],[UserGrade],[OrderProductMoney],[RegionID],[ShippingWay],[ReduceWay],[ReduceMoney],[ReduceDiscount],[GiftID] FROM SocoShop_FavorableActivity WHERE (([StartDate]<=@startDate AND [EndDate]>@startDate) OR ([StartDate]>=@startDate AND [StartDate]<@endDate)) AND [ID]!=@id




' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadFavorableActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadFavorableActivity]
@id int
AS 
	SELECT [ID],[Name],[Photo],[Content],[StartDate],[EndDate],[UserGrade],[OrderProductMoney],[RegionID],[ShippingWay],[ReduceWay],[ReduceMoney],[ReduceDiscount],[GiftID] FROM SocoShop_FavorableActivity WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddFavorableActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddFavorableActivity]
@name nvarchar(100),
@photo nvarchar(200),
@content ntext,
@startDate datetime,
@endDate datetime,
@userGrade nvarchar(50),
@orderProductMoney decimal(18,2),
@regionID nvarchar(500),
@shippingWay int,
@reduceWay int,
@reduceMoney decimal(18,2),
@reduceDiscount decimal(18,2),
@giftID nvarchar(200)
AS 
	INSERT INTO SocoShop_FavorableActivity([Name],[Photo],[Content],[StartDate],[EndDate],[UserGrade],[OrderProductMoney],[RegionID],[ShippingWay],[ReduceWay],[ReduceMoney],[ReduceDiscount],[GiftID]) VALUES(@name,@photo,@content,@startDate,@endDate,@userGrade,@orderProductMoney,@regionID,@shippingWay,@reduceWay,@reduceMoney,@reduceDiscount,@giftID)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateFavorableActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateFavorableActivity]
@id int,
@name nvarchar(100),
@photo nvarchar(200),
@content ntext,
@startDate datetime,
@endDate datetime,
@userGrade nvarchar(50),
@orderProductMoney decimal(18,2),
@regionID nvarchar(500),
@shippingWay int,
@reduceWay int,
@reduceMoney decimal(18,2),
@reduceDiscount decimal(18,2),
@giftID nvarchar(200)
AS 
	UPDATE SocoShop_FavorableActivity Set [Name]=@name,[Photo]=@photo,[Content]=@content,[StartDate]=@startDate,[EndDate]=@endDate,[UserGrade]=@userGrade,[OrderProductMoney]=@orderProductMoney,[RegionID]=@regionID,[ShippingWay]=@shippingWay,[ReduceWay]=@reduceWay,[ReduceMoney]=@reduceMoney,[ReduceDiscount]=@reduceDiscount,[GiftID]=@giftID WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateGiftPack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateGiftPack]
@id int,
@name nvarchar(200),
@photo nvarchar(200),
@startDate datetime,
@endDate datetime,
@price decimal(15,2),
@giftGroup ntext
AS 
	UPDATE SocoShop_GiftPack Set [Name]=@name,[Photo]=@photo,[StartDate]=@startDate,[EndDate]=@endDate,[Price]=@price,[GiftGroup]=@giftGroup WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadGiftPack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadGiftPack]
@id int
AS 
	SELECT [ID],[Name],[Photo],[StartDate],[EndDate],[Price],[GiftGroup] FROM SocoShop_GiftPack WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddGiftPack]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddGiftPack]
@name nvarchar(200),
@photo nvarchar(200),
@startDate datetime,
@endDate datetime,
@price decimal(15,2),
@giftGroup ntext
AS 
	INSERT INTO SocoShop_GiftPack([Name],[Photo],[StartDate],[EndDate],[Price],[GiftGroup]) VALUES(@name,@photo,@startDate,@endDate,@price,@giftGroup)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUpload]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUpload]
@tableID int,
@classID int,
@recordID int,
@randomNumber nvarchar(200)
AS 
	UPDATE SocoShop_Upload SET [ClassID]=@classID,[RecordID]=@recordID WHERE ([RecordID]=0 OR [RecordID]=@recordID) AND [TableID]=@tableID AND [RandomNumber]=@randomNumber

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUpload]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUpload]
@tableID int,
@classID int,
@recordID int,
@uploadName nvarchar(100),
@otherFile nvarchar(500),
@size int,
@fileType nvarchar(50),
@randomNumber nvarchar(100),
@date datetime,
@iP nvarchar(50)
AS 
	INSERT INTO SocoShop_Upload([TableID],[ClassID],[RecordID],[UploadName],[OtherFile],[Size],[FileType],[RandomNumber],[Date],[IP]) VALUES(@tableID,@classID,@recordID,@uploadName,@otherFile,@size,@fileType,@randomNumber,@date,@iP) 
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadTopUserCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SocoShop_ReadTopUserCoupon]
@couponID int
AS 	
	SELECT TOP 1 [ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName] FROM SocoShop_UserCoupon WHERE [CouponID]=@couponID ORDER BY ID DESC
	
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchUserCouponList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchUserCouponList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName] FROM SocoShop_UserCoupon 
	ELSE
		EXEC(''SELECT [ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName] FROM SocoShop_UserCoupon WHERE ''+ @condition)


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserCoupon]
@id int,
@isUse int,
@orderID int,
@userID int,
@userName nvarchar(50)
AS 
	UPDATE SocoShop_UserCoupon Set [IsUse]=@isUse,[OrderID]=@orderID,[UserID]=@userID,[UserName]=@userName WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserCoupon]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName] FROM SocoShop_UserCoupon WHERE [ID]=@id
	ELSE
		SELECT [ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName] FROM SocoShop_UserCoupon WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserCoupon]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserCoupon]
@couponID int,
@getType int,
@number nvarchar(50),
@password nvarchar(50),
@isUse int,
@orderID int,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_UserCoupon([CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName]) VALUES(@couponID,@getType,@number,@password,@isUse,@orderID,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserCouponByNumber]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserCouponByNumber]
@number nvarchar(50),
@password nvarchar(50)
AS 
	SELECT [ID],[CouponID],[GetType],[Number],[Password],[IsUse],[OrderID],[UserID],[UserName] FROM SocoShop_UserCoupon WHERE [Number]=@number AND [Password]=@password

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductCollectByProductID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductCollectByProductID]
@productID int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[ProductID],[Date],[UserID],[UserName] FROM SocoShop_ProductCollect WHERE [ProductID]=@productID
	ELSE
		SELECT [ID],[ProductID],[Date],[UserID],[UserName] FROM SocoShop_ProductCollect WHERE [ProductID]=@productID AND [UserID]=@userID

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadProductCollect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadProductCollect]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[ProductID],[Date],[UserID],[UserName] FROM SocoShop_ProductCollect WHERE [ID]=@id
	ELSE
		SELECT [ID],[ProductID],[Date],[UserID],[UserName] FROM SocoShop_ProductCollect WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateProductCollect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateProductCollect]
@id int,
@productID int
AS 
	UPDATE SocoShop_ProductCollect Set [ProductID]=@productID WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_HasCollectProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_HasCollectProduct]
@productID int,
@userID int 
AS
  SELECT [ID] FROM SocoShop_ProductCollect WHERE [ProductID]=@productID AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddProductCollect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddProductCollect]
@productID int,
@date datetime,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_ProductCollect([ProductID],[Date],[UserID],[UserName]) VALUES(@productID,@date,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeProductCollectCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeProductCollectCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_Product SET [CollectCount]=[CollectCount]+1 WHERE [ID] IN (SELECT [ProductID] FROM SocoShop_ProductCollect WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_Product SET [CollectCount]=[CollectCount]-1 WHERE [ID] IN (SELECT [ProductID] FROM SocoShop_ProductCollect WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadTags]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadTags]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[ProductID],[Word],[Color],[Size],[IsTop],[UserID],[UserName] FROM SocoShop_Tags WHERE [ID]=@id
	ELSE
		SELECT [ID],[ProductID],[Word],[Color],[Size],[IsTop],[UserID],[UserName] FROM SocoShop_Tags WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateTagsIsTop]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateTagsIsTop]
@id int,
@isTop int
AS 
	UPDATE SocoShop_Tags Set [IsTop]=@isTop WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateTags]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateTags]
@id int,
@productID int,
@word nvarchar(50),
@color nvarchar(20),
@size int,
@isTop int
AS 
	UPDATE SocoShop_Tags Set [ProductID]=@productID,[Word]=@word,[Color]=@color,[Size]=@size,[IsTop]=@isTop WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchTagsList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchTagsList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[ProductID],[Word],[Color],[Size],[IsTop],[UserID],[UserName] FROM SocoShop_Tags 
	ELSE
		EXEC(''SELECT [ID],[ProductID],[Word],[Color],[Size],[IsTop],[UserID],[UserName] FROM SocoShop_Tags WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddTags]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddTags]
@productID int,
@word nvarchar(50),
@color nvarchar(20),
@size int,
@isTop int,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_Tags([ProductID],[Word],[Color],[Size],[IsTop],[UserID],[UserName]) VALUES(@productID,@word,@color,@size,@isTop,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_CheckTagsExsits]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_CheckTagsExsits]
@word nvarchar(100),
@productID int,
@tagsID int
AS 
IF @tagsID=0 
	 SELECT [ID] FROM SocoShop_Tags WHERE [ProductID] =@productID AND [Word] =@word
ELSE
	 SELECT [ID] FROM SocoShop_Tags WHERE [ProductID] =@productID AND [Word] =@word AND [ID]!=@tagsID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeLinkOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeLinkOrder]
@action nvarchar(100),
@id int
AS 
	DECLARE @tempID int
	SET @tempID= 0
	DECLARE @tempOrder int
	SET @tempOrder= 0
	DECLARE @needChange bit 
	SET @needChange=0
	DECLARE @orderID int
	SET @orderID=0
	SELECT @orderID=[OrderID] FROM SocoShop_Link WHERE [ID]= @id
	IF @orderID=0
		RETURN			
	IF @action = ''Up''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_Link WHERE [OrderID]<@orderID ORDER BY [OrderID] DESC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @action = ''Down''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_Link WHERE [OrderID]>@orderID ORDER BY [OrderID] ASC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @needChange=1
		BEGIN
		UPDATE SocoShop_Link SET [OrderID]=@tempOrder  WHERE [ID]=@id
		UPDATE SocoShop_Link SET [OrderID]=@orderID  WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateLink]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateLink]
@id int,
@linkClass int,
@display nvarchar(100),
@uRL nvarchar(200),
@remark nvarchar(200)
AS 
	UPDATE SocoShop_Link Set [LinkClass]=@linkClass,[Display]=@display,[URL]=@uRL,[Remark]=@remark WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddLink]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddLink]
@linkClass int, 
@display nvarchar(100),
@uRL nvarchar(200),
@orderID int, 
@remark nvarchar(200)
AS 
	DECLARE @maxID int
	SELECT @maxID=MAX([OrderID]) FROM SocoShop_Link
	IF @maxID IS NULL	 
		SET @orderID= 1
        ELSE
		SET @orderID= @maxID+ 1
	INSERT INTO SocoShop_Link([LinkClass],[Display],[URL],[OrderID],[Remark]) VALUES(@linkClass,@display,@uRL,@orderID,@remark)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadLink]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadLink]
@id int
AS 
	SELECT [ID],[LinkClass],[Display],[URL],[OrderID],[Remark] FROM SocoShop_Link WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadLinkAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadLinkAllList]
AS
	SELECT [ID],[LinkClass],[Display],[URL],[OrderID],[Remark] FROM SocoShop_Link ORDER BY [OrderID]
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserMessageByTime]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserMessageByTime]
@userIP nvarchar(50),        
@userID int,
@time datetime
as
		SELECT [ID] FROM SocoShop_UserMessage WHERE ([UserID]=@userID OR [UserIP]=@userIP) AND [PostDate]>@time
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserMessage]
@messageClass int,
@title nvarchar(100),
@content ntext,
@userIP nvarchar(40),
@postDate datetime,
@isHandler int,
@adminReplyContent ntext,
@adminReplyDate datetime,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_UserMessage([MessageClass],[Title],[Content],[UserIP],[PostDate],[IsHandler],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName]) VALUES(@messageClass,@title,@content,@userIP,@postDate,@isHandler,@adminReplyContent,@adminReplyDate,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserMessage]
@id int,
@isHandler int,
@adminReplyContent ntext,
@adminReplyDate datetime
AS 
	UPDATE SocoShop_UserMessage Set [IsHandler]=@isHandler,[AdminReplyContent]=@adminReplyContent,[AdminReplyDate]=@adminReplyDate WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserMessage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserMessage]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[MessageClass],[Title],[Content],[UserIP],[PostDate],[IsHandler],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_UserMessage WHERE [ID]=@id
	ELSE
		SELECT [ID],[MessageClass],[Title],[Content],[UserIP],[PostDate],[IsHandler],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_UserMessage WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchUserMessageList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchUserMessageList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[MessageClass],[Title],[Content],[UserIP],[PostDate],[IsHandler],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_UserMessage 
	ELSE
		EXEC(''SELECT [ID],[MessageClass],[Title],[Content],[UserIP],[PostDate],[IsHandler],[AdminReplyContent],[AdminReplyDate],[UserID],[UserName] FROM SocoShop_UserMessage WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddVoteRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddVoteRecord]
@voteID int,
@itemID nvarchar(50),
@userIP nvarchar(40),
@addDate datetime,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_VoteRecord([VoteID],[ItemID],[UserIP],[AddDate],[UserID],[UserName]) VALUES(@voteID,@itemID,@userIP,@addDate,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadVoteRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadVoteRecord]
@id int
AS 
	SELECT [ID],[VoteID],[ItemID],[UserIP],[AddDate],[UserID],[UserName] FROM SocoShop_VoteRecord WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeVoteItemCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeVoteItemCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 
	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		DECLARE @itemID nvarchar(200)
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					SELECT @itemID=[ItemID] FROM SocoShop_VoteRecord WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1))
					IF @itemID!=''''
						BEGIN
							IF @action=''Plus''
								EXEC(''UPDATE SocoShop_VoteItem SET [VoteCount]=[VoteCount]+1 WHERE [ID] IN (''+@itemID+'')'')
							ELSE
								EXEC(''UPDATE SocoShop_VoteItem SET [VoteCount]=[VoteCount]-1 WHERE [ID] IN (''+@itemID+'')'')
						END
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateVoteRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateVoteRecord]
@id int,
@userIP nvarchar(40),
@addDate datetime
AS 
	UPDATE SocoShop_VoteRecord Set [UserIP]=@userIP,[AddDate]=@addDate WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUser]
@id int,
@sex int,
@introduce ntext,
@photo nvarchar(250),
@mSN nvarchar(50),
@qQ nvarchar(50),
@tel nvarchar(50),
@mobile nvarchar(50),
@regionID nvarchar(100),
@address nvarchar(250),
@birthday nvarchar(50),
@status int,
@email nvarchar(50)
AS 
	UPDATE SocoShop_User Set [Sex]=@sex,[Introduce]=@introduce,[Photo]=@photo,[MSN]=@mSN,[QQ]=@qQ,[Tel]=@tel,[Mobile]=@mobile,[RegionID]=@regionID,[Address]=@address,[Birthday]=@birthday,[Status]=@status,[Email]=@email WHERE [ID]=@id

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUser]
@id int
AS 
	SELECT [ID],[UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID] FROM SocoShop_User WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchUserList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchUserList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID] FROM SocoShop_User 
	ELSE
		EXEC(''SELECT [ID],[UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID] FROM SocoShop_User WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserByOpenID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserByOpenID]
@openID nvarchar(50)
AS 
		SELECT [ID],[UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID] FROM SocoShop_User WHERE [OpenID]=@openID


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUser]
@userName nvarchar(50),
@userPassword nvarchar(50),
@email nvarchar(50),
@sex int,
@introduce ntext,
@photo nvarchar(250),
@mSN nvarchar(50),
@qQ nvarchar(50),
@tel nvarchar(50),
@mobile nvarchar(50),
@regionID nvarchar(100),
@address nvarchar(250),
@birthday nvarchar(50),
@registerIP nvarchar(40),
@registerDate datetime,
@lastLoginIP nvarchar(40),
@lastLoginDate datetime,
@loginTimes int,
@safeCode nvarchar(50),
@findDate datetime,
@status int,
@openID nvarchar(50)
AS 
	INSERT INTO SocoShop_User([UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID]) VALUES(@userName,@userPassword,@email,@sex,@introduce,@photo,@mSN,@qQ,@tel,@mobile,@regionID,@address,@birthday,@registerIP,@registerDate,@lastLoginIP,@lastLoginDate,@loginTimes,@safeCode,@findDate,@status,@openID)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserByUserName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_ReadUserByUserName]
@userName nvarchar(50)
AS 
	SELECT [ID],[UserName],[UserPassword],[Email],[Sex],[Introduce],[Photo],[MSN],[QQ],[Tel],[Mobile],[RegionID],[Address],[Birthday],[RegisterIP],[RegisterDate],[LastLoginIP],[LastLoginDate],[LoginTimes],[SafeCode],[FindDate],[Status],[OpenID] FROM SocoShop_User WHERE [UserName]=@userName


' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddBookingProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddBookingProduct]
@productID int,
@productName nvarchar(400),
@relationUser nvarchar(50),
@email nvarchar(50),
@tel nvarchar(50),
@userNote nvarchar(100),
@bookingDate datetime,
@bookingIP nvarchar(40),
@isHandler int,
@handlerDate datetime,
@handlerAdminID int,
@handlerAdminName nvarchar(50),
@handlerNote nvarchar(100),
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_BookingProduct([ProductID],[ProductName],[RelationUser],[Email],[Tel],[UserNote],[BookingDate],[BookingIP],[IsHandler],[HandlerDate],[HandlerAdminID],[HandlerAdminName],[HandlerNote],[UserID],[UserName]) VALUES(@productID,@productName,@relationUser,@email,@tel,@userNote,@bookingDate,@bookingIP,@isHandler,@handlerDate,@handlerAdminID,@handlerAdminName,@handlerNote,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchBookingProductList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchBookingProductList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[ProductID],[ProductName],[RelationUser],[Email],[Tel],[UserNote],[BookingDate],[BookingIP],[IsHandler],[HandlerDate],[HandlerAdminID],[HandlerAdminName],[HandlerNote],[UserID],[UserName] FROM SocoShop_BookingProduct 
	ELSE
		EXEC(''SELECT [ID],[ProductID],[ProductName],[RelationUser],[Email],[Tel],[UserNote],[BookingDate],[BookingIP],[IsHandler],[HandlerDate],[HandlerAdminID],[HandlerAdminName],[HandlerNote],[UserID],[UserName] FROM SocoShop_BookingProduct WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadBookingProduct]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadBookingProduct]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[ProductID],[ProductName],[RelationUser],[Email],[Tel],[UserNote],[BookingDate],[BookingIP],[IsHandler],[HandlerDate],[HandlerAdminID],[HandlerAdminName],[HandlerNote],[UserID],[UserName] FROM SocoShop_BookingProduct WHERE [ID]=@id
	ELSE
		SELECT [ID],[ProductID],[ProductName],[RelationUser],[Email],[Tel],[UserNote],[BookingDate],[BookingIP],[IsHandler],[HandlerDate],[HandlerAdminID],[HandlerAdminName],[HandlerNote],[UserID],[UserName] FROM SocoShop_BookingProduct WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddVoteItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddVoteItem]
@voteID int, 
@itemName nvarchar(50),
@voteCount int, 
@orderID int
AS 
	DECLARE @maxID int
	SELECT @maxID=MAX([OrderID]) FROM SocoShop_VoteItem
	IF @maxID IS NULL	 
		SET @orderID= 1
        ELSE
		SET @orderID= @maxID+ 1
	INSERT INTO SocoShop_VoteItem([VoteID],[ItemName],[VoteCount],[OrderID]) VALUES(@voteID,@itemName,@voteCount,@orderID)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeVoteCountByGeneral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeVoteCountByGeneral]
@strID nvarchar(800),
@action nvarchar(10)
AS 

	DECLARE @strList nvarchar(200)
	DECLARE @seprate nvarchar(10)
	SET @seprate='',''
	SET @strList=@strID
	IF SUBSTRING(@strList,0,LEN(@strList)-1)!='',''
		SET @strList=@strList+'',''
		DECLARE @i int
		SET @strList=RTRIM(LTRIM(@strList))
		SET @i=CHARINDEX(@seprate,@strList)
		WHILE @i>=1
			BEGIN
				BEGIN
					IF @action=''Plus''
						UPDATE SocoShop_Vote SET [ItemCount]=[ItemCount]+1 WHERE [ID] IN (SELECT [VoteID] FROM SocoShop_VoteItem WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
					ELSE
						UPDATE SocoShop_Vote SET [ItemCount]=[ItemCount]-1 WHERE [ID] IN (SELECT [VoteID] FROM SocoShop_VoteItem WHERE [ID]=CONVERT(int,LEFT(@strList,@i-1)))
				END
				SET @strList=SUBSTRING(@strList,@i+1,LEN(@strList)-@i)
				SET @i=CHARINDEX(@seprate,@strList)
			END

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateVoteItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateVoteItem]
@id int,
@itemName nvarchar(50)
AS 
	UPDATE SocoShop_VoteItem Set [ItemName]=@itemName WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeVoteItemOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeVoteItemOrder]
@action nvarchar(100),
@id int
AS 
	DECLARE @tempID int
	SET @tempID= 0
	DECLARE @tempOrder int
	SET @tempOrder= 0
	DECLARE @needChange bit 
	SET @needChange=0
	DECLARE @orderID int
	SET @orderID=0
	SELECT @orderID=[OrderID] FROM SocoShop_VoteItem WHERE [ID]= @id
	IF @orderID=0
		RETURN			
	IF @action = ''Up''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_VoteItem WHERE [OrderID]<@orderID ORDER BY [OrderID] DESC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @action = ''Down''
		BEGIN
		SELECT TOP 1 @tempID=[ID],@tempOrder=[OrderID] FROM SocoShop_VoteItem WHERE [OrderID]>@orderID ORDER BY [OrderID] ASC
		IF @tempID>0
			SET @needChange = 1
		END

	IF @needChange=1
		BEGIN
		UPDATE SocoShop_VoteItem SET [OrderID]=@tempOrder  WHERE [ID]=@id
		UPDATE SocoShop_VoteItem SET [OrderID]=@orderID  WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadVoteItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadVoteItem]
@id int
AS 
	SELECT [ID],[VoteID],[ItemName],[VoteCount],[OrderID] FROM SocoShop_VoteItem WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadVoteItemByVote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadVoteItemByVote]
@voteID int
AS
	SELECT [ID],[VoteID],[ItemName],[VoteCount],[OrderID] FROM SocoShop_VoteItem WHERE [VoteID]=@voteID ORDER BY [OrderID]
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeVoteCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeVoteCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_Vote SET [ItemCount]=[ItemCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_Vote SET [ItemCount]=[ItemCount]-1 WHERE [ID] = @id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateVote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateVote]
@id int,
@title nvarchar(50),
@voteType int,
@note nvarchar(400)
AS 
	UPDATE SocoShop_Vote Set [Title]=@title,[VoteType]=@voteType,[Note]=@note WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddVote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddVote]
@title nvarchar(50),
@itemCount int,
@voteType int,
@note nvarchar(400)
AS 
	INSERT INTO SocoShop_Vote([Title],[ItemCount],[VoteType],[Note]) VALUES(@title,@itemCount,@voteType,@note)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadVote]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadVote]
@id int
AS 
	SELECT [ID],[Title],[ItemCount],[VoteType],[Note] FROM SocoShop_Vote WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserAddressIsDefault]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SocoShop_UpdateUserAddressIsDefault]
@isDefault int,
@userID int
AS 
	UPDATE SocoShop_UserAddress Set [IsDefault]=@isDefault WHERE [UserID]=@userID  

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddUserAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddUserAddress]
@consignee nvarchar(50),
@regionID nvarchar(100),
@address nvarchar(100),
@zipCode nvarchar(50),
@tel nvarchar(50),
@mobile nvarchar(50),
@isDefault int,
@userID int,
@userName nvarchar(50)
AS 
	INSERT INTO SocoShop_UserAddress([Consignee],[RegionID],[Address],[ZipCode],[Tel],[Mobile],[IsDefault],[UserID],[UserName]) VALUES(@consignee,@regionID,@address,@zipCode,@tel,@mobile,@isDefault,@userID,@userName)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateUserAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateUserAddress]
@id int,
@consignee nvarchar(50),
@regionID nvarchar(100),
@address nvarchar(100),
@zipCode nvarchar(50),
@tel nvarchar(50),
@mobile nvarchar(50),
@isDefault int
AS 
	UPDATE SocoShop_UserAddress Set [Consignee]=@consignee,[RegionID]=@regionID,[Address]=@address,[ZipCode]=@zipCode,[Tel]=@tel,[Mobile]=@mobile,[IsDefault]=@isDefault WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserAddress]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserAddress]
@id int,
@userID int
AS 
	IF @userID=0
		SELECT [ID],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Mobile],[IsDefault],[UserID],[UserName] FROM SocoShop_UserAddress WHERE [ID]=@id
	ELSE
		SELECT [ID],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Mobile],[IsDefault],[UserID],[UserName] FROM SocoShop_UserAddress WHERE [ID]=@id AND [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadUserAddressByUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadUserAddressByUser]
@userID int
AS
	SELECT [ID],[Consignee],[RegionID],[Address],[ZipCode],[Tel],[Mobile],[IsDefault],[UserID],[UserName] FROM SocoShop_UserAddress WHERE [UserID]=@userID
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_DeleteArticleClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_DeleteArticleClass]
@id int
AS 
	 DECLARE @temp int
	 SELECT @temp=COUNT(*) FROM SocoShop_ArticleClass WHERE [FatherID]=@id 
	 IF @temp=0
	 	DELETE FROM SocoShop_ArticleClass WHERE [ID]=@id
		
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveDownArticleClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_MoveDownArticleClass]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_ArticleClass WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_ArticleClass WHERE [OrderID]>@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] ASC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_ArticleClass SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_ArticleClass SET [OrderID]=@orderID WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MoveUpArticleClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_MoveUpArticleClass]
@id int
AS 
	DECLARE @tempID int
	DECLARE @tempOrderID int
	DECLARE @orderID int
	DECLARE @fatherID int
	SELECT @orderID=[OrderID],@fatherID=[FatherID] FROM SocoShop_ArticleClass WHERE [ID]=@id
	SELECT TOP 1 @tempID=[ID],@tempOrderID=[OrderID] FROM SocoShop_ArticleClass WHERE [OrderID]<@orderID AND [FatherID]=@fatherID ORDER BY [OrderID] DESC

	IF @tempID is null
		RETURN		
	ELSE
		BEGIN
		UPDATE SocoShop_ArticleClass SET [OrderID]=@tempOrderID WHERE [ID]=@id
		UPDATE SocoShop_ArticleClass SET [OrderID]=@orderID WHERE [ID]=@tempID
		END
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddArticleClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddArticleClass]
@fatherID int,
@orderID int,
@className nvarchar(50),
@description ntext,
@isSystem int
AS 
	INSERT INTO SocoShop_ArticleClass([FatherID],[OrderID],[ClassName],[Description],[IsSystem]) VALUES(@fatherID,@orderID,@className,@description,@isSystem)	
		SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadArticleClassAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadArticleClassAllList]
AS
	SELECT [ID],[FatherID],[OrderID],[ClassName],[Description],[IsSystem] FROM SocoShop_ArticleClass ORDER BY [OrderID] ASC,ID ASC
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateArticleClass]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateArticleClass]
@id int,
@fatherID int,
@orderID int,
@className nvarchar(50),
@description ntext
AS 
	UPDATE SocoShop_ArticleClass Set [FatherID]=@fatherID,[OrderID]=@orderID,[ClassName]=@className,[Description]=@description WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateArticle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateArticle]
@id int,
@title nvarchar(200),
@classID nvarchar(100),
@isTop int,
@author nvarchar(50),
@resource nvarchar(50),
@keywords nvarchar(100),
@url nvarchar(200),
@photo nvarchar(100),
@summary ntext,
@content ntext
AS 
	UPDATE SocoShop_Article Set [Title]=@title,[ClassID]=@classID,[IsTop]=@isTop,[Author]=@author,[Resource]=@resource,[Keywords]=@keywords,[Url]=@url,[Photo]=@photo,[Summary]=@summary,[Content]=@content WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddArticle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddArticle]
@title nvarchar(200),
@classID nvarchar(100),
@isTop int,
@author nvarchar(50),
@resource nvarchar(50),
@keywords nvarchar(100),
@url nvarchar(200),
@photo nvarchar(100),
@summary ntext,
@content ntext,
@date datetime
AS 
	INSERT INTO SocoShop_Article([Title],[ClassID],[IsTop],[Author],[Resource],[Keywords],[Url],[Photo],[Summary],[Content],[Date]) VALUES(@title,@classID,@isTop,@author,@resource,@keywords,@url,@photo,@summary,@content,@date)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SearchArticleList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_SearchArticleList]
@condition nvarchar(4000)
AS 
	IF @condition=''''
		SELECT [ID],[Title],[ClassID],[IsTop],[Author],[Resource],[Keywords],[Url],[Photo],[Summary],[Content],[Date] FROM SocoShop_Article 
	ELSE
		EXEC(''SELECT [ID],[Title],[ClassID],[IsTop],[Author],[Resource],[Keywords],[Url],[Photo],[Summary],[Content],[Date] FROM SocoShop_Article WHERE ''+ @condition)

' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadArticle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadArticle]
@id int
AS 
	SELECT [ID],[Title],[ClassID],[IsTop],[Author],[Resource],[Keywords],[Url],[Photo],[Summary],[Content],[Date] FROM SocoShop_Article WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadAdminGroupAllList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadAdminGroupAllList]
AS
	SELECT [ID],[Name],[Power],[AdminCount],[AddDate],[IP],[Note] FROM SocoShop_AdminGroup 
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateAdminGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateAdminGroup]
@id int,
@name nvarchar(50),
@power ntext,
@note ntext
AS 
	UPDATE SocoShop_AdminGroup Set [Name]=@name,[Power]=@power,[Note]=@note WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddAdminGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddAdminGroup]
@name nvarchar(50),
@power ntext,
@adminCount int,
@addDate datetime,
@iP nvarchar(50),
@note ntext
AS 
	INSERT INTO SocoShop_AdminGroup([Name],[Power],[AdminCount],[AddDate],[IP],[Note]) VALUES(@name,@power,@adminCount,@addDate,@iP,@note)
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ChangeAdminGroupCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ChangeAdminGroupCount]
@id int,
@action nvarchar(10)
AS 
	IF @action=''Plus''
		UPDATE SocoShop_AdminGroup SET [AdminCount]=[AdminCount]+1 WHERE [ID] = @id
	ELSE
		UPDATE SocoShop_AdminGroup SET [AdminCount]=[AdminCount]-1 WHERE [ID] = @id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UpdateThemeActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_UpdateThemeActivity]
@id int,
@name nvarchar(100),
@photo nvarchar(100),
@description ntext,
@css ntext,
@productGroup nvarchar(2000),
@style nvarchar(2000)
AS 
	UPDATE SocoShop_ThemeActivity Set [Name]=@name,[Photo]=@photo,[Description]=@description,[Css]=@css,[ProductGroup]=@productGroup,[Style]=@style WHERE [ID]=@id
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AddThemeActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_AddThemeActivity]
@name nvarchar(100),
@photo nvarchar(100),
@description ntext,
@css ntext,
@productGroup nvarchar(2000),
@style nvarchar(2000)
AS 
	INSERT INTO SocoShop_ThemeActivity([Name],[Photo],[Description],[Css],[ProductGroup],[Style]) VALUES(@name,@photo,@description,@css,@productGroup,@style)
	SELECT @@identity
' 
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReadThemeActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SocoShop_ReadThemeActivity]
@id int
AS 
	SELECT [ID],[Name],[Photo],[Description],[Css],[ProductGroup],[Style] FROM SocoShop_ThemeActivity WHERE [ID]=@id
' 
END
