using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace WenYan.Service.Util
{
    public class ComputerInfo
    {
        /// <summary>
        /// 内存信息
        /// </summary>
        /// <returns></returns>
        public static MemoryMetrics GetComputerInfo()
        {
            MemoryMetricsClient client = new();
            MemoryMetrics memoryMetrics = IsUnix() ? client.GetUnixMetrics() : client.GetWindowsMetrics();

            memoryMetrics.FreeRam = Math.Round(memoryMetrics.Free / 1024, 2) + "GB";
            memoryMetrics.UsedRam = Math.Round(memoryMetrics.Used / 1024, 2) + "GB";
            memoryMetrics.TotalRam = Math.Round(memoryMetrics.Total / 1024, 2) + "GB";
            memoryMetrics.RamRate = Math.Ceiling(100 * memoryMetrics.Used / memoryMetrics.Total).ToString() + "%";
            memoryMetrics.CpuRate = Math.Ceiling(GetCPURate().ParseToDouble()) + "%";
            return memoryMetrics;
        }

        /// <summary>
        /// 磁盘信息
        /// </summary>
        /// <returns></returns>
        public static List<DiskInfo> GetDiskInfos()
        {
            List<DiskInfo> diskInfos = new();

            if (IsUnix())
            {
                var output = ShellHelper.Bash(@"df -mT | awk '/^\/dev\/(sd|vd|xvd|nvme|sda|vda)/ {print $1,$2,$3,$4,$5,$6}'");
                var disks = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (disks.Length == 0) return diskInfos;

                var rootDisk = disks[1].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (rootDisk == null || rootDisk.Length == 0)
                    return diskInfos;

                foreach (var item in disks)
                {
                    var disk = item.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                    if (disk == null || disk.Length == 0)
                        continue;

                    var diskInfo = new DiskInfo()
                    {
                        DiskName = disk[0],
                        TypeName = disk[1],
                        TotalSize = long.Parse(disk[2]) / 1024,
                        Used = long.Parse(disk[3]) / 1024,
                        AvailableFreeSpace = long.Parse(disk[4]) / 1024,
                        AvailablePercent = decimal.Parse(disk[5].Replace("%", ""))
                    };
                    diskInfos.Add(diskInfo);
                }
            }
            else
            {
                var driv = DriveInfo.GetDrives().Where(u => u.IsReady);
                foreach (var item in driv)
                {
                    if (item.DriveType == DriveType.CDRom) continue;
                    var obj = new DiskInfo()
                    {
                        DiskName = item.Name,
                        TypeName = item.DriveType.ToString(),
                        TotalSize = item.TotalSize / 1024 / 1024 / 1024,
                        AvailableFreeSpace = item.AvailableFreeSpace / 1024 / 1024 / 1024,
                    };
                    obj.Used = obj.TotalSize - obj.AvailableFreeSpace;
                    obj.AvailablePercent = decimal.Ceiling(obj.Used / (decimal)obj.TotalSize * 100);
                    diskInfos.Add(obj);
                }
            }
            return diskInfos;
        }

        ///// <summary>
        ///// 获取外网IP地址
        ///// </summary>
        ///// <returns></returns>
        //public static string GetIpFromOnline()
        //{
        //    var url = "http://myip.ipip.net";
        //    var stream = url.GetAsStreamAsync().GetAwaiter().GetResult();
        //    var streamReader = new StreamReader(stream.Stream, stream.Encoding);
        //    var html = streamReader.ReadToEnd();
        //    return !html.Contains("当前 IP：") ? "未知" : html.Replace("当前 IP：", "").Replace("来自于：", "");
        //}

        public static bool IsUnix()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public static string GetCPURate()
        {
            string cpuRate;
            if (IsUnix())
            {
                string output = ShellHelper.Bash("top -b -n1 | grep \"Cpu(s)\" | awk '{print $2 + $4}'");
                cpuRate = output.Trim();
            }
            else
            {
                string output = ShellHelper.Cmd("wmic", "cpu get LoadPercentage");
                cpuRate = output.Replace("LoadPercentage", string.Empty).Trim();
            }
            return cpuRate;
        }

        /// <summary>
        /// 获取系统运行时间
        /// </summary>
        /// <returns></returns>
        public static string GetRunTime()
        {
            string runTime = string.Empty;
            if (IsUnix())
            {
                string output = ShellHelper.Bash("uptime -s").Trim();
                runTime = ((DateTime.Now - output.ParseToDateTime()).TotalMilliseconds.ToString().Split('.')[0].ParseToLong()).FormatTime();
            }
            else
            {
                string output = ShellHelper.Cmd("wmic", "OS get LastBootUpTime/Value");
                string[] outputArr = output.Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
                if (outputArr.Length == 2)
                    runTime = ((DateTime.Now - outputArr[1].Split('.')[0].ParseToDateTime()).TotalMilliseconds.ToString().Split('.')[0].ParseToLong()).FormatTime();
            }
            return runTime;
        }
    }

    /// <summary>
    /// 内存信息
    /// </summary>
    public class MemoryMetrics
    {
        [JsonIgnore]
        public double Total { get; set; }

        [JsonIgnore]
        public double Used { get; set; }

        [JsonIgnore]
        public double Free { get; set; }

        /// <summary>
        /// 已用内存
        /// </summary>
        public string UsedRam { get; set; }

        /// <summary>
        /// CPU使用率%
        /// </summary>
        public string CpuRate { get; set; }

        /// <summary>
        /// 总内存 GB
        /// </summary>
        public string TotalRam { get; set; }

        /// <summary>
        /// 内存使用率 %
        /// </summary>
        public string RamRate { get; set; }

        /// <summary>
        /// 空闲内存
        /// </summary>
        public string FreeRam { get; set; }
    }

    /// <summary>
    /// 磁盘信息
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// 磁盘名
        /// </summary>
        public string DiskName { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 总剩余
        /// </summary>
        public long TotalFree { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// 已使用
        /// </summary>
        public long Used { get; set; }

        /// <summary>
        /// 可使用
        /// </summary>
        public long AvailableFreeSpace { get; set; }

        /// <summary>
        /// 使用百分比
        /// </summary>
        public decimal AvailablePercent { get; set; }
    }

    public class MemoryMetricsClient
    {
        /// <summary>
        /// windows系统获取内存信息
        /// </summary>
        /// <returns></returns>
        public MemoryMetrics GetWindowsMetrics()
        {
            string output = ShellHelper.Cmd("wmic", "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value");
            var metrics = new MemoryMetrics();
            var lines = output.Trim().Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length <= 0) return metrics;

            var freeMemoryParts = lines[0].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);

            metrics.Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
            metrics.Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);//m
            metrics.Used = metrics.Total - metrics.Free;

            return metrics;
        }

        /// <summary>
        /// Unix系统获取
        /// </summary>
        /// <returns></returns>
        public MemoryMetrics GetUnixMetrics()
        {
            string output = ShellHelper.Bash("free -m | awk '{print $2,$3,$4,$5,$6}'");
            var metrics = new MemoryMetrics();
            var lines = output.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length <= 0) return metrics;

            if (lines != null && lines.Length > 0)
            {
                var memory = lines[1].Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                if (memory.Length >= 3)
                {
                    metrics.Total = double.Parse(memory[0]);
                    metrics.Used = double.Parse(memory[1]);
                    metrics.Free = double.Parse(memory[2]);//m
                }
            }
            return metrics;
        }
    }
}
