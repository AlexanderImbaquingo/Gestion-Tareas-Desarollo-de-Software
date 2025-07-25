using Dapper;
using GestionTareasDesarolloSoftware.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

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
    }
}