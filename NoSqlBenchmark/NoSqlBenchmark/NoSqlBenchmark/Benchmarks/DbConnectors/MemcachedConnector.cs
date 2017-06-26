using System.Configuration;
using System.Net;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
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
            FlushDb();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            _memcachedClient.Store(StoreMode.Set, data.Id.ToString(), data);
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            _memcachedClient.Store(StoreMode.Replace, id.ToString(), data);
            return data;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            return (T)_memcachedClient.Get(id.ToString());
        }

        public void FlushDb()
        {
            _memcachedClient.FlushAll();
        }

        public void InitScheme<T>() where T : BaseModel
        {
        }

        private static MemcachedClientConfiguration GetConfig()
        {
            var ip = IPAddress.Parse(ConfigurationManager.AppSettings["DbIpAddress"]);
            var port = 11211;
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(ip, port));
            return config;
        }
    }
}