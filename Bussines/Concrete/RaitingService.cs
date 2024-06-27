using Bussines.Abstract;
using DataAcces.SqlServerDbContext;
using Microsoft.EntityFrameworkCore;

namespace Bussines.Concrete
{
    public class RaitingService : IRaitingService
    {
        private readonly ApplicationDbContext _context;

        public RaitingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRatingAsync(int productId, int rating)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return false;

            product.Rating = rating; 

            await _context.SaveChangesAsync(); 
            return true;
        }

        public async Task<double> GetAverageRatingAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return 0;

            // Burada ürünün ortalama rating'ini hesaplama işlemi yapılır
            var averageRating = await _context.UserProducts
                .Where(up => up.ProductId == productId)
                .AverageAsync(up => up.Rating);

            return averageRating;
        }
    }
}
