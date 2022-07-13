using System;
using System.Collections.Generic;
using System.Text;
using EVote.Utilities.Models;

namespace EVote.Utilities.Views
{
    public class VoterSearchViewModelBase : ViewModelBase
    {
        public VoterSearchViewModelBase()
        {

        }

        #region Properties
        public string Type { get; set; }

        protected string _rollNumber;
        public string RollNumber
        {
            get { return _rollNumber; }
            set
            {
                _rollNumber = value;
            }
        }

        protected string _nameLast;
        public string NameLast
        {
            get { return _nameLast; }
            set
            {
                _nameLast = value;
            }
        }

        protected string _nameFirst;
        public string NameFirst
        {
            get { return _nameFirst; }
            set
            {
                _nameFirst = value;
            }
        }

        protected string _birthYear;
        public string BirthYear
        {
            get { return _birthYear; }
            set
            {
                _birthYear = value;
            }
        }

        protected DateSearch _birthDate;
        public DateSearch BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public string Month
        {
            get { return _birthDate.Month; }
            set { _birthDate.Month = value; }
        }

        public string Day
        {
            get { return _birthDate.Day; }
            set { _birthDate.Day = value; }
        }

        public string Year
        {
            get { return _birthDate.Year; }
            set { _birthDate.Year = value; }
        }

        protected VoterSearchModel _voterSearch;
        public VoterSearchModel VoterSearch
        {
            get { return _voterSearch; }
            //set { _voterSearch = value; }
        }

        public bool VoterClear { get; set; }
        public bool VoterScanSearch { get; set; }
        public bool VoterNameSearch { get; set; }
        public bool VoterDateSearch { get; set; }
        public bool MonthSearch { get; set; }
        public bool DaySearch { get; set; }
        public bool YearSearch { get; set; }
        #endregion
    }
}
