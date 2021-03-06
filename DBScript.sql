USE [DbLinkDevTask]
GO
/****** Object:  Table [dbo].[TBCategories]    Script Date: 01-Jan-21 2:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TBCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBDiscountRules]    Script Date: 01-Jan-21 2:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBDiscountRules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemCount] [int] NOT NULL,
 CONSTRAINT [PK_TBDiscountRules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBOrders]    Script Date: 01-Jan-21 2:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalPrice] [float] NOT NULL,
 CONSTRAINT [PK_TBOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBProducts]    Script Date: 01-Jan-21 2:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TBProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBProductVsDiscountRule]    Script Date: 01-Jan-21 2:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBProductVsDiscountRule](
	[ProductId] [int] NOT NULL,
	[DiscountRuleId] [int] NOT NULL,
	[Percentage] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK__TBProduc__5473D4D0FD758A0C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TBUsers]    Script Date: 01-Jan-21 2:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Birthdate] [datetime] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_TBUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[TBCategories] ON 

INSERT [dbo].[TBCategories] ([Id], [Name]) VALUES (1, N'TV')
INSERT [dbo].[TBCategories] ([Id], [Name]) VALUES (2, N'Laptop')
INSERT [dbo].[TBCategories] ([Id], [Name]) VALUES (3, N'SoundSystem')
SET IDENTITY_INSERT [dbo].[TBCategories] OFF
SET IDENTITY_INSERT [dbo].[TBDiscountRules] ON 

INSERT [dbo].[TBDiscountRules] ([Id], [ItemCount]) VALUES (1, 1)
INSERT [dbo].[TBDiscountRules] ([Id], [ItemCount]) VALUES (2, 2)
SET IDENTITY_INSERT [dbo].[TBDiscountRules] OFF
SET IDENTITY_INSERT [dbo].[TBProducts] ON 

INSERT [dbo].[TBProducts] ([Id], [Name], [Price], [CategoryId], [Description]) VALUES (2, N'samsung65', 5000, 1, N'etc etc etc')
INSERT [dbo].[TBProducts] ([Id], [Name], [Price], [CategoryId], [Description]) VALUES (3, N'dell', 15000, 2, N'etc etc etc')
SET IDENTITY_INSERT [dbo].[TBProducts] OFF
SET IDENTITY_INSERT [dbo].[TBProductVsDiscountRule] ON 

INSERT [dbo].[TBProductVsDiscountRule] ([ProductId], [DiscountRuleId], [Percentage], [Id]) VALUES (2, 1, 20, 4)
INSERT [dbo].[TBProductVsDiscountRule] ([ProductId], [DiscountRuleId], [Percentage], [Id]) VALUES (2, 2, 20, 6)
SET IDENTITY_INSERT [dbo].[TBProductVsDiscountRule] OFF
SET IDENTITY_INSERT [dbo].[TBUsers] ON 

INSERT [dbo].[TBUsers] ([Id], [Name], [Address], [Phone], [Birthdate], [Email], [Password], [IsAdmin]) VALUES (1, N'hassan moftah', N'asas as as ', N'15555', CAST(0x0000885E00000000 AS DateTime), N'h.moftah@gmail', N'123456', 1)
INSERT [dbo].[TBUsers] ([Id], [Name], [Address], [Phone], [Birthdate], [Email], [Password], [IsAdmin]) VALUES (2, N'h@gmail', N'aasfasf', N'15555', CAST(0x0000ACB700000000 AS DateTime), N'h@gmail', N'123456', 0)
SET IDENTITY_INSERT [dbo].[TBUsers] OFF
/****** Object:  Index [unique_DiscountVsProduct]    Script Date: 01-Jan-21 2:27:15 AM ******/
ALTER TABLE [dbo].[TBProductVsDiscountRule] ADD  CONSTRAINT [unique_DiscountVsProduct] UNIQUE NONCLUSTERED 
(
	[ProductId] ASC,
	[DiscountRuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TBOrders]  WITH CHECK ADD  CONSTRAINT [FK_TBOrders_TBProducts] FOREIGN KEY([ProductId])
REFERENCES [dbo].[TBProducts] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TBOrders] CHECK CONSTRAINT [FK_TBOrders_TBProducts]
GO
ALTER TABLE [dbo].[TBOrders]  WITH CHECK ADD  CONSTRAINT [FK_TBOrders_TBUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[TBUsers] ([Id])
GO
ALTER TABLE [dbo].[TBOrders] CHECK CONSTRAINT [FK_TBOrders_TBUsers]
GO
ALTER TABLE [dbo].[TBProducts]  WITH CHECK ADD  CONSTRAINT [FK_TBProducts_TBCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[TBCategories] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TBProducts] CHECK CONSTRAINT [FK_TBProducts_TBCategories]
GO
ALTER TABLE [dbo].[TBProductVsDiscountRule]  WITH CHECK ADD  CONSTRAINT [FK_TBProductVsDiscountRule_TBDiscountRules] FOREIGN KEY([DiscountRuleId])
REFERENCES [dbo].[TBDiscountRules] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TBProductVsDiscountRule] CHECK CONSTRAINT [FK_TBProductVsDiscountRule_TBDiscountRules]
GO
ALTER TABLE [dbo].[TBProductVsDiscountRule]  WITH CHECK ADD  CONSTRAINT [FK_TBProductVsDiscountRule_TBProducts] FOREIGN KEY([ProductId])
REFERENCES [dbo].[TBProducts] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TBProductVsDiscountRule] CHECK CONSTRAINT [FK_TBProductVsDiscountRule_TBProducts]
GO
