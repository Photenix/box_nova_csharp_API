using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoxNovaSoftAPI.Models;
using Microsoft.AspNetCore.Authorization;
using BoxNovaSoftAPI.Models.Update;
using BoxNovaSoftAPI.Models.DTO;

namespace BoxNovaSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly BoxNovaDbContext _context;

        public RolesController(BoxNovaDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        {
            //var roles = await _context.Roles.Include(r => r.PerXrolXprivs).ToListAsync();
            //var roles = await _context.PerXrolXprivs.ToListAsync();
            //return roles;
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PerXrolXprivDTO>>> GetRol(int id)
        {
            //var rol = await _context.Roles.FindAsync(id);
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == id);

            if (rol == null)
            {
                return NotFound();
            }

            var priv = await (
                from r in _context.Roles
                join p in _context.PerXrolXprivs on r.IdRol equals p.IdRol
                join x in _context.Permisos on p.IdPer equals x.IdPermiso
                join pri in _context.Privilegios on p.IdPriv equals pri.IdPrivilegio

                select new PerXrolXprivDTO
                {
                    IdPerxrol = p.IdPerxrol,
                    NombrePermiso = x.NombrePermiso,
                    NombreRol = r.NombreRol,
                    NombrePrivilegio = pri.NombrePrivilegio
                }
            ).ToListAsync();
            /*
            var priv = _context.PerXrolXprivs.Where(r => r.IdRol == id).Select(e => new PerXrolXprivDTO
            {
                IdPerxrol = e.IdPerxrol,

                IdPer = e.IdPer,

                IdRol = e.IdRol,

                IdPriv = e.IdPriv,
            }).ToList();
            */

            if (priv.Any()) {
                Console.WriteLine(priv[0].IdPerxrol);
                //rol.PerXrolXprivs = priv;
            }
            else
            {
                priv = [];
            }

            return priv;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize]
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutRol(int id, Rol rol)
        public async Task<IActionResult> PutRol(int id, [FromBody] RolUpdate formRol)
        {
            if (formRol == null)
            {
                return BadRequest();
            }

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            if( rol.NombreRol == "Administrador")
            {
                return BadRequest(new { mesage = "No se puede modificar el rol de Administrador" });
            }

            if (formRol.NombreRol != null) {
                rol.NombreRol = formRol.NombreRol;
            }

            if( formRol.EstadoRol != null )
                rol.EstadoRol = (bool)formRol.EstadoRol;
            
            /*

            if (id != rol.IdRol)
            {
                return BadRequest();
            }
            */

            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
            _context.Roles.Add(rol);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RolExists(rol.IdRol))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRol", new { id = rol.IdRol }, rol);
        }

        // DELETE: api/Roles/5
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            if( rol.NombreRol == "Administrador")
            {
                return BadRequest(new { message = "El rol Administrador no se puede eliminar ya que es un rol esencial" });
            }

            var user = _context.Usuarios.FirstOrDefault(e => e.FkRol == id);

            if( user != null)
            {
                //No se puede eliminar un rol que tiene un usuario
                return BadRequest(new { message = "No se puede eliminar el rol porque está asociado a uno o más usuarios." });
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.IdRol == id);
        }
    }
}
