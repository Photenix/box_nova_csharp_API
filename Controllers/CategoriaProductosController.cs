using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoxNovaSoftAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxNovaSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProductosController : ControllerBase
    {
        private readonly BoxNovaDbContext _context;

        public CategoriaProductosController(BoxNovaDbContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProducto>>> GetCategoriaProductos()
        {
            return await _context.CategoriaProductos
                //.Include(c => c.SubCategorias) // Incluye las subcategorías asociadas
                .ToListAsync();
        }

        // GET: api/CategoriaProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProducto>> GetCategoriaProducto(int id)
        {
            var categoriaProducto = await _context.CategoriaProductos
                //.Include(c => c.SubCategorias) // Incluye las subcategorías asociadas
                .FirstOrDefaultAsync(c => c.IdCProd == id);

            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return categoriaProducto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaProducto(int id, CategoriaProducto categoriaProducto)
        {
            if (id != categoriaProducto.IdCProd)
            {
                return BadRequest();
            }

            // Verificar si la categoría existe antes de actualizar
            var categoriaExistente = await _context.CategoriaProductos
                .FirstOrDefaultAsync(c => c.IdCProd == id);

            if (categoriaExistente == null)
            {
                return NotFound($"No se encontró la categoría con ID {id}");
            }

            // Actualizar propiedades específicas si es necesario
            categoriaExistente.NombreCProd = categoriaProducto.NombreCProd;
            // Actualiza otras propiedades según sea necesario

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Manejo más detallado de errores
                return StatusCode(500, $"Error al actualizar la categoría: {ex.Message}");
            }

            return NoContent();
        }
        // POST: api/CategoriaProductos
        [HttpPost]
        public async Task<ActionResult<CategoriaProducto>> PostCategoriaProducto(CategoriaProducto categoriaProducto)
        {
            _context.CategoriaProductos.Add(categoriaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaProducto", new { id = categoriaProducto.IdCProd }, categoriaProducto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaProducto(int id)
        {
            // Verificar si la categoría existe
            var categoriaProducto = await _context.CategoriaProductos
                .FirstOrDefaultAsync(c => c.IdCProd == id);

            if (categoriaProducto == null)
            {
                return NotFound($"No se encontró la categoría con ID {id}");
            }

            // Verificar si hay subcategorías asociadas
            bool tieneSubcategorias = await _context.SubCategoriaProductos
                .AnyAsync(sc => sc.IdCProd == id);

            if (tieneSubcategorias)
            {
                return BadRequest($"No se puede eliminar la categoría con ID {id} porque tiene subcategorías asociadas.");
            }

            // Si no hay subcategorías, proceder con la eliminación
            _context.CategoriaProductos.Remove(categoriaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}