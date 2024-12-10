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
        private readonly SebasSPContext _context;

        public SubCategoriaProductosController(SebasSPContext context)
        {
            _context = context;
        }

        // GET: api/SubCategoriaProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoriaProducto>>> GetSubCategoriaProductos()
        {
            return await _context.SubCategoriaProductos.Include(s => s.IdCProd).ToListAsync();
        }

        // GET: api/SubCategoriaProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoriaProducto>> GetSubCategoriaProducto(int id)
        {
            var subCategoriaProducto = await _context.SubCategoriaProductos.Include(s => s.IdCProd)
                .FirstOrDefaultAsync(s => s.IdSubCProd == id);

            if (subCategoriaProducto == null)
            {
                return NotFound();
            }

            return subCategoriaProducto;
        }

        // PUT: api/SubCategoriaProductos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoriaProducto(int id, SubCategoriaProducto subCategoriaProducto)
        {
            if (id != subCategoriaProducto.IdSubCProd)
            {
                return BadRequest();
            }

            _context.Entry(subCategoriaProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoriaProductoExists(id))
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

        // POST: api/SubCategoriaProductos
        [HttpPost]
        public async Task<ActionResult<SubCategoriaProducto>> PostSubCategoriaProducto(SubCategoriaProducto subCategoriaProducto)
        {
            _context.SubCategoriaProductos.Add(subCategoriaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoriaProducto", new { id = subCategoriaProducto.IdSubCProd }, subCategoriaProducto);
        }

        // DELETE: api/SubCategoriaProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoriaProducto(int id)
        {
            var subCategoriaProducto = await _context.SubCategoriaProductos.FindAsync(id);
            if (subCategoriaProducto == null)
            {
                return NotFound();
            }

            _context.SubCategoriaProductos.Remove(subCategoriaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoriaProductoExists(int id)
        {
            return _context.SubCategoriaProductos.Any(e => e.IdSubCProd == id);
        }
    }
}
