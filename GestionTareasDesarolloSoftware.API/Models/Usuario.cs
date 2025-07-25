using System.ComponentModel.DataAnnotations;

namespace GestionTareasDesarolloSoftware.API.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string passwordHash { get; set; }
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
        public ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
    }
}