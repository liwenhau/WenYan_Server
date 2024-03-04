namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 系统文件服务
    /// </summary>
    public interface ISys_FileBusiness : IBusRepository<Sys_File>
    {
        /// <summary>
        /// 获取目录下所有文件信息
        /// </summary>
        /// <returns></returns>
        Task<List<Sys_File>> GetDirAllFileAsync(FileQueryDto query);

        /// <summary>
        /// 保存文件信息
        /// </summary>
        /// <param name="data">合并文件信息</param>
        /// <returns></returns>
        Task SaveFileAsync(MergeFileInputDto data);

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="data">文件重命名信息</param>
        /// <returns></returns>
        Task<bool> FileReNameAsync(FileReNameDto data);
    }
}
