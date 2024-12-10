namespace BoxNovaSoftAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public int StockProducto { get; set; }
        public string? CategoriaProducto { get; set; }
        public string? ClasificacionProducto { get; set; }
        public bool EstadoProducto { get; set; }
        
        public string? Categoria { get; set; }
        public CategoriaProducto categoria { get; set; }

    }
}
