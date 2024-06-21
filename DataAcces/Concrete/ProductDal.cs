using Core.DataAcces.Concrete;
using DataAcces.Abstract;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.TableModels;

namespace DataAcces.Concrete
{
    public class ProductDal:BaseRepository<Product,ApplicationDbContext>,IProductDal
    {
      
    }
}
