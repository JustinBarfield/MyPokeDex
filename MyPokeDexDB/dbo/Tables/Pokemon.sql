CREATE TABLE [dbo].[Pokemon] (
    [PokemonID]   INT          NOT NULL,
    [Dex Number]  INT          NOT NULL,
    [Name]        TEXT         NOT NULL,
    [Type]        TEXT         NOT NULL,
    [State Total] INT          NOT NULL,
    [image URL]   VARCHAR (50) NOT NULL,
    [region]      TEXT         NOT NULL,
    CONSTRAINT [PK_MyPokedex] PRIMARY KEY CLUSTERED ([PokemonID] ASC)
);

