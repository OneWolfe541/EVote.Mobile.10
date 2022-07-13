using EVote.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EVote.Utilities.Models
{
    public class DateSearch
    {
        [MaxLength(2)]
        public string Month { get; set; }

        [MaxLength(2)]
        public string Day { get; set; }

        [MaxLength(4)]
        public string Year { get; set; }

        public override string ToString()
        {
            if (Month == null || Month == "" || Day == null || Day == "" || Year == null || Year == "")
            {
                return null;
            }
            else
            {
                return Month + "/" + Day + "/" + Year;
            }
        }

        public bool IsNullOrEmpty()
        {
            bool result = false;

            result = Month.IsNullOrEmpty() &&
                Day.IsNullOrEmpty() &&
                Year.IsNullOrEmpty();

            return result;
        }
    }
}
