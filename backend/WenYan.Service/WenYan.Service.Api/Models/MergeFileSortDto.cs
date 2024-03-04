namespace WenYan.Service.Api
{
    public class MergeFileSortDto
    {
        /// <summary>
        /// 分片序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath {  get; set; }
    }
}
