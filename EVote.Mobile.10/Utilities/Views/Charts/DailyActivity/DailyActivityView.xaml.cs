using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EVote.Factories;
using EVote.LocalDatabase;
using LiveCharts;
using LiveCharts.Wpf;

namespace EVote.Utilities.Views.Charts
{
    /// <summary>
    /// Interaction logic for DailyActivityView.xaml
    /// </summary>
    public partial class DailyActivityView : UserControl
    {
        // https://lvcharts.net/App/examples/v1/Wpf/Install

        public DailyActivityView()
        {
            InitializeComponent();

            GetDailyActivityValues();
        }

        public SeriesCollection DailyActivity { get; set; }
        public string[] DailyActivityLabels { get; set; }
        public Func<int, string> DailyActivityFormatter { get; set; }

        //private List<DailyActivityModel> Values;

        private async void GetDailyActivityValues()
        {
            ElectionFactory factory = new ElectionFactory();
            var values = await factory.VoterActivity();

            DailyCountsChart.Visibility = Visibility.Visible;

            DailyActivity = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Election Counts",
                    //Values = new ChartValues<double> { 10, 50, 39, 50 },
                    Values = new ChartValues<int>(values.OrderByDescending(l => l.Total).Select(l => l.Total).ToList()),
                    Fill = System.Windows.Media.Brushes.Gray,
                    Stroke = System.Windows.Media.Brushes.Black,
                    StrokeThickness = 1.50,
                    DataLabels = true,
                    FontSize = 10
                }
            };

            //adding series will update and animate the chart automatically
            //SeriesCollection.Add(new RowSeries
            //{
            //    Title = "2016",
            //    Values = new ChartValues<double> { 11, 56, 42 }
            //});

            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            //OfflineFactory factory = new OfflineFactory();
            //var list = factory.Locations();

            //Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            DailyActivityLabels = values.OrderByDescending(l => l.Total).Select(l => l.CategoryName).ToArray<string>();
            DailyActivityFormatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
