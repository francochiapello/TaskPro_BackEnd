namespace TaskPro.Models.Comentarios
{
    public class ComentarioDTO
    {
        public string Id { get; set; }
        public string Contenido { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public string TareaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class CreateComentarioDTO
    {
        public string Contenido { get; set; }
        public int UsuarioId { get; set; }
        public string TareaId { get; set; }
    }
    public class UpdateComentarioDTO : CreateComentarioDTO
    {
    }
}
