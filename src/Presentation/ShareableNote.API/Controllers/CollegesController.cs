using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SharedNote.Application.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareableNote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CollegesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCollegeQueries());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCollegeByIdQueries { Id = id});
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("department{id}")]
        public async Task<IActionResult> GetWithDepartmentById(int id)
        {
            var result = await _mediator.Send(new GetCollegeWithDepartmentByIdQueries { Id = id });
            return result.IsSuccess ? Ok(result) : BadRequest(result); 
        }
    }
}
