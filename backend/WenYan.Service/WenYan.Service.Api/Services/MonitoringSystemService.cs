
using Microsoft.AspNetCore.SignalR;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 监控系统服务
    /// </summary>
    public class MonitoringSystemService : BackgroundService
    {
        public ILogger<MonitoringSystemService>? _logger { get; set; }
        private readonly IServiceScopeFactory _scopeFactory;
        public MonitoringSystemService(IServiceScopeFactory scopeFactory, ILogger<MonitoringSystemService>? logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delay =  10 * 1000;
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var charthub = scope.ServiceProvider.GetRequiredService<IHubContext<ChartHub, IChartHub>>();
                await charthub.Clients.All.GetDiskInfos(ComputerInfo.GetDiskInfos());
                await charthub.Clients.All.GetMemoryMetrics(ComputerInfo.GetComputerInfo());
                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
