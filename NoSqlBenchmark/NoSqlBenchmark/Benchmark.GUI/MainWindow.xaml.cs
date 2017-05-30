using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NoSqlBenchmark;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;

namespace Benchmark.GUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BenchmarkTests _benchmarkTests;
        private readonly BenchmarkFactory _benchmarkFactory = new BenchmarkFactory();
        private ModelDataType selectedModel;
        private Int32 _countOfOperations = 1000;
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new ViewModel();
            Strategies = new StrategiesViewModel() {CountOfOperation = _countOfOperations };
            DataContext = ViewModel;

            StrategiesCmb.ItemsSource = Strategies.StrategyNames;
            ModelCmb.ItemsSource = Enum.GetValues(typeof(ModelDataType)).Cast<ModelDataType>(); 
            _benchmarkTests = new BenchmarkTests();
            OperationTxb.Text = "1000";
        }

        private ViewModel ViewModel { get; set; }
        private StrategiesViewModel Strategies { get; }

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

        private void RunAllBenchmarks_Clicked(object sender, RoutedEventArgs e)
        {
            var benchmarks = _benchmarkFactory.GetAllBenchmarks(ModelDataType.News);
            var tests = new BenchmarkTests();
            foreach (var benchmark in benchmarks)
            {
                var result = tests.TestSingle(benchmark, Strategies.GetStrategy());
                ViewModel.Results.Add(result);
            }
        }

        private void StrategiesCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Strategies.SelectedStrategy = StrategiesCmb.SelectedValue.ToString();
        }

        private void ModelCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedModel = (ModelDataType)ModelCmb.SelectedItem;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OperationTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Strategies.CountOfOperation = int.Parse(OperationTxb.Text);
        }

        private void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            for(var i = 0 ;i< ViewModel.Results.Count;i++)
                ViewModel.Results.RemoveAt(i);
        }
    }
}