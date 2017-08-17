using System;
using System.Configuration;
using System.Linq;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using Orient.Client;
using OrientDB_Net.binary.Innov8tive.API;

namespace NoSqlBenchmark.Benchmarks.DbConnectors
{
    public class OrientDbConnector : IDbConnector
    {
        private ODatabase _db;
        
        public void Connect()
        {
            var connectionOptyion = new ConnectionOptions()
            {
                DatabaseType = ODatabaseType.Document,
                Port = 2424,
                Password = "Administrator",
                HostName = ConfigurationManager.AppSettings["DbIpAddress"],
                DatabaseName = "test",
                UserName = "root"
            };
            _db = new ODatabase(connectionOptyion);
            if (!_db.Schema.IsClassExist(typeof(TweeterModel).Name))
            {
                _db
                    .Create.Class(typeof(TweeterModel).Name)
                    .Run();
            }
            if (!_db.Schema.IsClassExist(typeof(RedditModel).Name))
            {
                _db
                    .Create.Class(typeof(RedditModel).Name)
                    .Run();
            }
            if (!_db.Schema.IsClassExist(typeof(YoutubeModel).Name))
            {
                _db
                    .Create.Class(typeof(YoutubeModel).Name)
                    .Run();
            }

            FlushDb();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            var doc = data.ToDocument();
            _db.Create.Document(typeof(T).Name).Set(doc).Run();
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            var obj = Read<T>(id);
            Insert(data);
            return data;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            //var d = _db.Command("LOAD Record #12:" + id);
            var data = _db.Select().From($"[#12:{id}]").ToList<BaseModel>().FirstOrDefault();
            return (T)new BaseModel();
        }

        public void FlushDb()
        {
            _db.Command("TRUNCATE CLASS " + typeof(TweeterModel).Name);
            _db.Command("TRUNCATE CLASS " + typeof(YoutubeModel).Name);
            _db.Command("TRUNCATE CLASS " + typeof(RedditModel).Name);
        }

        public void InitScheme<T>() where T : BaseModel
        {

        }
    }
}