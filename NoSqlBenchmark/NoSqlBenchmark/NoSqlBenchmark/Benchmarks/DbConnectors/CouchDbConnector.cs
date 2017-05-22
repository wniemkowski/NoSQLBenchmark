using LoveSeat;

namespace NoSqlBenchmark.Benchmarks
{
    public class CouchDbConnector : IDbConnector
    {
        private CouchClient _client;
        private CouchDatabase _db;

        public void Connect()
        {
            _client = new CouchClient("localhost", 5984, "user", "password", false, AuthenticationType.Basic);
            _db = _client.GetDatabase("test");
            _db.SetDefaultDesignDoc("docs");
        }

        public T Insert<T>(T data)
        {
            var demo = _db.ObjectSerializer.Serialize(data);
            var a = _db.CreateDocument((data as BaseModel).Id.ToString(), demo);
            return data;
        }
        
        public T Update<T>(long id)
        {
            var doc = _db.GetDocument<T>(id.ToString());
            var serialized = _db.ObjectSerializer.Serialize(doc);
            _db.SaveDocument(new Document(serialized));
            return doc;
        }

        public T Read<T>(long id)
        {
            return _db.GetDocument<T>(id.ToString());
        }

        public void FlushDb()
        {
            _db.GetAllDocuments();
        }

        public void InitScheme<T>()
        {
            
        }
    }
}