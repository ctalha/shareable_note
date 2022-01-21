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

namespace SharedNote.Application.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private BaseExceptionResponse _exceptionResponse;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }
        private async Task HandleException(HttpContext context, Exception e)
        {
            if (e.GetType() == typeof(ValidationException))
            {
                _exceptionResponse =  new ValidationExceptionResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Validation Exception",
                    Errors = ((ValidationException)e).Errors.ToList().Select(p => p.ErrorMessage)
                };
                await WriteResponseAsync(_exceptionResponse, context);
                return;
            }
            else if (e is TransactionException)
            {
                _exceptionResponse = new TransactionExceptionResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = e.Message,
                    Error = "Transaction Exception"
                };
                await WriteResponseAsync(_exceptionResponse, context);
                return;

            }
            else if(e is NullReferenceException)
            {
                _exceptionResponse = new NullReferenceExceptionResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = e.Message,
                    Error = "Null Reference Exception"
                };
                await WriteResponseAsync(_exceptionResponse, context);
                return;

            }
            else
            {
                _exceptionResponse = new InternalServerExceptionResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = e.Message,
                    Error = "Internal Server Error"
                };
                await WriteResponseAsync(_exceptionResponse, context);
                return;
            }

        }

        private async Task WriteResponseAsync(BaseExceptionResponse baseException, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = baseException.StatusCode;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<object>(baseException,options);

            await context.Response.WriteAsync(json);
        }
    }
}
