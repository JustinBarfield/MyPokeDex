/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Justin')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Ethan')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (3, N'Luke')
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (1016, N'Justin', N'Barfield', N'justinbarfield02@gmail.com', N'$2a$13$mo6ORhK7tNT4EK/JsKdknePUD0yKJTOyD7nbqSDfZFI6LKpNFdHpe', 2, N'8178569902', CAST(N'2024-04-03' AS Date))
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [Email], [Password], [RoleID], [Phone], [LastLoginTime]) VALUES (1017, N'Tracey', N'Barfield', N'tracey@gmail.com', N'$2a$13$1VactTR55Uti1h3c7LL9jOvLz21pxFh8x/kwB297kqBp19gQJF0Ue', 2, N'8178569902', NULL)
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
