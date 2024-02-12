using TaskPro.Models.Shared;

namespace TaskPro.Models.Comentarios
{
    public static class ComentarioMAP
    {
        public static ComentarioDTO toDTO(this Data.Comentarios obj)
        {
            var usuario = obj.UsuarioId.getUser();
            return new ComentarioDTO{
                Id = obj.Id,
                TareaId = obj.TareaId,
                Contenido = obj.Contenido,
                UsuarioId = obj.UsuarioId,
                UsuarioNombre = usuario is null ? string.Empty : usuario.Nombre,
                UpdatedAt = obj.UpdatedAt,
                CreatedAt = obj.CreatedAt,
            };
        }
    }
}
