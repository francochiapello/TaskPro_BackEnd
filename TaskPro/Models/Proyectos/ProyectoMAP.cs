using TaskPro.Data;

namespace TaskPro.Models.Proyectos
{
    public static class ProyectoMAP
    {
        public static ProyectoDTO toDTO(this Proyecto obj)
        {
            return new ProyectoDTO
            {
                Id = obj.Id,
                CreadorId = obj.CreadorId is null ? 0 : (int)obj.CreadorId,
                CreadorNombre = obj.Creador is null ? string.Empty : obj.Creador.Nombre,
                Nombre = obj.Nombre,
                Descripcion = obj.Descripcion,
            };
        }
    }
}
