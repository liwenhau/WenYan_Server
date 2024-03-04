namespace WenYan.Service.Util
{
    /// <summary>
    /// 常量默认配置
    /// </summary>
    public static class ConstDefaultConfig
    {
        /// <summary>
        /// 状态启用
        /// </summary>
        public const string Enable = "Enable";

        /// <summary>
        /// 状态禁用
        /// </summary>
        public const string Disable = "Disable";
        /// <summary>
        /// 目录
        /// </summary>
        public const string Directory = "1";
        /// <summary>
        /// 按钮
        /// </summary>
        public const string Button = "3";
        /// <summary>
        /// 应用名
        /// </summary>
        public const string AppName = "WenYan";
        /// <summary>
        /// 默认密码
        /// </summary>
        public static string DefaultPwd = AppName + DateTime.Now.ToString("yyyy");
        /// <summary>
        /// 约定分片文件扩展
        /// </summary>
        public const string FilePartName = ".WenYanPart-";
    }
}
