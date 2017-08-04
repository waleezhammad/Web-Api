using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class FoodModel
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public List<MeasureModel> Measure { get; set; }
        public string Url { get; set; }
    }
}