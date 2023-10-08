using System.Text;

namespace WenYan.Service.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        protected ContentResult JsonContent(string jsonStr)
        {
            return base.Content(jsonStr, "application/json", Encoding.UTF8);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        protected AjaxResult<T> Success<T>(T data)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                Msg = "操作成功",
                Data = data
            };

            return res;
        }
    }
}
