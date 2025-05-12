using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mutual.server.Data;
using Mutual.Server.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Mutual.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public PersonaController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarPersona([FromBody] PersonaRegistro registro)
        {
            var nuevaPersona = new Persona
            {
                DniCuit = registro.DniCuit,
                Rol = registro.Rol,
                NombreRazonSocial = registro.NombreRazonSocial,
                Direccion = registro.Direccion,
                Telefono = registro.Telefono,
                Email = registro.Email,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                //Activo = true
            };

            _context.Personas.Add(nuevaPersona);
            await _context.SaveChangesAsync();

            return Ok(nuevaPersona);
        }

        [HttpPut("personas/{id}")]
        public async Task<IActionResult> ActualizarPersona(int id, [FromBody] PersonaRegistro actualizacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
                return NotFound(new { mensaje = "Persona no encontrada" });

            persona.DniCuit = actualizacion.DniCuit;
            persona.Rol = actualizacion.Rol;
            persona.UpdatedAt = DateTime.Now;

            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Persona actualizada correctamente", persona });
        }


        [HttpGet("personas")]
        public async Task<IActionResult> GetPersonas()
        {
            var personas = await _context.Personas.ToListAsync();
            return Ok(personas);
        }

        [HttpGet("personas/{id}")]
        public async Task<IActionResult> GetPersonaPorId(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
                return NotFound(new { mensaje = "Persona no encontrada" });

            return Ok(persona);
        }

    }
}
