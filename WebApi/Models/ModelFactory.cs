using NewApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WebApi.Models
{
    public class ModelFactory
    {
        UrlHelper urlHelper;
        public ModelFactory(HttpRequestMessage request)
        {
            urlHelper = new UrlHelper(request);
        }
        public FoodModel Create(Food food)
        {
            
            return new FoodModel()
            {
                Description = food.Description,
                Id = food.Id,
                Url=urlHelper.Link("FoodRounting",new { id=food.Id}),
                Measure = food.Measures!=null && food.Measures.Count>0? food.Measures.Select(m => Create(m)).ToList():new List<MeasureModel>()
            };
        }

        public DiaryModel Create(Diary d)
        {
            return new DiaryModel()
            {
                DiaryDate =  d.CurrentDate,
                Url = urlHelper.Link("DiaryRounting",new { DiaryId = d.CurrentDate.ToString("yyyy-MM-dd")}),
                DiaryEntries = d.DiaryEntries.Select(de=>Create(de))
            };
        }

        public MeasureModel Create(Measure m)
        {
            return new MeasureModel()
            {
                Url = urlHelper.Link("FoodMeasureRounting", new { foodid=m.Food_Id,id = m.Id }),
                Description = m.Description,
                Calories = m.Calories
            };
        }

        public DiaryEntryModel Create(DiaryEntry diary)
        {
            return new DiaryEntryModel()
            {
                Url = urlHelper.Link("DiaryEntryRounting", new { DiaryId = diary.Diary.CurrentDate.ToString("yyyy-MM-dd"), id = diary.Id }),
                Quantity = diary.Quantity,
                FoodDescription = diary.Food.Description,
                MeasureDescription = diary.Measure.Description,
                MeasureUrl = urlHelper.Link("FoodMeasureRounting", new { foodId = diary.FoodItem_Id, id = diary.Measure_Id}),
            };
        }
    }
}