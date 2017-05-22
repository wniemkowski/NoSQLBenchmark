using System.Net;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using NoSqlBenchmark.Benchmarks;

namespace NoSqlBenchmark
{
    public class MemcachedConnector : IDbConnector
    {
        private readonly MemcachedClient _memcachedClient;

        public MemcachedConnector()
        {
            _memcachedClient = new MemcachedClient(GetConfig());
        }

        public void Connect()
        {
            
        }

        public T Insert<T>(T data)
        {
            _memcachedClient.Store(StoreMode.Set, (data as BaseModel).Id.ToString(), data);
            return data;
        }

        public T Update<T>(long id)
        {
            var data = (T)_memcachedClient.Get(id.ToString());
            _memcachedClient.Store(StoreMode.Replace, id.ToString(), data);
            return data;
        }

        public T Read<T>(long id)
        {
            return (T)_memcachedClient.Get(id.ToString());
        }

        public void FlushDb()
        {
            _memcachedClient.FlushAll();
        }

        public void InitScheme<T>()
        {
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