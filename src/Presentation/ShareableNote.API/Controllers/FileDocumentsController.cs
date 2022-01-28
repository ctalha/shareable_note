using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedNote.Application.CQRS.Commands;
using SharedNote.Application.CQRS.Queries;
using SharedNote.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShareableNote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FileDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]AddFileDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? StatusCode(201, result) : BadRequest(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetFileDocumentQueries { Id = id});
            return result.IsSuccess ? StatusCode(200, result) : BadRequest(result);
        }


    }
}
