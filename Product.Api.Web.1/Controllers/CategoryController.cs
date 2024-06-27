using Bussines.Abstract;
using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.Dtos.Products;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product.Api.Web._1.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserProductService _userProductService;
        private readonly ApplicationDbContext _context;

        public CategoryController(ICategoryService categoryService, IUserProductService userProductService, ApplicationDbContext context)
        {
            _categoryService = categoryService;
            _userProductService = userProductService;
            _context = context;
        }

        [HttpGet]
        [Route("GetCategory")]
        [Authorize(Roles = $"{StaticUserRoles.OWNER},{StaticUserRoles.ADMIN},{StaticUserRoles.USER}")]
        public IActionResult GetCategory()
        {
            var result = _categoryService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("PostCategory")]
        [Authorize(Roles = $"{StaticUserRoles.OWNER},{StaticUserRoles.ADMIN}")]
        public IActionResult PostCategory([FromForm] CategoryCreateDto dto)
        {
            var result = _categoryService.Add(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
       
        [HttpPut]
        [Route("UpdateCategory")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult PutCategory([FromForm] CategoryUpdateDto dto)
        {
            var result = _categoryService.Update(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("DeleteCategory")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult DeleteCategory(CategoryDto dto)
        {
            var result = _categoryService.Delete(dto.ID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _categoryService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
