using TaskPro.Data;
using TaskPro.Models.Comentarios;
using TaskPro.Models.Proyectos;
using TaskPro.Models.Shared;

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
            var asignado = obj.AsignadoA.getUser();
            return new TareaDTO
            {
                Id = obj.Id,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion,
                AsignadoA = obj.AsignadoA,
                AsignadoNombre = asignado is null ? string.Empty : asignado.Nombre,
                ProyectoId = obj.ProyectoId,
                Estado = obj.Estado,
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                Comentarios = comentarios
            };
        }
        public static AsignedTareaDTO toDTOwithProyect(this Data.Tareas obj, List<ComentarioDTO> comentarios)
        {
            var asignado = obj.AsignadoA.getUser();
            var proyecto = obj.ProyectoId.getProyecto();
            return new AsignedTareaDTO
            {
                Id = obj.Id,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion,
                AsignadoA = obj.AsignadoA,
                AsignadoNombre = asignado is null ? string.Empty : asignado.Nombre,
                ProyectoId = obj.ProyectoId,
                Estado = obj.Estado,
                CreatedAt = obj.CreatedAt,
                UpdatedAt = obj.UpdatedAt,
                Comentarios = comentarios,
                Proyecto = proyecto is null ? new ProyectoDTO() : proyecto.toDTO()
            };
        }
    }
}
