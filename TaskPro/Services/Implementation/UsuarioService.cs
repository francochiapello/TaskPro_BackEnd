using TaskPro.Data;
using TaskPro.Helpers;
using TaskPro.Models.Shared;
using TaskPro.Models.Usuarios;
using TaskPro.Persistence;

namespace TaskPro.Services.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        private readonly SecurityHelper securityHelper = new SecurityHelper();
        public async Task<UsuarioDTO> createAsync(CreateUsuarioDTO data)
        {
            try
            {
                var exist = await this.usuarioDAO.findIfExistByEmail(data.Email);
                if (exist != null) throw new AlreadyExistException($"El usuario con el email={data.Email}, ya existe.");

                var newUser = new Usuario 
                { 
                    Nombre = data.Nombre,
                    Email = data.Email,
                    Contraseña = this.securityHelper.Encrypt(data.Contraseña),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var result = await this.usuarioDAO.create(newUser);
                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void deleteAsync(int id)
        {
            try
            {
                var exist = await this.usuarioDAO.getOneById(id);
                if (exist is null) throw new NotFoundException($"El usuario con el id={id}, no existe.");

                this.usuarioDAO.remove(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UsuarioDTO>> getAllAsync()
        {
            try
            {
                var result = await this.usuarioDAO.getAllUsuarios();

                return result.Select(x => x.toDTO()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> getOneAsync(int id)
        {
            try
            {
                var result = await this.usuarioDAO.getOneById(id);
                if (result is null) throw new NotFoundException($"El usuario con el id={id}, no existe.");

                return result.toDTO();  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> loginAsync(LoginDTO data)
        {
            try
            {
                var exist = await this.usuarioDAO.findIfExistByEmail(data.Email);
                if (exist is null)
                {
                    throw new NotFoundException($"El usuario con email={data.Email}, no existe.");
                }
                else
                {
                    if (!exist.Contraseña.Equals(this.securityHelper.Encrypt(data.Contraseña)))
                    {
                        //Generar token
                        return exist.toDTO();
                    }
                    else
                    {
                        throw new ValidationException($"La contraseña ingresada es invalida.");
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> registerAsync(RegisterDTO data)
        {
            try
            {
                var exist = await this.usuarioDAO.findIfExistByEmail(data.Email);
                if (exist is null) throw new AlreadyExistException($"El usuario con el email={data.Email}, ya existe.");

                var newUser = new Usuario
                {
                    Nombre = data.Nombre,
                    Email = data.Email,
                    Contraseña = this.securityHelper.Encrypt(data.Contraseña),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var result = await this.usuarioDAO.create(newUser);
                return result.toDTO();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> updateAsync(int id, UsuarioDTO data)
        {
            try
            {
                var usuarioExist = await this.usuarioDAO.getOneById(id);
                if (usuarioExist is null) throw new NotFoundException($"El usuario con el id={id}, no existe.");

                var exist = await this.usuarioDAO.findIfExistByEmail(data.Email);
                if (exist != null && exist.Id != id) throw new AlreadyExistException($"Ya existe un usuario con el email={data.Email}, ya existe.");

                usuarioExist.Email = data.Email;
                usuarioExist.Nombre = data.Nombre;
                usuarioExist.UpdatedAt = DateTime.Now;

                var result = await this.usuarioDAO.update(usuarioExist);
                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
