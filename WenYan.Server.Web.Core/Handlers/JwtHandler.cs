using Furion.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace WenYan.Server.Web.Core
{
    /// <summary>
    /// JWT自定义授权
    /// </summary>
    public class JwtHandler : AppAuthorizeHandler
    {
        public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            return await CheckAuthorzieAsync(httpContext);
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static async Task<bool> CheckAuthorzieAsync(DefaultHttpContext httpContext)
        {
            return true;
        }
    }
}
