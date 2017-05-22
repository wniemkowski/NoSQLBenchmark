using System;
using Amazon.DynamoDBv2;
using ServiceStack.Aws.DynamoDb;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    public class DynamoDbConnector : IDbConnector
    {
        private AmazonDynamoDBClient _client;
        private PocoDynamo _db;

        public void Connect()
        {
            _client = new AmazonDynamoDBClient("key", "pass", new AmazonDynamoDBConfig
            {
                ServiceURL = "http://localhost:8000/",
                AuthenticationRegion = "us-east-1",
                ProxyBypassOnLocal = true
            });
            _db = new PocoDynamo(_client);
        }

        public void FlushDb()
        {
            _db.DeleteAllTables();
        }

        public void InitScheme<T>()
        {
            _db.RegisterTable<T>();
            _db.InitSchema();
        }

        public T Insert<T>(T data)
        {

            var a = _db.PutItem(data);
            return a;
        }

        public T Read<T>(long id)
        {
            return _db.GetItem<T>(id);
        }

        public T Update<T>(long id)
        {
            throw new NotImplementedException();
        }
    }
}