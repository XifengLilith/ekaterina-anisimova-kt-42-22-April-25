using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ekaterina_anisimova_kt_42_22_April_25.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception occurred: {ExceptionMessage}", exception.Message);

                var httpResponse = context.Response;
                httpResponse.ContentType = "application/json";

                var responseModel = new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = exception.Message
                };

                switch (exception)
                {
                    default:
                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Errors = new List<string> { exception.InnerException?.Message ?? "No inner exception details." };
                        break;
                }

                await httpResponse.WriteAsJsonAsync(responseModel);
            }
        }
    }

    public class ResponseModel<T>
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        public ResponseModel()
        {
        }

        public ResponseModel(T data, string? message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public ResponseModel(string? message)
        {
            Succeeded = true;
            Message = message;
        }
    }
}