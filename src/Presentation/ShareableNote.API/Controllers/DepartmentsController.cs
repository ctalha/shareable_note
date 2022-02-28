using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedNote.Application.CQRS.Queries;
using System.Threading.Tasks;

namespace ShareableNote.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("college/{id:int}")]
        public async Task<IActionResult> GetAllDepartmentsByCollegeId(int id)
        {
            var result = await _mediator.Send(new GetAllDepartmentByCollegeIdQueries { CollegeId = id });
            return StatusCode(result.StatusCode, result);
        }
    }
}
