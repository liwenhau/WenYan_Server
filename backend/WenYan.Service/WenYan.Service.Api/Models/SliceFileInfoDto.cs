namespace WenYan.Service.Api
{
    /// <summary>
    /// 分片文件信息
    /// </summary>
    public class SliceFileInfoDto
    {
        /// <summary>
        /// 文件块
        /// </summary>
        public IFormFile File { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 当前分块
        /// </summary>
        public int Chunk {  get; set; }
        /// <summary>
        /// 总块数
        /// </summary>
        public int TotalChunk { get; set; }
        /// <summary>
        /// 文件Md5值
        /// </summary>
        public string Md5 {  get; set; }
    }
}
