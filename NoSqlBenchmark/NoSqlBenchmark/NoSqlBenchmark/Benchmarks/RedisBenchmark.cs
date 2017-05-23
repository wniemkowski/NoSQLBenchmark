using NoSqlBenchmark.Benchmarks.DbConnectors;
using ServiceStack.Redis;

namespace NoSqlBenchmark.Benchmarks
{
    public class RedisBenchmark<T> : IBenchmark where T:BaseModel
    {
        private readonly IDbConnector _db;

        public RedisBenchmark()
        {
            _db = new RedisConnector<T>();
            _db.Connect();
        }

        public void Dispose()
        {
            _db.FlushDb();
        }

        public void Connect()
        {
            _db.Connect();
        }

        public void Test<T>() where T : BaseModel
        {
            var mf = new ModelFactory();
            var data = mf.GetDemoModel<T>();
            _db.Insert(data);
            var readData = _db.Read<T>(data.Id);
        }

        public override string ToString()
        {
            return "Redis";
        }
    }
}