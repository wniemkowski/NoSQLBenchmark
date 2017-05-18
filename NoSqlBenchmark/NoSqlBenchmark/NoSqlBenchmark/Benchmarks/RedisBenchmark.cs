using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace NoSqlBenchmark
{
    public class RedisBenchmark : IBenchmark
    {
        private RedisClient _redisClient;
        
        public void Dispose()
        {
            _redisClient.Dispose();
        }

        public void Connect()
        {
            _redisClient = new RedisClient("localhost");
        }

        public void Test<T>()
        {
            var mf = new ModelFactory();
            var redis = _redisClient.As<T>();
            redis.SetValue("foo", (T)mf.GetDemoModel<T>());
            var value = _redisClient.Get<T>("foo");
        }
    }
}