using Microsoft.Extensions.Caching.Distributed;

namespace WenYan.Service.Util
{
    public static partial class Extention
    {
        public static Task SetAsync<T>(this IDistributedCache cacheSvc, string key, T value, TimeSpan? expiration = null)
        {
            var json = value.ToJson();
            return cacheSvc.SetStringAsync(key, json, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expiration });
        }
        public static async Task<T> GetAsync<T>(this IDistributedCache cacheSvc, string key)
        {
            var json = await cacheSvc.GetStringAsync(key);
            if (json.IsNullOrEmpty()) return default(T);
            else return json.ToObject<T>();
        }
    }
}
