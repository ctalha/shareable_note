using MediatR;
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
    public class FileDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FileDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [RequestSizeLimit(bytes: 20_000_000)]
        public async Task<IActionResult> Add([FromForm]AddFileDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetFileDocumentQueries { Id = id});
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
