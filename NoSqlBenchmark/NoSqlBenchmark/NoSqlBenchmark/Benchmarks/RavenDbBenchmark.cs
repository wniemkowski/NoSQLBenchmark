using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark.Benchmarks
{
    public class RavenDbBenchmark : IBenchmark
    {

        private readonly IDbConnector _db;

        public RavenDbBenchmark()
        {
            _db = new RavenDbConnector();
            _db.Connect();
        }
        public void Dispose()
        {
            _db.FlushDb();
        }

        public void Test<T>(IScenarioStrategy scenario) where T : BaseModel
        {
            var mf = new ModelFactory();
            var dataType = mf.GetModelDataType<T>();
            scenario.ExecuteStrategy(_db, dataType);
        }
    }
}