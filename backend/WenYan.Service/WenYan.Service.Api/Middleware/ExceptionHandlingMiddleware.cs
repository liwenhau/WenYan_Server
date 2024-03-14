using System.Net;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 全局异常捕获中间件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;  // 用来处理上下文请求
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); //要么在中间件中处理，要么被传递到下一个中间件中去
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex); // 捕获异常了 在HandleExceptionAsync中处理
            }
        }


        /// <summary>
        /// 异步处理异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";  // 返回json 类型
            var response = context.Response;
            var errorResponse = new AjaxResult
            {
                Success = false,
            };
            // 自定义的异常错误信息类型
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("token is expired"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        errorResponse.Code = response.StatusCode;
                        errorResponse.Message = ex.Message;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Code = response.StatusCode;
                    errorResponse.Message = ex.Message;
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Code = response.StatusCode;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Code = response.StatusCode;
                    errorResponse.Message = "Internal Server errors. Check Logs!";
                    break;
            }
            _logger.LogError(exception.Message);

            await context.Response.WriteAsync(response.ToJson());
        }


    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }

}
