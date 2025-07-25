namespace GestionTareasDesarolloSoftware.API.Models
{
    public class Tarea
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaLimite { get; set; }
        public string estado { get; set; }
        public int? usuarioId { get; set; }
        public int? proyectoId { get; set; }
    }
}
