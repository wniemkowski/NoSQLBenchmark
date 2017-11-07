using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var tmp = new List<long>();

            for (var i = 0; i < CountOfOperations; i++)
            {
                stopwatch.Start();

                var id = rand.Next(max - CountOfOperations+1, max-10);
                db.Read<BaseModel>(id);

                stopwatch.Stop();
                tmp.Add((stopwatch.ElapsedMilliseconds * 1000));
                stopwatch.Reset();
                if (tmp.Count == 100)
                {
                    Delays.Add((long)tmp.Average(x => x));
                    tmp = new List<long>();
                }
            }
        }
    }
}