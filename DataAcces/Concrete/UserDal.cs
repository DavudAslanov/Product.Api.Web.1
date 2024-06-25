using Core.DataAcces.Concrete;
using DataAcces.Abstract;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.TableModels;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Concrete
{
    public class UserDal : BaseRepository<User, ApplicationDbContext>, IUserDal
    {
    }
}
