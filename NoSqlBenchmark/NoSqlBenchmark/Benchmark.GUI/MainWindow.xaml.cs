using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using NoSqlBenchmark;
using NoSqlBenchmark.Benchmarks;
using NoSqlBenchmark.Benchmarks.Interfaces;
using NoSqlBenchmark.Models;
using NoSqlBenchmark.TestScenarios;
using OxyPlot;
using OxyPlot.Series;
using ServiceStack.Common;

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

            WindowState = WindowState.Maximized;
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
            RunSingleBenchmark(_benchmarkFactory.GetBenchmark(BenchmarkType.MongoDB, selectedModel));
        }

        private void RunMemcached_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(_benchmarkFactory.GetBenchmark(BenchmarkType.Memcached, selectedModel));
        }

        private void RunRedis_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(_benchmarkFactory.GetBenchmark(BenchmarkType.Redis, selectedModel));
        }

        private void RunDynamo_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(_benchmarkFactory.GetBenchmark(BenchmarkType.Riak, selectedModel));
        }

        private void RunCouch_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(_benchmarkFactory.GetBenchmark(BenchmarkType.Couchbase, selectedModel));
        }

        private void RunOrient_Clicked(object sender, RoutedEventArgs e)
        {
            RunSingleBenchmark(_benchmarkFactory.GetBenchmark(BenchmarkType.OrientDb, selectedModel));
        }

        private void RunSingleBenchmark(IBenchmark benchmark)
        {
            Result result;
            ModelFactory.Populate(selectedModel, Strategies.CountOfOperation);
            switch (selectedModel)
            {
                case ModelDataType.Reddit:
                    result = _benchmarkTests.TestSingle<RedditModel>(benchmark, Strategies.GetStrategy());
                    break;
                case ModelDataType.Tweeter:
                    result = _benchmarkTests.TestSingle<TweeterModel>(benchmark, Strategies.GetStrategy());
                    break;
                case ModelDataType.Youtube:
                    result = _benchmarkTests.TestSingle<YoutubeModel>(benchmark, Strategies.GetStrategy());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ViewModel.Results.Add(result);
            ViewModel.AddPoints(result.Points, benchmark.ToString());
            Chart.InvalidateVisual();
            OXplot.InvalidateVisual();
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
            MultiTest();
        }

        private void MultiTest()
        {
            //foreach (var model in Enum.GetValues(typeof(ModelDataType)))
            //{
            //    selectedModel = (ModelDataType) model;
                //foreach (var strategy in Strategies.StrategyNames)
                //{
                //    Strategies.SelectedStrategy = strategy;
                RunAll();
                //}
            //}
        }

        public void RunAll()
        {
            Result result;
            var tests = new BenchmarkTests();
            for (var i = 1; i < 2; i++)
            {
                var benchmarkTests = _benchmarkFactory.GetAllBenchmarks(selectedModel);
                foreach (var benchmark in benchmarkTests)
                {
                    Strategies.CountOfOperation = i == 0 ? 100 : int.Parse(OperationTxb.Text);
                    ModelFactory.Populate(selectedModel, int.Parse(OperationTxb.Text));
                    switch (selectedModel)
                    {
                        case ModelDataType.Reddit:
                            result = tests.TestSingle<RedditModel>(benchmark, Strategies.GetStrategy()); 
                            break;
                        case ModelDataType.Tweeter:
                            result = tests.TestSingle<TweeterModel>(benchmark, Strategies.GetStrategy());
                            break;
                        case ModelDataType.Youtube:
                            result = tests.TestSingle<YoutubeModel>(benchmark, Strategies.GetStrategy());
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    ViewModel.Results.Add(result);
                    ViewModel.AddPoints(result.Points, benchmark.ToString());
                }
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
            ScreenShot();
            ViewModel.ClearResults();
        }

        private void ScreenShot()
        {
            var left = Screen.PrimaryScreen.Bounds.X;
            var top = Screen.PrimaryScreen.Bounds.Y;
            var right = Screen.PrimaryScreen.Bounds.X + Screen.PrimaryScreen.Bounds.Width;
            var bottom = Screen.PrimaryScreen.Bounds.Y + Screen.PrimaryScreen.Bounds.Height;
            var width = right - left;
            var height = bottom - top;
            String filename;
            Thread.Sleep(1000);
            using (Bitmap bmp = new Bitmap((int)width,
                (int)height))

            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    filename = StrategiesCmb.SelectedValue + " " + selectedModel + " " + Strategies.CountOfOperation + " " + DateTime.Now.ToString("ddMMyyyy-hhmmss");
                    Opacity = .0;
                    g.CopyFromScreen((int)left, (int)top, 0, 0, bmp.Size);
                    bmp.Save("G:\\[Mgr]\\screens\\" + filename.Replace('/', '-') + ".png");
                    Opacity = 1;
                }
            }

            using (var file = File.CreateText("G:\\[Mgr]\\screens\\" + filename.Replace('/', '-') + ".csv"))
            {
                file.WriteLine("CouchBase,Memcached,Mongo,Orient,Redis,Riak");
                for (int i = 1; i < ViewModel.PointsCouchBase.Count; i++)
                {
                    file.WriteLine($"{ViewModel.PointsCouchBase[i].Y}," +
                                   $"{ViewModel.PointsMemcached[i].Y}," +
                                   $"{ViewModel.PointsMongo[i].Y}," +
                                   $"{ViewModel.PointsOrient[i].Y}," +
                                   $"{ViewModel.PointsRedis[i].Y}," +
                                   $"{ViewModel.PointsRiak[i].Y},");

                }
            }
        }

        private void Chart_LayoutUpdated(object sender, EventArgs e)
        {
        }

        private void OXplot_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!ViewModel.Results.IsEmpty())
            {
                //ScreenShot();
            }
        }
    }
}