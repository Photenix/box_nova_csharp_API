using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoxNovaSoftAPI.Models;
using BoxNovaSoftAPI.Models.Customs;
using BoxNovaSoftAPI.Services;

namespace BoxNovaSoftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BoxNovaDbContext _context;
        private readonly IAutorizationService _autorizationService;

        public UsuariosController(BoxNovaDbContext context, IAutorizationService autorizationService)
        {
            _context = context;
            _autorizationService = autorizationService;
        }

        // ************************** Autentificación de usuarios ************************** //

        //Cadena de autentificación de usuarios
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] AutorizacionRequest autorizacion )
        {
            var resultado_autorization = await _autorizationService.DevolverToken(autorizacion);
            if (resultado_autorization == null)
                return Unauthorized();
            return Ok(resultado_autorization);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] Usuario usuario)
        {
            var usuario_encontrado = _context.Usuarios.FirstOrDefault( x =>
                x.CorreoUsuario == usuario.CorreoUsuario
            );
            if( usuario_encontrado == null )
            {
                _context.Usuarios.Add(usuario);
                try
                {
                    await _context.SaveChangesAsync();
                    //_context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (UsuarioExists(usuario.IdUsuario))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(usuario);
            }
            //El correo ya ha sido creado por lo tanto no se crea el nuevo usuario
            else return BadRequest();
        }


        // ************************** INFORMACION DE USUARIOS ************************** //

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.IdUsuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
