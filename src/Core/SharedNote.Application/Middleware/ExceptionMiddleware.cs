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

        public ExceptionMiddleware(RequestDelegate next)
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
            context.Response.ContentType = "application/json";
            Type type = e.GetType();

            string json = ConvertJsonData(e,type,context);
            await context.Response.WriteAsync(json);
        }
        private string ConvertJsonData(Exception e, Type type,HttpContext context)
        {
  
            string json="";
            if (type == typeof(BaseException))
            {
                context.Response.StatusCode = (int)type.GetProperty("StatusCode").GetValue(e);
                json = JsonConvert.SerializeObject(new ExceptionReponse
                {
                    IsSuccess = (bool)type.GetProperty("IsSuccess").GetValue(e),
                    StatusCode = (int)type.GetProperty("StatusCode").GetValue(e),
                    Message = (string)type.GetProperty("Messages").GetValue(e),
                    Description = (string)type.GetProperty("Description").GetValue(e),               
                });
            }
            else if (type == typeof(ValidationException))
            {
                context.Response.StatusCode = 400;
                json = JsonConvert.SerializeObject(new ValidationExceptionResponse
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Validation Error",
                    Errors = ((ValidationException)e).Errors.ToList().Select(p => new ValidationResponse
                    {
                        PropertyName = p.PropertyName,
                        ErrorMessage = p.ErrorMessage
                    })
                });
            }
            else
            {
                context.Response.StatusCode = 500;
                json = JsonConvert.SerializeObject(new ExceptionReponse
                {
                    IsSuccess = (bool)type.GetProperty("IsSuccess").GetValue(e),
                    StatusCode = 500,
                    Message = "International Server Error",
                });
            }
            return json;
        }
    }
}
