﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var rand = new Random();
            var readEveryNWrite =(int)(CountOfOperations/(int)(ReadsToWritesRatio * CountOfOperations));
            var mf = new ModelFactory();
            var stopwatch = new Stopwatch();
            Delays = new List<long>();
            var tmp = new List<long>();
            var max = ModelFactory.Max-4;
            var minInsertedId = ModelFactory._iterator+1;

            for (var i = 0; i < CountOfOperations; i++)
            {
                stopwatch.Start();

                
                db.Read<BaseModel>(rand.Next(minInsertedId,max));
                if (i % (readEveryNWrite) == 0)
                {
                    var data = mf.GetDemoModel(dataType);
                    db.Insert(data);
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