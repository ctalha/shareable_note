using Microsoft.AspNetCore.Http;
using SharedNote.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedNote.Application.Valdiations.FluentValidation;

namespace SharedNote.Application.Middleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error->{@Error}", ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if(exception.GetType() == typeof(ValidationException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var json = JsonConvert.SerializeObject(new ValidationExceptionResponse()
                {
                    StatusCode = 400,
                    Errors = ((ValidationException)exception).Errors.ToList().Select(p => new ValidationResponse
                    {
                        PropertyName = p.PropertyName,
                        ErrorMessage = p.ErrorMessage
                    }),
                    IsSuccess = false,
                    Message = "Validation Error"
                });
                await context.Response.WriteAsync(json);
            }

            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                }.ToString());
            }
        }

    }
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
