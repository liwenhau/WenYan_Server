namespace WenYan.Service.Util
{
    /// <summary>
    /// 实体默认配置
    /// </summary>
    public static class EntityDefaultConf
    {
        /// <summary>
        /// 默认小字段长度
        /// </summary>
        public const int DefSmallColLen = 50;
        /// <summary>
        /// 默认中字段长度
        /// </summary>
        public const int DefMiddleColLen = 100;
        /// <summary>
        /// 默认大字段长度
        /// </summary>
        public const int DefLargeColLen = 2000;

        #region 计量字段默认配置
        /// <summary>
        /// 默认计量字段类型
        /// </summary>
        [Obsolete("在EFCore6中已经用HasPrecision替代了HasColumnType", UrlFormat = "https://docs.microsoft.com/zh-cn/ef/core/what-is-new/ef-core-6.0/whatsnew#new-mapping-attributes")]
        public const string DefDecimalType = "decimal(18,2)";
        /// <summary>
        /// 默认计量字段精度
        /// </summary>
        public const int DefDecimalPrecision = 18;
        /// <summary>
        /// 默认计量字段小数位数
        /// </summary>
        public const int DefDecimalScale = 2;
        #endregion

        /// <summary>
        /// 默认管理员Id
        /// </summary>
        public const string DefAdminUserId = "1";
    }
}
