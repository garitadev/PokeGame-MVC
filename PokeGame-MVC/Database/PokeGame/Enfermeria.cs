﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PokeGame_MVC.Database.PokeGame;

public partial class Enfermeria
{
    public int Id { get; set; }

    public int UsuarioSolicitaId { get; set; }

    public int UsuarioAtiendeId { get; set; }

    public int PokemonId { get; set; }

    public string Estado { get; set; }

    public string Descripcion { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaSalida { get; set; }

    public virtual Pokedex Pokemon { get; set; }

    public virtual Usuario UsuarioAtiende { get; set; }

    public virtual Usuario UsuarioSolicita { get; set; }
}