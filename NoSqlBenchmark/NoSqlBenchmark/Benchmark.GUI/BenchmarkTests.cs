using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NoSqlBenchmark;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;

namespace Benchmark.GUI
{
    class BenchmarkTests
    {
        int testCount = 100;
        private IList<IBenchmark> benchmarks;
        
        public void Connect()
        {
            benchmarks = new List<IBenchmark>
            {
                new MongoDbBenchmark<News>(),
                new MemcachedBenchmark(),
               // new RedisBenchmark<News>(),
                new DymanoDbBenchmark<News>(),
                new CouchDbBenchmark(),
            };
        }

        public Result TestSingle(IBenchmark benchmark)
        {
            var stopwatch = ExecuteTest(benchmark);
            var single = new Result();
            single.Db = benchmark.ToString();
            single.Time = stopwatch.ElapsedMilliseconds;
            return single;
        }

        public async Task<List<Result>> Test()
        {
            var results = new List<Result>();
            foreach (var benchmark in benchmarks)
            {
                var stopwatch = ExecuteTest(benchmark);
                results.Add(new Result {Db = benchmark.ToString(), Time = stopwatch.ElapsedMilliseconds});
            }
            return results;
        }

        private  Stopwatch ExecuteTest(IBenchmark benchmark)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 1; i <= testCount; i++)
            {
                benchmark.Test<News>();
            }
            stopwatch.Stop();
            benchmark.Dispose();
            return stopwatch;
        }
    }
}

