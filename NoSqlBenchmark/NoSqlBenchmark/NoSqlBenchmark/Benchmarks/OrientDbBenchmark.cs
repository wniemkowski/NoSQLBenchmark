using System.Collections.Generic;
using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark.Benchmarks
{
    public class OrientDbBenchmark : IBenchmark
    {
        private readonly IDbConnector _db;

        public OrientDbBenchmark()
        {
            _db = new OrientDbConnector();
            _db.Connect();
        }
        public void Dispose()
        {
            _db.FlushDb();
        }

        public List<long> Delays { get; set; }

        public void Test<T>(IScenarioStrategy scenario) where T : BaseModel
        {
            var mf = new ModelFactory();
            var dataType = mf.GetModelDataType<T>();
            scenario.ExecuteStrategy(_db, dataType);
            Delays = scenario.Delays;
        }

        public override string ToString()
        {
            return "OrientDB";
        }
    }
}