using System;

namespace NoSqlBenchmark
{
    internal interface IBenchmark : IDisposable
    {
        void Connect();
        void Test<T>();
    }
}