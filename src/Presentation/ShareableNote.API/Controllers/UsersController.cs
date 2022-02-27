using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedNote.Application.CQRS.Commands;
using SharedNote.Application.CQRS.Queries;
using SharedNote.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareableNote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _mediator.Send(new GetAllUserQueries());
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { UserId = id });
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "member,admin")]
        [HttpPatch]
        public async Task<IActionResult> UpdateUser(UpdateUserRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _mediator.Send(new GetUserByIdQueries { UserId = id });
            return StatusCode(result.StatusCode, result);
        }
    }
}
