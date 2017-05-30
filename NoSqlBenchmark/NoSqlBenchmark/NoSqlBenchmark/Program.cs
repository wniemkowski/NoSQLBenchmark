﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var testCount = 10000;
            var benchmarks = new BenchmarkFactory().GetAllBenchmarks(ModelDataType.News);
            foreach (var benchmark in benchmarks)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                //Console.Write($"\r{i * 100 / testCount}%\t");
                benchmark.Test<News>(new JustInsertsStrategy() {CountOfOperations = 1000});
                stopwatch.Stop();
                Console.WriteLine($"{benchmark,9} resulted with {stopwatch.Elapsed:g}");
                benchmark.Dispose();
            }

            Console.ReadKey();
        }
    }


}
