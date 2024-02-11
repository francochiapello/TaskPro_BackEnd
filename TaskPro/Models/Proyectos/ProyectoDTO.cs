using TaskPro.Data;

namespace TaskPro.Models.Proyectos
{
    public class ProyectoDTO
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int CreadorId { get; set; }

        public string CreadorNombre { get; set; }
    }
    public class CreateProyectoDTO
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
    public class UpdateProyectoDTO : CreateProyectoDTO
    {

    }
}
