using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGB.Application.DTOs.Users;
using PGB.Application.Models.Users.Commands;
using PGB.Application.Models.Users.Queries;
using PGB.Domain.Entities;

namespace PGB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannedUsersController : ControllerBase
    {
        private readonly ISender _mediator;
        public BannedUsersController(ISender mediator) =>_mediator = mediator;
        

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddBannedUser(int UserId)
        {
            var command = new AddBannedUser()
            {
                UserId = UserId
            };
            var bannUser = await _mediator.Send(command);
            return Ok(bannUser);
        }

        [HttpGet("AllUsers")]
        public async Task<ActionResult<IEnumerable<BannedUser>>> GetAllBannedUsers()
        {
            var query = new GetAllBannedUsers();
            var bannedUsers = await _mediator.Send(query);
            return Ok(bannedUsers);
        }

        [HttpGet("BannedUser{id}")]
        public async Task<ActionResult<BannedUserDto>> GetBannedUser(int id)
        {
            var cmd = new GetBannedUser()
            {
                id = id
            };
            var bannedUser= await _mediator.Send(cmd);
            if (bannedUser is not null) return Ok(bannedUser);
               else return NotFound();
        }

        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> RemoveUserFromBannedList(int id)
        {
            var cmd = new RemoveBannedUser()
            {
                userId= id
            };
            var unbannUser = await _mediator.Send(cmd);
            return Ok(unbannUser);
        }
    }
}
