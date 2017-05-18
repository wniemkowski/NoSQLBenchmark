using MongoDB.Driver;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.Benchmarks
{
    public class MongoDbBenchmark : IBenchmark
    {
        private MongoClient _client;
        private IMongoDatabase _db;

        public void Dispose()
        {
            _client = null;
        }

        public void Connect()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("test");
            
        }

        public void Test<T>()
        {
            var collection = _db.GetCollection<T>("News");
            var mf = new ModelFactory();

            collection.InsertOne((T)mf.GetDemoModel<T>());
            var a = collection.Find(news => (news as BaseModel).Id == long.MaxValue).First();
        }
    }
}