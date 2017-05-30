using NoSqlBenchmark.Benchmarks;

namespace NoSqlBenchmark.TestScenarios
{
    public interface IScenarioStrategy
    {
        int CountOfOperations { get; set; }
        void ExecuteStrategy(IDbConnector db, ModelDataType dataType);
    }

    public enum ModelDataType
    {
        News,
        Bank
    }

    public class JustInsertsStrategy : IScenarioStrategy
    {
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var mf = new ModelFactory();
            for (var i = 0; i < CountOfOperations; i++)
            {
                var data = mf.GetDemoModel(dataType);
                db.Insert(data);
            }
        }
    }

    public class JustReadsStrategy : IScenarioStrategy
    {
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            for (var i = 0; i < CountOfOperations; i++)
            {
                db.Read<BaseModel>(1);
            }
        }
    }

    public class EqualReadWriteStrategy : IScenarioStrategy
    {
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var mf = new ModelFactory();
            for (var i = 0; i < CountOfOperations; i++)
            {
                var data = mf.GetDemoModel(dataType);
                db.Insert(data);
                db.Read<BaseModel>(data.Id);
            }
        }
    }

    public class WritesToReadsWithRatioStrategy : IScenarioStrategy
    {
        public float WritesToReadsRatio { get; set; }
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var readEveryNWrite = (int) WritesToReadsRatio * CountOfOperations;
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

    public class ReadsToWritesWithRatioStrategy : IScenarioStrategy
    {
        public float ReadsToWritesRatio { get; set; }
        public int CountOfOperations { get; set; }

        public void ExecuteStrategy(IDbConnector db, ModelDataType dataType)
        {
            var readEveryNWrite = (int)ReadsToWritesRatio * CountOfOperations;
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
