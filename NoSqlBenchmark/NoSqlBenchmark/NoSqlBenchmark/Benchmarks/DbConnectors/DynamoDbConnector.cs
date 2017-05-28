using System;
using Amazon.DynamoDBv2;
//using ServiceStack.Aws.DynamoDb;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    public class DynamoDbConnector : IDbConnector
    {
        private AmazonDynamoDBClient _client;
  //      private PocoDynamo _db;

        public void Connect()
        {
            throw new NotImplementedException();
            _client = new AmazonDynamoDBClient("key", "pass", new AmazonDynamoDBConfig
            {
                ServiceURL = "http://localhost:8000/",
                AuthenticationRegion = "us-east-1",
                ProxyBypassOnLocal = true
            });
    //        _db = new PocoDynamo(_client);
            FlushDb();
        }

        public void FlushDb()
        {
            throw new NotImplementedException();
            //      _db.DeleteAllTables();
        }

        public void InitScheme<T>() where T : BaseModel
        {
            throw new NotImplementedException();
            //    _db.RegisterTable<T>();
            //  _db.InitSchema();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            throw new NotImplementedException();
            //var a = _db.PutItem(data);
            //return a;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            throw new NotImplementedException();
            //return _db.GetItem<T>(id);
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            throw new NotImplementedException();
           // _db.PutItem(data);
            //return data;
        }
    }
}