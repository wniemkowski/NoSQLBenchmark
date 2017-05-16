using System.Runtime.InteropServices;

namespace NoSqlBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var memcached = new MemcachedBenchmark())
            {
                memcached.Connect();
                memcached.Test();
            }
            
        }
    }
}
