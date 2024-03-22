namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 图表集线器
    /// </summary>
    public interface IChartHub
    {
        /// <summary>
        /// 系统内存
        /// </summary>
        /// <param name="memoryMetrics"></param>
        /// <returns></returns>
        Task GetMemoryMetrics(MemoryMetrics memoryMetrics);

        /// <summary>
        /// 系统磁盘
        /// </summary>
        /// <param name="diskInfos"></param>
        /// <returns></returns>
        Task GetDiskInfos(List<DiskInfo> diskInfos);
    }
}
