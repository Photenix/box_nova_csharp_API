﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoxNovaSoftAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxNovaSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly BoxNovaDbContext _context;

        public CarritosController(BoxNovaDbContext context)
        {
            _context = context;
        }

        // GET: api/Carritos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        {
            return await _context.Carritos
        .Include(c => c.Cliente) // Esto funciona si Cliente es una propiedad de navegación
        .ToListAsync();

        }

        // GET: api/Carritos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito>> GetCarrito(int id)
        {
            var carrito = await _context.Carritos.Include(c => c.Cliente).Include(c => c.IdProducto)
                                .FirstOrDefaultAsync(c => c.IdDetalle == id);

            if (carrito == null)
            {
                return NotFound();
            }

            return carrito;
        }

        // PUT: api/Carritos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.IdDetalle)
            {
                return BadRequest();
            }

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoExists(id))
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

        // POST: api/Carritos
        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCarrito(Carrito carrito)
        {
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrito", new { id = carrito.IdDetalle }, carrito);
        }

        // DELETE: api/Carritos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.IdDetalle == id);
        }
    }
}
