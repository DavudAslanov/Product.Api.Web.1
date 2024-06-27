using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete.Dtos.Products
{
    public class ProductCreateDto
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string? Message { get; set; } = string.Empty;

        public int Rating { get; set; }


        public IFormFile Photo { get; set; }

        public int CategoryID { get; set; }

        public static Product ToProduct(ProductCreateDto product)
        {
            Product products = new Product()
           {
               Name = product.Name,
               Title = product.Title,
               Price = product.Price,
               Description = product.Description,
               Message = product.Message,
               Rating = product.Rating,
               CategoryID= product.CategoryID,  
           };
            return products;
        }
    }
}
