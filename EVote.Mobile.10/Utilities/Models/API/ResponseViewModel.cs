using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVote.Utilities.Models
{
    public class ResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
        public ModelStateDictionary Error { get; set; }
    }

    public class ResponseViewModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ModelStateDictionary Error { get; set; }
        public Data Data { get; set; }
        public T Result { get; set; }
        public List<T> Results { get; set; }
    }

    public class Data
    {
        public int Added { get; set; }
        public int Updated { get; set; }
        public int Skipped { get; set; }
        public int Total { get; set; }
        public List<Guid> SyncIds { get; set; }
        public DateTime SyncDate { get; set; }
        public List<int> VoterIds { get; set; }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string Stack { get; set; }
        public string InnerException { get; set; }
    }
}
