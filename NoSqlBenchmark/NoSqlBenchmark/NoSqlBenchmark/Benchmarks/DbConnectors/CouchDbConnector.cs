using LoveSeat;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    public class CouchDbConnector : IDbConnector
    {
        private CouchClient _client;
        private CouchDatabase _db;

        public void Connect()
        {
            _client = new CouchClient("localhost", 5984, "user", "password", false, AuthenticationType.Basic);
            FlushDb();
            _client.CreateDatabase("test");
            _db = _client.GetDatabase("test");
            _db.SetDefaultDesignDoc("docs");
            
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            var demo = _db.ObjectSerializer.Serialize(data);
            var a = _db.CreateDocument(data.Id.ToString(), demo);
            return data;
        }
        
        public T Update<T>(long id, T data) where T : BaseModel
        {
            var doc = _db.GetDocument<T>(id.ToString());
            var serialized = _db.ObjectSerializer.Serialize(data);
            _db.SaveDocument(new Document(serialized));
            return doc;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            return _db.GetDocument<T>(id.ToString());
        }

        public void FlushDb()
        {
            _client.DeleteDatabase("test");
        }

        public void InitScheme<T>() where T : BaseModel
        {
            
        }
    }
}