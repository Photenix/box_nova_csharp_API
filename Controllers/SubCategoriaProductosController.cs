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
    public class SubCategoriaProductosController : ControllerBase
    {
        private readonly BoxNovaDbContext _context;

        public SubCategoriaProductosController(BoxNovaDbContext context)
        {
            _context = context;
        }

        // GET: api/SubCategoriaProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoriaProducto>>> GetSubCategoriaProductos()
        {
            // Incluir la propiedad de navegación CategoriaProducto
            return await _context.SubCategoriaProductos
                //.Include(s => s.CategoriaProducto)  // Incluir la relación con CategoriaProducto
                .ToListAsync();
        }

        // GET: api/SubCategoriaProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoriaProducto>> GetSubCategoriaProducto(int id)
        {
            var subCategoriaProducto = await _context.SubCategoriaProductos
                //.Include(s => s.CategoriaProducto)  // Incluir la relación con CategoriaProducto
                .FirstOrDefaultAsync(s => s.IdSubCProd == id);

            if (subCategoriaProducto == null)
            {
                return NotFound();
            }

            return subCategoriaProducto;
        }

        // Método para actualizar subcategoría con validaciones
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoriaProducto(int id, SubCategoriaProducto subCategoriaProducto)
        {
            // Validar que el ID de la solicitud coincida con el ID de la subcategoría
            if (id != subCategoriaProducto.IdSubCProd)
            {
                return BadRequest("El ID de la subcategoría no coincide");
            }

            // Verificar si la subcategoría existe
            var subcategoriaExistente = await _context.SubCategoriaProductos
                .FirstOrDefaultAsync(sc => sc.IdSubCProd == id);

            if (subcategoriaExistente == null)
            {
                return NotFound($"No se encontró la subcategoría con ID {id}");
            }

            // Verificar si la categoría asociada existe
            var categoriaExistente = await _context.CategoriaProductos
                .FirstOrDefaultAsync(c => c.IdCProd == subCategoriaProducto.IdCProd);

            if (categoriaExistente == null)
            {
                return BadRequest($"La categoría con ID {subCategoriaProducto.IdCProd} no existe");
            }

            try
            {
                // Actualizar propiedades específicas
                subcategoriaExistente.NombreSubCProd = subCategoriaProducto.NombreSubCProd;
                subcategoriaExistente.IdCProd = subCategoriaProducto.IdCProd;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error al actualizar la subcategoría: {ex.Message}");
            }
        }

        // POST: api/SubCategoriaProductos
        [HttpPost]
        public async Task<ActionResult<SubCategoriaProducto>> PostSubCategoriaProducto(SubCategoriaProducto subCategoriaProducto)
        {
            // Verificar si la categoría existe
            var categoria = await _context.CategoriaProductos
                                          .FirstOrDefaultAsync(c => c.IdCProd == subCategoriaProducto.IdCProd);

            if (categoria == null)
            {
                return BadRequest($"La categoría con ID {subCategoriaProducto.IdCProd} no existe.");
            }

            // Limpiar la propiedad CategoriaProducto para evitar conflictos
            subCategoriaProducto.CategoriaProducto = null;

            // Si la categoría existe, proceder con la inserción de la subcategoría
            _context.SubCategoriaProductos.Add(subCategoriaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoriaProducto", new { id = subCategoriaProducto.IdSubCProd }, subCategoriaProducto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoriaProducto(int id)
        {
            // Verificar si la subcategoría existe
            var subCategoriaProducto = await _context.SubCategoriaProductos
                //.Include(sc => sc.CategoriaProducto) // Incluir la categoría relacionada
                .FirstOrDefaultAsync(sc => sc.IdSubCProd == id);

            if (subCategoriaProducto == null)
            {
                return NotFound($"No se encontró la subcategoría con ID {id}");
            }

            // Verificar si hay productos asociados a esta subcategoría
            bool tieneProductosAsociados = await _context.Productos
                .AnyAsync(p => p.IdProducto == id);

            if (tieneProductosAsociados)
            {
                return BadRequest($"No se puede eliminar la subcategoría con ID {id} porque tiene productos asociados.");
            }

            try
            {
                // Eliminar la subcategoría
                _context.SubCategoriaProductos.Remove(subCategoriaProducto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                // Manejo de errores de base de datos
                return StatusCode(500, $"Error al eliminar la subcategoría: {ex.Message}");
            }
        }
    }
}

