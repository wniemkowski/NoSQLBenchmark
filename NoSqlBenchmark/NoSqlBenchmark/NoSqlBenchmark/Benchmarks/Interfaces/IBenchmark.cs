using System;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark.Benchmarks.Interfaces
{
    public interface IBenchmark : IDisposable
    {
        void Test<T>(IScenarioStrategy scenario) where T : BaseModel;
    }
}