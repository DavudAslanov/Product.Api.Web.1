using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAcces.Abstract
{
    public interface IBaseInterfeys<T> where T : BaseEntity , new()
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T GetById(int id);

        List<T> GetAll(Expression<Func<T,bool>>? filter=null);
    }
}
