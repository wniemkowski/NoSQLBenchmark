using System;

namespace NoSqlBenchmark
{
    public interface IBenchmark : IDisposable
    {
        void Test<T>() where T : BaseModel;
    }
}