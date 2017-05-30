﻿using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark.Benchmarks
{
    public class MemcachedBenchmark : IBenchmark
    {
        private MemcachedConnector _db;

        public MemcachedBenchmark()
        {
            _db = new MemcachedConnector();
            _db.Connect();
        }


        public void Test<T>(IScenarioStrategy scenario) where T : BaseModel
        {
            var mf = new ModelFactory();
            var dataType = mf.GetModelDataType<T>();
            scenario.ExecuteStrategy(_db, dataType);
        }

        public void Dispose()
        {
            _db.FlushDb();
        }
        public override string ToString()
        {
            return "Memcached";
        }
    }
}