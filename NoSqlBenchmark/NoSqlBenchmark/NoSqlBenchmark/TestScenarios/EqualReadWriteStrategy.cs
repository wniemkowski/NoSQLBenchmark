using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public class EqualReadWriteStrategy : IScenarioStrategy
    {
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var mf = new ModelFactory();
            for (var i = 0; i < CountOfOperations; i++)
            {
                var data = mf.GetDemoModel(dataType);
                db.Insert(data);
                db.Read<BaseModel>(data.Id);
            }
        }
    }
}