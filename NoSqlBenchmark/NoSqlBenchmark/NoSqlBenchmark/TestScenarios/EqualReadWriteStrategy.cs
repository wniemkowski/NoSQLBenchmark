using System.Collections.Generic;
using System.Diagnostics;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public class EqualReadWriteStrategy : IScenarioStrategy
    {
        public int CountOfOperations { get; set; }
        public List<long> Delays { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var mf = new ModelFactory();
            var stopwatch = new Stopwatch();
            Delays = new List<long>();
            for (var i = 0; i < CountOfOperations; i++)
            {
                if (i % 100 == 99)
                    stopwatch.Start();

                var data = mf.GetDemoModel(dataType);
                db.Insert(data);
                db.Read<BaseModel>(data.Id);

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