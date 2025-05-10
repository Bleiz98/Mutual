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

        [HttpPost]
        public IActionResult CrearPersona([FromBody] Persona persona)
        {
            // Aquí guardarías a la persona en la base de datos (por ahora solo devolvemos la persona como ejemplo)
            return Ok(persona);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarPersona([FromBody] PersonaRegistro registro)
        {
            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registro.PasswordHash);
            var hashedPassword = registro.PasswordHash;

            var nuevaPersona = new Persona
            {
                DniCuit = registro.DniCuit,
                Username = registro.Username,
                PasswordHash = hashedPassword,
                Rol = registro.Rol,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                //Activo = true
            };

            _context.Personas.Add(nuevaPersona);
            await _context.SaveChangesAsync();

            return Ok(nuevaPersona);
        }

        //[HttpPost("login")]//Version de login simpre
        //public async Task<IActionResult> Login([FromBody] PersonaLogin login)
        //{
        //    var persona = await _context.Personas
        //        .FirstOrDefaultAsync(p => p.Username == login.Username);

        //    if (persona == null)
        //    {
        //        return Unauthorized(new { mensaje = "Usuario no encontrado" });
        //    }

        //    // Comparar hashes directamente si ya están hasheados
        //    if (persona.PasswordHash != login.PasswordHash)
        //    {
        //        return Unauthorized(new { mensaje = "Contraseña incorrecta" });
        //    }

        //    // Opcional: Generar token JWT o retornar datos básicos
        //    return Ok(new
        //    {
        //        mensaje = "Login exitoso",
        //        persona.Id,
        //        persona.Username,
        //        persona.Rol
        //    });
        //}

        //[HttpPost("login")]//Version de login q verifica contrseña hasheada
        //public async Task<IActionResult> Login([FromBody] PersonaLogin login)
        //{
        //    var persona = await _context.Personas
        //        .FirstOrDefaultAsync(p => p.Username == login.Username);

        //    if (persona == null)
        //    {
        //        return Unauthorized(new { mensaje = "Usuario no encontrado" });
        //    }

        //    // Verificar la contraseña con BCrypt
        //    bool passwordValida = BCrypt.Net.BCrypt.Verify(login.PasswordHash, persona.PasswordHash);

        //    if (!passwordValida)
        //    {
        //        return Unauthorized(new { mensaje = "Contraseña incorrecta" });
        //    }

        //    return Ok(new
        //    {
        //        persona.Id,
        //        persona.Username,
        //        persona.Rol
        //    });
        //}

        [HttpPost("login")]//Version de login q verifica contrseña hasheada, y retorna un token con los datos ingresado en Claim
        public async Task<IActionResult> Login([FromBody] PersonaLogin login)
        {
            var persona = await _context.Personas
                .FirstOrDefaultAsync(p => p.Username == login.Username);

            if (persona == null)
                return Unauthorized(new { mensaje = "Usuario no encontrado" });

            if (!BCrypt.Net.BCrypt.Verify(login.PasswordHash, persona.PasswordHash))
                return Unauthorized(new { mensaje = "Contraseña incorrecta" });

            var claims = new[]//Aca se ingresan los datos q quieras q se guarden en el token
            {
                new Claim("id", persona.Id.ToString()),
                new Claim("username", persona.Username),
                new Claim("rol", persona.Rol)
            };

            var token = GenerarJwtToken(claims);

            return Ok(new { token });//solamente retornara un token q sera guardado temporalmente en una session
        }
        private string GenerarJwtToken(IEnumerable<Claim> claims)//genera un token, y en este se ingresaran los datos q este en Claim
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[Authorize]
        //[HttpGet("perfil")]
        //public IActionResult GetPerfil()
        //{
        //    var userClaims = HttpContext.User.Claims;

        //    var id = userClaims.FirstOrDefault(c => c.Type == "id")?.Value;
        //    var username = userClaims.FirstOrDefault(c => c.Type == "username")?.Value;
        //    var rol = userClaims.FirstOrDefault(c => c.Type == "rol")?.Value;

        //    return Ok(new { id, username, rol });
        //}
    }
}
