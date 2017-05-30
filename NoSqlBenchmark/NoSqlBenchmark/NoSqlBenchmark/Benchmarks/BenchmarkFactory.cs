using System;
using System.Collections.Generic;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace NoSqlBenchmark.Benchmarks
{
    public class BenchmarkFactory
    {
        public IBenchmark GetBenchmark(BenchmarkType benchmarkType, ModelDataType modelType)
        {
            switch (benchmarkType)
            {
                case BenchmarkType.CouchDB:
                    return new CouchDbBenchmark();
                case BenchmarkType.DynamoDB:
                    return GetDynamoBenchmark(modelType);
                case BenchmarkType.Memcached:
                    return new MemcachedBenchmark();
                case BenchmarkType.MongoDB:
                    return GetMongoBenchmark(modelType);
                case BenchmarkType.Redis:
                    return GetRedisBenchmark(modelType);
                default:
                    throw new ArgumentOutOfRangeException(nameof(benchmarkType), benchmarkType, null);
            }
        }

        public IList<IBenchmark> GetAllBenchmarks(ModelDataType modelType)
        {
            var benchmarks = new List<IBenchmark>();
            benchmarks.Add(new CouchDbBenchmark());
            benchmarks.Add(new MemcachedBenchmark());
            //benchmarks.Add(GetDynamoBenchmark(modelType));
            benchmarks.Add(GetMongoBenchmark(modelType));
            benchmarks.Add(GetRedisBenchmark(modelType));
            return benchmarks;
        }

        private static IBenchmark GetDynamoBenchmark(ModelDataType modelType)
        {
            switch (modelType)
            {
                case ModelDataType.News:
                    return new DymanoDbBenchmark<News>();
                case ModelDataType.Bank:
                    return new DymanoDbBenchmark<Bank>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelType), modelType, null);
            }
        }

        private static IBenchmark GetRedisBenchmark(ModelDataType modelType)
        {
            switch (modelType)
            {
                case ModelDataType.News:
                    return new RedisBenchmark<News>();
                case ModelDataType.Bank:
                    return new RedisBenchmark<Bank>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelType), modelType, null);
            }
        }

        private static IBenchmark GetMongoBenchmark(ModelDataType modelType)
        {
            switch (modelType)
            {
                case ModelDataType.News:
                    return new MongoDbBenchmark<News>();
                case ModelDataType.Bank:
                    return new MongoDbBenchmark<Bank>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelType), modelType, null);
            }
        }
    }

    public enum BenchmarkType
    {
        CouchDB,
        DynamoDB,
        Memcached,
        MongoDB,
        Redis
    }
}
