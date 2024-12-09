using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BoxNovaDB.Models
{
    public class Venta
    {
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public string? Estado { get; set; }

        public List<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    }
}

