using Bussines.Abstract;
using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Product.Api.Web._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalRequests : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRaitingService _ratingService;
        private readonly IUserProductService _userProductService;
        private readonly ApplicationDbContext _context;

        public AdditionalRequests(IProductService productService, IRaitingService ratingService, IUserProductService userProductService, ApplicationDbContext context)
        {
            _productService = productService;
            _ratingService = ratingService;
            _userProductService = userProductService;
            _context = context;
        }

        //Secilmis productlari istifadeci adina gore getirir
        [HttpGet("GetAll selected-products")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult GetAllSelectedProducts()
        {
            var selectedProducts = _context.UserProducts
                .Include(up => up.User)
                .Include(up => up.Product)
                .Where(up => up.IsSelected)
                .GroupBy(up => up.User.UserName)
                .Select(g => new
                {
                    UserName = g.Key,
                    SelectedProducts = g.Select(up => up.Product.Name).ToList()
                })
                .ToList();

            return Ok(selectedProducts);
        }

        //baxis sayinin artirilmasi
        [HttpPost("{productId}/increment-views")]
        public IActionResult IncrementViews(int productId)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                product.Views++;
                _context.SaveChanges();

                return Ok($"Product {productId} views incremented successfully. Current views: {product.Views}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to increment views for product {productId}: {ex.Message}");
            }
        }

        //ulduz 
        [HttpGet("{productId}/averagerating")]
        public async Task<IActionResult> GetAverageRating(int productId)
        {
            var averageRating = await _ratingService.GetAverageRatingAsync(productId);
            if (averageRating > 0)
            {
                return Ok($"Average rating for product {productId}: {averageRating}");
            }
            else if (averageRating == 0)
            {
                return NotFound("No ratings yet for this product.");
            }
            else
            {
                return NotFound("Product not found.");
            }
        }

        [HttpGet("filter-by-name")]
        //[Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public async Task<IActionResult> FilterProductsByName(string productName)
        {

            IQueryable<Entities.Concrete.TableModels.Product> query = _context.Products;

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.Name.Contains(productName));
            }
            var filteredProducts = await query.ToListAsync();

            return Ok(filteredProducts);
        }

        public async Task<T> RetryHttpRequest<T>(Func<Task<T>> requestFunc, int maxRetryCount = 3)
        {
            int retryCount = 0;
            while (true)
            {
                try
                {
                    return await requestFunc();
                }
                catch (HttpRequestException ex) when (retryCount < maxRetryCount)
                {

                    retryCount++;
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
        }
    }
}
