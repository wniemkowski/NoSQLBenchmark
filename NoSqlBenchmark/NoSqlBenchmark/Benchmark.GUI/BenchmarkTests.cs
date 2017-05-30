using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NoSqlBenchmark;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

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
                new RedisBenchmark<News>(),
                //new DymanoDbBenchmark<News>(),
                new CouchDbBenchmark(),
            };
        }

        public Result TestSingle(IBenchmark benchmark, IScenarioStrategy strategy)
        {
            var stopwatch = ExecuteTest(benchmark, strategy);
            return new Result
            {
                Db = benchmark.ToString(),
                Time = stopwatch.ElapsedMilliseconds
            };
            
        }

        public async Task<List<Result>> Test(IScenarioStrategy strategy)
        {
            var results = new List<Result>();
            foreach (var benchmark in benchmarks)
            {
                var stopwatch = ExecuteTest(benchmark, strategy);
                results.Add(new Result {Db = benchmark.ToString(), Time = stopwatch.ElapsedMilliseconds});
            }
            return results;
        }

        private  Stopwatch ExecuteTest(IBenchmark benchmark, IScenarioStrategy strategy)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            benchmark.Test<News>(strategy);
            stopwatch.Stop();
            benchmark.Dispose();
            return stopwatch;
        }
    }
}

