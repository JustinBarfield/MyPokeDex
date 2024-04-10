CREATE TABLE [dbo].[Pokemon] (
    [PokemonID]   INT          NOT NULL,
    [Dex Number]  INT          NOT NULL,
    [Name]        TEXT         NOT NULL,
    [TypeID]      INT          NULL,
    [State Total] INT          NOT NULL,
    [image URL]   VARCHAR (50) NULL,
    [RegionID]    INT          NULL,
    [Height]      TEXT         NULL,
    [Weight]      TEXT         NULL,
    [Audio]       VARCHAR (50) NULL,
    CONSTRAINT [PK_MyPokedex] PRIMARY KEY CLUSTERED ([PokemonID] ASC),
    CONSTRAINT [FK_Pokemon_Pokemon] FOREIGN KEY ([PokemonID]) REFERENCES [dbo].[Pokemon] ([PokemonID]),
    CONSTRAINT [FK_Pokemon_Pokemon1] FOREIGN KEY ([PokemonID]) REFERENCES [dbo].[Pokemon] ([PokemonID]),
    CONSTRAINT [FK_Pokemon_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([RegionID]),
    CONSTRAINT [FK_Pokemon_Type] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[Type] ([TypeID])
);









