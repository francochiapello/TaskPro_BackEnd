using TaskPro.Models.Usuarios;

namespace TaskPro.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> loginAsync(LoginDTO data);
        Task<UsuarioDTO> registerAsync(RegisterDTO data);
        Task<UsuarioDTO> createAsync(CreateUsuarioDTO data);
        Task<UsuarioDTO> updateAsync(int id, UsuarioDTO data);
        void deleteAsync(int id);
        Task<List<UsuarioDTO>> getAllAsync();
        Task<UsuarioDTO> getOneAsync(int id);
    }
}
