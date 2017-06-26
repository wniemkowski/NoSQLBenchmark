using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using Raven.Client.Changes;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    class CouchbaseConnector : IDbConnector
    {
        private Cluster _cluster;
        private IBucket _bucket;
        private const string bucketname = "test";
        private const string credential = "Administrator";
        public void Connect()
        {
            var config = new ClientConfiguration
            {
                BucketConfigs = new Dictionary<string, BucketConfiguration> {
                    {"authenticated", new BucketConfiguration {
                        Password = "Administrator",
                        Username = "Administrator",
                        BucketName = "authenticated"
                    }}
                }
            };
            ClusterHelper.Initialize(new ClientConfiguration()
            {
                Servers = new List<Uri> { new Uri(@"http://" + ConfigurationManager.AppSettings["DbIpAddress"] + ":8091") }
            });
            _bucket = ClusterHelper.GetBucket("default");
            FlushDb();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            var doc = new Document<T> { Id = data.Id.ToString(), Content = data };
            var result = _bucket.Insert(doc);
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            var result = _bucket.Get<T>(id.ToString()).Value;
            result = data;
            var doc = new Document<T> { Id = data.Id.ToString(), Content = data };
            _bucket.Insert(doc);
            return data;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            return _bucket.Get<T>(id.ToString()).Value;
        }

        public void FlushDb()
        {
            var bucket = ClusterHelper.GetBucket("default");
            var bucketManager = bucket.CreateManager(credential, credential);
            bucketManager.Flush();
        }

        public void InitScheme<T>() where T : BaseModel
        {
            throw new NotImplementedException();
        }
    }
}
