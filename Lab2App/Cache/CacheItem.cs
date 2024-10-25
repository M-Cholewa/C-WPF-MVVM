using System;

namespace Lab2App.Cache
{
    public class CacheItem<T>(T value, TimeSpan duration)
    {
        public T Value { get; } = value;
        public DateTime Expiration { get; } = DateTime.Now.Add(duration);

        public bool IsExpired => DateTime.Now >= Expiration;
    }
}
