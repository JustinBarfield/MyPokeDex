USE [MyPokedex]
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'User')
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (4, N'Justin', N'Barfield', N'justinbarfield02@gmail.com', N'$2a$13$WMYtvHkj1GLuO/wMiWVNhe8Oj5.Jztb6cLBVGbOw5zjUHVTznZjjO', 1, N'8178569902', CAST(N'2024-04-21' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (1002, N'demo', N'demo', N'demo@gmail.com', N'$2a$13$eBAEUKM4KKeGqW82q3LOOO4.A9X3kOUe1m9JkOYvEYKK1iMSMgWPy', 2, N'12345', CAST(N'2024-04-15' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (2002, N'AD', N'ASDF', N'justin@gmail.com', N'$2a$13$/VZEHlVwT9QDHBeW5QW1xu1ba7WwyuDjr1eOT84EQLoJ7V/lFNKyi', 2, N'321', NULL)
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (0, N'Any')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (1, N'Fire')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (2, N'Water')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (3, N'Electric')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (4, N'Bug')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (5, N'Flying')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (6, N'Ground')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (7, N'Rock')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (8, N'Fairy')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (9, N'Dark')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (10, N'Fighting')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (11, N'Ghost')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (12, N'Ice')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (13, N'Steel')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (14, N'Normal')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (15, N'Dragon')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (16, N'Grass')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (17, N'Poison')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (0, N'Any')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (1, N'Johto')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (2, N'Unova')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (3, N'Alola')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (4, N'Kalos')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (5, N'Hisui')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (6, N'Almia')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (7, N'Orre')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (8, N'Hoenn')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (9, N'Galar')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (10, N'Kansai')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (11, N'Fiore')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (12, N'Kanto')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (13, N'Sinnoh')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (14, N'Paldea')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (15, N'Kyushu')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (16, N'Hokkaido')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (1, 1, N'pokemon123', 1, 100, N'adf', 1, N'12', N'12', N'SDF')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (2, 11, N'test', 1, 100, N'sadf', 2, N'5415', N'545f', N'afd')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (651, 651, N'asf', 1, 65, N'sadf', 1, N'9', N'651', N'asdf')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (4654, 55555555, N'Demo5', 1, 55555555, N'asdf', 1, N'5555555', N'555555', N'five')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (5151, 6969, N'asd', 1, 2165, N'asdf', 1, N'516', N'1651', N'asdf')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (61651, 651, N'test123', 1, 516651, N'afds', 1, N'651', N'5665', N'asdf')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (515165465, 6969, N'afsd', 15, 6969, N'adsf', 14, N'545', N'515', N'afsas')
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (1, N'MyPokeDex')
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (2, N'Team A')
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (3, N'Team B')
GO
