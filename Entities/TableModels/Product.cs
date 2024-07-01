using Core.Entities.Concrete;
using Entities.TableModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete.TableModels
{
    public class Product:BaseEntity
    {
        public Product()
        {
            UserProducts=new HashSet<UserProduct>();
            CurrentPrice = Price;
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime? DiscountEndTime { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        public int CategoryID { get; set; }

        public string? Message { get; set; } = string.Empty;

        public int Rating { get; set; } = 0;

        public decimal? Views { get; set; } = 0;
        public ICollection<UserProduct> UserProducts { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
