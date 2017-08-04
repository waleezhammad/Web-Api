using NewApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ModelFactory
    {
        public FoodModel Create(Food food)
        {
            return new FoodModel()
            {
                Description = food.Description,
                Id = food.Id,
                Measure = food.Measures!=null && food.Measures.Count>0? food.Measures.Select(m => Create(m)).ToList():new List<MeasureModel>()
            };
        }

        public MeasureModel Create(Measure m)
        {
            return new MeasureModel()
            {
                Description = m.Description,
                Calories = m.Calories
            };
        }
    }
}