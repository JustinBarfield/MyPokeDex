CREATE TABLE [dbo].[UserPokemon] (
    [UserPokemonID] INT          NOT NULL,
    [UserID]        INT          NOT NULL,
    [PokemonID]     INT          NOT NULL,
    [RegisterDate]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserPokemon] PRIMARY KEY CLUSTERED ([UserPokemonID] ASC),
    CONSTRAINT [FK_UserPokemon_UserPokemon] FOREIGN KEY ([UserPokemonID]) REFERENCES [dbo].[UserPokemon] ([UserPokemonID])
);

