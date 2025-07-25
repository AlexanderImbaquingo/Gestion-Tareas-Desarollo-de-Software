using Dapper;
using GestionTareasDesarolloSoftware.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace GestionTareasDesarolloSoftware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DbConnection connection;

        public UsuariosController(IConfiguration config)
        {
            var connString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }

        // GET: api/Usuarios
        [HttpGet]
        [Authorize]
        public IEnumerable<dynamic> Get()
        {
            var usuarios = connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
            return usuarios;
        }

        // GET api/Usuarios/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var usuario = connection.QuerySingleOrDefault<Usuario>(
                "SELECT * FROM Usuarios WHERE id = @id", new { id });
            return usuario;
        }

        // POST api/Usuarios
        [HttpPost]
        public dynamic Post([FromBody] Usuario usuario)
        {
            connection.Execute(
                "INSERT INTO Usuarios (nombre, email, passwordHash) VALUES (@nombre, @email, @passwordHash)",
                new
                {
                    usuario.nombre,
                    usuario.email,
                    usuario.passwordHash
                });
            return usuario;
        }

        // PUT api/Usuarios/5
        [HttpPut("{id}")]
        public dynamic Put(int id, [FromBody] Usuario usuario)
        {
            connection.Execute(
                "UPDATE Usuarios SET nombre = @nombre, email = @email, passwordHash = @passwordHash WHERE id = @id",
                new
                {
                    id,
                    usuario.nombre,
                    usuario.email,
                    usuario.passwordHash
                });
            return usuario;
        }

        // DELETE api/Usuarios/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Usuarios WHERE id = @id", new { id });
        }

        // POST api/Usuarios/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario login)
        {
            var usuario = connection.QuerySingleOrDefault<Usuario>(
                "SELECT * FROM Usuarios WHERE email = @email", new { login.email });

            if (usuario == null || usuario.passwordHash != login.passwordHash)
                return Unauthorized("Usuario o contraseña incorrectos");

            var jwtKey = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Key"];
            var jwtIssuer = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Issuer"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
                new Claim(ClaimTypes.Name, usuario.nombre),
                new Claim(ClaimTypes.Email, usuario.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}