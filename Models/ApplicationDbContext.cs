using Microsoft.EntityFrameworkCore;
using WebEmpresa.Models;

namespace BoxNovaSoftAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<CategoriaProducto> CatProductos { get; set; }
        public DbSet<CodigoDeBarras> CodigosBarras { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                .HasMany(p => p.CodigosBarras) // Un producto tiene muchos códigos de barras
                .WithOne(c => c.Producto) // Un código de barras pertenece a un solo producto
                .HasForeignKey(c => c.IdProducto) // La clave foránea es IdProducto en CodigoDeBarras
                .OnDelete(DeleteBehavior.Cascade); // Si se elimina un producto, los códigos de barras relacionados se eliminan también
        }
    }
}
