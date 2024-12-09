namespace BoxNovaSoftAPI.Models
{
    public class Carrito
    {
        public int IdDetalle { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public float Subtotal { get; set; }

        public Cliente Cliente { get; set; } = null!;
    }
}
