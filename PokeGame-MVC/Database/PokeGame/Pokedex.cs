﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PokeGame_MVC.Database.PokeGame;

public partial class Pokedex
{
    public int Id { get; set; }

    public int IdPokemon { get; set; }

    public string Nombre { get; set; }

    public string Debilidad { get; set; }

    public string Tipo { get; set; }

    public string Evoluciones { get; set; }

    public int Peso { get; set; }

    public int Numero { get; set; }

    public string Imagen { get; set; }
}