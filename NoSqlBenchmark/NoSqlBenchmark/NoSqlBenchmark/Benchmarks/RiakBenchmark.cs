using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;
using Renci.SshNet;
using RiakClient;
using RiakClient.Models;
using ServiceStack.Messaging.Rcon;

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

        public List<long> Delays { get; set; }

        public void Test<T>(IScenarioStrategy scenario) where T : BaseModel
        {
            var mf = new ModelFactory();
            var dataType = mf.GetModelDataType<T>();
            scenario.ExecuteStrategy(_db, dataType);
            Delays = scenario.Delays;
        }

        public override string ToString()
        {
            return "RiakKV";
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
            FlushDb();
        }

        public T Insert<T>(T data) where T : BaseModel
        {
            var o = new RiakObject("test",data.Id.ToString(),data);
            var a =_client.Put(o);
            return data;
        }

        public T Update<T>(long id, T data) where T : BaseModel
        {
            var a = Read<T>(id);
            data.Id = a.Id;
            Insert(data);
            return data;
        }

        public T Read<T>(long id) where T : BaseModel
        {
            var a =_client.Get(new RiakObjectId("test", id.ToString()));
            return a.Value.GetObject<T>();
        }

        public void FlushDb()
        {
            using (var client = new SshClient(ConfigurationManager.AppSettings["DbIpAddress"], "nosql", "nosql01"))
            {
                client.Connect();
                var cmd = client.RunCommand("sudo /var/lib/riak/delete.sh");

                IDictionary<Renci.SshNet.Common.TerminalModes, uint> modes =
                    new Dictionary<Renci.SshNet.Common.TerminalModes, uint>();
                modes.Add(Renci.SshNet.Common.TerminalModes.ECHO, 53);

                ShellStream shellStream =
                    client.CreateShellStream("xterm", 80, 24, 800, 600, 1024, modes);
                var output = shellStream.Expect(new Regex(@"[$>]"));

                shellStream.WriteLine("sudo /var/lib/riak/delete.sh");

                output = shellStream.Expect(new Regex(@"([$#>:])"));
                shellStream.WriteLine("nosql01");
                client.Disconnect();
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

        public void InitScheme<T>() where T : BaseModel
        {
            throw new NotImplementedException();
        }
    }
}