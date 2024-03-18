CREATE TABLE [dbo].[User] (
    [UserID]          INT          IDENTITY (1, 1) NOT NULL,
    [First Name]      VARCHAR (50) NOT NULL,
    [Last Name]       VARCHAR (50) NOT NULL,
    [Email]           VARCHAR (50) NOT NULL,
    [Password]        VARCHAR (50) NOT NULL,
    [Last Login Time] DATETIME     NULL,
    [RoleID]          INT          NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_User_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]),
    CONSTRAINT [FK_User_User1] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID])
);

