using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    public class RedisConnector<TA> : IDbConnector where TA:BaseModel 
    {
        private RedisClient _redisClient;
        private IRedisTypedClient<TA> _typedClient;
        public void Connect()
        {
            _redisClient = new RedisClient("localhost");
            _typedClient = _redisClient.As<TA>();
            FlushDb();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            _typedClient.SetEntry(data.Id.ToString(), (TA)(object)data);
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            _typedClient.SetEntry(id.ToString(), (TA)(object)data);
            return data;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            return _redisClient.Get<T>(id.ToString());
        }

        public void FlushDb()
        {
            _redisClient.FlushAll();
            _redisClient.Dispose();
        }

        public void InitScheme<T>() where T : BaseModel
        {

        }
    }
}