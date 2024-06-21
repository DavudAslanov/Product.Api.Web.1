using Core.DataAcces.Abstract;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAcces.Concrete
{
    public class BaseRepository<Tentity, Tcontext> : IBaseInterfeys<Tentity>
    where Tentity : BaseEntity, new()
    where Tcontext : DbContext, new()
    {
        public void Add(Tentity entity)
        {
           using(Tcontext tcontext = new Tcontext())
            {
                var Added= tcontext.Entry(entity);
                Added.State = EntityState.Added;
                tcontext.SaveChanges();
            }
        }

        public void Delete(Tentity entity)
        {
            using(Tcontext context=new Tcontext())
            {
                var Deleted=context.Entry(entity);
                Deleted.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>>? filter = null)
        {
            using(Tcontext context=new Tcontext())
            {
                if(filter == null)
                {
                    return context.Set<Tentity>().ToList();
                }
                else
                {
                    return context.Set<Tentity>().Where(filter).ToList();
                }
            }
        }

        public Tentity GetById(int id)
        {
            using(Tcontext context= new Tcontext())
            {
                return context.Set<Tentity>().FirstOrDefault(x => x.Id == id);
            }
        }

        public void Update(Tentity entity)
        {
            using(Tcontext tcontext=new Tcontext())
            {
                var Update= tcontext.Entry(entity);
                Update.State = EntityState.Modified;
                tcontext.SaveChanges();
            }
        }
    }
}
