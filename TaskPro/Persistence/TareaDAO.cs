using MongoDB.Driver;
using TaskPro.Data;
using TaskPro.Models.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskPro.Persistence
{
    public class TareaDAO
    {
        private IMongoCollection<Tareas> _Tareas;
        public TareaDAO(IDbMongoSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            this._Tareas = database.GetCollection<Tareas>("Tareas");
        }

        public async Task<List<Tareas>> getAll()
        {
            try
            {
                return await this._Tareas.Find(d => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<List<Tareas>> findByProyectId(int id)
        {
            try
            {
                var result = await this._Tareas.Find(x => x.ProyectoId == id).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Tareas?> getOneById(string id)
        {
            try
            {
                var result = await this._Tareas.Find(x => x.Id == id).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Tareas?> findIfExist(string nombre, int proyectId)
        {
            try
            {
                var result = await this._Tareas.Find(x => x.Nombre.ToUpper().Equals(nombre.ToUpper()) && x.ProyectoId == proyectId).SingleOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Tareas> create(Tareas data)
        {
            try
            {
                await this._Tareas.InsertOneAsync(data);
                return data;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Tareas> update(Tareas data)
        {
            try
            {
                await this._Tareas.ReplaceOneAsync(x => x.Id == data.Id, data);

                var result = await this._Tareas.Find(x => x.Id == data.Id).SingleAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Tareas> remove(string id)
        {
            try
            {
                var result = await this._Tareas.Find(x => x.Id == id).SingleAsync();

                await this._Tareas.DeleteOneAsync(x => x.Id == id);

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
    }
}
