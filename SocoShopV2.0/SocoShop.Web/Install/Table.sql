



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Menu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FatherID] [int] NULL,
	[OrderID] [int] NULL,
	[MenuName] [nvarchar](50) NULL,
	[MenuImage] [int] NULL,
	[URL] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
	[IP] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ProductBrand]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ProductBrand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Logo] [nvarchar](100) NULL,
	[Url] [nvarchar](200) NULL,
	[Description] [ntext] NULL,
	[OrderID] [int] NULL,
	[IsTop] [int] NULL,
	[ProductCount] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Attribute]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Attribute](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[AttributeClassID] [int] NULL,
	[InputType] [int] NULL,
	[InputValue] [nvarchar](200) NULL,
	[OrderID] [int] NULL,
 CONSTRAINT [PK_Sky_Shop_ATTRIBUTE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_SendMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_SendMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Content] [ntext] NULL,
	[Date] [datetime] NULL,
	[ToUserID] [ntext] NULL,
	[ToUserName] [ntext] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[IsAdmin] [int] NULL,
 CONSTRAINT [PK_Sky_Shop_GroupMessage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AttributeRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_AttributeRecord](
	[AttributeID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Value] [nvarchar](100) NULL
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ReceiveMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ReceiveMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [ntext] NULL,
	[Date] [datetime] NULL,
	[IsRead] [int] NULL,
	[IsAdmin] [int] NULL,
	[FromUserID] [int] NULL,
	[FromUserName] [nvarchar](50) NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_Message_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Order]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](50) NOT NULL,
	[IsActivity] [int] NULL,
	[OrderStatus] [int] NOT NULL,
	[OrderNote] [nvarchar](50) NULL,
	[ProductMoney] [decimal](15, 2) NOT NULL,
	[Balance] [decimal](15, 2) NOT NULL,
	[FavorableMoney] [decimal](15, 2) NOT NULL,
	[OtherMoney] [decimal](15, 2) NOT NULL,
	[CouponMoney] [decimal](15, 2) NOT NULL,
	[Consignee] [nvarchar](50) NULL,
	[RegionID] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[ShippingID] [int] NOT NULL,
	[ShippingDate] [datetime] NOT NULL,
	[ShippingNumber] [nvarchar](50) NULL,
	[ShippingMoney] [decimal](15, 2) NOT NULL,
	[PayKey] [nvarchar](50) NULL,
	[PayName] [nvarchar](50) NOT NULL,
	[PayDate] [datetime] NULL,
	[IsRefund] [int] NULL,
	[FavorableActivityID] [int] NULL,
	[GiftID] [int] NOT NULL,
	[InvoiceTitle] [nvarchar](100) NULL,
	[InvoiceContent] [nvarchar](200) NULL,
	[UserMessage] [nvarchar](500) NULL,
	[AddDate] [datetime] NOT NULL,
	[IP] [nvarchar](40) NULL,
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_ORDER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Region]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Region](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FatherID] [int] NULL,
	[OrderID] [int] NULL,
	[RegionName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_REGION] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_MemberPrice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_MemberPrice](
	[ProductID] [int] NOT NULL,
	[GradeID] [int] NOT NULL,
	[Price] [decimal](15, 2) NOT NULL
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AttributeClass]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_AttributeClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[AttributeCount] [int] NULL CONSTRAINT [DF_Sky_Shop_ProductType_AttributeCount]  DEFAULT ((0))
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_OrderAction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_OrderAction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[OrderOperate] [int] NOT NULL,
	[StartOrderStatus] [int] NOT NULL,
	[EndOrderStatus] [int] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[IP] [nvarchar](40) NOT NULL,
	[Date] [datetime] NOT NULL,
	[AdminID] [int] NOT NULL,
	[AdminName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Sky_Shop_OrderAction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserApply]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserApply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](50) NULL,
	[Money] [decimal](15, 2) NULL,
	[UserNote] [nvarchar](4000) NULL,
	[Status] [int] NULL,
	[ApplyDate] [datetime] NULL,
	[ApplyIP] [nvarchar](40) NULL,
	[AdminNote] [nvarchar](500) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateAdminID] [int] NULL,
	[UpdateAdminName] [nvarchar](50) NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_XWX_User_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Coupon]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Coupon](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Money] [decimal](18, 2) NULL,
	[UseMinAmount] [decimal](18, 2) NULL,
	[UseStartDate] [datetime] NULL,
	[UseEndDate] [datetime] NULL,
 CONSTRAINT [PK_Sky_Shop_BONUS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserFriend]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserFriend](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FriendID] [int] NULL,
	[FriendName] [nvarchar](50) NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_UserCollection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Cart]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Cart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](200) NULL,
	[BuyCount] [int] NOT NULL,
	[FatherID] [int] NOT NULL CONSTRAINT [DF_Sky_Shop_Cart_FatherID]  DEFAULT ((0)),
	[RandNumber] [nvarchar](50) NULL,
	[GiftPackID] [int] NOT NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_CART] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_FlashPhoto]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_FlashPhoto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FlashID] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[FileName] [nvarchar](100) NULL,
	[URL] [nvarchar](100) NULL,
	[OrderID] [int] NULL,
	[Date] [datetime] NULL
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Standard]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Standard](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DisplayTye] [int] NOT NULL,
	[ValueList] [nvarchar](500) NULL,
	[PhotoList] [nvarchar](500) NULL
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Gift]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Gift](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Photo] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_Sky_Shop_APPENDANT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_StandardRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_StandardRecord](
	[ProductID] [int] NULL,
	[StandardIDList] [nvarchar](500) NULL,
	[ValueList] [nvarchar](500) NULL,
	[GroupTag] [nvarchar](500) NULL
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_OrderDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_OrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](200) NULL,
	[ProductWeight] [decimal](18, 2) NOT NULL,
	[SendPoint] [int] NOT NULL,
	[ProductPrice] [decimal](18, 2) NOT NULL,
	[BuyCount] [int] NOT NULL,
	[FatherID] [int] NOT NULL,
	[RandNumber] [nvarchar](50) NULL,
	[GiftPackID] [int] NULL,
 CONSTRAINT [PK_Sky_Shop_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Flash]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Flash](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Introduce] [ntext] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[PhotoCount] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AdRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_AdRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AdID] [int] NULL,
	[IP] [nvarchar](40) NULL,
	[Date] [datetime] NULL,
	[Page] [nvarchar](100) NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_AdRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AdminLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_AdminLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupID] [int] NULL,
	[Action] [nvarchar](200) NULL,
	[IP] [nvarchar](40) NULL,
	[AddDate] [datetime] NULL,
	[AdminID] [int] NULL,
	[AdminName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_AdminLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Ad]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Ad](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Introduction] [ntext] NULL,
	[AdClass] [int] NULL,
	[Display] [ntext] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Url] [nvarchar](200) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Remark] [nvarchar](200) NULL,
	[ClickCount] [int] NULL,
	[IsEnabled] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserAccountRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserAccountRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Money] [decimal](15, 2) NULL,
	[Point] [int] NULL,
	[Date] [datetime] NULL,
	[IP] [nvarchar](50) NULL,
	[Note] [nvarchar](50) NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_UserAccountRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ProductReply]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ProductReply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[CommentID] [int] NULL,
	[Content] [ntext] NULL,
	[UserIP] [nvarchar](40) NULL,
	[PostDate] [datetime] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_Reply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Spelling] [nvarchar](1000) NULL,
	[Color] [nvarchar](50) NULL,
	[FontStyle] [nvarchar](50) NULL,
	[ProductNumber] [nvarchar](50) NULL,
	[ClassID] [nvarchar](500) NULL,
	[BrandID] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_BrandID]  DEFAULT ((0)),
	[MarketPrice] [decimal](18, 2) NOT NULL CONSTRAINT [DF_SocoShop_Product_MarketPrice]  DEFAULT ((0)),
	[Weight] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_Weight]  DEFAULT ((0)),
	[SendPoint] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_SendPoint]  DEFAULT ((0)),
	[Photo] [nvarchar](100) NULL,
	[Keywords] [nvarchar](200) NULL,
	[Summary] [ntext] NULL,
	[Introduction] [ntext] NULL,
	[Remark] [text] NULL,
	[IsSpecial] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_IsSpecial]  DEFAULT ((0)),
	[IsNew] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_IsNew]  DEFAULT ((0)),
	[IsHot] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_IsHot]  DEFAULT ((0)),
	[IsSale] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_IsSale]  DEFAULT ((0)),
	[IsTop] [int] NOT NULL CONSTRAINT [DF_Sky_Shop_ProductSale_IsTop]  DEFAULT ((0)),
	[Accessory] [nvarchar](500) NULL,
	[RelationProduct] [nvarchar](500) NULL,
	[RelationArticle] [nvarchar](500) NULL,
	[ViewCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_ViewCount]  DEFAULT ((0)),
	[AllowComment] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_AllowComment]  DEFAULT ((0)),
	[CommentCount] [int] NOT NULL CONSTRAINT [DF_Sky_Shop_ProductSale_CommentCount]  DEFAULT ((0)),
	[SumPoint] [int] NOT NULL CONSTRAINT [DF_Sky_Shop_ProductSale_SumPoint]  DEFAULT ((0)),
	[PerPoint] [decimal](15, 2) NOT NULL CONSTRAINT [DF_SocoShop_Product_PerPoint]  DEFAULT ((0)),
	[PhotoCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_PhotoCount]  DEFAULT ((0)),
	[CollectCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_CollectCount]  DEFAULT ((0)),
	[TotalStorageCount] [int] NOT NULL CONSTRAINT [DF_Sky_Shop_ProductSale_ProductCount]  DEFAULT ((0)),
	[OrderCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_OrderCount]  DEFAULT ((0)),
	[SendCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_SendCount]  DEFAULT ((0)),
	[ImportActualStorageCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_ImportActualStorageCount]  DEFAULT ((0)),
	[ImportVirtualStorageCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_ImportVirtualStorageCount]  DEFAULT ((0)),
	[LowerCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_LowerCount]  DEFAULT ((0)),
	[UpperCount] [int] NOT NULL CONSTRAINT [DF_SocoShop_Product_UpperCount]  DEFAULT ((0)),
	[AttributeClassID] [int] NOT NULL CONSTRAINT [DF_Sky_Shop_ProductSale_ProductTypeID]  DEFAULT ((0)),
	[StandardType] [int] NULL CONSTRAINT [DF_SocoShop_Product_StandardType]  DEFAULT ((0)),
	[AddDate] [datetime] NOT NULL,
	[TaobaoID] [bigint] NOT NULL CONSTRAINT [DF_SocoShop_Product_TaobaoID]  DEFAULT ((0)),
 CONSTRAINT [PK_Sky_Shop_ProductSale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Admin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Admin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[GroupID] [int] NULL,
	[Password] [nvarchar](50) NULL,
	[LastLoginIP] [nvarchar](40) NULL,
	[LastLoginDate] [datetime] NULL,
	[LoginTimes] [int] NULL,
	[NoteBook] [ntext] NULL,
	[IsCreate] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ProductComment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ProductComment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Title] [nvarchar](500) NULL,
	[Content] [ntext] NULL,
	[UserIP] [nvarchar](50) NULL,
	[PostDate] [datetime] NULL,
	[Support] [int] NULL,
	[Against] [int] NULL,
	[Status] [int] NULL,
	[Rank] [int] NULL,
	[ReplyCount] [int] NULL,
	[AdminReplyContent] [nvarchar](500) NULL,
	[AdminReplyDate] [datetime] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_PRODUCTCOMMENT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserRecharge]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserRecharge](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[Money] [decimal](18, 2) NOT NULL,
	[PayKey] [nvarchar](50) NULL,
	[PayName] [nvarchar](50) NOT NULL,
	[RechargeDate] [datetime] NOT NULL,
	[RechargeIP] [nvarchar](50) NOT NULL,
	[IsFinish] [int] NULL,
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Sky_Shop_UserRecharge] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Shipping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Shipping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [ntext] NULL,
	[IsEnabled] [int] NULL,
	[ShippingType] [int] NULL,
	[FirstWeight] [int] NULL,
	[AgainWeight] [int] NULL,
	[OrderID] [int] NULL,
 CONSTRAINT [PK_Sky_Shop_Shipping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ProductPhoto]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ProductPhoto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Photo] [nvarchar](100) NULL,
 CONSTRAINT [PK_Sky_Shop_PRODURCTALBUM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ShippingRegion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ShippingRegion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[ShippingID] [int] NULL,
	[RegionID] [nvarchar](500) NULL,
	[FixedMoeny] [decimal](18, 2) NULL,
	[FirstMoney] [decimal](18, 2) NULL,
	[AgainMoney] [decimal](18, 2) NULL,
	[OneMoeny] [decimal](18, 2) NULL,
	[AnotherMoeny] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Sky_Shop_ShippingArea] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_EmailSendRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_EmailSendRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [ntext] NULL,
	[IsSystem] [int] NULL,
	[EmailList] [ntext] NULL,
	[OpenEmailList] [varchar](max) NULL CONSTRAINT [DF_Sky_Shop_EmailSend_OpenEmailList]  DEFAULT (N','),
	[IsStatisticsOpendEmail] [int] NULL,
	[SendStatus] [int] NULL,
	[Note] [nvarchar](500) NULL,
	[AddDate] [datetime] NULL,
	[SendDate] [datetime] NULL,
 CONSTRAINT [PK_Sky_Shop_EmailSend] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ProductClass]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ProductClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FatherID] [int] NULL,
	[OrderID] [int] NULL,
	[ClassName] [nvarchar](50) NULL,
	[Keywords] [nvarchar](200) NULL,
	[Description] [text] NULL,
	[TaobaoID] [bigint] NULL,
 CONSTRAINT [PK_SocoShop_ProductClass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserGrade]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserGrade](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[MinMoney] [decimal](15, 2) NULL,
	[MaxMoney] [decimal](15, 2) NULL,
	[Discount] [decimal](15, 2) NULL
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_FavorableActivity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_FavorableActivity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Photo] [nvarchar](200) NULL,
	[Content] [ntext] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[UserGrade] [nvarchar](50) NULL,
	[OrderProductMoney] [decimal](18, 2) NULL,
	[RegionID] [nvarchar](500) NULL,
	[ShippingWay] [int] NULL,
	[ReduceWay] [int] NULL,
	[ReduceMoney] [decimal](18, 2) NULL,
	[ReduceDiscount] [decimal](18, 2) NULL,
	[GiftID] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sky_Shop_FavorableActivity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_GiftPack]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_GiftPack](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Photo] [nvarchar](200) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Price] [decimal](15, 2) NULL,
	[GiftGroup] [ntext] NULL,
 CONSTRAINT [PK_Sky_Shop_PRODUCTGROUP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Upload]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Upload](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableID] [int] NULL,
	[ClassID] [int] NULL,
	[RecordID] [int] NULL,
	[UploadName] [nvarchar](100) NULL,
	[OtherFile] [nvarchar](500) NULL,
	[Size] [int] NULL,
	[FileType] [nvarchar](50) NULL,
	[RandomNumber] [nvarchar](100) NULL,
	[Date] [datetime] NULL,
	[IP] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_ProductUpload] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserCoupon]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserCoupon](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CouponID] [int] NOT NULL,
	[GetType] [int] NOT NULL,
	[Number] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IsUse] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_USERBONUS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ProductCollect]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ProductCollect](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Date] [datetime] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_PRODUCTCOLLECT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Tags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Word] [nvarchar](50) NULL,
	[Color] [nvarchar](20) NULL,
	[Size] [int] NULL CONSTRAINT [DF_Sky_Shop_Tags_Size]  DEFAULT ((12)),
	[IsTop] [int] NULL CONSTRAINT [DF_Sky_Shop_Tags_IsTop]  DEFAULT ((0)),
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_TAGS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Link]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Link](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LinkClass] [int] NULL,
	[Display] [nvarchar](100) NULL,
	[URL] [nvarchar](200) NULL,
	[OrderID] [int] NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sky_Shop_Link] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserMessage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MessageClass] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[Content] [ntext] NULL,
	[UserIP] [nvarchar](40) NULL,
	[PostDate] [datetime] NULL,
	[IsHandler] [int] NULL,
	[AdminReplyContent] [ntext] NULL,
	[AdminReplyDate] [datetime] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_UserMessage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_VoteRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_VoteRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoteID] [int] NULL,
	[ItemID] [nvarchar](50) NULL,
	[UserIP] [nvarchar](40) NULL,
	[AddDate] [datetime] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_VoteRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserPassword] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Sex] [int] NOT NULL CONSTRAINT [DF__Sky_Shop_User__Sex__1B0907CE]  DEFAULT ((1)),
	[Introduce] [ntext] NULL,
	[Photo] [nvarchar](250) NULL,
	[MSN] [nvarchar](50) NULL,
	[QQ] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[RegionID] [nvarchar](100) NULL,
	[Address] [nvarchar](250) NULL,
	[Birthday] [nvarchar](50) NULL,
	[RegisterIP] [nvarchar](40) NULL,
	[RegisterDate] [datetime] NOT NULL,
	[LastLoginIP] [nvarchar](40) NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LoginTimes] [int] NOT NULL CONSTRAINT [DF__Sky_Shop_User__LoginT__1FCDBCEB]  DEFAULT ((0)),
	[SafeCode] [nvarchar](50) NULL,
	[FindDate] [datetime] NOT NULL CONSTRAINT [DF_Sky_Shop_User_PostDate]  DEFAULT (getdate()),
	[Status] [int] NOT NULL,
	[OpenID] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_USER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END




IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_BookingProduct]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_BookingProduct](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](400) NULL,
	[RelationUser] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NULL,
	[UserNote] [nvarchar](100) NULL,
	[BookingDate] [datetime] NOT NULL,
	[BookingIP] [nvarchar](40) NULL,
	[IsHandler] [int] NOT NULL CONSTRAINT [DF_Sky_Shop_BookingProduct_IsHandler]  DEFAULT ((0)),
	[HandlerDate] [datetime] NOT NULL,
	[HandlerAdminID] [int] NOT NULL,
	[HandlerAdminName] [nvarchar](50) NULL,
	[HandlerNote] [nvarchar](100) NULL,
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_BookingProduct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_VoteItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_VoteItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoteID] [int] NULL,
	[ItemName] [nvarchar](50) NULL,
	[VoteCount] [int] NULL,
	[OrderID] [int] NULL,
 CONSTRAINT [PK_Sky_Shop_VoteItem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Vote]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Vote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ItemCount] [int] NOT NULL,
	[VoteType] [int] NOT NULL,
	[Note] [nvarchar](400) NULL,
 CONSTRAINT [PK_Sky_Shop_Vote] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_UserAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_UserAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Consignee] [nvarchar](50) NULL,
	[RegionID] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[IsDefault] [int] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sky_Shop_UserAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ArticleClass]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ArticleClass](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FatherID] [int] NULL,
	[OrderID] [int] NULL,
	[ClassName] [nvarchar](50) NULL,
	[Description] [text] NULL,
	[IsSystem] [int] NULL,
 CONSTRAINT [PK_SocoShop_ArticleClass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_Article]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_Article](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[ClassID] [nvarchar](100) NULL,
	[IsTop] [int] NULL,
	[Author] [nvarchar](50) NULL,
	[Resource] [nvarchar](50) NULL,
	[Keywords] [nvarchar](100) NULL,
	[Url] [nvarchar](200) NULL,
	[Photo] [nvarchar](100) NULL,
	[Summary] [ntext] NULL,
	[Content] [ntext] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Sky_Shop_ARTICLE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_AdminGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_AdminGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Power] [ntext] NULL,
	[AdminCount] [int] NULL,
	[AddDate] [datetime] NULL,
	[IP] [nvarchar](50) NULL,
	[Note] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END





IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SocoShop_ThemeActivity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SocoShop_ThemeActivity](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Photo] [nvarchar](100) NULL,
	[Description] [text] NULL,
	[Css] [ntext] NULL,
	[ProductGroup] [nvarchar](2000) NULL,
	[Style] [nvarchar](2000) NULL,
 CONSTRAINT [PK_Sky_Shop_ThemeActivity] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

