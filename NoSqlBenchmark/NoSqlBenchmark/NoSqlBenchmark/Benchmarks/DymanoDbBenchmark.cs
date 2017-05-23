using Amazon.DynamoDBv2;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Models;
using ServiceStack.Aws.DynamoDb;

namespace NoSqlBenchmark
{
    public class DymanoDbBenchmark<T> : IBenchmark where T : BaseModel
    {
        private readonly IDbConnector _db;
        public DymanoDbBenchmark()
        {
            _db = new DynamoDbConnector();
            _db.Connect();
            _db.InitScheme<T>();
        }
        
        public void Test<T>() where T : BaseModel
        {
            var mf = new ModelFactory();
            var data = (T) mf.GetDemoModel<T>();
            var inserted = _db.Insert<T>(data);
            var a = _db.Read<T>(data.Id);
        }
        public void Dispose()
        {
            _db.FlushDb();
        }

        public override string ToString()
        {
            return "DynamoDB";
        }
    }
}