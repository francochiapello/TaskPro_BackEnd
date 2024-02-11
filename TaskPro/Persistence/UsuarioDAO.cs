using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskPro.Data;
using TaskPro.Models.Shared;

namespace TaskPro.Persistence
{
    public class UsuarioDAO
    {
        private readonly DbgrpContext db = new DbgrpContext();

        public async Task<Usuario?> getOneById(int id)
        {
            try
            {
                var result = await db.Usuarios.Where(x =>  x.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public async Task<List<Usuario>> getAllUsuarios()
        {
            try
            {
                var result = await this.db.Usuarios.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
        public List<Usuario> getListByName(string? name)
        {
            try
            {
                var query = db.Usuarios.AsQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(x => x.Nombre.ToUpper().Contains(name.ToUpper()));
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public async Task<Usuario?> findIfExistByEmail(string email)
        {
            try
            {
                var result = await db.Usuarios.Where(x => x.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public async Task<Usuario> create(Usuario data)
        {
            try
            {
                using (DbgrpContext db1 = new DbgrpContext())
                {
                    await db1.Usuarios.AddAsync(data);
                    await db1.SaveChangesAsync();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public async Task<Usuario> update(Usuario data)
        {
            try
            {
                using (DbgrpContext db1 = new DbgrpContext())
                {
                    var result = await db1.Usuarios.Where(x => x.Id == data.Id).FirstOrDefaultAsync();
                    if (result is not null)
                    {
                        db1.Update<Usuario>(data);

                        await db1.SaveChangesAsync();
                    }

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
                    var result = await db1.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
                    if (result is not null)
                    {
                        db1.Usuarios.Attach(result);
                        db1.Usuarios.Remove(result);

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
