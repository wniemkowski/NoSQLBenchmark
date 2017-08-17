using System;
using System.Collections.Generic;
using System.Diagnostics;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public class JustReadsStrategy : IScenarioStrategy
    {
        public int CountOfOperations { get; set; }
        public List<long> Delays { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var stopwatch = new Stopwatch();
            Delays = new List<long>();
            var rand = new Random();
            var max = ModelFactory.Max;
            for (var i = 0; i < CountOfOperations; i++)
            {
                if (i % 100 == 99)
                    stopwatch.Start();
                var id = rand.Next(max - CountOfOperations+1, max);

                db.Read<BaseModel>(id);

                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                    Delays.Add(stopwatch.ElapsedTicks);
                    stopwatch.Reset();
                }
            }
        }
    }
}