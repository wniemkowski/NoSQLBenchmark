using System;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark
{
    public interface IBenchmark : IDisposable
    {
        void Test<T>(IScenarioStrategy scenario) where T : BaseModel;
    }
}