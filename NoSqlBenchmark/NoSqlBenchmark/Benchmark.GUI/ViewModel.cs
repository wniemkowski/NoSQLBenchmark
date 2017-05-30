using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NoSqlBenchmark.TestScenarios;

namespace Benchmark.GUI
{
    public class ViewModel
    {
        public ObservableCollection<Result> Results { get; }

        public ViewModel(List<Result> test)
        {
            Results = new ObservableCollection<Result>();
            foreach (var result in test)
                Results.Add(result);
        }

        public ViewModel()
        {
            Results = new ObservableCollection<Result>();
        }

        public object SelectedItem { get; set; } = null;
    }

    public class Result
    {
        public string Db { get; set; }
        public double Time { get; set; }
    }

    public class StrategiesViewModel
    {
        public ObservableCollection<string> StrategyNames { get; }
        public string SelectedStrategy { get; set; }
        public int OperationCount = 100;

        public StrategiesViewModel()
        {
            StrategyNames = new ObservableCollection<string>
            {
                "Writes",
                "Reads",
                "Writes/Reads - 1/10",
                "Writes/Reads - 1/1",
                "Writes/Reads - 10/1"
            };
        }

        public IScenarioStrategy GetStrategy()
        {
            switch (SelectedStrategy)
            {
                case "Writes": return new JustInsertsStrategy {CountOfOperations = OperationCount};
                case "Reads": return new JustReadsStrategy {CountOfOperations = OperationCount};
                case "Writes/Reads - 1/10":
                    return
                        new WritesToReadsWithRatioStrategy
                        {
                            CountOfOperations = OperationCount,
                            WritesToReadsRatio = 0.1f
                        };
                case "Writes/Reads - 1/1":
                    return
                        new EqualReadWriteStrategy()
                        {
                            CountOfOperations = OperationCount
                        };
                case "Writes/Reads - 10/1":
                    return
                        new ReadsToWritesWithRatioStrategy()
                        {
                            CountOfOperations = OperationCount,
                            ReadsToWritesRatio = 0.1f
                        };
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
