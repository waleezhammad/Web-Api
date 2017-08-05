using CountingKs.Data;
using NewApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class MeasuresV2Controller : BaseController
    {
        
        public MeasuresV2Controller(ICountingKsRepository reposotiry):base(reposotiry)
        {
           
        }
        public IEnumerable<MeasureModelV2> Get(int foodid)
        {
            return reposotiry.GetMeasuresForFood(foodid).ToList().Select(f=>ModelFactory.CreateV2(f));
        }

        public MeasureModel Get(int foodid, int id)
        {
            Measure measure = reposotiry.GetMeasure(id);
            if (measure.Food_Id == foodid)
                return ModelFactory.CreateV2(measure);
            else
                return null;
        }
    }
}
