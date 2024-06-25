using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TableModels
{
    public class User:BaseEntity
    {
        public User()
        {
            UserProducts = new HashSet<UserProduct>();
        }
        public int UserId { get; set; }
        public ICollection<UserProduct> UserProducts { get; set; }
    }
}
