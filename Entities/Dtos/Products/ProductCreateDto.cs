using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.Products
{
    public class ProductCreateDto
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile Photo { get; set; }

        public static Product ToProduct(ProductCreateDto product)
        {
            Product products = new Product()
           {
               Name = product.Name,
               Title = product.Title,
               Price = product.Price,
               Description = product.Description,
           };
            return products;
        }
    }
}
