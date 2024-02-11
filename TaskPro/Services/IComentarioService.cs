using TaskPro.Models.Comentarios;
using TaskPro.Models.Tareas;
using TaskPro.Persistence;

namespace TaskPro.Services
{
    public interface IComentarioService
    {
        Task<List<ComentarioDTO>> GetAll();
        Task<ComentarioDTO> getOneByIdAsync(string id);
        Task<ComentarioDTO> createAsync(CreateComentarioDTO data);
        Task<ComentarioDTO> updateAsync(string id, UpdateComentarioDTO data);
        Task<ComentarioDTO> deleteAsync(string id);
        void setDao(ComentarioDAO comentarioDAO, TareaDAO tareaDAO);
        void setUsuario(int id);
    }
}
