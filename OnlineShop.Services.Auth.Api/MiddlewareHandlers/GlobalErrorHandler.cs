using OnlineShop.Services.Auth.BusinessLayer.Exceptions;
using OnlineShop.Services.Auth.BusinessLayer.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace OnlineShop.Services.Auth.Api.MiddlewareHandlers
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
                    RegisterException => (int)HttpStatusCode.BadRequest,
                    LoginException => (int)HttpStatusCode.Unauthorized,
                    AssignRoleException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new ResponseDto<object>() { IsSuccess = false, Message = error.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
