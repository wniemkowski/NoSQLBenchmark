using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public interface IScenarioStrategy
    {
        int CountOfOperations { get; set; }
        void ExecuteStrategy(IDbConnector db, ModelDataType dataType);
    }
}
