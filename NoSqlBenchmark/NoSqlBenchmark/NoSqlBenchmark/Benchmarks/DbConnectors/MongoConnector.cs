using System;
using System.Configuration;
using MongoDB.Driver;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    public class MongoConnector<TA> : IDbConnector where TA:BaseModel
    {
        private MongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<TA> _collection;

        public void Connect()
        {
            _client = new MongoClient("mongodb://" + ConfigurationManager.AppSettings["DbIpAddress"] + ":27017");
            _db = _client.GetDatabase("test");
            InitScheme<TA>();
            FlushDb();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            _collection.InsertOne((TA)(object)data);
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            var update = Builders<TA>.Update.Set(s => s.Id, id-1);
            _collection.UpdateOne(news => news.Id == id, update);
            return data;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            return (T)(object)_collection.Find(news => news.Id == id).First();
        }

        public void FlushDb()
        {
            _collection.DeleteMany(a => true);
        }

        public void InitScheme<T>() where T : BaseModel
        {
            _collection = _db.GetCollection<TA>(typeof(TA).ToString());
        }
    }
}