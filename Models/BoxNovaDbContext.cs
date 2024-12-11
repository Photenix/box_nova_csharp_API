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

    public virtual DbSet<Cliente> Clientes { get; set; } = null!;
    public virtual DbSet<Carrito> Carritos { get; set; } = null!;
    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; } = null!;
    public virtual DbSet<SubCategoriaProducto> SubCategoriaProductos { get; set; } = null!;


    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetalleVentas { get; set; }
    //public DbSet<Devolucion> Devoluciones { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<DetallePedido> DetallePedidos { get; set; }
    //public DbSet<CategoriaProducto> CatProductos { get; set; }
    //public DbSet<CodigoDeBarras> CodigosBarras { get; set; }

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

        // Configuración de Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);
            entity.Property(e => e.NombreCliente).IsRequired().HasMaxLength(30);
            entity.Property(e => e.ApellidoCliente).IsRequired().HasMaxLength(30);
            entity.Property(e => e.CedulaCliente).IsRequired().HasMaxLength(20);
            entity.HasIndex(e => e.CedulaCliente).IsUnique();
            entity.Property(e => e.GeneroCliente).HasMaxLength(1);
            entity.Property(e => e.FechaRegistro).IsRequired();

            entity.HasMany(e => e.Carritos)
                  .WithOne(e => e.Cliente)
                  .HasForeignKey(e => e.IdCliente)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de Carrito
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdDetalle);
            entity.Property(e => e.Cantidad).IsRequired().HasDefaultValue(1);
            entity.Property(e => e.PrecioUnitario).IsRequired();
            entity.Property(e => e.Subtotal).HasComputedColumnSql("[Cantidad] * [PrecioUnitario]");

            entity.HasOne(e => e.Cliente)
                  .WithMany(e => e.Carritos)
                  .HasForeignKey(e => e.IdCliente);
        });

        // Configuración de CategoriaProducto
        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdCProd);
            entity.Property(e => e.NombreCProd).IsRequired().HasMaxLength(30);
            entity.Property(e => e.EstadoCProd).IsRequired();
        });

        // Configuración de SubCategoriaProducto
        modelBuilder.Entity<SubCategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdSubCProd);
            entity.Property(e => e.NombreSubCProd).IsRequired().HasMaxLength(30);
            entity.Property(e => e.EstadoSubCProd).IsRequired();

            entity.HasOne(e => e.CategoriaProducto)
                  .WithMany(e => e.SubCategorias)
                  .HasForeignKey(e => e.IdCProd)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de la relación entre DetalleVenta y SubTotal
        modelBuilder.Entity<DetalleVenta>()
            .Property(d => d.SubTotal)
            .HasColumnType("decimal(18,2)");

        // Configuración de la relación entre Cliente y Venta
        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Ventas)
            .WithOne(v => v.Cliente)
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuración de la relación entre Venta y DetalleVenta
        modelBuilder.Entity<Venta>()
            .HasMany(v => v.DetalleVenta)
            .WithOne(d => d.Venta)
            .HasForeignKey(d => d.VentaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuración de la relación entre Pedido y DetallePedido
        modelBuilder.Entity<Pedido>()
            .HasMany(p => p.DetallePedidos)
            .WithOne(d => d.Pedido)
            .HasForeignKey(d => d.IdPedido)  // Asegúrate de que 'IdPedido' está bien definido como clave foránea
            .OnDelete(DeleteBehavior.Cascade);

        // Configuración de la relación entre Producto y CodigoDeBarras
        modelBuilder.Entity<Producto>()
             .HasKey(e => e.IdProducto);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
