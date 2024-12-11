using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoxNovaSoftAPI.Models;

namespace BoxNovaSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegiosController : ControllerBase
    {
        private readonly BoxNovaDbContext _context;

        public PrivilegiosController(BoxNovaDbContext context)
        {
            _context = context;
        }

        // GET: api/Privilegios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Privilegio>>> GetPrivilegios()
        {
            return await _context.Privilegios.ToListAsync();
        }

        // GET: api/Privilegios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Privilegio>> GetPrivilegio(int id)
        {
            var privilegio = await _context.Privilegios.FindAsync(id);

            if (privilegio == null)
            {
                return NotFound();
            }

            return privilegio;
        }

        // PUT: api/Privilegios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrivilegio(int id, Privilegio privilegio)
        {
            if (id >= 1 && id <= 5)
            {
                return BadRequest(new { message = "El id seleccionado no se puede editar es información base" });
            }

            if (id != privilegio.IdPrivilegio)
            {
                return BadRequest();
            }

            _context.Entry(privilegio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrivilegioExists(id))
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

        // POST: api/Privilegios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Privilegio>> PostPrivilegio(Privilegio privilegio)
        {
            _context.Privilegios.Add(privilegio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PrivilegioExists(privilegio.IdPrivilegio))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPrivilegio", new { id = privilegio.IdPrivilegio }, privilegio);
        }

        // DELETE: api/Privilegios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrivilegio(int id)
        {
            if( id >= 1 && id <= 5)
            {
                return BadRequest(new { message = "El id seleccionado no se puede eliminar es información base" });
            }

            var privilegio = await _context.Privilegios.FindAsync(id);
            if (privilegio == null)
            {
                return NotFound();
            }

            _context.Privilegios.Remove(privilegio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrivilegioExists(int id)
        {
            return _context.Privilegios.Any(e => e.IdPrivilegio == id);
        }
    }
}
