using System.Collections;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class DiaryEntryModel
    {
        public string Url { get; set; }
        public double Quantity { get; set; }
        public string MeasureDescription { get; set; }
        public string FoodDescription { get; set; }
        public string MeasureUrl { get; set; }
    }
}