using Core.Entities.Concrete;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TableModels
{
    public class UserProduct:BaseEntity
    {
        public  User User { get; set; }

        public int ProductId { get; set; }
        public  Product Product { get; set; }

        public bool IsSelected { get; set; }

        public int Rating { get; set; } = 0;
    }
}
