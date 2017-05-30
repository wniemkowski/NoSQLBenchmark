using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public class JustReadsStrategy : IScenarioStrategy
    {
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            for (var i = 0; i < CountOfOperations; i++)
            {
                db.Read<BaseModel>(1);
            }
        }
    }
}