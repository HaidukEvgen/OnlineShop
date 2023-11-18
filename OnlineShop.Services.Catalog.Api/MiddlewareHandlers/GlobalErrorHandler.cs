using FluentValidation;
using OnlineShop.Services.Catalog.Application.Exceptions;
using OnlineShop.Services.Catalog.Application.Models.Dto;
using System.Net;
using System.Text.Json;
namespace OnlineShop.Services.Catalog.Api.MiddlewareHandlers
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    ProductNotFoundException => (int)HttpStatusCode.NotFound,
                    FormatException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new ResponseDto<object>() { IsSuccess = false, Message = error.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
