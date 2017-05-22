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
                    //new DymanoDbBenchmark(),
                    //new MongoDbBenchmark(),
                    new MemcachedBenchmark(),
                    //new RedisBenchmark(),
                    //new CouchDbBenchmark()
                };

            foreach (var benchmark in benchmarks)
            {
                benchmark.Test<News>();
                benchmark.Dispose();
            }
        }
    }


}
