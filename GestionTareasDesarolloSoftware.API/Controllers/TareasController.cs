using Dapper;
using GestionTareasDesarolloSoftware.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Threading;

namespace GestionTareasDesarolloSoftware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly DbConnection connection;

        public TareasController(IConfiguration config)
        {
            var connString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connString);
            connection.Open();
        }

        // GET: api/Proyectos
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var tareas = connection.Query<Tarea>("SELECT * FROM Tareas").ToList();
            return tareas;
        }

        // GET api/Proyectos/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var tarea = connection.QuerySingleOrDefault<Tarea>(
                "SELECT * FROM Tareas WHERE id = @id", new { id });
            return tarea;
        }

        // POST api/Proyectos
        [HttpPost]
        public dynamic Post([FromBody] Tarea tarea)
        {
            connection.Execute(
                "INSERT INTO Tareas (nombre, descripcion, FechaInicio, FechaLimite, estado, usuarioId) VALUES (@nombre, @descripcion, @FechaInicio, @FechaLimite, @estado, @usuarioId)",
                new
                {
                    tarea.nombre,
                    tarea.descripcion,
                    tarea.FechaInicio,
                    tarea.FechaLimite,
                    tarea.estado,
                    tarea.usuarioId
                });
            return tarea;
        }

        // PUT api/Proyectos/5
        [HttpPut("{id}")]
        public dynamic Put(int id, [FromBody] Tarea tarea)
        {
            connection.Execute(
                "UPDATE Tareas SET nombre = @nombre, descripcion = @descripcion, FechaInicio = @FechaInicio, FechaLimite = @FechaLimite, estado = @estado, usuarioId = @usuarioId WHERE id = @id",
                new
                {
                    id,
                    tarea.nombre,
                    tarea.descripcion,
                    tarea.FechaInicio,
                    tarea.FechaLimite,
                    tarea.estado,
                    tarea.usuarioId
                });
            return tarea;
        }

        // DELETE api/Proyectos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connection.Execute("DELETE FROM Tareas WHERE id = @id", new { id });
        }
    }
}
