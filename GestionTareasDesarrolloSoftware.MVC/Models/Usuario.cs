using System.ComponentModel.DataAnnotations;

namespace GestionTareasDesarrolloSoftware.MVC.Models
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
    }
}