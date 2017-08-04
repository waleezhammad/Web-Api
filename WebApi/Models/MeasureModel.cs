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
}