using System;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

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

        internal BaseModel GetDemoModel(ModelDataType dataType)
        {
            switch (dataType)
            {
                case ModelDataType.News:
                    return News.GetDemo();
                case ModelDataType.Bank:
                    return BankModel.GetDemo();
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
            }
        }

        public ModelDataType GetModelDataType<T>()
        {
            return typeof(T) == typeof(News) ? ModelDataType.News : ModelDataType.Bank;
        }
    }
}