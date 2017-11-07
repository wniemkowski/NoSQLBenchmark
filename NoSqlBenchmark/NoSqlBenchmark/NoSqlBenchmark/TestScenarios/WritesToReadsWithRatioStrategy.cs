using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public class WritesToReadsWithRatioStrategy : IScenarioStrategy
    {
        public float WritesToReadsRatio { get; set; }
        public int CountOfOperations { get; set; }
        public List<long> Delays { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var readEveryNWrite = (int) (WritesToReadsRatio * CountOfOperations);
            var mf = new ModelFactory();
            var stopwatch = new Stopwatch();
            Delays = new List<long>();
            var tmp = new List<long>();
            var rand = new Random();

            for (var i = 0; i < CountOfOperations; i++)
            {

                stopwatch.Start();

                var data = mf.GetDemoModel(dataType);
                db.Insert(data);
                if (i % (readEveryNWrite) == 9)
                {
                    db.Read<BaseModel>(rand.Next((int)data.Id, (int)data.Id+9));
                }

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