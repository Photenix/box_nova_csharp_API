using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BoxNovaSoftAPI.Models;

public partial class BoxNovaDbContext : DbContext
{

    public BoxNovaDbContext(DbContextOptions<BoxNovaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PerXrolXpriv> PerXrolXprivs { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Privilegio> Privilegios { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PerXrolXpriv>(entity =>
        {
            entity.HasKey(e => e.IdPerxrol).HasName("pk_perxrol");

            entity.ToTable("PerXRolXPriv");

            entity.HasIndex(e => new { e.IdPer, e.IdRol, e.IdPriv }, "un_perxrolxpriv").IsUnique();

            entity.Property(e => e.IdPerxrol)
                .ValueGeneratedNever()
                .HasColumnName("id_perxrol");
            entity.Property(e => e.IdPer).HasColumnName("id_per");
            entity.Property(e => e.IdPriv).HasColumnName("id_priv");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");

            entity.HasOne(d => d.IdPerNavigation).WithMany(p => p.PerXrolXprivs)
                .HasForeignKey(d => d.IdPer)
                .HasConstraintName("fk_permiso");

            entity.HasOne(d => d.IdPrivNavigation).WithMany(p => p.PerXrolXprivs)
                .HasForeignKey(d => d.IdPriv)
                .HasConstraintName("fk_priv");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.PerXrolXprivs)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fk_rol");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("pk_permiso");

            entity.HasIndex(e => e.NombrePermiso, "un_nombre_permiso").IsUnique();

            entity.Property(e => e.IdPermiso)
                .ValueGeneratedNever()
                .HasColumnName("id_permiso");
            entity.Property(e => e.EstadoPermiso)
                .HasDefaultValue(true)
                .HasColumnName("estado_permiso");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre_permiso");
        });

        modelBuilder.Entity<Privilegio>(entity =>
        {
            entity.HasKey(e => e.IdPrivilegio).HasName("pk_privilegio");

            entity.Property(e => e.IdPrivilegio)
                .ValueGeneratedNever()
                .HasColumnName("id_privilegio");
            entity.Property(e => e.NombrePrivilegio)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre_privilegio");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("pk_rol");

            entity.HasIndex(e => e.NombreRol, "un_nombre_rol").IsUnique();

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("id_rol");
            entity.Property(e => e.EstadoRol)
                .HasDefaultValue(true)
                .HasColumnName("estado_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("pk_user");

            entity.HasIndex(e => e.TarjetaIdentidad, "UQ__Usuarios__CC7F75E4F10CEE4D").IsUnique();

            entity.HasIndex(e => e.CorreoUsuario, "UQ__Usuarios__CD54AB1CAFE69B7A").IsUnique();

            entity.HasIndex(e => e.CorreoUsuario, "un_correo_usuario").IsUnique();

            entity.HasIndex(e => e.TarjetaIdentidad, "un_tarjeta_identidad").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.ContrasenaUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contrasena_usuario");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_usuario");
            entity.Property(e => e.CumpleanoUsuario).HasColumnName("cumpleano_usuario");
            entity.Property(e => e.EstadoUsuario)
                .IsUnicode(false)
                .HasDefaultValue(true)
                .HasColumnName("estado_usuario");
            entity.Property(e => e.FechaCreacionUsuario).HasColumnName("fecha_creacion_usuario");
            entity.Property(e => e.FkRol).HasColumnName("fk_rol");
            entity.Property(e => e.GeneroUsuario)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("genero_usuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.TarjetaIdentidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tarjeta_identidad");

            entity.HasOne(d => d.FkRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkRol)
                .HasConstraintName("fk_rol_user");
                //.OnDelete(DeleteBehavior.Restrict)//.OnDelete(DeleteBehavior.ClientSetNull)
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
