namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 合并文件
    /// </summary>
    public class MergeFileInputDto
    {
        /// <summary>
        /// 文件MD5
        /// </summary>
        public string Md5 { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件保存目录
        /// </summary>
        public string FileDirPath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string? FilePath { get; set; }
        /// <summary>
        /// 文件后缀名，不带"."
        /// </summary>
        public string? ExtendName { get; set; }
    }
}
