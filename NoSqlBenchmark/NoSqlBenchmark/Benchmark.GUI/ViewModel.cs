using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using NoSqlBenchmark.TestScenarios;
using OxyPlot;
using OxyPlot.Series;

namespace Benchmark.GUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Result> Results { get; set; }
        public ObservableCollection<DataPoint> PointsMemcached { get; set; }
        public ObservableCollection<DataPoint> PointsRiak { get; set; }
        public ObservableCollection<DataPoint> PointsCouchBase { get; set; }
        public ObservableCollection<DataPoint> PointsRedis { get; set; }
        public ObservableCollection<DataPoint> PointsMongo { get; set; }
        public ObservableCollection<DataPoint> PointsOrient { get; set; }

        public ViewModel(List<Result> test)
        {
            Results = new ObservableCollection<Result>();
            foreach (var result in test)
            {
                Results.Add(result);
            }
        }

        public ViewModel()
        {
            Results = new ObservableCollection<Result>();

            PointsMongo = new ObservableCollection<DataPoint>() { new DataPoint(0, 0), new DataPoint(5, 15) };
            PointsRedis = new ObservableCollection<DataPoint>() { new DataPoint(0, 0), new DataPoint(6, 26) };
            PointsMemcached = new ObservableCollection<DataPoint>() { new DataPoint(0, 0), new DataPoint(7, 37) };
            PointsCouchBase = new ObservableCollection<DataPoint>() { new DataPoint(0, 0), new DataPoint(8, 48) };
            PointsRiak = new ObservableCollection<DataPoint>() { new DataPoint(0, 0), new DataPoint(9, 59) };
            PointsOrient= new ObservableCollection<DataPoint>() { new DataPoint(0, 0), new DataPoint(9, 69) };
        }

        public void ClearResults()
        {
            while (Results.Count > 0)
            {
                Results.RemoveAt(Results.Count - 1);
            }
        }

        public object SelectedItem { get; set; } = null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AddPoints(List<DataPoint> resultPoints, string dbName)
        {
            resultPoints.RemoveAt(0);
            resultPoints.ForEach( x => new DataPoint(x.X-100,x.Y));
            ClearPoints(dbName);
            switch (dbName)
            {
                case "MongoDB":
                    foreach (var r in resultPoints)
                        PointsMongo.Add(r);
                    break;
                case "RiakKV":
                    foreach (var r in resultPoints)
                        PointsRiak.Add(r);
                    break;
                case "Redis":
                    foreach (var r in resultPoints)
                        PointsRedis.Add(r);
                    break;
                case "Memcached":
                    foreach (var r in resultPoints)
                        PointsMemcached.Add(r);
                    break;
                case "CouchBase":
                    foreach (var r in resultPoints)
                        PointsCouchBase.Add(r);
                    break;
                case "OrientDB":
                    foreach (var r in resultPoints)
                        PointsOrient.Add(r);
                    break;
            }
        }

        private void ClearPoints(string dbName)
        {
            switch (dbName)
            {
                case "MongoDB":
                    while (PointsMongo.Count > 0)
                    {
                        PointsMongo.RemoveAt(PointsMongo.Count - 1);
                    }
                    break;
                case "RiakKV":
                    while (PointsRiak.Count > 0)
                    {
                        PointsRiak.RemoveAt(PointsRiak.Count - 1);
                    }
                    break;
                case "Redis":
                    while (PointsRedis.Count > 0)
                    {
                        PointsRedis.RemoveAt(PointsRedis.Count - 1);
                    }
                    break;
                case "Memcached":
                    while (PointsMemcached.Count > 0)
                    {
                        PointsMemcached.RemoveAt(PointsMemcached.Count - 1);
                    }
                    break;
                case "CouchBase":
                    while (PointsCouchBase.Count > 0)
                    {
                        PointsCouchBase.RemoveAt(PointsCouchBase.Count - 1);
                    }
                    break;
                case "OrientDB":
                    while (PointsOrient.Count > 0)
                    {
                        PointsOrient.RemoveAt(PointsOrient.Count - 1);
                    }
                    break;
            }
        }
    }

    public class StrategiesViewModel
    {
        public ObservableCollection<string> StrategyNames { get; }
        public string SelectedStrategy { get; set; }
        public int CountOfOperation;

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
                case "Writes": return new JustInsertsStrategy {CountOfOperations = CountOfOperation};
                case "Reads": return new JustReadsStrategy {CountOfOperations = CountOfOperation};
                case "Writes/Reads - 1/10":
                    return
                        new WritesToReadsWithRatioStrategy
                        {
                            CountOfOperations = CountOfOperation,
                            WritesToReadsRatio = 0.1f
                        };
                case "Writes/Reads - 1/1":
                    return
                        new EqualReadWriteStrategy()
                        {
                            CountOfOperations = CountOfOperation
                        };
                case "Writes/Reads - 10/1":
                    return
                        new ReadsToWritesWithRatioStrategy()
                        {
                            CountOfOperations = CountOfOperation,
                            ReadsToWritesRatio = 0.1f
                        };
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
