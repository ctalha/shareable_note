using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedNote.Application.CQRS.Commands;
using SharedNote.Application.CQRS.Queries;
using SharedNote.Application.Dtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShareableNote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public FileDocumentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "member")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddFileDto dto)
        {
            var command = _mapper.Map<AddFileDocumentCommand>(dto);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetFileDocumentQueries { Id = id });
            if (result.IsSuccess)
                return File(result.Data.FileContext, result.Data.ContentType, result.Data.FileName);
            return StatusCode(result.StatusCode,result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteFileCommand { Id = id });
            return StatusCode(result.StatusCode);
        }

    }
}
