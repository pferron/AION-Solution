using System;
using System.Runtime.Caching;

namespace AION.Base
{
    public class LocalStorage<T>
    {
        public static T GetValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return (T)memoryCache.Get(key);
        }

        public static bool Add(string key, T value, int minutesToExpire = 1440)
        {
            DateTimeOffset absExpiration = DateTimeOffset.Now.AddMinutes(minutesToExpire);
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, value, absExpiration);

        }

        public static void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }
    }
}
