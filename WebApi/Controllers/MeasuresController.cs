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
    public class MeasuresController : ApiController
    {
        ICountingKsRepository reposotiry;
        ModelFactory modelFactory;
        public MeasuresController(ICountingKsRepository reposotiry)
        {
            this.reposotiry = reposotiry;
            modelFactory = new ModelFactory();
        }
        public IEnumerable<MeasureModel> Get(int foodid)
        {
            return reposotiry.GetMeasuresForFood(foodid).ToList().Select(f=>modelFactory.Create(f));
        }

        public MeasureModel Get(int foodid, int id)
        {
            Measure measure = reposotiry.GetMeasure(id);
            if (measure.Food_Id == foodid)
                return modelFactory.Create(measure);
            else
                return null;
        }
    }
}
