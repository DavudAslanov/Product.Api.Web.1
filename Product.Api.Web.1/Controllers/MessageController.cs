using Bussines.Abstract;
using Bussines.Concrete;
using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product.Api.Web._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ApplicationDbContext _context;

        public MessageController(IMessageService messageService, ApplicationDbContext context)
        {
            _messageService = messageService;
            _context = context;
        }

        [HttpGet]
        [Route("GetMessage")]
        [Authorize(Roles = $"{StaticUserRoles.OWNER},{StaticUserRoles.ADMIN}")]
        public IActionResult GetMessage()
        {
            var result = _messageService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("PostMessage")]
        [Authorize]
        public IActionResult PostMessage([FromForm] MessageCreateDto dto)
        {
            var result = _messageService.Add(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("UpdateMessage")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult UpdateMessage([FromForm] MessageUpdateDto dto)
        {
            var result = _messageService.Update(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPut]
        [Route("DeleteMessage")]
        [Authorize(Roles = $"{StaticUserRoles.ADMIN},{StaticUserRoles.OWNER}")]
        public IActionResult DeleteMessage(MessageDto dto)
        {
            var result = _messageService.Delete(dto.ID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _messageService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
