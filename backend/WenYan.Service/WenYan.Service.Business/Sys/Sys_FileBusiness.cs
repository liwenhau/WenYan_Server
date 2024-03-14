using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

using System.Linq.Dynamic.Core;

namespace WenYan.Service.Business
{
    public class Sys_FileBusiness : BusRepository<Sys_File>, ISys_FileBusiness, IScopedDependency
    {
        private IConfiguration _config { get; set; }
        private FileConfig? _fileConfig { get; set; }
        private IServiceProvider _svcProvider { get; set; }
        private readonly ILogger<Sys_FileBusiness> _logger;
        public Sys_FileBusiness(GDbContext context, IConfiguration config, IServiceProvider svcProvider, ILogger<Sys_FileBusiness> logger) : base(context)
        {
            this._config = config;
            this._fileConfig = config.GetSection("FileConfig").Get<FileConfig>();
            this._svcProvider = svcProvider;
            this._logger = logger;
        }

        public async Task<List<Sys_File>> GetDirAllFileAsync(FileQueryDto query)
        {
            var queryable = this.GetQueryable<Sys_File>();
            var rootDirInfo = new Sys_File();
            if (query.IsRootDir)
            {
                rootDirInfo = await queryable.FirstOrDefaultAsync(x => x.IsDir == true && x.Name == _fileConfig.RootDir);
                if (rootDirInfo != null)
                    queryable = queryable.Where(x => x.DirId == rootDirInfo.Id);
            }
            if (!query.KeyWord.IsNullOrEmpty())
                queryable = queryable.Where(x => x.Name.Contains(query.KeyWord));
            if (!query.Type.IsNullOrEmpty())
                queryable = queryable.Where(x => x.Type == query.Type);
            if (!query.DirId.IsNullOrEmpty() && !query.IsRootDir)
                queryable = queryable.Where(x => x.DirId == query.DirId);
            return await queryable.ToListAsync();
        }

        public async Task SaveFileAsync(MergeFileInputDto data)
        {
            var parent = await this.GetQueryable(true)
                .FirstOrDefaultAsync(x => x.FilePath == data.FileDirPath && x.IsDir);
            string parentId = "1";
            if (parent != null)
                parentId = parent.Id;
            var idSvc = this._svcProvider.GetRequiredService<IIdService>();
            var opSvc = this._svcProvider.GetRequiredService<IOperator>();
            var now = DateTime.Now;

            var file = new Sys_File()
            {
                Id = idSvc.GetId(),
                CreateTime = now,
                CreateUserId = opSvc.UserId,
                ModifyTime = now,
                ModifyUserId = opSvc.UserId,
                DirId = parentId,
                IsDir = false,
                FileMd5 = data.Md5,
                FilePath = data.FilePath,
                Name = FileHelper.GetFilePrefix(data.FileName),
                SizeKb = (data.FileSize / 1024).ToString(),
                ExtendName = data.ExtendName,
                Type = FileHelper.GetType(data.ExtendName),
            };
            await AddAsync(file);
        }

        public async Task<bool> FileReNameAsync(FileReNameDto data)
        {
            var fileInfo = await this.GetQueryable()
                .SingleOrDefaultAsync(x => x.Id == data.Id);

            if (fileInfo != null)
            {
                var opSvc = this._svcProvider.GetRequiredService<IOperator>();
                fileInfo.Name = data.Name;
                fileInfo.ModifyUserId = opSvc.UserId;
                fileInfo.ModifyTime = DateTime.Now;
                await this.UpdateAsync(fileInfo);
                return true;
            }
            else
            {
                throw new Exception($"没有找到文件信息");
            }
        }

        public override async Task<int> DeleteAsync(List<string> ids)
        {
            int count = 0;
            var list = await this.GetListAsync(ids);
            var _fileProvider = _svcProvider.GetRequiredService<IFileProvider>();
            foreach (var item in list)
            {
                //即使我们指定的路径对应着一个具体的目录，这个FileInfo对象的IsDirectory也总是返回False（它的Exists属性也返回False）
                var file = _fileProvider.GetFileInfo(item.FilePath);
                if (item.Type == "Dir")
                {
                    FileHelper.DeleteSpecifiedPathAllFile(file.PhysicalPath);
                }
                else
                {
                    if (file == null || !file.Exists)
                    {
                        _logger.LogError($"删除物理文件发生异常,文件名：{item.Name}，文件不存在");
                        continue;
                    }
                    try
                    {
                        var fileInfo = new FileInfo(file.PhysicalPath);
                        fileInfo.Attributes = FileAttributes.Normal;
                        fileInfo.Delete();
                    }
                    catch (IOException ex)
                    {
                        _logger.LogError($"删除物理文件发生异常,文件名：{item.Name}，异常信息：{ex.Message}");
                        throw new Exception($"文件：{item.Name}，正在被使用，请稍后删除");
                    }
                    

                }
                count += await base.DeleteAsync(item);
            }
            return count;
        }
    }
}
