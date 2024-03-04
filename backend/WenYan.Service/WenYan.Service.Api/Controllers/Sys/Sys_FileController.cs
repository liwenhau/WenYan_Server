using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace WenYan.Service.Api.Controllers
{
    public class Sys_FileController : BaseController
    {
        private ISys_FileBusiness _bus { get; set; }
        private IServiceProvider _svcProvider { get; set; }
        private FileExtensionContentTypeProvider _contentTypeProvider { get; set; }
        private FileConfig _fileConfig { get; set; }
        private readonly ILogger<Sys_FileController> _logger;
        private readonly IConfiguration _configuration;
        public Sys_FileController(ISys_FileBusiness bus, IServiceProvider svcProvider, ILogger<Sys_FileController> logger, IConfiguration configuration)
        {
            this._bus = bus;
            this._svcProvider = svcProvider;
            this._logger = logger;
            this._contentTypeProvider = new FileExtensionContentTypeProvider();
            this._configuration = configuration;
            this._fileConfig = configuration.GetSection(nameof(FileConfig)).Get<FileConfig>();
        }

        /// <summary>
        /// 校验文件
        /// </summary>
        /// <param name="md5">文件MD5</param>
        /// <returns></returns>
        [HttpGet]
        public Task<bool> CheckFileAsync(string md5)
        {
            return this._bus.GetQueryable().AnyAsync(x => x.FileMd5 == md5);
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> FileReNameAsync(FileReNameDto data)
        {
            return await this._bus.FileReNameAsync(data);
        }

        /// <summary>
        /// 文件分片上传
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UploadFileChunkAsync([FromForm] SliceFileInfoDto data)
        {
            //保存分片文件
            if (this._fileConfig == null)
                throw new Exception("没有设置临时文件目录！");
            var hostSvc = this._svcProvider.GetRequiredService<IWebHostEnvironment>();
            var dirPath = Path.Combine(hostSvc.ContentRootPath, this._fileConfig.Temp, data.Md5);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            //string extension = Path.GetExtension(data.Name);
            var filePath = Path.Combine(dirPath, $"{data.Md5}{ConstDefaultConfig.FilePartName}{data.Chunk}");
            using (var fileStream = System.IO.File.Open(filePath, FileMode.OpenOrCreate))
            {
                await data.File.CopyToAsync(fileStream);
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> MergeFileAsync(MergeFileInputDto data)
        {
            var hostSvc = this._svcProvider.GetRequiredService<IWebHostEnvironment>();
            var mergeFileDirPath = Path.Combine(hostSvc.ContentRootPath, this._fileConfig.Temp, data.Md5);
            if (!Directory.Exists(mergeFileDirPath))
            {
                throw new Exception($"{data.FileName}路径不存在！");
            }
            //获取所有分片文件列表
            var filesList = Directory.GetFiles(mergeFileDirPath);
            var partName = ConstDefaultConfig.FilePartName;
            var mergeFiles = new List<MergeFileSortDto>();
            foreach (var fileitem in filesList)
            {
                var mergeFile = new MergeFileSortDto();
                var fileIndex = fileitem.Substring(fileitem.IndexOf(partName) + partName.Length);
                _ = int.TryParse(fileIndex, out var index);
                mergeFile.Index = index;
                mergeFile.FilePath = fileitem;
                mergeFiles.Add(mergeFile);
            }
            var mergeOrders = mergeFiles.OrderBy(s => s.Index).ToList();
            // 合并文件
            if (data.ExtendName.IsNullOrEmpty())
            {
                data.ExtendName = FileHelper.GetFileSuffix(data.FileName);
            }
            data.FilePath = Path.Combine(data.FileDirPath, $"{data.Md5}.{data.ExtendName}");
            var saveFilePath = Path.Combine(hostSvc.ContentRootPath, this._fileConfig.RootDir, data.FilePath);
            using var fileStream = new FileStream(saveFilePath, FileMode.Create);
            foreach (var fileSort in mergeOrders)
            {
                using FileStream fileChunk = new FileStream(fileSort.FilePath, FileMode.Open);
                await fileChunk.CopyToAsync(fileStream);
            }
            //添加文件信息
            await _bus.SaveFileAsync(data);
            //删除分片文件和目录           
            return FileHelper.DeleteSpecifiedPathAllFile(mergeFileDirPath);
        }

        /// <summary>
        /// 获取目录下所有文件信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Sys_File>> GetDirAllFileAsync(FileQueryDto query)
        {
            return await _bus.GetDirAllFileAsync(query);
        }

        /// <summary>
        /// 根据路径获取文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public FileStreamResult GetFileByPathAsync(string path)
        {
            var _fileProvider = _svcProvider.GetRequiredService<IFileProvider>();
            var fileInfo = _fileProvider.GetFileInfo(path);
            if (fileInfo == null || !fileInfo.Exists)
                throw new Exception($"文件不存在，文件路径:{path}");
            this._contentTypeProvider.TryGetContentType(fileInfo.PhysicalPath, out string contentType);
            if (contentType == null) throw new Exception($"文件没有匹配到对应的MIME类型，文件路径：{fileInfo.PhysicalPath}");
            return new FileStreamResult(fileInfo.CreateReadStream(), contentType);
        }

        /// <summary>
        /// 根据文件id获取文件
        /// </summary>
        /// <param name="id">文件id</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<FileStreamResult> GetFileByIdAsync(string id)
        {
            var _fileProvider = _svcProvider.GetRequiredService<IFileProvider>();
            var fileData = await _bus.GetAsync(id);
            if (fileData == null)
                throw new Exception($"文件不存在，文件id:{id}");
            var fileInfo = _fileProvider.GetFileInfo(fileData.FilePath);
            if (fileInfo == null || !fileInfo.Exists)
                throw new Exception($"文件不存在，文件路径:{fileData.FilePath}");
            this._contentTypeProvider.TryGetContentType(fileInfo.PhysicalPath, out string contentType);
            if (contentType == null) throw new Exception($"文件没有匹配到对应的MIME类型，文件路径：{fileInfo.PhysicalPath}");
            return new FileStreamResult(fileInfo.CreateReadStream(), contentType);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="ids">数据主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<int> DeleteAsync(List<string> ids)
        {
            return await this._bus.DeleteAsync(ids);
        }
    }
}
