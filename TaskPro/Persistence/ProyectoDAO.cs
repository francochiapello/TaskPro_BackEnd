using Microsoft.EntityFrameworkCore;
using TaskPro.Data;
using TaskPro.Models.Shared;

namespace TaskPro.Persistence
{
    public class ProyectoDAO
    {
        private readonly DbgrpContext db = new DbgrpContext();
        public async Task<Proyecto?> getOneById(int id)
        {
            try
            {
                var result = await this.db.Proyectos.Where(x => x.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<List<Proyecto>> getAll()
        {
            try
            {
                var result = await this.db.Proyectos.OrderByDescending(x => x.UpdatedAt).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<List<Proyecto>> getAllByUserId(int id)
        {
            try
            {
                var result = await this.db.Usuarios.Where(x => x.Id == id).Include(x => x.Proyectos).Select(x => x.Proyectos.ToList()).FirstOrDefaultAsync();

                return result is null ? new List<Proyecto>() : result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Proyecto?> findIfExistByUser(int id,string nombre)
        {
            try
            {
                var result = await this.db.Proyectos.Where(x => x.CreadorId == id && x.Nombre.ToUpper().Equals(nombre.ToUpper())).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Proyecto> create(Proyecto data)
        {
            try
            {
                using (DbgrpContext db1 = new DbgrpContext())
                {
                    await db1.AddAsync(data);
                    await db1.SaveChangesAsync();   

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<Proyecto> update(Proyecto data)
        {
            try
            {
                using (DbgrpContext db1 = new DbgrpContext())
                {

                    db1.Entry<Proyecto>(data).State = EntityState.Modified;

                    await db1.SaveChangesAsync();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async void remove(int id)
        {
            try
            {
                using (DbgrpContext db1 = new DbgrpContext())
                {
                    var result = db1.Proyectos.Where(x => x.Id == id).FirstOrDefault();
                    if (result is not null)
                    {
                        db1.Proyectos.Attach(result);
                        db1.Proyectos.Remove(result);

                        await db1.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
    }
}
