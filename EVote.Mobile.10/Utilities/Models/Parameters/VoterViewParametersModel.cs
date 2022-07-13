using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Utilities.Models
{
    public class VoterViewParametersModel : IViewParametersModel
    {
        public VoterDataModel Voter { get; set; }

        public VoterSearchModel Search { get; set; }

        public VoterViewParametersModel()
        {
            Voter = new VoterDataModel();
            Search = new VoterSearchModel();
        }
    }
}
