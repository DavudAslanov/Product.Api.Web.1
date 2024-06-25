using Bussines.Abstract;
using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.Dtos.Products;
using Entities.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ProductWeb.Apis.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserProductService _userProductService;
        private readonly ApplicationDbContext _context;

        public ProductController(IProductService productService, IUserProductService userProductService, ApplicationDbContext context)
        {
            _productService = productService;
            _userProductService = userProductService;
            _context = context;
        }
        [HttpGet]
        [Route("GetUserRole")]
        [Authorize(Roles = StaticUserRoles.USER)]
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
        [Route("GetAdminRole")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        [Authorize(Roles = StaticUserRoles.OWNER)]
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
        [Route("GetOwnerRole")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        [Authorize(Roles = StaticUserRoles.OWNER)]
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
        [Route("GetAdminRole")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        [Authorize(Roles = StaticUserRoles.OWNER)]
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

        [HttpGet("GetSelectedProducts")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public IActionResult GetSelectedProducts()
        {
          

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userIdGuid))
            {
                return BadRequest("User id claim not found or invalid.");
            }

            int userId = userIdGuid.GetHashCode();

            var result = _userProductService.GetSelectedProducts(userId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("SelectProduct")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public IActionResult SelectProduct([FromBody] int productId)
        {
           

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userIdGuid))
            {
                return BadRequest("User id claim not found or invalid.");
            }

            int userId = userIdGuid.GetHashCode();

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
           
            if (!_context.Users.Any(u => u.UserId == userId))
            {
                var newUser = new User
                {
                    UserId = userId
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
            }

            var result = _userProductService.SelectProduct(user.UserId, productId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);


        }

        [HttpPost("DeselectProduct")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public IActionResult DeselectProduct([FromBody] int productId)
        {

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userIdGuid))
            {
                return BadRequest("User id claim not found or invalid.");
            }

            int userId = userIdGuid.GetHashCode();

            var result = _userProductService.DeselectProduct(userId, productId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
