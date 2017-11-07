﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var tmp = new List<long>();

            for (var i = 0; i < CountOfOperations; i++)
            {
                stopwatch.Start();

                var data = mf.GetDemoModel(dataType);
                try
                {
                    db.Insert(data);
                    db.Read<BaseModel>(data.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR");
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