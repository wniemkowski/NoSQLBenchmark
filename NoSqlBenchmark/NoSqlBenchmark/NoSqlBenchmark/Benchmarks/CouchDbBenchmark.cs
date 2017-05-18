using LoveSeat;

namespace NoSqlBenchmark.Benchmarks
{
    public class CouchDbBenchmark : IBenchmark
    {
        private CouchClient _client;
        private CouchDatabase _db;
        
        public void Dispose()
        {
            _client = null;
        }

        public void Connect()
        {
            _client = new CouchClient("localhost", 5984, "user", "password", false, AuthenticationType.Basic);
            _db = _client.GetDatabase("test");
            _db.SetDefaultDesignDoc("docs");
        }

        public void Test<T>()
        {
            var mf = new ModelFactory();
            var demo = _db.ObjectSerializer.Serialize(mf.GetDemoModel<T>());
            _db.CreateDocument("123", demo);
            var myObj = _db.GetDocument<T>("123");
        }
    }
}