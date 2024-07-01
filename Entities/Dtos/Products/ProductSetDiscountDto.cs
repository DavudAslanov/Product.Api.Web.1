using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ProductSetDiscountDto
    {
        public int Id { get; set; }

        public decimal DiscountPrice { get; set; }
        public int DiscountDurationInDays { get; set; }
    }
}
