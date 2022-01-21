﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedNote.Application.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("college/{id}")]
        public async Task<IActionResult> GetAllDepartmentsByCollegeId(int id)
        {
            var result = await _mediator.Send(new GetAllDepartmentByCollegeIdQueries { CollegeId = id });
            return result.IsSuccess ? Ok(result) : BadRequest(false);
        }
        
    }
}