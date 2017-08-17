using System.Collections.Generic;
using System.Diagnostics;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public class ReadsToWritesWithRatioStrategy : IScenarioStrategy
    {
        public float ReadsToWritesRatio { get; set; }
        public int CountOfOperations { get; set; }
        public List<long> Delays { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var readEveryNWrite = (int) (ReadsToWritesRatio * CountOfOperations);
            var mf = new ModelFactory();
            var stopwatch = new Stopwatch();
            Delays = new List<long>();
            for (var i = 0; i < CountOfOperations; i++)
            {
                if (i % 100 == 99)
                    stopwatch.Start();

                var data = mf.GetDemoModel(dataType);
                db.Insert(data);
                if (i % (readEveryNWrite) == 0)
                {
                    db.Read<BaseModel>(data.Id);
                }

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