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
    /// Interaction logic for ElectionActivityView.xaml
    /// </summary>
    public partial class ElectionActivityView : UserControl
    {
        // https://lvcharts.net/App/examples/v1/Wpf/Install

        public ElectionActivityView()
        {
            InitializeComponent();

            GetElectionActivityValues();
        }

        public SeriesCollection AllActivity { get; set; }
        public string[] AllActivityLabels { get; set; }
        public Func<int, string> AllActivityFormatter { get; set; }

        private async void GetElectionActivityValues()
        {
            ElectionFactory factory = new ElectionFactory();
            var values = await factory.ElectionActivity();

            AllActivityChart.Visibility = Visibility.Visible;

            AllActivity = new SeriesCollection
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

            AllActivityLabels = values.OrderByDescending(l => l.Total).Select(l => l.CategoryName).ToArray<string>();
            AllActivityFormatter = value => value.ToString("N");

            DataContext = this;
        }
    }
}
