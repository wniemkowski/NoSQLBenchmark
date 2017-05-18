using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.Benchmarks
{
    public class ModelFactory
    {
        public object GetDemoModel<T>()
        {
            if (typeof(T) == typeof(News))
            {
                return News.GetDemo();
            }
            return new BaseModel();
        }
    }
}