



IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_View_SaleDetail]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[SocoShop_View_SaleDetail]
AS
SELECT     dbo.SocoShop_OrderDetail.ID, dbo.SocoShop_OrderDetail.ProductID, dbo.SocoShop_Product.Name, dbo.SocoShop_Product.ClassID, dbo.SocoShop_Product.BrandID, 
                      dbo.SocoShop_OrderDetail.BuyCount, dbo.SocoShop_OrderDetail.ProductPrice * dbo.SocoShop_OrderDetail.BuyCount AS Money, dbo.SocoShop_Order.OrderNumber, 
                      dbo.SocoShop_Order.AddDate, dbo.SocoShop_Order.UserName
FROM         dbo.SocoShop_OrderDetail INNER JOIN
                      dbo.SocoShop_Product ON dbo.SocoShop_OrderDetail.ProductID = dbo.SocoShop_Product.ID INNER JOIN
                      dbo.SocoShop_Order ON dbo.SocoShop_OrderDetail.OrderID = dbo.SocoShop_Order.ID
WHERE     (dbo.SocoShop_Order.OrderStatus = 6)
' 




IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_View_UserActive]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[SocoShop_View_UserActive]
AS
SELECT     dbo.SocoShop_User.ID, dbo.SocoShop_User.UserName, dbo.SocoShop_User.RegisterDate, dbo.SocoShop_User.LoginTimes, ISNULL(TEMP2.CommentCount, 0) 
                      AS CommentCount, ISNULL(TEMP3.ReplyCount, 0) AS ReplyCount, ISNULL(TEMP4.MessageCount, 0) AS MessageCount, dbo.SocoShop_User.Sex
FROM         dbo.SocoShop_User LEFT OUTER JOIN
                          (SELECT     UserID, COUNT(*) AS CommentCount
                            FROM          dbo.SocoShop_ProductComment
                            GROUP BY UserID) AS TEMP2 ON dbo.SocoShop_User.ID = TEMP2.UserID LEFT OUTER JOIN
                          (SELECT     UserID, COUNT(*) AS ReplyCount
                            FROM          dbo.SocoShop_ProductReply
                            GROUP BY UserID) AS TEMP3 ON dbo.SocoShop_User.ID = TEMP3.UserID LEFT OUTER JOIN
                          (SELECT     UserID, COUNT(*) AS MessageCount
                            FROM          dbo.SocoShop_UserMessage
                            GROUP BY UserID) AS TEMP4 ON dbo.SocoShop_User.ID = TEMP4.UserID
' 
