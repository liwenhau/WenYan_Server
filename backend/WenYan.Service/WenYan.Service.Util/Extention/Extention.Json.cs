using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace WenYan.Service.Util
{
    /// <summary>
    /// Json扩展
    /// </summary>
    public static partial class Extention
    {

        public static JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            //跳过注释
            ReadCommentHandling = JsonCommentHandling.Skip,
            // 允许在反序列化的时候原本应为数字的字符串（带引号的数字）转为数字
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            // 忽略处理循环引用类型
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            // 不允许结尾有逗号的不标准json
            AllowTrailingCommas = false,
            //自定义日期格式转换
            Converters = { new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss")}
        };

        //
        //    public static JsonSerializerSettings DefaultJsonSetting = new JsonSerializerSettings
        //    {
        //        //ContractResolver = new DefaultContractResolver(),
        //        //首字母小写
        //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //        DateFormatString = "yyyy-MM-dd HH:mm:ss.fff",
        //};

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">要序列化的实体</param>
        /// <returns></returns>
        public static string ToJson(this object? obj)
        {
            return JsonSerializer.Serialize(obj, DefaultJsonSerializerOptions);
            //return JsonConvert.SerializeObject(obj, DefaultJsonSetting);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">返回的实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, DefaultJsonSerializerOptions);
            //return JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 自定义配置序列化
        /// </summary>
        /// <param name="obj">要序列化的实体</param>
        /// <param name="options">自定义配置</param>
        /// <returns></returns>
        public static string ToJson(this object? obj, JsonSerializerOptions options)
        {
            if (options != null)
                return JsonSerializer.Serialize(obj, options);
            else return JsonSerializer.Serialize(obj, DefaultJsonSerializerOptions);
            //return JsonConvert.SerializeObject(obj, DefaultJsonSetting);
        }
        /// <summary>
        /// 自定义配置反序列化
        /// </summary>
        /// <typeparam name="T">返回的实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <param name="options">自定义配置</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json, JsonSerializerOptions options)
        {
            if (options != null)
                return JsonSerializer.Deserialize<T>(json, options);
            else return JsonSerializer.Deserialize<T>(json, DefaultJsonSerializerOptions);
            //return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
