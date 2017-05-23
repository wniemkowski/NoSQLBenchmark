using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var testCount = 1000;
            IList<IBenchmark> benchmarks =
                new List<IBenchmark>
                {
                    new MongoDbBenchmark<News>(),
                    new MemcachedBenchmark(),
                    new RedisBenchmark<News>(),
                    new DymanoDbBenchmark<News>(),
                    new CouchDbBenchmark(),
                };

            foreach (var benchmark in benchmarks)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                for (var i = 1; i <= testCount; i++)
                {
                    Console.Write($"\r{i * 100 / testCount}%\t");
                    benchmark.Test<News>();
                }
                stopwatch.Stop();
                Console.WriteLine($"{benchmark,9} resulted with {stopwatch.Elapsed:g}");
                benchmark.Dispose();
            }

            Console.ReadKey();
        }
    }


}
