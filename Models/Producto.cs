namespace BoxNovaSoftAPI.Models
{
    public class Producto
    {
        public string NombreProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public int StockProducto { get; set; }
        public string? CategoriaProducto { get; set; }
        public string? ClasificacionProducto { get; set; }
        public bool EstadoProducto { get; set; }
    }
}
