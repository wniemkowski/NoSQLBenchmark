using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;

namespace NoSqlBenchmark.TestScenarios
{
    public class ReadsToWritesWithRatioStrategy : IScenarioStrategy
    {
        public float ReadsToWritesRatio { get; set; }
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var readEveryNWrite = (int) (ReadsToWritesRatio * CountOfOperations);
            var mf = new ModelFactory();
            for (var i = 0; i < CountOfOperations; i++)
            {
                var data = mf.GetDemoModel(dataType);
                db.Insert(data);
                if (i % (readEveryNWrite) == 0)
                {
                    db.Read<BaseModel>(data.Id);
                }
            }
        }
    }
}