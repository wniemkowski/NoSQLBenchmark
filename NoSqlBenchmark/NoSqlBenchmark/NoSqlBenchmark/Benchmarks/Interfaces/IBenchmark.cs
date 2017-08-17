using System;
using System.Collections.Generic;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark.Benchmarks.Interfaces
{
    public interface IBenchmark : IDisposable
    {
        List<long> Delays{ get; set; }
        void Test<T>(IScenarioStrategy scenario) where T : BaseModel;
    }
}