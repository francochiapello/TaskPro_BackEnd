namespace TaskPro.Models.Comentarios
{
    public static class ComentarioMAP
    {
        public static ComentarioDTO toDTO(this Data.Comentarios obj)
        {
            return new ComentarioDTO{
                Id = obj.Id,
                TareaId = obj.TareaId,
                Contenido = obj.Contenido,
                UsuarioId = obj.UsuarioId,
                UpdatedAt = obj.UpdatedAt,
                CreatedAt = obj.CreatedAt,
            };
        }
    }
}
