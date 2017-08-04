using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class DiaryModel
    {
        public string Url { get; set; }
        public DateTime DiaryDate { get; set; }

        public IEnumerable<DiaryEntryModel> DiaryEntries { get; set; }
    }
}