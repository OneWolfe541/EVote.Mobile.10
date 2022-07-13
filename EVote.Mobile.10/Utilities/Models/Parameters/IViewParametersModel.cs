using System;
using System.Collections.Generic;
using System.Text;

namespace EVote.Utilities.Models
{
    public interface IViewParametersModel
    {
        VoterDataModel Voter { get; set; }

        VoterSearchModel Search { get; set; }
    }
}
