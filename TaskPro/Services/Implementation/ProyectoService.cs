using TaskPro.Data;
using TaskPro.Models.Proyectos;
using TaskPro.Models.Shared;
using TaskPro.Persistence;

namespace TaskPro.Services.Implementation
{
    public class ProyectoService : IProyectoService
    {
        private int userLogged;
        private readonly ProyectoDAO proyectoDAO = new ProyectoDAO();
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        public async Task<ProyectoDTO> createAsync(CreateProyectoDTO data)
        {
            try
            {
                var usuarioLogged = await this.getOneByIdAsync(this.userLogged);
                if(usuarioLogged is null) throw new NotFoundException($"El usuario con el id={this.userLogged}, no existe.");

                var exist = await this.proyectoDAO.findIfExistByUser(userLogged, data.Nombre);
                if (exist != null) throw new AlreadyExistException($"El proyecto con el nombre={data.Nombre}, ya existe.");

                var nuevoProyecto = new Proyecto
                {
                    Nombre = data.Nombre,
                    Descripcion = data.Descripcion,
                    CreadorId = this.userLogged,
                };

                var result = await this.proyectoDAO.create(nuevoProyecto);
                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void delete(int id)
        {
            try
            {
                var exist = await this.proyectoDAO.getOneById(id);
                if (exist is null) throw new NotFoundException($"El proyecto con el id={id}, no existe.");

                this.proyectoDAO.remove(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProyectoDTO>> getAllAsync()
        {
            try
            {
                var result = await this.proyectoDAO.getAll();

                return result.Select(x => x.toDTO()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProyectoDTO?> getOneByIdAsync(int id)
        {
            try
            {
                var exist = await this.proyectoDAO.getOneById(id);
                if (exist is null) throw new NotFoundException($"El proyecto con el id={id}, no existe.");

                return exist.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setId(int userLogged)
        {
            this.userLogged = userLogged;
        }

        public async Task<ProyectoDTO> updateAsync(int id, UpdateProyectoDTO data)
        {
            try
            {
                var usuarioLogged = await this.getOneByIdAsync(this.userLogged);
                if (usuarioLogged is null) throw new NotFoundException($"El usuario con el id={this.userLogged}, no existe.");

                var exist = await this.proyectoDAO.findIfExistByUser(userLogged, data.Nombre);
                if (exist != null && exist.Id != id) throw new AlreadyExistException($"El proyecto con el nombre={data.Nombre}, ya existe.");

                var proyectoExist = await this.proyectoDAO.getOneById(id);
                if (proyectoExist is null) throw new NotFoundException($"El proyecto con el id={id}, no existe.");

                proyectoExist.Nombre = data.Nombre;
                proyectoExist.Descripcion = data.Descripcion;
                proyectoExist.UpdatedAt = DateTime.Now;

                var result = await this.proyectoDAO.update(proyectoExist);

                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
