using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Benchmark.GUI
{
    public class ViewModel
    {
        public ObservableCollection<Result> Results { get; private set; }

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
        }

        public object SelectedItem { get; set; } = null;
    }

    public class Result
    {
        public string Db { get; set; }
        public double Time { get; set; }
    }
}
