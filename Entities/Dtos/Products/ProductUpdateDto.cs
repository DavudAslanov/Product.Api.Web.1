using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.Products
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string? Message { get; set; } = string.Empty;

        public int Rating { get; set; } = 0;

        public IFormFile Photo { get; set; }

        public int CategoryID { get; set; }

        public static ProductUpdateDto Toproduct(Product product)
        {
           ProductUpdateDto dto = new()
           {
               Id = product.Id,
               Name = product.Name,
               Title = product.Title,
               Price = product.Price,
               Description = product.Description,
               Message = product.Message,
               Rating = product.Rating,
               CategoryID = product.CategoryID,
           };
            return dto;
        }

        public static Product Toproduct(ProductUpdateDto product)
        {
            Product dto = new()
            {
                Id = product.Id,
                Name = product.Name,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Message = product.Message,
                CategoryID= product.CategoryID,
            };
            return dto;
        }
    }
}
