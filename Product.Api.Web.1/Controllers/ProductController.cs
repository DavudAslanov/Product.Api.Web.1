using Bussines.Abstract;
using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.Dtos.Products;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductWeb.Apis.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRaitingService _ratingService;
        private readonly IUserProductService _userProductService;
        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobService _backgroundJobService;

        public ProductController(IProductService productService, IUserProductService userProductService, ApplicationDbContext context, IRaitingService ratingService, IBackgroundJobService backgroundJobService)
        {
            _productService = productService;
            _userProductService = userProductService;
            _context = context;
            _ratingService = ratingService;
            _backgroundJobService = backgroundJobService;
        }

        [HttpGet]
        [Route("GetProduct")]
        public IActionResult GetProduct()
        {
            var result = _productService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("PostProduct")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult PostProduct([FromForm] ProductCreateDto dto)
        {
            var result = _productService.Add(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("PutProduct")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult PutProduct([FromForm] ProductUpdateDto dto)
        {
            var result = _productService.Update(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult DeleteProduct(ProductDto dto)
        {
            var result = _productService.Delete(dto.Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("set-discount")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public async Task<IActionResult> SetDiscount([FromForm] ProductSetDiscountDto dto)
        {
            // Ürünü veritabanından bulma
            var product = await _context.Products.FindAsync(dto.Id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            // İndirimli fiyat belirtilmişse güncelle, belirtilmemişse mevcut fiyatı koru
            if (dto.DiscountPrice > 0)
            {
                product.CurrentPrice = dto.DiscountPrice;
                product.DiscountEndTime = DateTime.UtcNow.AddDays(dto.DiscountDurationInDays);

                // İndirimin bitiş zamanında fiyatın geri alınması için bir arka plan işi planlama
                //_backgroundJobService.ScheduleRevertPriceJob(product.Id, product.DiscountEndTime.Value);
                Task.Delay(dto.DiscountDurationInDays * 24 * 60 * 60 * 1000) // ms cinsinden süre
                .ContinueWith(async (_) =>
                {
                    var existingProduct = await _context.Products.FindAsync(dto.Id);
                        if (existingProduct != null && existingProduct.DiscountEndTime.HasValue && existingProduct.DiscountEndTime.Value <= DateTime.UtcNow)
                    {
                        existingProduct.CurrentPrice = existingProduct.Price;
                        existingProduct.DiscountEndTime = null;
                        await _context.SaveChangesAsync();
                }
                });
            }
            else
            {
                // İndirimli fiyat belirtilmemişse, mevcut fiyatı orijinal fiyat olarak ayarla
                product.CurrentPrice = product.Price;
                product.DiscountEndTime = null;
            }

            await _context.SaveChangesAsync();

            return Ok("Discount set successfully");
        }

        [HttpGet("discounted-products")]
        public async Task<IActionResult> GetDiscountedProducts()
        {
            var discountedProducts = await _context.Products
                .Where(p => p.CurrentPrice < p.Price) 
                .ToListAsync();

            return Ok(discountedProducts);
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
