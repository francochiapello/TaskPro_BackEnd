using MongoDB.Driver;
using TaskPro.Data;
using TaskPro.Models.Shared;

namespace TaskPro.Persistence
{
    public class ComentarioDAO
    {
        private IMongoCollection<Comentarios> _Comentarios;
        public ComentarioDAO(IDbMongoSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            this._Comentarios = database.GetCollection<Comentarios>("Comentarios");
        }

        public async Task<List<Comentarios>> getAll(int id)
        {
            try
            {
                var result = await this._Comentarios.Find(x => x.UsuarioId == id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<List<Comentarios>> findByTareaId(string tareaId)
        {
            try
            {
                var result = await this._Comentarios.Find(x => x.TareaId == tareaId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Comentarios?> getOneById(string id)
        {
            try
            {
                var result = await this._Comentarios.Find(x => x.Id == id).SingleOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Comentarios> create(Comentarios data)
        {
            try
            {
                await this._Comentarios.InsertOneAsync(data);
                return data;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Comentarios> update(Comentarios data)
        {
            try
            {
                await this._Comentarios.ReplaceOneAsync(x => x.Id == data.Id, data);

                var result = await this._Comentarios.Find(x => x.Id == data.Id).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Comentarios> remove(string id)
        {
            try
            {
                var result = await this._Comentarios.Find(x => x.Id == id).SingleAsync();

                await this._Comentarios.DeleteOneAsync(x => x.Id == id);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
    }
}
