using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootcamp.ApplicationService;
using Microsoft.Extensions.Caching.Memory;

namespace Bootcamp.Cache
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public void Add<T>(string key, T value)
        {
            memoryCache.Set(key, value);
        }

        public T? Get<T>(string key)
        {
            return memoryCache.Get<T>(key);
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }
    }
}