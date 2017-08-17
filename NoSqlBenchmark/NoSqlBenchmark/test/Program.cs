using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;
using Orient.Client;
using OrientDB_Net.binary.Innov8tive.API;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var db = new Orient.Client.ODatabase("10.10.116.6", 2480, "test", ODatabaseType.Document, "root", "Administrator");
            //var b = db.DatabaseProperties.OClassName.Length;
            //db.Create.Document(RedditModel.GetDemo());

            //var _server = new OServer("10.10.116.6", 2424, "root", "Administrator");
            //if(!_server.DatabaseExist("test", OStorageType.Memory));

            var connectionOptyion = new ConnectionOptions()
            {
                DatabaseType = ODatabaseType.Document,
                Port = 2424,
                Password = "Administrator",
                HostName = "10.10.116.6",
                DatabaseName = "test",
                UserName = "root"
            };
                
            
            using (ODatabase database = new ODatabase(connectionOptyion))
            {

                database.Command("TRUNCATE CLASS RedditModel");


                // prerequisites
                if (!database.Schema.IsClassExist("TweeterModel")){
                    database
                        .Create.Class("TweeterModel")
                        .Run();
                }
                var mf = new ModelFactory();
                ModelFactory.Populate(ModelDataType.Reddit, 10);
                var model = mf.GetDemoModel(ModelDataType.Reddit);
                var doc = model.ToDocument();

                var data = database.Select("*").From("RedditModel").Where("Id=" + 7).ToList<RedditModel>();

                database.Create.Document(typeof(RedditModel).Name).Set(doc).Run();
                
            }
            


            var a = new RavenDbBenchmark();
            ModelFactory.Populate(ModelDataType.Reddit,1000);
            a.Test<RedditModel>(new EqualReadWriteStrategy() {CountOfOperations = 1000});
        }

        public class TestProfileClass : OBaseRecord
        {
            public string Name { get; set; }
            public string Surname { get; set; }

            public OP op { get; set; }
        }

    }
}
