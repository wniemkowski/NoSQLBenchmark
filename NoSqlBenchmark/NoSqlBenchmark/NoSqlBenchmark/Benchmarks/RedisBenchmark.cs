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

    class RedisConnector : IDbConnector
    {
        private RedisClient _redisClient;
        public void Connect()
        {
            _redisClient = new RedisClient("localhost");
            _typedClient = _redisClient.As<A>();
        }

        public T Insert<T>(T data)
        {
            _typedClient.SetValue("foo", data as A);
        }

        public T Update<T>(long id)
        {
            throw new System.NotImplementedException();
        }

        public T Read<T>(long id)
        {
            throw new System.NotImplementedException();
        }

        public void FlushDb()
        {
            _redisClient.FlushAll();
            _redisClient.Dispose();
        }

        public void InitScheme<T>()
        {
            
        }

        public static IRedisTypedClient<T> GetClient<T>()
        {
            
        }
    }
}