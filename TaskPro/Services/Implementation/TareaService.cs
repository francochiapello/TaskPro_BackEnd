using TaskPro.Data;
using TaskPro.Models.Comentarios;
using TaskPro.Models.Proyectos;
using TaskPro.Models.Shared;
using TaskPro.Models.Tareas;
using TaskPro.Persistence;

namespace TaskPro.Services.Implementation
{
    public class TareaService : ITareaService
    {
        private TareaDAO tareaDAO;
        private ComentarioDAO comentarioDAO;
        private int userLogged;
        private readonly ProyectoDAO proyectoDAO = new ProyectoDAO();
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        public void setDao(TareaDAO tareaDAO, ComentarioDAO comentarioDAO)
        {
            this.tareaDAO = tareaDAO;
            this.comentarioDAO = comentarioDAO;
        }
        public void setUser(int userLogged)
        {
            this.userLogged = userLogged;
        }
        public async Task<List<AsignedTareaDTO>> GetAll()
        {
            try
            {
                var tareas = await this.tareaDAO.getAll();

                var task = tareas.Select(async (x) =>
                {
                    var comentarios = await this.comentarioDAO.findByTareaId(x.Id);
                    var comentariosDTO = comentarios.Select(c => c.toDTO()).ToList();

                    return x.toDTOwithProyect(comentariosDTO);
                });

                var result = await Task.WhenAll(task);

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<TareaDTO>> findByProyect(int proyectId)
        {
            try
            {
                var tareas = await this.tareaDAO.findByProyectId(proyectId);

                var task = tareas.Select(async x =>
                {
                    var comentarios = await this.comentarioDAO.findByTareaId(x.Id);
                    var comentariosDTO = comentarios.Select(c => c.toDTO()).ToList();   

                    return x.toDTO(comentariosDTO);
                });

                var result = await Task.WhenAll(task);

                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        public async Task<TareaDTO> getOneByIdAsync(string id)
        {
            try
            {
                var result = await this.tareaDAO.getOneById(id);
                if(result is null) throw new NotFoundException($"La tarea con el id={id}, no existe.");

                var comentarios = await this.comentarioDAO.findByTareaId(id);
                var comentariosDTO = comentarios.Select(c => c.toDTO()).ToList();

                return result.toDTO(comentariosDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TareaDTO> createAsync(CreateTareaDTO data)
        {
            try
            {
                var exist = await this.tareaDAO.findIfExist(data.Nombre, data.ProyectoId);
                if (exist != null) throw new AlreadyExistException($"La tarea ingresada ya existe para el proyecto con id={data.ProyectoId}.");

                var asignadoA = await this.usuarioDAO.getOneById(data.AsignadoA);
                if (asignadoA is null) throw new NotFoundException($"El usuario a asignar con el id={data.AsignadoA}, no existe.");

                var proyecto = await proyectoDAO.getOneById(data.ProyectoId);
                if (proyecto is null) throw new NotFoundException($"El proyecto ingresado con el id={data.ProyectoId}, no existe.");

                var newTarea = new Tareas
                {
                    Nombre = data.Nombre,
                    Descripcion = data.Descripcion,
                    AsignadoA = data.AsignadoA,
                    ProyectoId = data.ProyectoId,
                    Estado = data.Estado,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var result = await this.tareaDAO.create(newTarea);

                if (!string.IsNullOrWhiteSpace(data.Comentario))
                {
                    var newComentario = new Comentarios
                    {
                        Contenido = data.Comentario,
                        TareaId = result.Id,
                        UsuarioId = this.userLogged,
                        UpdatedAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                    };

                    var comentario = await this.comentarioDAO.create(newComentario);
                }

                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TareaDTO> updateAsync(string id, UpdateTareaDTO data)
        {
            try
            {
                var tareaExist = await this.tareaDAO.getOneById(id);
                if (tareaExist is null) throw new NotFoundException($"La tarea con el id={id}, no existe.");

                var exist = await this.tareaDAO.findIfExist(data.Nombre, data.ProyectoId);
                if (exist != null && exist.Id != id) throw new AlreadyExistException($"La tarea ingresada ya existe para el proyecto con id={data.ProyectoId}.");

                var asignadoA = await this.usuarioDAO.getOneById(data.AsignadoA);
                if (asignadoA is null) throw new NotFoundException($"El usuario a asignar con el id={data.AsignadoA}, no existe.");

                var proyecto = await proyectoDAO.getOneById(data.ProyectoId);
                if (proyecto is null) throw new NotFoundException($"El proyecto ingresado con el id={data.ProyectoId}, no existe.");

                tareaExist.Nombre = data.Nombre;
                tareaExist.Descripcion = data.Descripcion;
                tareaExist.ProyectoId = data.ProyectoId;
                tareaExist.Estado = data.Estado;
                tareaExist.AsignadoA = data.AsignadoA;
                tareaExist.UpdatedAt = DateTime.Now;

                var result = await this.tareaDAO.update(tareaExist);
                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TareaDTO> deleteAsync(string id)
        {
            try
            {
                var tareaExist = await this.tareaDAO.getOneById(id);
                if (tareaExist is null) throw new NotFoundException($"La tarea con el id={id}, no existe.");

                var result = await this.tareaDAO.remove(id);
                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
