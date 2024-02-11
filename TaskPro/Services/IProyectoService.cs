using TaskPro.Models.Proyectos;

namespace TaskPro.Services
{
    public interface IProyectoService
    {
        Task<ProyectoDTO?> getOneByIdAsync(int id);  
        Task<List<ProyectoDTO>> getAllAsync();
        Task<ProyectoDTO> createAsync(CreateProyectoDTO data);
        Task<ProyectoDTO> updateAsync(int id,UpdateProyectoDTO data);
        void delete(int id);
        void setId(int userLogged);
    }
}
