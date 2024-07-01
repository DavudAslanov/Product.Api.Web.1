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

        public decimal CurrentPrice { get; set; }
        public DateTime? DiscountEndTime { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public string? Message { get; set; } = string.Empty;

        public decimal? Views { get; set; } = 0;

        public int Rating { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public static List<ProductDto> ToProduct(Product product)
        {
            ProductDto dto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Title = product.Title,
                Price = product.Price,
                CurrentPrice = product.CurrentPrice,
                DiscountEndTime = product.DiscountEndTime,
                Description = product.Description,
                PhotoUrl=product.Photo,
                Message = product.Message,
                Rating = product.Rating,
                Views=product.Views,
                CategoryID= product.CategoryID,

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
                CurrentPrice= dto.CurrentPrice,
                DiscountEndTime = dto.DiscountEndTime,
                Description = dto.Description,
                Photo=dto.PhotoUrl,
                Message=dto.Message,
                Rating=dto.Rating,
                Views=dto.Views,
                CategoryID= dto.CategoryID,
            };
            return product;
        }
    }
}
