using Amazon.DynamoDBv2;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

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
        
        public void Test<T>(IScenarioStrategy scenario) where T : BaseModel
        {
            var mf = new ModelFactory();
            var dataType = mf.GetModelDataType<T>();
            scenario.ExecuteStrategy(_db, dataType);
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