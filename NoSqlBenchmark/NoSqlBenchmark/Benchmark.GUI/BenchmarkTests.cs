using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace Benchmark.GUI
{
    class BenchmarkTests
    {
        private IList<IBenchmark> benchmarks;
        
        public void Connect()
        {
            benchmarks = new BenchmarkFactory().GetAllBenchmarks(ModelDataType.Reddit);
        }

        public Result TestSingle<T>(IBenchmark benchmark, IScenarioStrategy strategy) where T : BaseModel
        {
            var delays = new List<long>();
            var stopwatch = ExecuteTest<T>(benchmark, strategy, ref delays);
            return new Result
            {
                Db = benchmark.ToString(),
                Time = stopwatch.ElapsedMilliseconds,
                Delays = delays
            };            
        }

        private  Stopwatch ExecuteTest<T>(IBenchmark benchmark, IScenarioStrategy strategy, ref List<long> delays) where T : BaseModel
        {
            var stopwatch = new Stopwatch();
            if (strategy is JustReadsStrategy)
            {
                //populateDB
                benchmark.Test<T>(new JustInsertsStrategy{CountOfOperations = strategy.CountOfOperations});
                ModelFactory._iterator = ModelFactory.Max;
            }
            stopwatch.Start();
            benchmark.Test<T>(strategy);
            stopwatch.Stop();
            benchmark.Dispose();
            delays = benchmark.Delays;
            return stopwatch;
        }
    }
}

