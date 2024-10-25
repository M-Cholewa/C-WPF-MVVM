using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab2App.Cache
{
    public class CacheService<TKey, TValue>(TimeSpan defaultCacheDuration) where TKey : notnull
    {
        private readonly Dictionary<TKey, CacheItem<TValue>> _cache = new();
        private readonly TimeSpan _defaultCacheDuration = defaultCacheDuration;

        public async Task<TValue> GetOrAddAsync(TKey key, Func<Task<TValue>> getDataFunc)
        {
            if (_cache.TryGetValue(key, out CacheItem<TValue>? cacheItem))
            {
                if (!cacheItem.IsExpired)
                {
                    return cacheItem.Value;
                }
                else
                {
                    _cache.Remove(key);
                }
            }

            TValue data = await getDataFunc();
            _cache[key] = new CacheItem<TValue>(data, _defaultCacheDuration);
            return data;
        }

        public void Update(TKey key, TValue value)
        {
            _cache[key] = new CacheItem<TValue>(value, _defaultCacheDuration);
        }

        public void Remove(TKey key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
