using System;
using System.Collections.Generic;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

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
                case BenchmarkType.RavenDB:
                    return new RavenDbBenchmark();
                case BenchmarkType.Couchbase:
                    return new CouchbaaseBenchmark();
                case BenchmarkType.OrientDb:
                    return new OrientDbBenchmark();
                case BenchmarkType.Riak:
                    return new RiakBenchmark();
                default:
                    throw new ArgumentOutOfRangeException(nameof(benchmarkType), benchmarkType, null);
            }
        }

        public IList<IBenchmark> GetAllBenchmarks(ModelDataType modelType)
        {
            var benchmarks = new List<IBenchmark>
            {
                GetMongoBenchmark(modelType),
                new MemcachedBenchmark(),
                GetRedisBenchmark(modelType),
                new CouchbaaseBenchmark(),
                new RiakBenchmark(),
                new OrientDbBenchmark()
            };

            //benchmarks.Add(new RavenDbBenchmark());
            return benchmarks;
        }

        private static IBenchmark GetDynamoBenchmark(ModelDataType modelType)
        {
            switch (modelType)
            {
                case ModelDataType.Reddit:
                    return new DymanoDbBenchmark<RedditModel>();
                case ModelDataType.Tweeter:
                    return new DymanoDbBenchmark<YoutubeModel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelType), modelType, null);
            }
        }

        private static IBenchmark GetRedisBenchmark(ModelDataType modelType)
        {
            switch (modelType)
            {
                case ModelDataType.Reddit:
                    return new RedisBenchmark<RedditModel>();
                case ModelDataType.Tweeter:
                    return new RedisBenchmark<TweeterModel>();
                case ModelDataType.Youtube:
                    return new RedisBenchmark<YoutubeModel>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelType), modelType, null);
            }
        }

        private static IBenchmark GetMongoBenchmark(ModelDataType modelType)
        {
            switch (modelType)
            {
                case ModelDataType.Reddit:
                    return new MongoDbBenchmark<RedditModel>();
                case ModelDataType.Tweeter:
                    return new MongoDbBenchmark<TweeterModel>();
                case ModelDataType.Youtube:
                    return new MongoDbBenchmark<YoutubeModel>();
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
        Redis,
        RavenDB,
        Couchbase,
        OrientDb,
        Riak
    }
}
