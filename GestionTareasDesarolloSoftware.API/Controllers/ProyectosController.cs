using Dapper;
using GestionTareasDesarolloSoftware.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace GestionTareasDesarolloSoftware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectosController : ControllerBase
    {
        private readonly DbConnection connection;

        public ProyectosController(IConfiguration config)
        {
            var connString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }

        // GET: api/Proyectos
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var proyectos = connection.Query<Proyecto>("SELECT * FROM Proyectos").ToList();
            return proyectos;
        }

        // GET api/Proyectos/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var proyecto = connection.QuerySingleOrDefault<Proyecto>(
                "SELECT * FROM Proyectos WHERE id = @id", new { id });
            return proyecto;
        }

        // POST api/Proyectos
        [HttpPost]
        public dynamic Post([FromBody] Proyecto proyecto)
        {
            connection.Execute(
                "INSERT INTO Proyectos (nombre, descripcion, FechaInicio, FechaEntrega, estado) VALUES (@nombre, @descripcion, @FechaInicio, @FechaEntrega, @estado)",
                new
                {
                    proyecto.nombre,
                    proyecto.descripcion,
                    proyecto.FechaInicio,
                    proyecto.FechaEntrega,
                    proyecto.estado
                });
            return proyecto;
        }

        // PUT api/Proyectos/5
        [HttpPut("{id}")]
        public dynamic Put(int id, [FromBody] Proyecto proyecto)
        {
            connection.Execute(
                "UPDATE Proyectos SET nombre = @nombre, descripcion = @descripcion, FechaCreacion = @FechaCreacion, FechaLimite = @FechaLimite, estado = @estado WHERE id = @id",
                new
                {
                    id,
                    proyecto.nombre,
                    proyecto.descripcion,
                    proyecto.FechaInicio,
                    proyecto.FechaEntrega,
                    proyecto.estado
                });
            return proyecto;
        }

        // DELETE api/Proyectos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Proyectos WHERE id = @id", new { id });
        }
    }
}