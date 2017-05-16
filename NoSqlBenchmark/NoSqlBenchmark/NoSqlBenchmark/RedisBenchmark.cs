using NoSqlBenchmark.Models;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace NoSqlBenchmark
{
    internal class RedisBenchmark : IBenchmark
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

        public void Test()
        {
            IRedisTypedClient<News> redis = _redisClient.As<News>();
            redis.SetEntry("foo",News.GetDemo());
            //_redisClient.Add("foo", News.GetDemo());
            var value = _redisClient.Get<News>("foo");
        }
    }
}