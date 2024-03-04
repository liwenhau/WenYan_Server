namespace WenYan.Service.IBusiness
{
    public class FileQueryDto : SearchVM
    {
        /// <summary>
        /// 是否根目录
        /// </summary>
        public bool IsRootDir { get; set; } = false;
        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 所属目录
        /// </summary>
        public string DirId { get; set; }
    }
}
