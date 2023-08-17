using Microsoft.Extensions.Caching.Memory;

namespace FirstApi.Service.Cache
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public void Add(string key, object value)
        {
            _memoryCache.Set(key, value);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

      

     

        public void Clear()
        {
        }
    }
}
