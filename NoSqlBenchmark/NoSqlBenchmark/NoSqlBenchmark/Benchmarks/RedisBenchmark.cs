using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

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

        public void Test<T>(IScenarioStrategy scenario) where T : BaseModel
        {
            var mf = new ModelFactory();
            var dataType = mf.GetModelDataType<T>();
            scenario.ExecuteStrategy(_db, dataType);
        }

        public override string ToString()
        {
            return "Redis";
        }
    }
}