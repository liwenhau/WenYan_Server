using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WenYan.Service.Util
{
    /// <summary>
    /// Json扩展
    /// </summary>
    public static partial class Extention
    {
        public static JsonSerializerSettings DefaultJsonSetting = new JsonSerializerSettings
        {
            //ContractResolver = new DefaultContractResolver(),
            //首字母小写
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatString = "yyyy-MM-dd HH:mm:ss.fff",
    };

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">要序列化的实体</param>
        /// <returns></returns>
        public static string ToJson(this object? obj)
        {
            //return JsonSerializer.Serialize(obj, DefaultJsonSerializerOptions);
            return JsonConvert.SerializeObject(obj, DefaultJsonSetting);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">返回的实体类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            //return JsonSerializer.Deserialize<T>(json, DefaultJsonSerializerOptions);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
