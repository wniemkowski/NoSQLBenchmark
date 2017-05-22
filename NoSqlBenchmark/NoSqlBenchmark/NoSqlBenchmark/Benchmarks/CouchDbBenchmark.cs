namespace NoSqlBenchmark.Benchmarks
{
    public class CouchDbBenchmark : IBenchmark
    {
        private IDbConnector _db;

        public CouchDbBenchmark()
        {
            _db = new CouchDbConnector();
            _db.Connect();
        }

        public void Dispose()
        {
            _db.FlushDb();
        }
        
        public void Test<T>()
        {
            var mf = new ModelFactory();
            BaseModel model = mf.GetDemoModel<T>() as BaseModel;

            _db.Insert(model);
            var a = _db.Read<T>(model.Id);
        }
    }
}