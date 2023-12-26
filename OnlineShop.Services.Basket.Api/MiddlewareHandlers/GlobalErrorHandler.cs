using FluentValidation;
using Grpc.Core;
using OnlineShop.Services.Basket.BusinessLayer.Exceptions;
using OnlineShop.Services.Basket.BusinessLayer.Models.Dto;
using System.Net;
using System.Text.Json;
namespace OnlineShop.Services.Basket.Api.MiddlewareHandlers
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
                    BasketNotFoundException => (int)HttpStatusCode.NotFound,
                    FormatException => (int)HttpStatusCode.BadRequest,
                    InvalidBasketException => (int)HttpStatusCode.BadRequest,
                    RpcException => GetStatusCodeForGrpcException((RpcException)error),
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new ResponseDto() { IsSuccess = false, Message = error.Message });
                await response.WriteAsync(result);
            }
        }

        private int GetStatusCodeForGrpcException(RpcException rpcException)
        {
            return rpcException.StatusCode switch
            {
                StatusCode.InvalidArgument => (int)HttpStatusCode.BadRequest,
                StatusCode.Unavailable => (int)HttpStatusCode.ServiceUnavailable,
                _ => (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}
