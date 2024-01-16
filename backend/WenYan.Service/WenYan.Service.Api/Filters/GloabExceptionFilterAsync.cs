using Microsoft.AspNetCore.Mvc.Filters;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GloabExceptionFilterAsync : BaseActionFilterAsync, IAsyncExceptionFilter
    {
        private readonly ILogger<GloabExceptionFilterAsync> _logger;
        public GloabExceptionFilterAsync(ILogger<GloabExceptionFilterAsync> logger)
        {
            _logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            Exception ex = context.Exception;
            //异常没有被处理则进行处理
            if (context.ExceptionHandled == false)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException);
                context.Result = Error(ex.Message);
            }
            //异常已处理
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
