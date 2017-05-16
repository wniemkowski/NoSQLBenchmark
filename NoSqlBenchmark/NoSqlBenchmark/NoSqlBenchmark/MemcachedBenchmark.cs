using System.Net;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace NoSqlBenchmark
{
    public class MemcachedBenchmark : IBenchmark
    {
        private MemcachedClient _memcachedClient;

        public void Connect()
        {
            _memcachedClient = new MemcachedClient(GetConfig());
        }

        public void Test()
        {
            _memcachedClient.Store(StoreMode.Set, "foo3", "baar");
            var value = _memcachedClient.Stats("items");
        }

        public void Dispose()
        {
            _memcachedClient.Dispose();
        }

        private static MemcachedClientConfiguration GetConfig()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            var port = 11211;
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(ip, port));
            return config;
        }
    }
}