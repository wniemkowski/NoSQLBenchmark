using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.Benchmarks
{
    public class ModelFactory
    {
        public BaseModel GetDemoModel<T>() where T:BaseModel
        {
            if (typeof(T) == typeof(News))
            {
                return News.GetDemo();
            }
            return new BaseModel();
        }
    }
}