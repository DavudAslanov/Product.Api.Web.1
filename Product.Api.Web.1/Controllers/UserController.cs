﻿using Bussines.Abstract;
using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Entities.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Product.Api.Web._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRaitingService _ratingService;
        private readonly IUserProductService _userProductService;
        private readonly ApplicationDbContext _context;

        public UserController(IProductService productService, IRaitingService ratingService, IUserProductService userProductService, ApplicationDbContext context)
        {
            _productService = productService;
            _ratingService = ratingService;
            _userProductService = userProductService;
            _context = context;
        }

        [HttpPost("SelectProduct")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<IActionResult> SelectProductAsync([FromForm] int productId, [FromForm] int rating)
        {

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userNameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userIdGuid))
            {
                return BadRequest("Kullanıcı kimlik id si bulunamadı veya geçersiz.");
            }

            int userId = userIdGuid.GetHashCode();

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                var newUser = new User
                {
                    UserId = userId,
                    UserName = userNameClaim,
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                user = newUser;
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            var userProduct = _context.UserProducts.FirstOrDefault(up => up.Id == user.UserId && up.ProductId == productId);

            if (userProduct == null)
            {
                userProduct = new UserProduct
                {
                    Id = user.UserId,
                    ProductId = productId,
                    IsSelected = true,
                    Rating = rating
                };

                _context.UserProducts.Add(userProduct);
            }
            else
            {
                userProduct.IsSelected = true;
                userProduct.Rating = rating;
            }

            await _context.SaveChangesAsync();

            var ratingResult = await _ratingService.AddRatingAsync(productId, rating);
            if (!ratingResult)
            {
                return BadRequest("Failed to update product rating.");
            }

            return Ok("Product selected successfully.");
        }

        [HttpPost("DeselectProduct")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<IActionResult> DeselectProductAsync([FromBody] int productId)
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
