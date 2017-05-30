using System;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark.Benchmarks
{
    public class MongoDbBenchmark<T> : IBenchmark where T:BaseModel
    {
        private readonly IDbConnector _db;

        public MongoDbBenchmark()
        {
            _db = new MongoConnector<T>();
            Connect();
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
            return "MongoDB";
        }
    }
}