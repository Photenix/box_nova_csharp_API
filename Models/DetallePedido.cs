using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEmpresa.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetPedido { get; set; }

        public int IdPedido { get; set; }
        [ForeignKey("IdPedido")]
        public Pedido Pedido { get; set; }

        public int IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecUnitario { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
    }
}
