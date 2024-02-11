using TaskPro.Data;
using TaskPro.Models.Comentarios;

namespace TaskPro.Models.Tareas
{
    public static class TareaMAP
    {
        public static TareaDTO toDTO (this Data.Tareas obj)
        {
            return new TareaDTO
            {
                Id = obj.Id,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion,
                AsignadoA = obj.AsignadoA,
                ProyectoId = obj.ProyectoId,
                Estado = obj.Estado,
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                Comentarios = new List<Comentarios.ComentarioDTO>()
            };
        }
        public static TareaDTO toDTO(this Data.Tareas obj,List<ComentarioDTO> comentarios)
        {
            return new TareaDTO
            {
                Id = obj.Id,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion,
                AsignadoA = obj.AsignadoA,
                ProyectoId = obj.ProyectoId,
                Estado = obj.Estado,
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                Comentarios = comentarios
            };
        }
    }
}
