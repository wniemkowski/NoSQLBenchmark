using System.Collections.Generic;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public interface IScenarioStrategy
    {
        int CountOfOperations { get; set; }
        List<long> Delays { get; set; }
        void ExecuteStrategy(IDbConnector db, ModelDataType dataType);
    }
}
