using System;
using NoSqlBenchmark.Benchmarks.DbConnectors;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;
using RiakClient;
using RiakClient.Models;

namespace NoSqlBenchmark.Benchmarks
{
    public class RiakBenchmark : IBenchmark
    {
        private IDbConnector _db;
        public RiakBenchmark()
        {
            _db = new RiakConnector();
            _db.Connect();
        }

        public void Dispose()
        {
            _db.FlushDb();
        }

        public void Test<T>(IScenarioStrategy scenario) where T : BaseModel
        {
            var mf = new ModelFactory();
            var dataType = mf.GetModelDataType<T>();
            scenario.ExecuteStrategy(_db, dataType);
        }

        public override string ToString()
        {
            return "CouchBase";
        }
    }

    public class RiakConnector : IDbConnector
    {
        private IRiakEndPoint _cluster;
        private IRiakClient _client;
        public void Connect()
        {
            _cluster = RiakCluster.FromConfig("riakConfig");
            _client = _cluster.CreateClient();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            var o = new RiakObject("test",data.Id.ToString(),data);
            _client.Put(o);
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public T Read<T>(long id) where T : BaseModel
        {
            throw new NotImplementedException();
        }

        public void FlushDb()
        {
            throw new NotImplementedException();
        }

        public void InitScheme<T>() where T : BaseModel
        {
            throw new NotImplementedException();
        }
    }
}