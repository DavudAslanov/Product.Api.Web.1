using Core.DataAcces.Concrete;
using DataAcces.Abstract;
using DataAcces.SqlServerDbContext;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Concrete
{
    public class CategoryDal : BaseRepository<Category, ApplicationDbContext>, ICategoryDal
    {
    }
}
