using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using BoxNovaSoftAPI.Data;
using BoxNovaSoftAPI.Models;

namespace BoxNovaSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoesController : ControllerBase
    {
        private readonly BoxNovaDbContext _context;

        public PedidoesController(BoxNovaDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/Pedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            // Validar que el cliente exista
            var cliente = await _context.Clientes.FindAsync(pedido.IdCliente);
            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }

            // Establecer fecha actual si no se proporcionó
            if (pedido.FechaDelPedido == default)
            {
                pedido.FechaDelPedido = DateTime.Now;
            }

            // Establecer estado inicial si no se proporcionó
            if (string.IsNullOrEmpty(pedido.EstadoPedido))
            {
                pedido.EstadoPedido = "Pendiente";
            }

            // Verificar si los detalles del pedido tienen información
            if (pedido.DetallePedidos == null || !pedido.DetallePedidos.Any())
            {
                return BadRequest("El pedido debe contener al menos un detalle");
            }

            // Calcular monto total y subtotales
            try
            {
                pedido.MontoTotal = 0; // Resetear a 0 antes de calcular

                foreach (var detalle in pedido.DetallePedidos)
                {
                    // Validar que el producto exista
                    var producto = await _context.Productos.FindAsync(detalle.IdProducto);
                    if (producto == null)
                    {
                        return BadRequest($"Producto con ID {detalle.IdProducto} no encontrado");
                    }

                    // Calcular subtotal para cada detalle
                    detalle.Subtotal = detalle.Cantidad * detalle.PrecUnitario;

                    // Sumar al monto total del pedido
                    pedido.MontoTotal += detalle.Subtotal;
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al calcular el monto total: {ex.Message}");
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.IdPedido }, pedido);
        }

        // DELETE: api/Pedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedido == id);
        }
    }
}
