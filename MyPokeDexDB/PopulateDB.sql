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
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserID], [First Name], [Last Name], [Email], [Password], [Last Login Time], [RoleID]) VALUES (4, N'Justin', N'Barfield', N'BarfieldJ@jacks.sfasu.edu', N'12345', NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
