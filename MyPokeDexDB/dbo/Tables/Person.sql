CREATE TABLE [dbo].[Person] (
    [PersonID]      INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (50)  NOT NULL,
    [LastName]      VARCHAR (50)  NOT NULL,
    [Email]         VARCHAR (50)  NOT NULL,
    [Password]      VARCHAR (MAX) NOT NULL,
    [RoleID]        INT           NULL,
    [Phone]         VARCHAR (50)  NULL,
    [LastLoginTime] DATE          NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([PersonID] ASC),
    CONSTRAINT [FK_User_User] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([PersonID]),
    CONSTRAINT [FK_User_User1] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID])
);



