using Bussines.Abstract;
using Core.Results.Concrete;
using DataAcces.SqlServerDbContext;
using Entities.TableModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class UserProductService : IUserProductService
    {
        private readonly ApplicationDbContext _context;

        public UserProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ServiceResult DeselectProduct(int userId, int productId)
        {
            var userProduct = _context.UserProducts
                                     .FirstOrDefault(up => up.Id == userId && up.ProductId == productId);

            if (userProduct != null)
            {
                _context.UserProducts.Remove(userProduct);
                _context.SaveChanges();

                return new ServiceResult { IsSuccess = true };
            }

            return new ServiceResult { IsSuccess = false, Message = "Product not selected by user." };
        }

        public ServiceResult GetSelectedProducts(int userId)
        {
            var products = _context.UserProducts
                                .Where(up => up.Id == userId)
                                .Select(up => up.Product)
                                .ToList();

            return new ServiceResult { IsSuccess = true, Data = products };
        }
        public ServiceResult SelectProduct(int userId, int productId)
        {
            if (_context.Products.Any(p => p.Id == productId))
            {
                var userProduct = new UserProduct
                {
                    Id = userId,
                    ProductId = productId
                };

                _context.UserProducts.Add(userProduct);
                //_context.SaveChanges();
                try
                {
                    _context.SaveChanges();
                    return new ServiceResult { IsSuccess = true };
                }
                catch (DbUpdateException ex)
                {
                    // İstisna detaylarını inceleyin
                    var errorMessage = ex.InnerException?.Message;
                    // Hata mesajını loglayabilir veya dönüştürebilirsiniz
                    return new ServiceResult { IsSuccess = false, Message = errorMessage };
                }

                return new ServiceResult { IsSuccess = true };
            }
            return new ServiceResult { IsSuccess = false, Message = "Product not found." };
        }
    }
}
