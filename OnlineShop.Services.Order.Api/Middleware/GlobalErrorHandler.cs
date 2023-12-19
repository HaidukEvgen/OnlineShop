using FluentValidation;
using OnlineShop.Services.Order.BusinessLayer.Exceptions;
using OnlineShop.Services.Order.BusinessLayer.Models.Dto;
using System.Net;
using System.Text.Json;

namespace OnlineShop.Services.Order.Api.Middleware
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = ex switch
                {
                    OrderNotFoundException => (int)HttpStatusCode.NotFound,
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var responseObject = new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                var result = JsonSerializer.Serialize(responseObject);

                await response.WriteAsync(result);
            }
        }
    }
}
