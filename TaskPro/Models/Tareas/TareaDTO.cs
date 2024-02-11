using TaskPro.Models.Comentarios;

namespace TaskPro.Models.Tareas
{
    public class TareaDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ProyectoId { get; set; }
        public int AsignadoA { get; set; }
        public string Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ComentarioDTO> Comentarios { get; set; }
    }
    public class CreateTareaDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Comentario { get; set; }
        public int ProyectoId { get; set; }
        public int AsignadoA { get; set; }
        public string Estado { get; set; }
    }
    public class UpdateTareaDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ProyectoId { get; set; }
        public int AsignadoA { get; set; }
        public string Estado { get; set; }
    }
}
