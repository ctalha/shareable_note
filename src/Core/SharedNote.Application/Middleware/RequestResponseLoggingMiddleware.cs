using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using SharedNote.Application.Helpers.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedNote.Application.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        private static LoggingResponse loggingResponse = new LoggingResponse();
        Stopwatch stopwatch = new Stopwatch();
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            stopwatch.Restart();
            stopwatch.Start();   
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            loggingResponse.Path = request;

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

                //TODO: Save log to chosen datastore
                loggingResponse.Response = response;

                stopwatch.Stop();

                loggingResponse.ElapsedMilliseconds =  stopwatch.ElapsedMilliseconds;
                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                Log.Information("R-R");
                Log.Information("Request-Reponse {@LoggingInformation}", loggingResponse);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            #region eski
            //var body = request.Body;

            ////This line allows us to set the reader for the request back at the beginning of its stream.
            //request.EnableBuffering();
            ////We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            //var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            ////...Then we copy the entire request stream into the new buffer.
            //await request.Body.ReadAsync(buffer, 0, buffer.Length);

            ////We convert the byte[] into a string using UTF8 encoding...
            //var bodyAsText = Encoding.UTF8.GetString(buffer);

            //loggingResponse.Request = bodyAsText;

            ////..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            //request.Body = body;
            #endregion

            try
            {
                request.EnableBuffering();

                var buff = new byte[Convert.ToInt32(request.ContentLength)];

                if (buff.Length == 0) return string.Empty;

                await request.Body.ReadAsync(buff, 0, buff.Length);

                var result =  Encoding.UTF8.GetString(buff);

                return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {result}";
            }
            finally
            {
                request.Body.Position = 0;
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }
    }
}
