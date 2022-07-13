using System;
using System.Collections.Generic;
using System.Text;
using EVote.Utilities.Models;
using EVote.Utilities.Views;

namespace EVote.Views
{
    public class SignatureCaptureViewModel : ViewModelBase
    {
        public VoterDataModel Voter { get; set; }
        public string DateOfBirth
        {
            get
            {
                return "DOB " + Voter.DOB.Value.ToShortDateString();
            }
        }
        public SignatureCaptureViewModel(IViewParametersModel Parameters)
        {
            Voter = Parameters.Voter;
        }
    }
}
