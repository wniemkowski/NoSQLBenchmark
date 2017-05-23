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
            _db.Connect();
        }


        public void Test<T>() where T : BaseModel
        {
            var mf = new ModelFactory();
            var demoModel = mf.GetDemoModel<T>();
            _db.Insert(demoModel);
            var a = _db.Read<T>(demoModel.Id);
        }

        public void Dispose()
        {
            _db.FlushDb();
        }
        public override string ToString()
        {
            return "Memcached";
        }
    }
}