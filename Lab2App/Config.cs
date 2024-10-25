using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2App
{
    public static class Config
    {
        public static TimeSpan ProductCacheDurationMinutes { get; set; } = TimeSpan.FromMinutes(5);
        public static TimeSpan ProductListCacheDurationMinutes { get; set; } = TimeSpan.FromMinutes(5);
        public static string ProductUrl { get; set; } = "https://localhost:58530/Product";
        public static string ProductListCacheKey { get; set; } = "ProductList";
    }
}
