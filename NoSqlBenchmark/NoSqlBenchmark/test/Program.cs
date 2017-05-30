using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new RedisBenchmark<News>();
            a.Test<News>(new EqualReadWriteStrategy());
        }
    }
}
