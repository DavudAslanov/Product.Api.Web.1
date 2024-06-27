using Bussines.Abstract;
using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.Dtos.Products;
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

        public ProductController(IProductService productService, IUserProductService userProductService, ApplicationDbContext context, IRaitingService ratingService)
        {
            _productService = productService;
            _userProductService = userProductService;
            _context = context;
            _ratingService = ratingService;
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
