using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EVote.Utilities.Models;

namespace EVote.Utilities.Extensions
{
    internal static class VoterDataExtensions
    {
        internal static IQueryable<T> IDEquals<T>(this IQueryable<T> queryable, string strID) where T : VoterDataModel
        {
            if (strID.IsNullOrEmpty() || queryable == null) return queryable;
            else
                return queryable.Where(arg => arg.VoterID == strID);
        }

        internal static IQueryable<T> LastNameStartsWith<T>(this IQueryable<T> queryable, string strLastName) where T : VoterDataModel
        {
            if (strLastName.IsNullOrEmpty() || queryable == null) return queryable;
            else
                return queryable.Where(arg => arg.LastName.ToUpper().StartsWith(strLastName.ToUpper()));
        }

        internal static IQueryable<T> FirstNameStartsWith<T>(this IQueryable<T> queryable, string strFirstName) where T : VoterDataModel
        {
            if (strFirstName.IsNullOrEmpty() || queryable == null) return queryable;
            else
                return queryable.Where(arg => arg.FirstName.ToUpper().StartsWith(strFirstName.ToUpper()));
        }

        internal static IQueryable<T> BirthDateContains<T>(this IQueryable<T> queryable, string strDate) where T : VoterDataModel
        {
            if (strDate.IsNullOrEmpty() || queryable == null) return queryable;
            else
                return queryable.Where(arg => arg.DOBSearch.Contains(strDate));
        }

        internal static IQueryable<T> BirthDateEquals<T>(this IQueryable<T> queryable, string strDate) where T : VoterDataModel
        {
            if (strDate.IsNullOrEmpty() || queryable == null) return queryable;
            else
            {
                // Convert to DateTime then back to string
                DateTime searchDate;
                if (DateTime.TryParse(strDate, out searchDate))
                {
                    return queryable.Where(arg => arg.DOBSearch == searchDate.ToString());
                }
                else
                {
                    return queryable;
                }
            }
        }

        internal static IQueryable<T> BirthDateEqualsDate<T>(this IQueryable<T> queryable, string strDate) where T : VoterDataModel
        {
            if (strDate.IsNullOrEmpty() || queryable == null) return queryable;
            else
            {
                // Convert to DateTime then back to string
                DateTime searchDate;
                if (DateTime.TryParse(strDate, out searchDate))
                {
                    return queryable.Where(arg => arg.DOB == searchDate);
                }
                else
                {
                    return queryable;
                }
            }
        }

        //internal static IQueryable<T> BirthYearEquals<T>(this IQueryable<T> queryable, string strYear) where T : VoterDataModel
        //{
        //    if (strYear.IsNullOrEmpty() || queryable == null) return queryable;
        //    else
        //        return queryable.Where(arg => arg.DOBYear == strYear);
        //}

        //internal static IQueryable<T> BirthYearContains<T>(this IQueryable<T> queryable, string strYear) where T : VoterDataModel
        //{
        //    if (strYear.IsNullOrEmpty() || queryable == null) return queryable;
        //    else
        //        return queryable.Where(arg => arg.DOBYear.Contains(strYear));
        //}

        // https://stackoverflow.com/questions/5624614/get-a-list-of-elements-by-their-id-in-entity-framework?lq=1
        internal static IQueryable<T> FromList<T>(this IQueryable<T> queryable, List<string> voterIdList) where T : VoterDataModel
        {
            if (voterIdList == null || queryable == null) return queryable;
            else
                return queryable.Where(arg => voterIdList.Contains(arg.VoterID));
        }

        internal static IQueryable<T> AtPollSite<T>(this IQueryable<T> queryable, int? pollID) where T : VoterDataModel
        {
            if (pollID == null || queryable == null) return queryable;
            else
                return queryable.Where(arg => arg.LocationID == pollID);
        }

        //internal static IQueryable<T> FederalBallot<T>(this IQueryable<T> queryable, int? pollID) where T : VoterDataModel
        //{
        //    if (pollID == null || queryable == null) return queryable;
        //    else
        //        return queryable.Where(arg => arg.PollID == pollID && arg.LogCode == 5);
        //}

        //internal static IQueryable<T> OnMachine<T>(this IQueryable<T> queryable, int? computerID) where T : VoterDataModel
        //{
        //    if (computerID == null || queryable == null) return queryable;
        //    else
        //        return queryable.Where(arg => arg.ComputerID == computerID);
        //}

        internal static IQueryable<T> WithLogCode<T>(this IQueryable<T> queryable, int? logCode) where T : VoterDataModel
        {
            if (logCode == null || queryable == null) return queryable;
            else
                return queryable.Where(arg => arg.LogCode == logCode);
        }

        //internal static IQueryable<T> WithBallotStyle<T>(this IQueryable<T> queryable, int? ballotStyleID) where T : VoterDataModel
        //{
        //    if (ballotStyleID == null || queryable == null) return queryable;
        //    else
        //        return queryable.Where(arg => arg.BallotStyleID == ballotStyleID);
        //}

        internal static IQueryable<T> ListContains<T>(this IQueryable<T> queryable, List<string> idList) where T : VoterDataModel
        {
            if (idList == null || queryable == null) return queryable;
            else
                return queryable.Where(arg => idList.Contains(arg.VoterID));
        }

        //internal static IQueryable<T> IdRequired<T>(this IQueryable<T> queryable, bool? required) where T : VoterDataModel
        //{
        //    if (required == null || queryable == null) return queryable;
        //    else
        //        return queryable.Where(arg => arg.IDRequired == required);
        //}

        //internal static IQueryable<T> FromBatch<T>(this IQueryable<T> queryable, Guid? batchId) where T : VoterDataModel
        //{
        //    if (batchId == null || queryable == null)
        //        return queryable.Where(arg => arg.BatchID == null);
        //    else
        //        return queryable.Where(arg => arg.BatchID == batchId);
        //}
    }
}
