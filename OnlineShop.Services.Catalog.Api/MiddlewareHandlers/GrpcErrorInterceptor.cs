using Grpc.Core;
using Grpc.Core.Interceptors;
namespace OnlineShop.Services.Catalog.Api.MiddlewareHandlers
{
    public class GrpcErrorInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception exception)
            {
                var statusCode = exception switch
                {
                    FormatException => StatusCode.InvalidArgument,
                    _ => StatusCode.Internal,
                };

                throw new RpcException(new Status(statusCode, exception.Message));
            }
        }
    }
}
