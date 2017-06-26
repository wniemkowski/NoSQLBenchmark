using System;
using System.Configuration;
using System.Diagnostics;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;
using System.Threading;
using System.Globalization;
using Couchbase.Configuration.Client;
using MongoDB.Driver;
using NoSqlBenchmark.Benchmarks.DbConnectors;
using Raven.Client.Document;
using RiakClient;

namespace NoSqlBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            var testCount = 10000;

            //var conn = new CouchbaseConnector();
            //conn.Connect();
            //conn.Insert(News.GetDemo());

            var cluster = RiakCluster.FromConfig("riakConfig");
            //IRiakClient client = cluster.CreateClient();
            //var a = client.Ping();

            var benchmarks = new BenchmarkFactory().GetAllBenchmarks(ModelDataType.News);

            foreach (var benchmark in benchmarks)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                //Console.Write($"\r{i * 100 / testCount}%\t");
                benchmark.Test<News>(new JustInsertsStrategy() {CountOfOperations = testCount});
                stopwatch.Stop();
                Console.WriteLine($"{benchmark,9} resulted with {stopwatch.Elapsed:g}");
                benchmark.Dispose();
            }

            Console.ReadKey();
        }
    }


}
