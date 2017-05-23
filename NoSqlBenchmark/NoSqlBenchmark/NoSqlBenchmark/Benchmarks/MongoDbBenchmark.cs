using System;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.Benchmarks
{
    public class MongoDbBenchmark<T> : IBenchmark where T:BaseModel
    {
        private readonly IDbConnector _db;

        public MongoDbBenchmark()
        {
            _db = new MongoConnector<T>();
            Connect();
        }

        public void Dispose()
        {
            _db.FlushDb();
        }

        public void Connect()
        {
            _db.Connect();
        }

        public void Test<T>() where T : BaseModel
        {
            var mf = new ModelFactory();
            var demo = mf.GetDemoModel<T>();
            _db.Insert(demo);
            var a =_db.Read<T>(demo.Id);
        }

        public override string ToString()
        {
            return "MongoDB";
        }
    }
}