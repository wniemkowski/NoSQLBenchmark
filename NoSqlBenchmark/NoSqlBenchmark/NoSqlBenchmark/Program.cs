using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ServiceStack.Redis;

namespace NoSqlBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {

            var redisClient = new RedisClient("localhost");
            
            //using (var memcached = new MemcachedBenchmark())
            //{
            //    memcached.Connect();
            //    memcached.Test();
            //}
            IList<IBenchmark> benchmarks = new List<IBenchmark> {new MemcachedBenchmark(), new RedisBenchmark()};
            foreach (var benchmark in benchmarks)
            {
                benchmark.Connect();
                benchmark.Test();
                benchmark.Dispose();
            }
        }
    }
}
