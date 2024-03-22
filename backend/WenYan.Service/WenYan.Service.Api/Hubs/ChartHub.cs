using Microsoft.AspNetCore.SignalR;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 在线用户集线器
    /// </summary>
    //[MapHub("/hubs/onlineUser")]
    public class ChartHub : Hub<IChartHub>
    {
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}
