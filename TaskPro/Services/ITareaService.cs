using TaskPro.Models.Tareas;
using TaskPro.Persistence;

namespace TaskPro.Services
{
    public interface ITareaService
    {
        Task<List<AsignedTareaDTO>> GetAll();
        Task<List<TareaDTO>> findByProyect(int proyectId); 
        Task<TareaDTO> getOneByIdAsync(string id);
        Task<TareaDTO> createAsync(CreateTareaDTO data);
        Task<TareaDTO> updateAsync(string id, UpdateTareaDTO data);
        Task<TareaDTO> deleteAsync(string id);
        void setDao(TareaDAO tareaDAO,ComentarioDAO comentarioDAO);
        void setUser(int userLogged);
    }
}
