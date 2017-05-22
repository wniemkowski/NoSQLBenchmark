using System.Net;
using Enyim.Caching.Configuration;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark
{
    public class MemcachedBenchmark : IBenchmark
    {
        private MemcachedConnector _db;

        public MemcachedBenchmark()
        {
            _db = new MemcachedConnector();
        }


        public void Test<T>()
        {
            var mf = new ModelFactory();
            var demoModel = mf.GetDemoModel<T>() as BaseModel;
            _db.Insert(demoModel);
            var a = _db.Read<T>(demoModel.Id);
        }

        public void Dispose()
        {
            _db.FlushDb();
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