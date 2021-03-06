

Create database CardGame_v2


USE [CardGame_v2]
GO
/****** Object:  Table [dbo].[tblCard]    Script Date: 14.04.2017 10:18:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCard](
	[idCard] [int] IDENTITY(1,1) NOT NULL,
	[cardname] [nvarchar](20) NOT NULL,
	[manacost] [int] NOT NULL,
	[attack] [int] NOT NULL,
	[life] [int] NOT NULL,
	[fkCardType] [int] NULL,
	[cardimage] [varbinary](max) NULL,
 CONSTRAINT [PK_tblCard] PRIMARY KEY CLUSTERED 
(
	[idCard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCardPack]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCardPack](
	[idCardPack] [int] IDENTITY(1,1) NOT NULL,
	[packname] [nvarchar](20) NOT NULL,
	[numcards] [int] NOT NULL,
	[packprice] [int] NOT NULL,
 CONSTRAINT [PK_tblCardPack] PRIMARY KEY CLUSTERED 
(
	[idCardPack] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCardType]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCardType](
	[idCardType] [int] IDENTITY(1,1) NOT NULL,
	[typename] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_tblCardType] PRIMARY KEY CLUSTERED 
(
	[idCardType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDeck]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDeck](
	[idDeck] [int] IDENTITY(1,1) NOT NULL,
	[deckname] [nvarchar](20) NOT NULL,
	[fkUser] [int] NOT NULL,
 CONSTRAINT [PK_tblDeck] PRIMARY KEY CLUSTERED 
(
	[idDeck] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDeckCard]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDeckCard](
	[fkDeck] [int] NOT NULL,
	[fkCard] [int] NOT NULL,
	[numcards] [int] NOT NULL,
 CONSTRAINT [PK_tblDeckCard] PRIMARY KEY CLUSTERED 
(
	[fkDeck] ASC,
	[fkCard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](50) NOT NULL,
	[lastname] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[username] [nvarchar](20) NOT NULL,
	[userpassword] [char](128) NOT NULL,
	[usersalt] [char](128) NOT NULL,
	[fkUserRole] [int] NULL,
	[avatar] [varbinary](max) NULL,
	[currency] [int] NOT NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUserCardCollection]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserCardCollection](
	[fkUser] [int] NOT NULL,
	[fkCard] [int] NOT NULL,
	[numcards] [int] NOT NULL,
 CONSTRAINT [PK_tblUserCardCollection] PRIMARY KEY CLUSTERED 
(
	[fkUser] ASC,
	[fkCard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUserRole]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserRole](
	[idUserrole] [int] IDENTITY(1,1) NOT NULL,
	[rolename] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_tblUserRole] PRIMARY KEY CLUSTERED 
(
	[idUserrole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVirtualPurchase]    Script Date: 14.04.2017 10:18:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVirtualPurchase](
	[idVirtualPurchase] [int] IDENTITY(1,1) NOT NULL,
	[fkUser] [int] NOT NULL,
	[fkCardPack] [int] NOT NULL,
	[timeofpurchase] [datetime] NOT NULL,
	[numpacks] [int] NOT NULL,
 CONSTRAINT [PK_tblVirtualPurchase] PRIMARY KEY CLUSTERED 
(
	[idVirtualPurchase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tblCard] ON 

INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (1, N'Elven Archer', 1, 1, 1, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (2, N'Goldshire Footman', 1, 1, 2, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (3, N'Acidic Swamp Ooze', 2, 3, 2, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (4, N'Frostwolf Grunt', 2, 2, 2, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (5, N'Dalaran Mage', 3, 1, 4, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (6, N'Ironfur Grizzly', 3, 3, 3, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (7, N'Chillwind Yeti', 4, 4, 5, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (8, N'Ogre Magi', 4, 4, 4, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (9, N'Gurubashi Berserker', 5, 2, 7, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (10, N'Booty Bay Bodyguard', 5, 5, 4, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (11, N'Archmage', 6, 4, 7, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (12, N'Lord of the Arena', 6, 6, 5, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (13, N'War Golem', 7, 7, 7, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (14, N'Core Hound', 7, 9, 5, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (15, N'Gruul', 8, 7, 7, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (16, N'Ragnaros', 8, 8, 8, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (17, N'Malygos', 9, 4, 12, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (18, N'Alexstrasza', 9, 8, 8, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (19, N'Deathwing', 10, 12, 12, 1, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (20, N'Arcane Missiles', 1, 3, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (21, N'Arcane Shot', 1, 2, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (22, N'Frostbolt', 2, 3, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (23, N'Holy Light', 2, 0, 6, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (24, N'Lava Burst', 3, 5, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (25, N'Ice Barrier', 3, 0, 8, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (26, N'Bite', 4, 4, 4, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (27, N'Fireball', 4, 6, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (28, N'Explosive Shot', 5, 7, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (29, N'Force of Nature', 5, 6, 6, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (30, N'Avenging Wrath', 6, 8, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (31, N'Holy Fire', 6, 5, 5, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (32, N'Lay on Hands', 8, 0, 8, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (33, N'Pyroblast', 10, 10, 0, 2, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (34, N'Light''s Justice', 1, 1, 4, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (35, N'Fiery War Axe', 2, 3, 2, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (36, N'Truesilver Champion', 4, 4, 2, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (37, N'Assassin''s Blade', 5, 3, 4, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (38, N'Arcanite Reaper', 5, 5, 2, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (39, N'Stormforged Axe', 2, 2, 3, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (40, N'Sword of Justice', 3, 1, 5, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (41, N'Perdition''s Blade', 3, 2, 2, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (42, N'Eaglehorn Bow', 3, 3, 2, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (43, N'Doomhammer', 5, 2, 8, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (44, N'Gorehowl', 7, 7, 1, 3, NULL)
INSERT [dbo].[tblCard] ([idCard], [cardname], [manacost], [attack], [life], [fkCardType], [cardimage]) VALUES (45, N'Gladiator''s Longbow', 7, 5, 2, 3, NULL)
SET IDENTITY_INSERT [dbo].[tblCard] OFF
SET IDENTITY_INSERT [dbo].[tblCardPack] ON 

INSERT [dbo].[tblCardPack] ([idCardPack], [packname], [numcards], [packprice]) VALUES (1, N'Five Friends', 5, 4)
INSERT [dbo].[tblCardPack] ([idCardPack], [packname], [numcards], [packprice]) VALUES (2, N'Eight Mate', 8, 6)
INSERT [dbo].[tblCardPack] ([idCardPack], [packname], [numcards], [packprice]) VALUES (3, N'Seven Eleven', 11, 9)
INSERT [dbo].[tblCardPack] ([idCardPack], [packname], [numcards], [packprice]) VALUES (4, N'The Big Guns', 20, 15)
SET IDENTITY_INSERT [dbo].[tblCardPack] OFF
SET IDENTITY_INSERT [dbo].[tblCardType] ON 

INSERT [dbo].[tblCardType] ([idCardType], [typename]) VALUES (1, N'Minion')
INSERT [dbo].[tblCardType] ([idCardType], [typename]) VALUES (2, N'Spell')
INSERT [dbo].[tblCardType] ([idCardType], [typename]) VALUES (3, N'Weapon')
SET IDENTITY_INSERT [dbo].[tblCardType] OFF
SET IDENTITY_INSERT [dbo].[tblDeck] ON 

INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (1, N'playerDeck1', 7)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (2, N'playerDeck2', 7)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (3, N'playerDeck3', 7)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (4, N'userDeck1', 8)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (5, N'userDeck2', 8)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (6, N'userDeck3', 8)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (13, N'jaina1', 12)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (14, N'jaina2', 12)
INSERT [dbo].[tblDeck] ([idDeck], [deckname], [fkUser]) VALUES (15, N'jaina3', 12)
SET IDENTITY_INSERT [dbo].[tblDeck] OFF
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (1, 1, 2)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (1, 2, 2)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (1, 3, 1)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (2, 2, 2)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (2, 3, 2)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (2, 4, 1)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (3, 3, 2)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (3, 4, 2)
INSERT [dbo].[tblDeckCard] ([fkDeck], [fkCard], [numcards]) VALUES (3, 5, 1)
SET IDENTITY_INSERT [dbo].[tblUser] ON 

INSERT [dbo].[tblUser] ([idUser], [firstname], [lastname], [email], [username], [userpassword], [usersalt], [fkUserRole], [avatar], [currency]) VALUES (1, N'uther', N'lightbringer', N'lightbringer@email.com', N'admin', N'admin                                                                                                                           ', N'admin                                                                                                                           ', 1, NULL, 1000)
INSERT [dbo].[tblUser] ([idUser], [firstname], [lastname], [email], [username], [userpassword], [usersalt], [fkUserRole], [avatar], [currency]) VALUES (2, N'arthas', N'menethil', N'lichking@email.com', N'lichking', N'lichking                                                                                                                        ', N'lichking                                                                                                                        ', 2, NULL, 1000)
INSERT [dbo].[tblUser] ([idUser], [firstname], [lastname], [email], [username], [userpassword], [usersalt], [fkUserRole], [avatar], [currency]) VALUES (5, N'tirion', N'fordring', N'tirion@email.com', N'tirion', N'50A88FEEBA6623AD628DC6E3AC4B13BAF80D696149A3A7902E6954620E5F5BD9AE2F15BD1C37FA7BDD8DF9F5FDF9ED5CC827F70F8670A727BF8607F1D8FD6731', N'DCA0D0C91F4E69E9C5FAF9977FAC5158FEF1A1130A9A053511A27D46C417899EBA964A68947EEC19421A1AFC486A785A74AD4C17554EE1DED733B15E77063BAF', 2, NULL, 1000)
INSERT [dbo].[tblUser] ([idUser], [firstname], [lastname], [email], [username], [userpassword], [usersalt], [fkUserRole], [avatar], [currency]) VALUES (6, N'admin', N'admin', N'admin', N'', N'23248C83B42D2AF395BFFF414F21295C06082AB6733D18C98FD557E697EE8226C5C92DB05A302202497E3EC1CF0516E62357E634ABBB66B6D18C7BCFF6717C35', N'F5B719D49A2854C7DFA8BE7891E61918D7C68E3A48A3F3FBD5A45E47FA74946DAE9DDD4D82546E71AE5F034B0E58D640ED1F99B695989C18D54C8B64722793D3', 1, NULL, 1000)
INSERT [dbo].[tblUser] ([idUser], [firstname], [lastname], [email], [username], [userpassword], [usersalt], [fkUserRole], [avatar], [currency]) VALUES (7, N'player', N'player', N'player', N'', N'9143D3A49FC005F8838942356FD192621B8A189E6D1B4D6DCB7489B42EE150F0C951FD2F4E796ED1932FD74AAB7139BB6F1525857109B5C9D4177D5A1C115DDD', N'63153ACA8A6280165820AA1A0AA64E08716B060A0A06E9F5C844269650D09CDDE526C8B5964D990B10829F4BF9E5093AFC0F0E30A8B13438C076ACF71EDCC3A3', 2, NULL, 141)
INSERT [dbo].[tblUser] ([idUser], [firstname], [lastname], [email], [username], [userpassword], [usersalt], [fkUserRole], [avatar], [currency]) VALUES (8, N'user', N'user', N'user', N'', N'A3B593E5C751D3CEF95927E03090A3CED509402E3F757915526337102BCC07E5356C34CD69D77505F7F75857BA80EA9792B4E728C9CD5102586611ADE0106C06', N'BF3E011570B6B3C86C7FF16E1F7A8FE4A7738314A4EFBC66311E23124115F7D5FD55B40525AC1086CE4548E296EB4DB355B0D1885AF7CB815C5EA0B425E4E66B', 2, NULL, 996)
INSERT [dbo].[tblUser] ([idUser], [firstname], [lastname], [email], [username], [userpassword], [usersalt], [fkUserRole], [avatar], [currency]) VALUES (12, N'jaina', N'jaina', N'jaina', N'', N'D32603654352782562AD0DB8E0393CB71C013495E68F138305BAA98988D1EC1CD27A65BBD9F6292168B43CCBF1EEF6A185F1A42CD28C867FC24A9B46D03D7DF7', N'13FAEE195E49ABCD90A674681BAF429BC9D73D63CE6B9C1E817B37F1BE975CDA6C10AA68CBF9D46E0CA1D0A09A459FD183F0BBCF98B6693C25452F971673E1E3', 2, NULL, 1000)
SET IDENTITY_INSERT [dbo].[tblUser] OFF
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 1, 32)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 2, 31)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 3, 28)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 4, 33)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 5, 23)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 6, 24)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 7, 39)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 8, 22)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 9, 23)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 10, 34)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 11, 24)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 12, 30)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 13, 23)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 14, 29)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 15, 22)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 16, 33)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 17, 28)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 18, 20)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 19, 29)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 20, 21)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 21, 27)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 22, 21)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 23, 23)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 24, 29)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 25, 31)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 26, 19)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 27, 25)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 28, 17)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 29, 26)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 30, 33)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 31, 18)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 32, 34)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 33, 31)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 34, 21)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 35, 25)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 36, 27)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 37, 26)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 38, 36)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 39, 30)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 40, 17)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 41, 38)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 42, 23)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 43, 29)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (7, 44, 26)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (8, 5, 1)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (8, 9, 1)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (8, 31, 1)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (8, 35, 1)
INSERT [dbo].[tblUserCardCollection] ([fkUser], [fkCard], [numcards]) VALUES (8, 37, 1)
SET IDENTITY_INSERT [dbo].[tblUserRole] ON 

INSERT [dbo].[tblUserRole] ([idUserrole], [rolename]) VALUES (1, N'admin')
INSERT [dbo].[tblUserRole] ([idUserrole], [rolename]) VALUES (2, N'player')
SET IDENTITY_INSERT [dbo].[tblUserRole] OFF
ALTER TABLE [dbo].[tblCard]  WITH CHECK ADD  CONSTRAINT [FK_tblCard_tblCardType] FOREIGN KEY([fkCardType])
REFERENCES [dbo].[tblCardType] ([idCardType])
GO
ALTER TABLE [dbo].[tblCard] CHECK CONSTRAINT [FK_tblCard_tblCardType]
GO
ALTER TABLE [dbo].[tblDeck]  WITH CHECK ADD  CONSTRAINT [FK_tblDeck_tblUser] FOREIGN KEY([fkUser])
REFERENCES [dbo].[tblUser] ([idUser])
GO
ALTER TABLE [dbo].[tblDeck] CHECK CONSTRAINT [FK_tblDeck_tblUser]
GO
ALTER TABLE [dbo].[tblDeckCard]  WITH CHECK ADD  CONSTRAINT [FK_tblDeckCard_tblCard] FOREIGN KEY([fkCard])
REFERENCES [dbo].[tblCard] ([idCard])
GO
ALTER TABLE [dbo].[tblDeckCard] CHECK CONSTRAINT [FK_tblDeckCard_tblCard]
GO
ALTER TABLE [dbo].[tblDeckCard]  WITH CHECK ADD  CONSTRAINT [FK_tblDeckCard_tblDeck] FOREIGN KEY([fkDeck])
REFERENCES [dbo].[tblDeck] ([idDeck])
GO
ALTER TABLE [dbo].[tblDeckCard] CHECK CONSTRAINT [FK_tblDeckCard_tblDeck]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_tblUser_tblUserRole] FOREIGN KEY([fkUserRole])
REFERENCES [dbo].[tblUserRole] ([idUserrole])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_tblUser_tblUserRole]
GO
ALTER TABLE [dbo].[tblUserCardCollection]  WITH CHECK ADD  CONSTRAINT [FK_tblUserCardCollection_tblCard] FOREIGN KEY([fkCard])
REFERENCES [dbo].[tblCard] ([idCard])
GO
ALTER TABLE [dbo].[tblUserCardCollection] CHECK CONSTRAINT [FK_tblUserCardCollection_tblCard]
GO
ALTER TABLE [dbo].[tblUserCardCollection]  WITH CHECK ADD  CONSTRAINT [FK_tblUserCardCollection_tblUser] FOREIGN KEY([fkUser])
REFERENCES [dbo].[tblUser] ([idUser])
GO
ALTER TABLE [dbo].[tblUserCardCollection] CHECK CONSTRAINT [FK_tblUserCardCollection_tblUser]
GO
ALTER TABLE [dbo].[tblVirtualPurchase]  WITH CHECK ADD  CONSTRAINT [FK_tblVirtualPurchase_tblCardPack] FOREIGN KEY([fkCardPack])
REFERENCES [dbo].[tblCardPack] ([idCardPack])
GO
ALTER TABLE [dbo].[tblVirtualPurchase] CHECK CONSTRAINT [FK_tblVirtualPurchase_tblCardPack]
GO
ALTER TABLE [dbo].[tblVirtualPurchase]  WITH CHECK ADD  CONSTRAINT [FK_tblVirtualPurchase_tblUser] FOREIGN KEY([fkUser])
REFERENCES [dbo].[tblUser] ([idUser])
GO
ALTER TABLE [dbo].[tblVirtualPurchase] CHECK CONSTRAINT [FK_tblVirtualPurchase_tblUser]
GO
