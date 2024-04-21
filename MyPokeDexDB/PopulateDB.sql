USE [MyPokedex]
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (0, N'Any')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (1, N'JOHTO')
GO
INSERT [dbo].[Region] ([RegionID], [RegionName]) VALUES (2, N'test')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (0, N'Any')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (1, N'fire')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (2, N'water')
GO
INSERT [dbo].[Type] ([TypeID], [TypeName]) VALUES (3, N'Electric')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (1, 1, N'pokemon123', 1, 100, N'adfasf', 1, N'12', N'12', N'SDF')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (2, 11, N'test', 1, 100, N'asf', 2, N'5415', N'545f', N'afd')
GO
INSERT [dbo].[Pokemon] ([PokemonID], [Dex Number], [Name], [TypeID], [State Total], [image URL], [RegionID], [Height], [Weight], [Audio]) VALUES (5151, 6969, N'asd', 1, 2165, N'asdf', 1, N'516', N'1651', N'asdf')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, NULL)
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (4, N'Justin', N'Barfield', N'justinbarfield02@gmail.com', N'$2a$13$WMYtvHkj1GLuO/wMiWVNhe8Oj5.Jztb6cLBVGbOw5zjUHVTznZjjO', 2, N'8178569902', CAST(N'2024-04-16' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (1002, N'demo', N'demo', N'demo@gmail.com', N'$2a$13$eBAEUKM4KKeGqW82q3LOOO4.A9X3kOUe1m9JkOYvEYKK1iMSMgWPy', 2, N'12345', CAST(N'2024-04-15' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
