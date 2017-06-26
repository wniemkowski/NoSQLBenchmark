using System;
using System.Configuration;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    public class RavenDbConnector : IDbConnector
    {
        private DocumentStore store;
        private IDocumentSession _session;
        public void Connect()
        {
            store = new DocumentStore() { Url = @"http://" + ConfigurationManager.AppSettings["DbIpAddress"] + ":8080" };
            store.Initialize();
            var a = store.OpenSession("test");
            FlushDb();
            //CreateDB();
            InitScheme<BaseModel>();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            using (_session = store.OpenSession("test"))
            {
                _session.Store(data);
                _session.SaveChanges();
            }
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            using (_session = store.OpenSession("test"))
            {
                var dbData = Read<T>(id);
                dbData = data;
                _session.SaveChanges();
            }
            return data;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            using (_session = store.OpenSession("test"))
            {
                var type = typeof(T).ToString();
                var data = _session.Load<T>(type + "/" + id);

                return data;
            }
        }

        public void FlushDb()
        {
            try
            {
                store.DatabaseCommands.GlobalAdmin.DeleteDatabase("test", hardDelete: true);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void CreateDB()
        {
            store
                .DatabaseCommands
                .GlobalAdmin
                .CreateDatabase(new DatabaseDocument
                {
                    Id = "test",
                    Settings =
                    {
                        { "Raven/ActiveBundles", "PeriodicExport" },
                        { "Raven/DataDir", @"~\Databases\NewDatabase" }
                    }
                });
        }

        public void InitScheme<T>() where T : BaseModel
        {
            _session = store.OpenSession("test");
        }
    }
}