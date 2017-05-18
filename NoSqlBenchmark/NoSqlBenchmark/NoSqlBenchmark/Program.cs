using System.Collections.Generic;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<IBenchmark> benchmarks =
                new List<IBenchmark>
                {
                    new DymanoDbBenchmark(),
                    new MemcachedBenchmark(),
                    new RedisBenchmark(),
                   // new MongoDbBenchmark(),
                    new CouchDbBenchmark()
                };

            foreach (var benchmark in benchmarks)
            {
                benchmark.Connect();
                benchmark.Test<News>();
                benchmark.Dispose();
            }
        }
    }


}
