using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using EVote.Factories;
using EVote.Utilities.Views;
using LiveCharts;
using LiveCharts.Wpf;

namespace EVote.Views
{
    public class VotingActivityViewModel : ViewModelBase
    {
        public UserControl LeftChart { get; set; }

        public UserControl RightChart { get; set; }

        public string TotalVoters { get; set; }
        public string ActiveVoters { get; set; }
        public string ActivePercent { get; set; }

        public VotingActivityViewModel()
        {
            LoadCounts();

            LeftChart = new Utilities.Views.Charts.DailyActivityView();
            LeftChart.DataContext = new Utilities.Views.Charts.DailyActivityView();

            RightChart = new Utilities.Views.Charts.ElectionActivityView();
            RightChart.DataContext = new Utilities.Views.Charts.ElectionActivityView();
        }

        public async void LoadCounts()
        {
            ElectionFactory factory = new ElectionFactory();
            var stats = await factory.VoterCounts();

            TotalVoters = stats.TotalVoters.ToString();
            RaisePropertyChanged("TotalVoters");

            ActiveVoters = stats.ActiveVoters.ToString();
            RaisePropertyChanged("ActiveVoters");

            ActivePercent = "[" +  stats.ActivePercent.ToString("N") + "%]";
            RaisePropertyChanged("ActivePercent");
        }
    }
}
