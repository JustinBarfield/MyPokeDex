USE [MyPokedex]
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'User')
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (4, N'Justin', N'Barfield', N'justinbarfield02@gmail.com', N'$2a$13$WMYtvHkj1GLuO/wMiWVNhe8Oj5.Jztb6cLBVGbOw5zjUHVTznZjjO', 1, N'8178569902', CAST(N'2024-04-30' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (1002, N'demo', N'demo', N'demo@gmail.com', N'$2a$13$eBAEUKM4KKeGqW82q3LOOO4.A9X3kOUe1m9JkOYvEYKK1iMSMgWPy', 2, N'12345', CAST(N'2024-04-25' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (2002, N'AD', N'ASDF', N'justin@gmail.com', N'$2a$13$/VZEHlVwT9QDHBeW5QW1xu1ba7WwyuDjr1eOT84EQLoJ7V/lFNKyi', 2, N'321', NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (3002, N'demo5', N'demo5', N'demo5@gmail.com', N'$2a$13$19zBx1wFX8CZQtcRzADdyu1E5EERdu1yzaoRMcL1NWo3stoAztQA2', 2, N'12345', CAST(N'2024-04-24' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (4002, N'Justin', N'Barfield', N'justinbarfield03@gmail.com', N'$2a$13$Qk7u8GOhCfXnLIOQ/akPlueUynFJVpWMJut8hfhkS7KLYNJqqrlCK', 2, N'8178569902', NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (4003, N'Justin', N'Barfield', N'justinbarfield05@gmail.com', N'$2a$13$bRBfW7clfQvkPgiyDUCJf.q9CjtoW/5vR8BYR72tdqCDOelqI7PbO', 2, N'8178569902', CAST(N'2024-04-25' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (5002, N'Justin', N'Barfield', N'justinbarfield06@gmail.com', N'$2a$13$iSrIqLncaZs0O3bbjs/cGeJM0zvno3/WspbaHtf32beiMKEALMWZC', 2, N'8178569902', NULL)
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
SET IDENTITY_INSERT [dbo].[Pokemon] ON 
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (1, 1, N'pokemon123', 1, 100, N'https://assets.pokemon.com/assets/cms2/img/pokedex/full//133.png', 1, N'12', N'12', N'SDF')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (2, 11, N'test', 1, 100, N'https://www.google.com/imgres?q=pokemon&imgurl=https%3A%2F%2Fupload.wikimedia.org%2Fwikipedia%2Fcommons%2Fthumb%2F9%2F98%2FInternational_Pok%25C3%25A9mon_logo.svg%2F1200px-International_Pok%25C3%25A9mon_logo.svg.png&imgrefurl=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FPok%25C3%25A9mon&docid=VWc1sn0WEVMNkM&tbnid=6Mje8iNBjoua8M&vet=12ahUKEwjEr-zXw-qFAxUxJNAFHUN-BvIQM3oECA4QAA..i&w=1200&h=442&hcb=2&itg=1&ved=2ahUKEwjEr-zXw-qFAxUxJNAFHUN-BvIQM3oECA4QAA', 2, N'5415', N'545f', N'afd')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (3, 3, N'3', 3, 3, N'3', 3, N'3', N'3', N'3')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (4, 4, N'4', 4, 4, N'4', 4, N'4', N'4', N'4')
GO
SET IDENTITY_INSERT [dbo].[Pokemon] OFF
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [PokemonId]) VALUES (1, N'Team1', N'1         ')
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [PokemonId]) VALUES (2, N'Team2', N'2         ')
GO
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [PokemonId]) VALUES (3, N'Pokedex', N'651       ')
GO
