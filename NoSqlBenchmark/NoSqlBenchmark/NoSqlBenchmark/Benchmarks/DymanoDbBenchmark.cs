using Amazon.DynamoDBv2;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;
using ServiceStack.Aws.DynamoDb;

namespace NoSqlBenchmark
{
    public class DymanoDbBenchmark : IBenchmark
    {
        private readonly AmazonDynamoDBClient _client;
        private PocoDynamo _db;

        public DymanoDbBenchmark()
        {
            _client = new AmazonDynamoDBClient("key", "pass", new AmazonDynamoDBConfig
            {
                ServiceURL = "http://localhost:8000/",
                AuthenticationRegion = "us-east-1",
                ProxyBypassOnLocal = true
            });
        }

        public void Dispose()
        {
            _client.Dispose();
            //_db.DeleteAllTables();
        }

        public void Connect()
        {
            _db = new PocoDynamo(_client);
        }

        public void Test<T>()
        {
            var mf = new ModelFactory();
            _db.RegisterTable<T>();
            _db.InitSchema();
            _db.PutItem((T)mf.GetDemoModel<T>());
            var a = _db.GetItem<T>(((BaseModel) mf.GetDemoModel<T>()).Id);
        }
    }
}