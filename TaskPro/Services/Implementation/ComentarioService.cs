using TaskPro.Data;
using TaskPro.Models.Comentarios;
using TaskPro.Models.Shared;
using TaskPro.Persistence;

namespace TaskPro.Services.Implementation
{
    public class ComentarioService : IComentarioService
    {
        private ComentarioDAO comentarioDAO;
        private TareaDAO tareaDAO;
        private int usuarioId;
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        public void setDao(ComentarioDAO comentarioDAO, TareaDAO tareaDAO)
        {
            this.comentarioDAO = comentarioDAO;
            this.tareaDAO = tareaDAO;
        }

        public void setUsuario(int id)
        {
            this.usuarioId = id;
        }
        public async Task<ComentarioDTO> createAsync(CreateComentarioDTO data)
        {
            try
            {
                var tareaExist = await this.tareaDAO.getOneById(data.TareaId);
                if (tareaExist is null) throw new NotFoundException($"La tarea con el id={data.TareaId}, no existe.");

                var usuario = await this.usuarioDAO.getOneById(data.UsuarioId);
                if (usuario is null) throw new NotFoundException($"El usuario con el id={data.UsuarioId}, no existe.");

                var newComentario = new Comentarios
                {
                    Contenido = data.Contenido,
                    TareaId = data.TareaId,
                    UsuarioId = this.usuarioId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                var result = await this.comentarioDAO.create(newComentario);

                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ComentarioDTO> deleteAsync(string id)
        {
            try
            {
                var comentarioExist = await this.comentarioDAO.getOneById(id);
                if (comentarioExist is null) throw new NotFoundException($"El comentario con el id={id}, no existe.");

                var result = await this.comentarioDAO.remove(id);
                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ComentarioDTO>> GetAll()
        {
            try
            {
                var result = await this.comentarioDAO.getAll(this.usuarioId);

                return result.Select(x => x.toDTO()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ComentarioDTO> getOneByIdAsync(string id)
        {
            try
            {
                var result = await this.comentarioDAO.getOneById(id);
                if (result is null) throw new NotFoundException($"El comentario con el id={id}, no existe.");

                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ComentarioDTO> updateAsync(string id, UpdateComentarioDTO data)
        {
            try
            {
                var comentarioExist = await this.comentarioDAO.getOneById(id );
                if (comentarioExist is null) throw new NotFoundException($"El comentario con el id={id}, no existe.");

                var tareaExist = await this.tareaDAO.getOneById(data.TareaId);
                if (tareaExist is null) throw new NotFoundException($"La tarea con el id={data.TareaId}, no existe.");

                var usuario = await this.usuarioDAO.getOneById(data.UsuarioId);
                if (usuario is null) throw new NotFoundException($"El usuario con el id={data.UsuarioId}, no existe.");

                comentarioExist.Contenido = data.Contenido;
                comentarioExist.UpdatedAt = DateTime.Now;

                var result = await this.comentarioDAO.update(comentarioExist);

                return result.toDTO();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
