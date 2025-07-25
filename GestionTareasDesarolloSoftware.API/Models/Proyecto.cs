namespace GestionTareasDesarolloSoftware.API.Models
{
    public class Proyecto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string estado { get; set; }
        public int? usuarioId { get; set; }
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
