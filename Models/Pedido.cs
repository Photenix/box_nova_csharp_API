using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoxNovaSoftAPI.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }

        [Required]
        public DateTime FechaDelPedido { get; set; }

        [Required]
        public string EstadoPedido { get; set; }

        public ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
    }
}
