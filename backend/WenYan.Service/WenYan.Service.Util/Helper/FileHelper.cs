namespace WenYan.Service.Util
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {

        /// <summary>
        /// 获取后缀名，扩展名不带“.”
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件后缀名</returns>
        public static string GetFileSuffix(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            int lastDotIndex = filePath.LastIndexOf('.');
            if (lastDotIndex < 0 || lastDotIndex >= filePath.Length - 1)
            {
                return string.Empty;
            }

            return filePath.Substring(lastDotIndex + 1);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void Rm(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                throw new Exception($"删除文件 {path} 失败：{ex.Message}");
            }
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void Mkdir(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw new Exception($"创建目录 {path} 失败：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void Rmdir(string path)
        {
            try
            {
                Directory.Delete(path);
                Console.WriteLine($"目录 {path} 已成功删除");
            }
            catch (Exception ex)
            {
                throw new Exception($"删除目录 {path} 失败：{ex.Message}", ex);
            }
        }

        public static string GetType(string extension)
        {
            extension = extension.ToLower();
            //默认为其它类型
            string type = "";
            switch (extension)
            {
                case "jpg":
                case "jpeg":
                case "png":
                case "gif":
                case "webp":
                    type = "Image";
                    break;
                case "doc":
                case "docx":
                case "xls":
                case "xlsx":
                case "ppt":
                case "pptx":
                case "pdf":
                case "txt":
                    type = "Document";
                    break;
                case "mp3":
                    type = "Audio";
                    break;
                case "mp4":
                    type = "Video";
                    break;
                default:
                    type = "Other";
                    break;
            }
            return type;
        }
        /// <summary>
        /// 获取文件名，不带扩展名
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>文件名</returns>
        public static string GetFilePrefix(FileInfo file)
        {
            if (file == null)
            {
                return null;
            }
            string fileName = file.Name;
            int lastIndex = fileName.LastIndexOf('.');
            if (lastIndex <= 0)
            {
                return fileName;
            }
            return fileName.Substring(0, lastIndex);
        }


        /// <summary>
        /// 获取文件名，不带扩展名
        /// </summary>
        /// <param name="fileName">文件名称带后缀名</param>
        /// <returns>文件名</returns>
        public static string GetFilePrefix(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf('.'));
        }

        /// <summary>
        /// 删除指定路径下的所有文件
        /// </summary>
        /// <param name="filepath">指定路径</param>
        /// <returns></returns>
        public static bool DeleteSpecifiedPathAllFile(string filepath)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(filepath);
                // 去除文件夹的只读属性
                info.Attributes = FileAttributes.Normal & FileAttributes.Directory;
                // 去除文件的只读属性
                File.SetAttributes(filepath, FileAttributes.Normal);
                // 判断文件夹是否存在
                if (Directory.Exists(filepath))
                {
                    foreach (var file in Directory.GetFileSystemEntries(filepath))
                    {
                        if (File.Exists(file))
                        {
                            // 如果有子文件则删除子文件的所有文件
                            File.Delete(file);
                        }
                        else
                        {
                            // 循环递归删除子文件夹
                            DeleteSpecifiedPathAllFile(file);
                        }
                    }
                    // 删除已空文件夹(此步骤会删除指定目录的最底层文件夹，建议保留文件夹目录，此句注释)
                     Directory.Delete(filepath, true);
                }
                else
                {
                    throw new Exception($"{filepath},路径不存在");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"删除文件路径发生异常，文件路径:{filepath}，异常信息：{ex.Message}");            
            }
        }
    }
}
