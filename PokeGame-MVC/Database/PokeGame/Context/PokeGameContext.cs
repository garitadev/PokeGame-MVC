﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PokeGame_MVC.Database.PokeGame.Context;

public partial class PokeGameContext : DbContext
{
    public PokeGameContext()
    {
        
    }
    public PokeGameContext(DbContextOptions<PokeGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Enfermeria> Enfermeria { get; set; }

    public virtual DbSet<Equipos> Equipos { get; set; }

    public virtual DbSet<Mensajes> Mensajes { get; set; }

    public virtual DbSet<Permisos> Permisos { get; set; }

    public virtual DbSet<Pokedex> Pokedex { get; set; }

    public virtual DbSet<Retos> Retos { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<RolPermiso> RolPermiso { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enfermeria>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.Enfermeria)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enfermeria_Pokedex");

            entity.HasOne(d => d.UsuarioAtiende).WithMany(p => p.EnfermeriaUsuarioAtiende)
                .HasForeignKey(d => d.UsuarioAtiendeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enfermeria_Usuario1");

            entity.HasOne(d => d.UsuarioSolicita).WithMany(p => p.EnfermeriaUsuarioSolicita)
                .HasForeignKey(d => d.UsuarioSolicitaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enfermeria_Usuario");
        });

        modelBuilder.Entity<Equipos>(entity =>
        {
            entity.HasOne(d => d.Pokedex).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.PokedexId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipos_Pokedex");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipos_Usuario");
        });

        modelBuilder.Entity<Mensajes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mensajes__FEA0555F28E32824");

            entity.Property(e => e.Contenido).IsRequired();
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Destinatario).WithMany(p => p.MensajesDestinatario)
                .HasForeignKey(d => d.DestinatarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensajes_DestinatarioId");

            entity.HasOne(d => d.Remitente).WithMany(p => p.MensajesRemitente)
                .HasForeignKey(d => d.RemitenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensajes_RemitenteId");
        });

        modelBuilder.Entity<Permisos>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pokedex>(entity =>
        {
            entity.Property(e => e.Debilidad)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Evoluciones)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdPokemon).HasColumnName("idPokemon");
            entity.Property(e => e.Imagen)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Retos>(entity =>
        {
            entity.HasKey(e => e.RetoId).HasName("PK__Retos__8D02CD4BDFA4E386");

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaRespuesta).HasColumnType("datetime");

            entity.HasOne(d => d.Retado).WithMany(p => p.RetosRetado)
                .HasForeignKey(d => d.RetadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Retos_RetadoId");

            entity.HasOne(d => d.RetadoPokemon).WithMany(p => p.RetosRetadoPokemon)
                .HasForeignKey(d => d.RetadoPokemonId)
                .HasConstraintName("FK_Retos_RetadoPokemonId");

            entity.HasOne(d => d.Retador).WithMany(p => p.RetosRetador)
                .HasForeignKey(d => d.RetadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Retos_RetadorId");

            entity.HasOne(d => d.RetadorPokemon).WithMany(p => p.RetosRetadorPokemon)
                .HasForeignKey(d => d.RetadorPokemonId)
                .HasConstraintName("FK_Retos_RetadorPokemonId");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolPermiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Permiso");

            entity.ToTable("Rol_Permiso");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPermiso).HasColumnName("idPermiso");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.RolPermiso)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol_Permiso_Permisos");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolPermiso)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol_Permiso_Rol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("date");
            entity.Property(e => e.FotoPerfil).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}