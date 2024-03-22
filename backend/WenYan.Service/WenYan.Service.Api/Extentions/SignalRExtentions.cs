namespace WenYan.Service.Api
{
    /// <summary>
    /// 即时消息注册Signalr
    /// </summary>
    public static class SignalRExtentions
    {
        public   static void AddSignalRSercice(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(2);
                options.KeepAliveInterval = TimeSpan.FromMinutes(1);
                options.MaximumReceiveMessageSize = 1024 * 1024 * 10; // 数据包大小10M，默认最大为32K
            }).AddJsonProtocol();
        }
    }
}
