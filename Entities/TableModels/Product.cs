using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete.TableModels
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
