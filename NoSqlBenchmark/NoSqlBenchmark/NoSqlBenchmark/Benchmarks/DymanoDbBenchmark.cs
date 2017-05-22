using Amazon.DynamoDBv2;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Models;
using ServiceStack.Aws.DynamoDb;

namespace NoSqlBenchmark
{
    public class DymanoDbBenchmark : IBenchmark
    {
        private readonly AmazonDynamoDBClient _client;
        private PocoDynamo _db1;
        private IDbConnector _db;
        public DymanoDbBenchmark()
        {
            _db = new DynamoDbConnector();
            _db.Connect();
        }
        
        public void Test<T>()
        {
            var mf = new ModelFactory();
            _db.InitScheme<T>();
            var data = (T) mf.GetDemoModel<T>();
            var inserted = _db.Insert<T>(data);
            var a = _db.Read<T>((data as BaseModel).Id);
        }
        public void Dispose()
        {
            _db.FlushDb();
        }
    }
}