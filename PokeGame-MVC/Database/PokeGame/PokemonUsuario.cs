﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PokeGame_MVC.Database.PokeGame;

public partial class PokemonUsuario
{
    public int Id { get; set; }

    public int IdPokemon { get; set; }

    public int IdUsuario { get; set; }

    public virtual Pokemon IdPokemonNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}