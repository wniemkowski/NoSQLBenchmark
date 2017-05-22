using System;

namespace NoSqlBenchmark
{
    internal interface IBenchmark : IDisposable
    {
        void Test<T>();
    }
}