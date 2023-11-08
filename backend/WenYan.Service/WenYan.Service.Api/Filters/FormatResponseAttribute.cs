using Microsoft.AspNetCore.Mvc.Filters;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 统一格式化返回结果
    /// </summary>
    public class FormatResponseAttribute : BaseActionFilterAsync
    {
        public override async Task OnActionExecuted(ActionExecutedContext context)
        {
            //不需要统一格式
            //if (context.ContainsFilter<NoFormatResponseAttribute>())
            //    return;

            if (context.Result is EmptyResult)
                context.Result = Success();
            else if (context.Result is ObjectResult res)
            {
                if (res.Value is AjaxResult)
                    context.Result = JsonContent(res.Value.ToJson());
                else
                    context.Result = Success(res.Value);
            }

            await Task.CompletedTask;
        }
    }
}
