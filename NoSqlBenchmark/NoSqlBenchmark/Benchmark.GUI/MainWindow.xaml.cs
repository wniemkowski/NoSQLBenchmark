using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NoSqlBenchmark;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Models;

namespace Benchmark.GUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BenchmarkTests _benchmarkTests;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new ViewModel();
            Strategies = new StrategiesViewModel();
            DataContext = ViewModel;

            StrategiesCmb.ItemsSource = Strategies.StrategyNames;
            _benchmarkTests = new BenchmarkTests();
        }

        private ViewModel ViewModel { get; set; }
        private StrategiesViewModel Strategies { get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var benchmarks = new List<IBenchmark>
            {
                new MongoDbBenchmark<News>(),
                new MemcachedBenchmark(),
                new RedisBenchmark<News>(),
                //new DymanoDbBenchmark<News>(),
                new CouchDbBenchmark()
            };
            var tests = new BenchmarkTests();
            foreach (var benchmark in benchmarks)
            {
                var result = tests.TestSingle(benchmark, Strategies.GetStrategy());
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
            var result = _benchmarkTests.TestSingle(benchmark, Strategies.GetStrategy());
            ViewModel.Results.Add(result);
        }

        private void StartDB_Clicked(object sender, RoutedEventArgs e)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            currentPath = currentPath.Replace("NoSqlBenchmark\\NoSqlBenchmark\\Benchmark.GUI\\bin\\Debug", "");
            var strCmdText = currentPath + "DBs\\startAll.bat";
            Process.Start("CMD.exe", "/C" + strCmdText);
        }

        private void StrategiesCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Strategies.SelectedStrategy = StrategiesCmb.SelectedValue.ToString();
        }
    }
}