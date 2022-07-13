using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using EVote.Methods;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{
    public class VoterSearchPanelViewModel : ViewModelBase
    {
        public string Text { get; set; }

        public ObservableCollection<VoterDataModel> VoterList { get; set; }

        public bool IsStandardVoter { get; set; }

        private bool _isEnabled;
        public bool IsEnabled 
        {
            get
            {
                return _isEnabled;
            } 
            set
            {
                _isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }

        public VoterSearchPanelViewModel(List<VoterDataModel> voters)
        {
            Text = "Voter Search Panel";

            //IsStandardVoter = true;

            if (voters != null)
            {
                foreach (var voter in voters)
                {
                    if (voter.DOB != null)
                    {
                        voter.DOBSearch = voter.DOB.Value.ToShortDateString();
                    }
                }
            }

            VoterList = new ObservableCollection<VoterDataModel>(voters);

            IsEnabled = true;
        }

        private VoterDataModel _selectedVoter;
        public VoterDataModel SelectedVoter
        {
            get
            {
                return _selectedVoter;
            }

            set
            {
                _selectedVoter = value;
                RaisePropertyChanged("SelectedVoter");
            }
        }

        public bool? ShowDistrict
        {
            get
            {
                return AppSettings.Config.ShowDistrict;
            }
        }
    }
}
