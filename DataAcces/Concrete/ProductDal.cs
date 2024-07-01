using Core.DataAcces.Concrete;
using DataAcces.Abstract;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.Dtos.Products;
using Entities.Concrete.TableModels;
using Entities.TableModels;
using System;

namespace DataAcces.Concrete
{
    public class ProductDal:BaseRepository<Product,ApplicationDbContext>,IProductDal
    {
        private readonly ApplicationDbContext _context;

        public ProductDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProductDto> GetCategories()
        {
            var result = from Product in _context.Products
                         where Product.Deleted == 0
                         join Category in _context.Categories
                         on Product.CategoryID equals Category.Id
                         where Category.Deleted == 0
                         select new ProductDto
                         {
                            Id= Product.Id,
                            CategoryID=Category.Id,
                            CategoryName=Category.CategoryName,
                            Name=Product.Name,
                            Title=Product.Title,
                            Price = Product.Price,
                            CurrentPrice = Product.CurrentPrice,
                            DiscountEndTime=Product.DiscountEndTime,
                            Description=Product.Description,
                            Message=Product.Message,
                            Rating=Product.Rating,
                            Views=Product.Views,
                            PhotoUrl=Product.Photo, 
                         };
            return result.ToList();
        }
    }
}
