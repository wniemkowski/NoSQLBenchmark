using System.Collections.Generic;
using System.Windows;
using NoSqlBenchmark;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;

namespace Benchmark.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel ViewModel { get; set; }
        BenchmarkTests _benchmarkTests;
        public MainWindow()
        {
           // InitializeComponent();

            ViewModel = new ViewModel();
            DataContext = ViewModel;

            _benchmarkTests = new BenchmarkTests();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel = new ViewModel();
            DataContext = ViewModel;
            var benchmarks = new List<IBenchmark>
            {
                new MongoDbBenchmark<News>(),
                new MemcachedBenchmark(),
                new RedisBenchmark<News>(),
                new DymanoDbBenchmark<News>(),
                new CouchDbBenchmark(),
            };
            var tests = new BenchmarkTests();
            foreach (var benchmark in benchmarks)
            {
                var result = tests.TestSingle(benchmark);
                ViewModel.Results.Add(result);
            }
            
            //var bench = tests.TestSingleAsync(new CouchDbBenchmark());
            //DataContext = new ViewModel(tests.Test().Result);
        }

        private void RunMongo_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(new MongoDbBenchmark<News>());
        }

        private void RunMemcached_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(new MemcachedBenchmark());
        }

        private void RunRedis_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(new RedisBenchmark<News>());
        }

        private void RunDynamo_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(new DymanoDbBenchmark<News>());
        }

        private void RunCouch_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(new CouchDbBenchmark());
        }

        private void RunSingleBenchmark(IBenchmark benchmark)
        {
            var result = _benchmarkTests.TestSingle(benchmark);
            ViewModel.Results.Add(result);
        }
    }
}
