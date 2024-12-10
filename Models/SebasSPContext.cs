using Microsoft.EntityFrameworkCore;

namespace BoxNovaSoftAPI.Models
{
    public class SebasSPContext : DbContext

    {

        public SebasSPContext(DbContextOptions<SebasSPContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Carrito> Carritos { get; set; } = null!;
        public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; } = null!;
        public virtual DbSet<SubCategoriaProducto> SubCategoriaProductos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                entity.Property(e => e.NombreCProd).IsRequired().HasMaxLength(30);
                entity.Property(e => e.EstadoCProd).IsRequired();

                entity.HasOne(e => e.Categoria)
                      .WithMany(e => e.SubCategorias)
                      .HasForeignKey(e => e.IdCProd)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    
}
}
