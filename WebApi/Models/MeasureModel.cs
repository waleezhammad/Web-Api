using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class MeasureModel
    {
        public string Description { get; set; }
        public double Calories { get; set; }

        public string Url { get; set; }
    }

    public class MeasureModelV2 : MeasureModel
    {
        public double TotalFat { get; set; }
        public double SaturatedFat { get; set; }
        public double Protein { get; set; }
        public double Carbohydartes { get; set; }
    }
}