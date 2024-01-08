namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 基础传输对象
    /// </summary>
    public class BaseDto<k>
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public k Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }
}
