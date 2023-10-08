namespace WenYan.Service.Util
{
    public class SnowflakeConfig
    {
        /// <summary>
        /// 工作机器ID
        /// </summary>
        public long WorkerId { get; set; }
        /// <summary>
        /// 数据中心ID
        /// </summary>
        public long DataCenterId { get; set; }
    }
}
