using Entities.Concrete.TableModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public static List<ProductDto> ToProduct(Product product)
        {
            ProductDto dto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                PhotoUrl=product.Photo

            };
            List<ProductDto> productDtos = new List<ProductDto>();
            productDtos.Add(dto);
            return productDtos;
        }
        public static Product ToProduct(ProductDto dto)
        {
            Product product = new Product()
            {
                Id= dto.Id,
                Name = dto.Name,
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                Photo=dto.PhotoUrl,
                
            };
            return product;
        }
    }
}
